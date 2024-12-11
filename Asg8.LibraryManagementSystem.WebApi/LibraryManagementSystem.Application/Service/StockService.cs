using LibraryManagementSystem.Application.Helpers;
using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Helpers;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests;
using LibraryManagementSystem.Domain.Models.Requests.CheckOuts;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using LMS.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Application.Service
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookUserTransactionRepository _bookUserTransactionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly LibraryOptions _libraryOptions;
        private readonly IEmailService _emailService;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StockService(IStockRepository stockRepository, 
            IBookRepository bookRepository, 
            IUserRepository userRepository,
            IBookUserTransactionRepository bookUserTransactionRepository, 
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IWorkflowRepository workflowRepository,
            IHttpContextAccessor httpContextAccessor,
            IOptions<LibraryOptions> libraryOptions)
        {
            _stockRepository = stockRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _bookUserTransactionRepository = bookUserTransactionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _workflowRepository = workflowRepository;
            _httpContextAccessor = httpContextAccessor;
            _libraryOptions = libraryOptions.Value;
        }

        public async Task<object> AddStocks(AddStockRequest request)
        {
            var findStock = await _stockRepository.Get(request.BookId, request.LocationId);
            var findBook = await _bookRepository.Get(request.BookId);
            if (findStock == null)
            {
                var newStock = new Stock()
                {
                    BookId = request.BookId,
                    LocationId = request.LocationId,
                    StockCount = request.Stock,
                };
                var newResponse = await _stockRepository.Add(newStock);

                // update book stock
                findBook.Stock = findBook.Stock + newResponse.StockCount;
                await _bookRepository.Update(findBook);
                return newResponse.ToStockResponse();
            }
            findStock.StockCount = findStock.StockCount + request.Stock;
            var updateResponse = await _stockRepository.Update(findStock);

            // update book stock
            findBook.Stock = findBook.Stock + updateResponse.StockCount;
            await _bookRepository.Update(findBook);
            return updateResponse.ToStockResponse();
        }

        public async Task<object> BookCheckOut(BookCheckOutRequest request)
        {
            //find book
            var findBook = await _bookRepository.GetByIsbn(request.Isbn);
            if(findBook == null)
            {
                return null;
            }
           
            //find stock
            var findStock = await _stockRepository.Get(findBook.BookId, request.LocationId.GetValueOrDefault());
            if (findStock == null)
            {
                return null;
            }


            // update stock in stocks
            findStock.StockCount = findStock.StockCount - 1;
            await _stockRepository.Update(findStock);

            // update book in books
            findBook.Stock = findBook.Stock - 1;
            await _bookRepository.Update(findBook);

            // add transactions
            var newTransactions = new BookUserTransactions()
            {
                BookId = findBook.BookId,
                UserId = request.UserId.GetValueOrDefault(),
                AppUserId = request.AppUserId,
                Title = findBook.Title,
                Isbn = findBook.Isbn,
                LocationId = request.LocationId.GetValueOrDefault(),
                DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                BorrowedAt = DateOnly.FromDateTime(DateTime.Now)
            };
            var response = await _bookUserTransactionRepository.Add(newTransactions);
            return response.ToCheckOutResponse();
        }

        public async Task<object> BookCheckOutUserInformation(BookCheckOutUserRequest request)
        {
           // var user = await _userRepository.GetByAppUserId(request.AppUserId);
            var user = await _userRepository.GetByLibraryCardNumber(request.LibraryCardNumber);
            var transactions = await _bookUserTransactionRepository.GetAll();
            if(user == null)
            {
                return null;
            }
            return user.ToLibrarianCheckOutUserResponse(transactions.Where(w=>w.UserId == user.UserId).Count());
        }
         
        public async Task<object> BookCheckOutBookInformation(BookCheckOutBookRequest request)
        {
            var transactions = await _bookUserTransactionRepository.GetAll();
            var findTransactions = transactions.Where(w => w.Isbn == request.Isbn);
            return findTransactions.Select(s => s.ToLibrarianCheckOutBookResponse());
        }

        public async Task<object> UserBookRequest(UserBookRequest request)
        {
            var role_LU = await _roleManager.FindByNameAsync("Library User");

            var bookInLocation = await _bookRepository.GetByIsbn(request.Isbn);

            if (bookInLocation.Stock < 0 || bookInLocation == null)
            {
                return null;
            }

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var ws = await _workflowRepository.GetAllWorkflowSequence();
            var ns = await _workflowRepository.GetAllNextStepRules();
            var wf = await _workflowRepository.GetAllWorkflow();

            //steps
            var wfId = wf.Where(w => w.WorkflowName == "Book Borrowing").Select(s => s.WorkflowId).FirstOrDefault();
            var currStepId = ws.Where(w => w.WorkflowId == wfId && w.RequiredRole == role_LU.Id).Select(s => s.WorkflowId).FirstOrDefault();
            var nextStepId = ns.Where(w=>w.CurrentStepId == currStepId).Select(s=>s.NextStepId).FirstOrDefault();
            var currAction = ns.Where(w=>w.CurrentStepId == currStepId).Select(s=>s.ConditionValue).FirstOrDefault();

            // add to process
            Process newProcess = new()
            {
                RequestDate = DateTime.UtcNow,
                RequesterId = user.Id,
                CurrentStepId = nextStepId,
                Status = "Pending",
                WorkflowId = wfId,
            };
            var jc_process = await _workflowRepository.AddProcess(newProcess);
            if (jc_process == null)
            {
                return null;
            }
            //add to book request
            BookRequest newBookRequest = new()
            {
                Title = request.Title,
                Author = request.Author,
                Isbn = request.Isbn,
                Publisher = request.Publisher,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ProcessId = jc_process.ProcessId,
            };
            var jc_bookRequest = await _workflowRepository.AddBookRequest(newBookRequest);
            if (jc_bookRequest == null)
            {
                return null;
            }

            //add to workflow action
            WorkflowAction newWa = new()
            {
                Action = currAction,
                ActionDate = DateTime.UtcNow,
                ActorId = user.Id,
                Comments = request.Notes,
                StepId = currStepId,
                ProcessId = jc_process.ProcessId,
            };

            await _workflowRepository.AddWorkflowAction(newWa);

            MailData mailData = new MailData();
            var emailBody = System.IO.File.ReadAllText(@"./EmailTemplate.html");
            emailBody = string.Format(emailBody,
                    request.Title,
                    request.Isbn,
                    request.Author,
                    request.Publisher
                    //request.LocationId
                );

            List<string> emailTo = new List<string>();
            List<string> emailCC = new List<string>();

            emailTo.Add(user.Email);

            var isRequiredRole = ws.Where(w => w.StepId == nextStepId).Select(s => s.RequiredRole).FirstOrDefault();
            if (isRequiredRole != null)
            {
                var rr = await _roleManager.FindByIdAsync(isRequiredRole);
                var rrList = await _userManager.GetUsersInRoleAsync(rr.Name);
                foreach (var i in rrList)
                {
                    emailCC.Add(i.Email);
                }
            }
            mailData.EmailBody = emailBody;
            mailData.EmailSubject = "Book Request";
            mailData.EmailToIds = emailTo;
            mailData.EmailCCIds = emailCC;
            mailData.EmailBody = emailBody;
           
            var response = _emailService.SendMail(mailData);
            return response;
        }

        public async Task<object> ApprovalBookRequest(ApprovalBookRequest request)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);


            var role_LN = await _roleManager.FindByNameAsync("Librarian");
            var role_LM = await _roleManager.FindByNameAsync("Library Manager");  

            var br = await _workflowRepository.GetBookRequest(request.BookRequestId);
            if (br == null)
            {
                return null;
            }

            var process = await _workflowRepository.GetProcess(br.ProcessId);
            if(process == null)
            {
                return null;
            }



            //steps
            var ns = await _workflowRepository.GetAllNextStepRules();

            var wfId = process.WorkflowId;
            var currStepId = process.CurrentStepId;

            var nextStepId = 0;
            var currAction = "";

            if (request.Approval == "Approved")
            {
                nextStepId = ns
                    .Where(w => w.CurrentStepId == currStepId && w.ConditionValue == "Approved")
                    .Select(s => s.NextStepId)
                    .FirstOrDefault();
                currAction = ns
                   .Where(w => w.CurrentStepId == currStepId && w.ConditionValue == "Approved")
                   .Select(s => s.ConditionValue)
                   .FirstOrDefault();
            }
            if (request.Approval == "Rejected")
            {
                nextStepId = ns
                    .Where(w => w.CurrentStepId == currStepId && w.ConditionValue == "Rejected")
                    .Select(s => s.NextStepId)
                    .FirstOrDefault();
                currAction = ns
                   .Where(w => w.CurrentStepId == currStepId && w.ConditionValue == "Rejected")
                   .Select(s => s.ConditionValue)
                   .FirstOrDefault();
            }
            var ws = await _workflowRepository.GetAllWorkflowSequence();

            process.CurrentStepId = nextStepId;
            process.Status = request.Approval;
            var ju_process = await _workflowRepository.UpdateProcess(process);
            if (ju_process == null)
            {
                return null;
            }
            //add to workflow action
            WorkflowAction newWa = new()
            {
                Action = currAction,
                ActionDate = DateTime.UtcNow,
                ActorId = user.Id,
                Comments = request.Notes,
                StepId = currStepId,
                ProcessId = ju_process.ProcessId,
            };

            await _workflowRepository.AddWorkflowAction(newWa);


            MailData mailData = new MailData();
            var emailBody = System.IO.File.ReadAllText(@"./EmailTemplate.html");
            emailBody = string.Format(emailBody,
                    br.Title,
                    br.Isbn,
                    br.Author,
                    br.Publisher
                    //br.LocationId
                );

            List<string> emailTo = new List<string>();
            List<string> emailCC = new List<string>();


            var wa = await _workflowRepository.GetAllWorkflowAction();

            var isRequiredRole = ws.Where(w => w.StepId == nextStepId).Select(s => s.RequiredRole).FirstOrDefault();
            if (isRequiredRole != null)
            {
                var rr = await _roleManager.FindByIdAsync(isRequiredRole);
                var rrList = await _userManager.GetUsersInRoleAsync(rr.Name);
                foreach (var i in rrList)
                {
                    emailCC.Add(i.Email);
                }
            }

            emailTo.Add(user.Email);
            var ccIds = wa.Where(w => w.ProcessId == process.ProcessId).Select(s => s.ActorId).ToList();
            foreach (var id in ccIds)
            {
                var ccUser = await _userManager.FindByIdAsync(id);
                emailCC.Add(ccUser.Email);
            }

            mailData.EmailBody = emailBody;
            mailData.EmailSubject = "Book Request Approval";
            mailData.EmailToIds = emailTo;
            mailData.EmailCCIds = emailCC;
            mailData.EmailBody = emailBody;

            var response = _emailService.SendMail(mailData);
            return response;
        }

        // Get Dashboard
        public async Task<object> GetDashboard()
        {
            var bookList = await _bookRepository.GetAll();
            var userList = await _userRepository.GetAll();
            var bookTransaction = await _bookUserTransactionRepository.GetAll();
            var bookOverdue = bookTransaction.Where(w => w.DueDate < DateOnly.FromDateTime(DateTime.Now)).ToList();

            var ma = bookTransaction
               .GroupBy(gb => gb.UserId)
               .Select(s => new
               {
                   UserId = s.Key,
                   Count = s.Count(),
               })
               .OrderByDescending(ob => ob.Count)
               .ToList();

            List<object> bookCategoryCounts = new();
            var ctg = bookList.Select(s => s.Category).Distinct().ToList();
            foreach (var c in ctg)
            {
                var count = 0;
                foreach(var b in bookList)
                {
                    if(c == b.Category)
                    {
                        count++;
                    }
                }

                bookCategoryCounts.Add(new
                {
                    Category = c,
                    Count = count
                });
            }

            //list book overdue
            List<object> listBo = new List<object>();
            foreach (var value in bookOverdue)
            {
                var getUser = await _userRepository.Get(value.UserId);
                var overdue = DateOnly.FromDateTime(DateTime.Now).DayNumber - value.DueDate.DayNumber;
                //var penalty = overdue * _libraryOptions.PenaltyPerDay;

                listBo.Add(new
                {
                    Username = getUser.FName + " " + getUser.LName,
                    Title = value.Title,
                    OverdueDays = overdue,
                    DueDate = value.DueDate
                    //penalty = penalty,
                });
            }

            //list most active member
            List<object> listMa = new List<object>();
            foreach (var value in ma)
            {
                var getUser = await _userRepository.Get(value.UserId);
                listMa.Add(new
                {
                    UserId = value.UserId,
                    Name = getUser.FName + " " + getUser.LName,
                    Position = getUser.UserPosition,
                    LibraryCardNumber = getUser.LibraryCardNumber,
                    TotalTransaction = value.Count,
                });
            }


         

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _userManager.GetRolesAsync(user);



            var bookRequest = await _workflowRepository.GetAllBookRequest();
            var process = await _workflowRepository.GetAllProcess();
            var workflowSequences = await _workflowRepository.GetAllWorkflowSequence();
            var users = await _userRepository.GetAll();

           

            List<string> roleId = new();
            foreach (var r in role)
            {
                var appRole = await _roleManager.FindByNameAsync(r);
                roleId.Add(appRole.Id);
            }


            var result = from value in process
                         join br in bookRequest on value.ProcessId equals br.ProcessId
                         join ws in workflowSequences on value.CurrentStepId equals ws.StepId
                         where roleId.Any(a => a == ws.RequiredRole) || ws.RequiredRole == null
                         select new
                         {
                             BookRequestId = br.BookRequestId,
                             RequestDate = DateOnly.FromDateTime(value.RequestDate.GetValueOrDefault()),
                             RequesterId = value.RequesterId,
                             RequesterName = $"{users.Where(w => w.AppUserId == value.RequesterId).FirstOrDefault().FName} {users.Where(w => w.AppUserId == value.RequesterId).FirstOrDefault().LName}",
                             Title = br.Title,
                             Author = br.Author,
                             Publisher = br.Publisher,
                             Isbn = br.Isbn,
                             Status = ws.StepName,
                             StepId = ws.StepId
                         };


            var userStepId = workflowSequences.Where(w => roleId.Any(a => a == w.RequiredRole)).Select(s => s.StepId).FirstOrDefault();
            var processToFollowUpData = result.Where(w => w.StepId == userStepId).OrderBy(s => s.StepId);

            return new
            {
                TotalBooks = bookList.Count(),
                MostActiveUsers = listMa,
                OverdueBooks = listBo,
                BooksPerCategory = bookCategoryCounts,
                ProcessToFollowUp = processToFollowUpData.Count(),
                ProccessToFollowUpData = processToFollowUpData.Take(5).ToList()
            };
        }

        // Get Workflow Dashboard  
        public async Task<List<object>> GetWorkflowDashboard()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _userManager.GetRolesAsync(user);

            List<string> roleId = new();
            foreach(var r in role)
            {
                var appRole = await _roleManager.FindByNameAsync(r);
                roleId.Add(appRole.Id);
            }
             
 
            var ns = await _workflowRepository.GetAllNextStepRules();
            var wf = await _workflowRepository.GetAllWorkflow();

            var ws = await _workflowRepository.GetAllWorkflowSequence();
            var userStepId = ws.Where(w => roleId.Any(a => a == w.RequiredRole)).Select(s => s.StepId).FirstOrDefault();
            var allProcess = await _workflowRepository.GetAllProcess();
            var userProcess = allProcess.Where(w => w.CurrentStepId == userStepId).ToList();

            List<object> result = new List<object>();
            foreach (var value in userProcess)
            {
                var wfName = wf.Where(w=>w.WorkflowId == value.WorkflowId).Select(s=>s.WorkflowName).FirstOrDefault();
                var stepName = ws.Where(w=>w.StepId == value.CurrentStepId).Select(s=>s.StepName).FirstOrDefault();
                var requester = await _userRepository.GetByAppUserId(value.RequesterId);
                result.Add(new
                {
                    ProcessId = value.ProcessId,
                    Workflow = wfName,
                    Requester = requester.FName + " " + requester.LName,
                    RequestDate = value.RequestDate,
                    Status = value.Status,
                    CurrentStep = stepName,
                });
            }
            return result;
        }


        //GET REQUEST BOOK LIST
        public async Task<List<object>> GetRequestBookList()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _userManager.GetRolesAsync(user);

            var roleId = _roleManager.Roles.Where(w => role.Contains(w.Name)).FirstOrDefault().Id;

            var bookRequest = await _workflowRepository.GetAllBookRequest();
            var process = await _workflowRepository.GetAllProcess();
            var workflowSequences = await _workflowRepository.GetAllWorkflowSequence();
            var users = await _userRepository.GetAll();



            var result = from value in process
                 
                         join br in bookRequest on value.ProcessId equals br.ProcessId
                         join ws in workflowSequences on value.CurrentStepId equals ws.StepId
                         where ws.RequiredRole == roleId || ws.RequiredRole == null
                         select new
                         {
                             BookRequestId = br.BookRequestId,
                             RequestDate = DateOnly.FromDateTime(value.RequestDate.GetValueOrDefault()),
                             RequesterId = value.RequesterId,
                             RequesterName = $"{users.Where(w => w.AppUserId == value.RequesterId).FirstOrDefault().FName} {users.Where(w => w.AppUserId == value.RequesterId).FirstOrDefault().LName}",
                             Title = br.Title,
                             Author = br.Author,
                             Publisher = br.Publisher,
                             Isbn = br.Isbn,
                             Status = ws.StepName,
                             StepId = ws.StepId
                         };

            return result.OrderBy(ob=>ob.BookRequestId).ToList<object>();
        }

        //GET REQUEST BOOK LIST
        public async Task<object> GetRequestBookListPaged(SortRequestBookQuery query, PageRequest pageRequest)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _userManager.GetRolesAsync(user);

            var roleId = _roleManager.Roles.Where(w => role.Contains(w.Name)).FirstOrDefault().Id;

            var bookRequest = await _workflowRepository.GetAllBookRequest();
            var process = await _workflowRepository.GetAllProcess();
            var workflowSequences = await _workflowRepository.GetAllWorkflowSequence();
            var users = await _userRepository.GetAll();



            var result = from value in process
                         join br in bookRequest on value.ProcessId equals br.ProcessId
                         join ws in workflowSequences on value.CurrentStepId equals ws.StepId
                         where ws.RequiredRole == roleId || ws.RequiredRole == null
                        select new
                          {
                                BookRequestId = br.BookRequestId,
                                RequestDate = DateOnly.FromDateTime(value.RequestDate.GetValueOrDefault()),
                                RequesterId = value.RequesterId,
                                RequesterName = $"{users.Where(w => w.AppUserId == value.RequesterId).FirstOrDefault().FName} {users.Where(w => w.AppUserId == value.RequesterId).FirstOrDefault().LName}",
                                Title = br.Title,
                                Author = br.Author,
                                Publisher = br.Publisher,
                                Isbn = br.Isbn,
                                Status = ws.StepName,
                                StepId = ws.StepId
                          };

            var total = result.Count();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy)
                {

                    case "status":
                        result = query.SortOrder.Equals("asc") ? result.OrderBy(s => s.StepId) :
                        result.OrderByDescending(s => s.StepId);
                        break;

                    case "requester":
                        result = query.SortOrder.Equals("asc") ? result.OrderBy(s => s.RequesterId) :
                        result.OrderByDescending(s => s.RequesterId);
                        break;

                    case "title":
                        result = query.SortOrder.Equals("asc") ? result.OrderBy(s => s.Title) :
                        result.OrderByDescending(s => s.Title);
                        break;

                    case "author":
                        result = query.SortOrder.Equals("asc") ? result.OrderBy(s => s.Author) :
                        result.OrderByDescending(s => s.Author);
                        break;

                    case "publisher":
                        result = query.SortOrder.Equals("asc") ? result.OrderBy(s => s.Publisher) :
                        result.OrderByDescending(s => s.Publisher);
                        break;

                    case "isbn":
                        result = query.SortOrder.Equals("asc") ? result.OrderBy(s => s.Isbn) :
                        result.OrderByDescending(s => s.Isbn);
                        break;

                    default:
                        result = query.SortOrder.Equals("asc") ? result.OrderBy(s => s.StepId) :
                        result.OrderByDescending(s => s.StepId);
                        break;
                }
            }


            return new
            {
                Total = total,
                Page = pageRequest.PageNumber,
                Data = result
                .Skip((pageRequest.PageNumber - 1) * pageRequest.PerPage)
                .Take(pageRequest.PerPage)
                .ToList()
            };

           
        }

        //GET REQUEST BOOK DETIAL
        public async Task<object> GetRequestBookDetail(int id)
        {

            var bookRequest = await _workflowRepository.GetBookRequest(id);

            if(bookRequest == null)
            {
                return null;
            }

            var process = await _workflowRepository.GetProcess(bookRequest.ProcessId);
            var workflowSequences = await _workflowRepository.GetAllWorkflowSequence();
            var workflowActions = await _workflowRepository.GetAllWorkflowAction();
            var users = await _userRepository.GetAll();

            var result = new
            {
                BookRequestId = bookRequest.BookRequestId,
                ProcessId = process.ProcessId,
                RequestDate = DateOnly.FromDateTime(process.RequestDate.GetValueOrDefault()),
                RequesterId = process.RequesterId,
                RequesterName = $"{users.Where(w => w.AppUserId == process.RequesterId).FirstOrDefault().FName} {users.Where(w => w.AppUserId == process.RequesterId).FirstOrDefault().LName}",
                Title = bookRequest.Title,
                Author = bookRequest.Author,
                Publisher = bookRequest.Publisher,
                Isbn = bookRequest.Isbn,
                Status = workflowSequences.Where(w=>w.StepId == process.CurrentStepId).Select(s=>s.StepName).FirstOrDefault(),
                RequestHistory = workflowActions.Where(w=>w.ProcessId == process.ProcessId).Select(s => new
                {
                    ActorId = s.ActorId,
                    StepId = s.StepId,
                    ProcessId = s.ProcessId,
                    ActionDate = s.ActionDate,
                    ActorName = $"{users.Where(w=>w.AppUserId == s.ActorId).FirstOrDefault().FName} {users.Where(w => w.AppUserId == s.ActorId).FirstOrDefault().LName}",
                    Action = s.Action,
                    Comments = s.Comments,
                }),
            };


            return result;
        }


    }
}
