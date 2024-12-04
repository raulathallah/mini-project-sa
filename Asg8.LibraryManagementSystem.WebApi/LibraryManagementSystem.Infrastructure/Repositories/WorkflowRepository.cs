using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly LMSDbContext Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkflowRepository(LMSDbContext context, 
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BookRequest> AddBookRequest(BookRequest br)
        {
            Context.BookRequests.Add(br);
            await Context.SaveChangesAsync();
            return br;
        }

        public async Task<Process> AddProcess(Process process)
        {
            Context.Process.Add(process);
            await Context.SaveChangesAsync();
            return process;
        }

        public async Task<WorkflowAction> AddWorkflowAction(WorkflowAction wa)
        {
            Context.WorkflowActions.Add(wa);
            await Context.SaveChangesAsync();
            return wa;
        }

        public async Task<IQueryable<WorkflowSequence>> GetAllWorkflowSequence()
        {
            /*            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                        var user = await _userManager.FindByNameAsync(userName);
                        var userRole = await _userManager.GetRolesAsync(user);*/

            /* var role = await _roleManager.FindByNameAsync("Library User");

             var req = from value in Context.Requests
                       join ws in Context.WorkflowSequences on value.CurrentStepId equals ws.StepId
                       where value.Status == "Pending" && ws.RequiredRole == role.Id
                       select ws;*/

            return Context.WorkflowSequences;
        }


        public async Task<IQueryable<NextStepRules>> GetAllNextStepRules()
        {
            return Context.NextStepRules;
        }

        public async Task<IQueryable<Workflow>> GetAllWorkflow()
        {
            return Context.Workflows;
        }

        public async Task<BookRequest> GetBookRequest(int id)
        {
            return await Context.BookRequests.Where(w => w.BookRequestId == id).FirstOrDefaultAsync();
        }

        public async Task<Process> GetProcess(int id)
        {
            return await Context.Process.Where(w => w.ProcessId == id).FirstOrDefaultAsync();
        }

        public async Task<Process> UpdateProcess(Process process)
        {
            Context.Process.Update(process);
            await Context.SaveChangesAsync();
            return process;
        }

        public async Task<BookRequest> UpdateBookRequest(BookRequest br)
        {
            Context.BookRequests.Update(br);
            await Context.SaveChangesAsync();
            return br;
        }

        public async Task<IQueryable<WorkflowAction>> GetAllWorkflowAction()
        {
            return Context.WorkflowActions;
        }

        public async Task<IQueryable<Process>> GetAllProcess()
        {
            return Context.Process;
        }

        public async Task<IQueryable<BookRequest>> GetAllBookRequest()
        {
            return Context.BookRequests;
        }
    }
}
