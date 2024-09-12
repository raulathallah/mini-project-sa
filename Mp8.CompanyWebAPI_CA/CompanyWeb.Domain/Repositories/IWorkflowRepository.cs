using CompanyWeb.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface IWorkflowRepository
    {
        Task<Process> AddProcess(Process process);
        Task<Process> UpdateProcess(Process process);
        Task<Process> GetProcess(int id);
        Task<IQueryable<Process>> GetAllProcess();
        Task<LeaveRequest> AddLeaveRequest(LeaveRequest lr);
        Task<LeaveRequest> UpdateLeaveRequest(LeaveRequest lr);
        Task<LeaveRequest> GetLeaveRequest(int id);
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequest();
        Task<WorkflowAction> AddWorkflowAction(WorkflowAction wa);
        Task<IQueryable<WorkflowSequence>> GetAllWorkflowSequence();
        Task<IQueryable<NextStepRules>> GetAllNextStepRules();
        Task<IQueryable<Workflow>> GetAllWorkflow();
        Task<IQueryable<WorkflowAction>> GetAllWorkflowAction();
    }
}
