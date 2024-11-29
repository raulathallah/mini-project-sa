using CompanyWeb.Application.Mappers;
using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Helpers;
using CompanyWeb.Domain.Models.Mail;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Requests.Update;
using CompanyWeb.Domain.Models.Responses.Base;
using CompanyWeb.Domain.Models.Responses.Employee;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompanyWeb.Application.Services
{
    public class EmployeeService :  IEmployeeService
    {
        private readonly ICompanyService _companyService;
        private readonly IDepartementRepository _departementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeDependentRepository _employeeDependentRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IEmailService _emailService;
        private readonly CompanyOptions _companyOptions;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeService(ICompanyService companyService, 
            IDepartementRepository departementRepository,
            IEmployeeRepository employeeRepository, 
            IEmployeeDependentRepository employeeDependentRepository, 
            IOptions<CompanyOptions> companyOptions,
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor,
            IWorkflowRepository workflowRepository,
            IEmailService emailService
            )
        {
            _companyService = companyService;
            _departementRepository = departementRepository;
            _employeeRepository = employeeRepository;
            _employeeDependentRepository = employeeDependentRepository;
            _companyOptions = companyOptions.Value;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _workflowRepository = workflowRepository;
            _emailService = emailService;
        }

        public async Task<object> AssignEmployee(int id, int deptNo)
        {
            var emp = await _employeeRepository.GetEmployee(id);
            if (emp == null)
            {
                return null;
            }

            if(emp.Deptno != deptNo)
            {
                emp.DirectSupervisor = null;
            }

            emp.Deptno = deptNo;

            await _employeeRepository.Update(emp);


            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(id);
            return emp.ToEmployeeResponse(dependents);
        }

        public async Task<object> CreateEmployee(AddEmployeeRequest request)
        {
            var response = CreateResponse();
            var newEmp = new Employee()
            {
                Fname = request.Fname,
                Lname = request.Lname,
                Dob = request.Dob,
                Sex = request.Sex,
                AppUserId = "",
                Address = request.Address,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Position = request.Position,
                Deptno = request.Deptno,
                EmpLevel = request.EmpLevel,
                EmpType = request.EmpType,
                Ssn = request.Ssn,
                Salary = request.Salary,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                DirectSupervisor = request.DirectSupervisor,
                UpdatedAt = null,
            };
            int deptId = await _departementRepository.GetDepartementIdByName("IT");
            if(request.Deptno == deptId)
            {
                int memberCount = _companyService.GetEmployeeIT().Result.Count;
                if (memberCount >= _companyOptions.MaxDepartementMemberIT)
                {
                    response.Message = $"IT Employee full ({memberCount}/9). Please try again";
                    return response;
                }
            }
            
            var data = await _employeeRepository.Create(newEmp);

            foreach (var item in request.EmpDependents)
            {
                var newEmpDependent = new EmployeeDependent()
                {
                    DependentEmpno = data.Empno,
                    BirthDate = item.BirthDate,
                    Fname = item.Fname,
                    Lname = item.Lname,
                    Sex = item.Sex,
                    Relation = item.Relation
                };
                await _employeeDependentRepository.Create(newEmpDependent);
            }

            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(data.Empno);

            response.Status = true;
            response.Message = "Success";
            response.Data = data.ToEmployeeResponse(dependents);
            return response;
        }

        public async Task<object> DeactivateEmployee(int id, DeactivateEmployeeRequest request)
        {
            var emp = await _employeeRepository.GetEmployee(id);
            if(emp == null || emp.IsActive == false)
            {
                return null;
            }
            if(emp.IsActive == true)
            {
                emp.IsActive = false;
                emp.UpdatedAt = DateTime.UtcNow;
                emp.DeactivateReason = request.DeactivateReason;
            }

            var response = await _employeeRepository.Update(emp);
            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(id);
            return response.ToEmployeeResponse(dependents);

        }

        public async Task<object> DeleteEmployee(int id)
        {
            var e = await _employeeRepository.GetEmployee(id);
            if (e == null)
            {
                return null;
            }
            var response = await _employeeRepository.Delete(e);
            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(id);
            await _employeeDependentRepository.Delete(id);

            var user = await _userManager.FindByEmailAsync(e.EmailAddress);
            if (user == null)
            {
                return null;
            }
            await _userManager.DeleteAsync(user);

            return response.ToEmployeeResponse(dependents);
        }

        // Generate Employee Report
        public async Task<byte[]> GenerateEmployeeReportPDF(int deptNo)
        {

            var document = new PdfDocument();
            var config = new PdfGenerateConfig();
            config.PageOrientation = PageOrientation.Landscape;
            config.SetMargins(8);
            config.PageSize = PageSize.A4;
            string cssStr = File.ReadAllText(@"./style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);

            var employees = await _employeeRepository.GetAllEmployees();
            var employeeFiltered = employees.Where(w => w.Deptno == deptNo);
            var totalPage = 0;
            var page = 1;
            var perPage = 2;
            if (employeeFiltered.Count() > perPage)
            {
                totalPage = employeeFiltered.Count() / perPage;
            }
            else
            {
                totalPage = 1;
            }


            for(int i = 0; i < totalPage; i++)
            {
                string htmlcontent = String.Empty;
                var paged = employeeFiltered.Skip((page - 1) * perPage).Take(perPage).ToList();
                htmlcontent += "</table>"; 
                htmlcontent += "<h3>Employees Information</h3>";
                htmlcontent += "<table>";
                htmlcontent +=
                    "<tr>" +
                    "<td>Employee No</td>" +
                    "<td>Name</td>" +
                    "<td>Email</td>" +
                    "<td>Phone Number</td>" +
                    "<td>Address</td>" +
                    "<td>Position</td>" +
                    "<td>Status</td>" +
                    "<td>Department</td>" +
                    "</tr>";
                foreach (var value in paged)
                {
                    htmlcontent += "<tr>";
                    htmlcontent += "<td>" + value.Empno + "</td>";
                    htmlcontent += "<td>" + value.Fname + " " + value.Lname + "</td>";
                    htmlcontent += "<td>" + value.EmailAddress + "</td>";
                    htmlcontent += "<td>" + value.PhoneNumber + "</td>";
                    htmlcontent += "<td>" + value.Address + "</td>";
                    htmlcontent += "<td>" + value.Position + "</td>";

                    var status = "";
                    if (value.IsActive)
                    {
                        status = "ACTIVE";
                    }
                    if (!value.IsActive)
                    {
                        status = "INACTIVE";

                    }
                    htmlcontent += "<td>" + status + "</td>";
                    htmlcontent += "<td>" + value.Deptno + "</td>";
                    htmlcontent += "</tr>";
                }
                htmlcontent += "</table>";
                PdfGenerator.AddPdfPages(document, htmlcontent, config, css);
                page++;
            }

            MemoryStream stream = new();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return bytes;
        }

        // EMPLOYEE REPORT JSON
        public async Task<List<object>> GetEmployeeReport(int deptNo, int page)
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var employeeFiltered = employees.Where(w => w.Deptno == deptNo);
            var totalPage = 0;
            var perPage = 20;

            if (employeeFiltered.Count() > perPage)
            {
                totalPage = employeeFiltered.Count() / perPage;
            }
            else
            {
                totalPage = 1;
            }

            var paged = employeeFiltered.Skip((page - 1) * perPage).Take(perPage).ToList();

            return paged.Select(s => new
            {
                Empno = s.Empno,
                EmpName = s.Fname + " " + s.Lname,
                Email = s.EmailAddress,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                Position = s.Position,
                Status = s.IsActive,
                Department = s.Deptno
            })
            .ToList<object>();

        }

        // Generate Leave Report 
        public async Task<byte[]> GenerateLeaveReportPDF(LeaveReportRequest request)
        {
            string htmlcontent = String.Empty;
            var document = new PdfDocument();
            var config = new PdfGenerateConfig();
            config.PageOrientation = PageOrientation.Landscape;
            config.SetMargins(8);
            config.PageSize = PageSize.A4;
            string cssStr = File.ReadAllText(@"./style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);

            var process = await _workflowRepository.GetAllProcess();
            var requests = await _workflowRepository.GetAllLeaveRequest();
            var workflow = await _workflowRepository.GetAllWorkflow();

            var leaveWorkflowId = workflow
                .Where(w => w.WorkflowName == "Employee Leave Request")
                .Select(s => s.WorkflowId)
                .FirstOrDefault();

            var join = (from value in process.ToList()
                        join req in requests.ToList() on value.ProcessId equals req.ProcessId
                        where value.Status == "Approved" && value.WorkflowId == leaveWorkflowId
                        select req)
                        .Where(w=>w.StartDate >= request.StartDate && w.EndDate <= request.EndDate)
                        .GroupBy(gb => gb.LeaveType)
                        .ToList();

            htmlcontent += "<h3>Total Leave Taken</h3>";
            htmlcontent += "<table>";
            htmlcontent +=
                "<tr>" +
                "<td>Leave Type</td>" +
                "<td>Total</td>" +
                "</tr>";
            foreach (var value in join)
            { 
                htmlcontent += "<tr>";
                htmlcontent += "<td>" + value.Key + "</td>";
                htmlcontent += "<td>" + value.Count() + "</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";
            PdfGenerator.AddPdfPages(document, htmlcontent, config, css);
            MemoryStream stream = new();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return bytes;
        }

        // LEAVE REPORT JSON
        public async Task<List<object>> GetLeaveReport(LeaveReportRequest request)
        {

            var process = await _workflowRepository.GetAllProcess();
            var requests = await _workflowRepository.GetAllLeaveRequest();
            var workflow = await _workflowRepository.GetAllWorkflow();

            var leaveWorkflowId = workflow
                .Where(w => w.WorkflowName == "Employee Leave Request")
                .Select(s => s.WorkflowId)
                .FirstOrDefault();

            var join = (from value in process.ToList()
                        join req in requests.ToList() on value.ProcessId equals req.ProcessId
                        where value.Status == "Approved" && value.WorkflowId == leaveWorkflowId
                        select req)
                        .Where(w => w.StartDate >= request.StartDate && w.EndDate <= request.EndDate)
                        .GroupBy(gb => gb.LeaveType)
                        .Select(s => new
                        {
                            LeaveType = s.Key,
                            Total = s.Count()
                        })
                        .ToList<object>();
            return join;
        }
        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequest()
        {
            return await _workflowRepository.GetAllLeaveRequest();
        }

        public async Task<object> GetEmployee(int id)
        {
            var emp = await _employeeRepository.GetEmployee(id);
            var employee = await _employeeRepository.GetAllEmployees();
            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(id);
            return emp.ToEmployeeDetailResponse(dependents);
        }

        public async Task<object> GetEmployeeByAppUserId(string id)
        {
            var allEmp = await _employeeRepository.GetAllEmployees();
            var employee = allEmp.Where(w => w.AppUserId == id).FirstOrDefault();

            if(employee == null)
            {
                return null;
            }
            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(employee.Empno);
            return employee.ToEmployeeDetailResponse(dependents);
        }

        public async Task<List<object>> GetEmployees(int pageNumber, int perPage)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var employees = await _employeeRepository.GetEmployees(pageNumber, perPage);

            var allEmployees = await _employeeRepository.GetAllEmployees();
            var userRequest = allEmployees.Where(w => w.AppUserId == user.Id).FirstOrDefault();


            if(roles.Any(x=>x == "Employee Supervisor"))
            {
                return employees
               .Where(w => w.DirectSupervisor == userRequest.Empno) 
               .Select(s => s.ToEmployeeResponse(
                   _employeeDependentRepository.GetEmployeeDependentByEmpNo(s.Empno).Result
                   ))
               .ToList<object>();
            }

            return employees
                .Select(s => s.ToEmployeeResponse(
                    _employeeDependentRepository.GetEmployeeDependentByEmpNo(s.Empno).Result
                    ))
                .ToList<object>();
        }

        public async Task<List<object>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return employees.Select(s => s.ToEmployeeResponse(null)).ToList<object>();

        }

        public async Task<object> LeaveApproval(EmployeeLeaveApprovalRequest request)
        {
            //langsung dari process aja
            var ws = await _workflowRepository.GetAllWorkflowSequence();
            var ns = await _workflowRepository.GetAllNextStepRules();
            var wf = await _workflowRepository.GetAllWorkflow();

            // approver
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var leaveRequest = await _workflowRepository.GetLeaveRequest(request.LeaveRequestId);
            if (leaveRequest == null)
            {
                return @"Leave Request Null";
            }

            // get process
            var process = await _workflowRepository.GetProcess(leaveRequest.ProcessId);
            if( process == null )
            {
                return @"Process Null";
            }

            //find employee who request to leave
            var leaveEmployee = await _employeeRepository.GetEmployee(process.Empno);

            var wfId = process.WorkflowId;
            var currStepId = process.CurrentStepId;

            var requiredRoleId = ws.Where(w => w.StepId == currStepId).Select(s => s.RequiredRole).FirstOrDefault();
            var requiredRole = await _roleManager.FindByIdAsync(requiredRoleId);

            if (!roles.Any(a=>a == requiredRole.Name))
            {
                return @"Unauthorized";
            }


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
            if(request.Approval == "Rejected")
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
            
    
            // update process
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
                Comments = currAction,
                StepId = currStepId,
                ProcessId = ju_process.ProcessId,
            };
            await _workflowRepository.AddWorkflowAction(newWa);


            //var manager = await _userManager.GetUsersInRoleAsync("HR Manager");
            MailData mailData = new MailData();
            List<string> emailTo = new List<string>();
            List<string> emailCC = new List<string>();

            var wa = await _workflowRepository.GetAllWorkflowAction();

            var rrName = "";
            var isRequiredRole = ws.Where(w=>w.StepId == nextStepId).Select(s=>s.RequiredRole).FirstOrDefault();
            if (isRequiredRole != null)
            {
                var rr = await _roleManager.FindByIdAsync(isRequiredRole);
                var rrList = await _userManager.GetUsersInRoleAsync(rr.Name);
                foreach(var i in rrList)
                {
                    emailCC.Add(i.Email);
                }
            }
            emailTo.Add(user.Email);
            var ccIds = wa.Where(w=>w.ProcessId == process.ProcessId).Select(s=>s.ActorId).ToList();
            foreach(var id in ccIds)
            {
                var ccUser = await _userManager.FindByIdAsync(id);
                emailCC.Add(ccUser.Email);
            }

            var emailBody = System.IO.File.ReadAllText(@"./EmailTemplate.html");
            if(roles.Any(a=>a == "Employee Supervisor"))
            {
                emailBody = string.Format(emailBody,
                   leaveEmployee.Empno,
                   leaveRequest.StartDate,
                   leaveRequest.EndDate,
                   request.Approval,
                   "Pending"
               );
            }
            if (roles.Any(a => a == "HR Manager"))
            {
                emailBody = string.Format(emailBody,
                   leaveEmployee.Empno,
                   leaveRequest.StartDate,
                   leaveRequest.EndDate,
                   "Approved",
                   request.Approval
               );
            }

            mailData.EmailSubject = "Leave Request";
            mailData.EmailToIds = emailTo;
            mailData.EmailCCIds = emailCC;
            mailData.EmailBody = emailBody;

            var response = _emailService.SendMail(mailData);
            return response;
            /*return new BaseResponse()
            {
                Status = true,
                Message = "Success approval leave request"
            }; ;*/
        }

        public async Task<object> LeaveRequest(EmployeeLeaveRequest request)
        {
            var role_E = await _roleManager.FindByNameAsync("Employee");
            var allEmp = await _employeeRepository.GetAllEmployees();

            // requester
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var requesterEmp = allEmp
                .Where(w=>w.AppUserId == user.Id)
                .FirstOrDefault();
            var requesterDirectSpv = await _employeeRepository
                .GetEmployee(requesterEmp.DirectSupervisor.GetValueOrDefault());

            // direct spv
            var directSpvUser = await _userManager.FindByIdAsync(requesterDirectSpv.AppUserId);

            var ws = await _workflowRepository.GetAllWorkflowSequence();
            var ns = await _workflowRepository.GetAllNextStepRules();
            var wf = await _workflowRepository.GetAllWorkflow();

            //steps
            var wfId = wf
                .Where(w => w.WorkflowName == "Employee Leave Request")
                .Select(s => s.WorkflowId)
                .FirstOrDefault();

            var currStepId = ws
                .Where(w => w.WorkflowId == wfId && w.RequiredRole == role_E.Id)
                .Select(s => s.WorkflowId)
                .FirstOrDefault();

            var nextStepId = ns
                .Where(w => w.CurrentStepId == currStepId)
                .Select(s => s.NextStepId)
                .FirstOrDefault();

            var currAction = ns
                .Where(w => w.CurrentStepId == currStepId)
                .Select(s => s.ConditionValue)
                .FirstOrDefault();

            // add to process
            Process newProcess = new()
            {
                RequestDate = DateTime.UtcNow,
                RequesterId = user.Id,
                Empno = request.Empno,
                CurrentStepId = nextStepId,
                Status = "Pending",
                WorkflowId = wfId,
            };
            var jc_process = await _workflowRepository.AddProcess(newProcess);
            if (jc_process == null)
            {
                return null;
            }
            //add to leave request
            LeaveRequest newLeaveRequest = new()
            {
                Empno = request.Empno,
                LeaveReason = request.LeaveReason,
                LeaveType = request.LeaveType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ProcessId = jc_process.ProcessId,
            };
            var jc_bookRequest = await _workflowRepository.AddLeaveRequest(newLeaveRequest);
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
                Comments = currAction,
                StepId = currStepId,
                ProcessId = jc_process.ProcessId,
            };

            await _workflowRepository.AddWorkflowAction(newWa);

            MailData mailData = new MailData();
            var emailBody = System.IO.File.ReadAllText(@"./EmailTemplate.html");
            emailBody = string.Format(emailBody,
                    request.Empno,
                    request.StartDate,
                    request.EndDate,
                    "Pending",
                    "Pending"
                );

            List<string> emailTo = new List<string>();
            List<string> emailCC = new List<string>();

            emailTo.Add(user.Email);
            emailCC.Add(directSpvUser.Email);

            mailData.EmailBody = emailBody;
            mailData.EmailSubject = "Leave Request";
            mailData.EmailToIds = emailTo;
            mailData.EmailCCIds = emailCC;
            mailData.EmailBody = emailBody;

            var response = _emailService.SendMail(mailData);
           /* return new BaseResponse()
            { 
                Status = true,
                Message = "Success submit leave request"
            };*/
            return response;
        }

        public async Task<object> SearchEmployee(SearchEmployeeQuery query, PageRequest pageRequest)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var employees = await _employeeRepository.GetAllEmployees();
            var userRequest = employees.Where(w => w.AppUserId == user.Id).FirstOrDefault();


            if (roles.Any(x => x == "Employee Supervisor"))
            {
                employees = employees.Where(w => w.DirectSupervisor == userRequest.Empno);
            }

            if (roles.Any(x => x == "Department Manager"))
            {
                employees = employees.Where(w => w.Deptno == userRequest.Deptno);

            }

            var total = employees.Count();
            
            bool isKeyWord = !string.IsNullOrWhiteSpace(query.KeyWord);
            bool isSearchBy = !string.IsNullOrWhiteSpace(query.SearchBy);
            bool isSort = !string.IsNullOrWhiteSpace(query.SortBy);

            Console.WriteLine(query.KeyWord);
            if (isKeyWord && isSearchBy)
            {
                if(query.SearchBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.Fname.ToLower().Contains(query.KeyWord.ToLower())
                   || x.Lname.ToLower().Contains(query.KeyWord.ToLower()));
                };
                if (query.SearchBy.Equals("dept", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.DeptnoNavigation.Deptname.ToLower().Contains(query.KeyWord.ToLower()));
                };
                if (query.SearchBy.Equals("position", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.Position.ToLower().Contains(query.KeyWord.ToLower()));
                };
                if (query.SearchBy.Equals("level", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.EmpLevel == int.Parse(query.KeyWord));
                };
                if (query.SearchBy.Equals("type", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.EmpType.ToLower().Contains(query.KeyWord.ToLower()));
                };
            }

            if (isSort)
            {
                employees = SortEmployeeByField(employees, query.SortBy, query.isDescending);
            }

            if (query.isActive == false)
            {
                employees = employees.Where(w => w.IsActive == false);
            }
            else
            if (query.isActive == true)
            {
                employees = employees.Where(w => w.IsActive == true);
            }



            return new
            {
                Total = total,
                Data = await employees
                .Skip((pageRequest.PageNumber - 1) * pageRequest.PerPage)
                .Take(pageRequest.PerPage)
                .Select(s => s.ToEmployeeSearchResponse(s.DeptnoNavigation.Deptname))
                .ToListAsync()
            };
        }

        public async Task<object> UpdateEmployee(int id, UpdateEmployeeRequest request)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var e = await _employeeRepository.GetEmployee(id);
            if (e == null)
            {
                return null;
            }

            var updateUser = await _userManager.FindByEmailAsync(e.EmailAddress);
            if (updateUser != null)
            {
                updateUser.Email = request.EmailAddress;
                await _userManager.UpdateAsync(updateUser);
            }


            e.EmailAddress = request.EmailAddress;

            //if (roles.Any(x => x == "HR Manager" || x == "Administrator"))
            //{
            //    e.Deptno = request.Deptno;
            //}

            e.Address = request.Address;
            e.Position = request.Position;
            e.Dob = request.Dob;
            e.Fname = request.Fname;
            e.Lname = request.Lname;
            e.Sex = request.Sex;
            e.UpdatedAt = DateTime.UtcNow;

            if (roles.Any(x => x == "Employee Supervisor" || x == "Administrator"))
            {
                e.DirectSupervisor = request.DirectSupervisor;
            }

            if (roles.Any(x=>x == "Administrator"))
            {
                e.Ssn = request.Ssn;
                e.Salary = request.Salary;
            }

            e.EmpType = request.EmpType;
            e.EmpLevel = request.EmpLevel;
            var response = await _employeeRepository.Update(e);

            // update dependent
            await _employeeDependentRepository.Delete(id);
            foreach (var item in request.EmpDependents)
            {
                var newEmpDependent = new EmployeeDependent()
                {
                    DependentEmpno = id,
                    BirthDate = item.BirthDate,
                    Fname = item.Fname,
                    Lname = item.Lname,
                    Sex = item.Sex,
                    Relation = item.Relation
                };
                await _employeeDependentRepository.Create(newEmpDependent);
            }

            var dependents = await _employeeDependentRepository.GetAllEmployeeDependents();

  
            return response.ToEmployeeResponse(dependents.Where(w=>w.DependentEmpno == id).ToList());
        }

        MSEmployeeDetailResponse CreateResponse()
        {
            return new MSEmployeeDetailResponse()
            {
                Status = false,
                Message = "",
                Data = null,
            };
        }

        private IQueryable<Employee> SortEmployeeByField(IQueryable<Employee> employees, string field, bool isDescending)
        {
            if(field.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                employees =  isDescending ? employees.OrderByDescending(x=>x.Fname): employees.OrderBy(x=>x.Fname);
            }
            if (field.Equals("departement", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.DeptnoNavigation.Deptname) : employees.OrderBy(x => x.DeptnoNavigation.Deptname);
           
            }
            if (field.Equals("position", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.Position) : employees.OrderBy(x => x.Position);
             
            }
            if (field.Equals("level", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.EmpLevel) : employees.OrderBy(x => x.EmpLevel);
               
            }
            if (field.Equals("type", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.EmpType) : employees.OrderBy(x => x.EmpType);
              
            }
            if (field.Equals("updateDate", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.UpdatedAt) : employees.OrderBy(x => x.UpdatedAt);
              
            }
            return employees;
        }


    }
}
