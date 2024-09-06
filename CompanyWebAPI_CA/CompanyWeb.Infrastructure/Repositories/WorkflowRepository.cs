using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Repositories;
using LMS.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly CompanyDbContext Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkflowRepository(CompanyDbContext context, 
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LeaveRequest> AddLeaveRequest(LeaveRequest lr)
        {
            Context.LeaveRequests.Add(lr);
            await Context.SaveChangesAsync();
            return lr;
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

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequest()
        {
            return Context.LeaveRequests.ToList();
        }

        public async Task<IQueryable<NextStepRules>> GetAllNextStepRules()
        {
            return Context.NextStepRules;
        }

        public async Task<IQueryable<Workflow>> GetAllWorkflow()
        {
            return Context.Workflows;

        }

        public async Task<IQueryable<WorkflowAction>> GetAllWorkflowAction()
        {
            return Context.WorkflowActions;
        }

        public async Task<IQueryable<WorkflowSequence>> GetAllWorkflowSequence()
        {
            return Context.WorkflowSequences;
        }

        public async Task<LeaveRequest> GetLeaveRequest(int id)
        {
            return await Context.LeaveRequests.Where(w=>w.LeaveRequestId == id).FirstOrDefaultAsync();
        }

        public async Task<Process> GetProcess(int id)
        {
            return await Context.Process.Where(w => w.ProcessId == id).FirstOrDefaultAsync();

        }

        public async Task<LeaveRequest> UpdateLeaveRequest(LeaveRequest lr)
        {
            Context.LeaveRequests.Update(lr);
            await Context.SaveChangesAsync();
            return lr;
        }

        public async Task<Process> UpdateProcess(Process process)
        {
            Context.Process.Update(process);
            await Context.SaveChangesAsync();
            return process;
        }
    }
}
