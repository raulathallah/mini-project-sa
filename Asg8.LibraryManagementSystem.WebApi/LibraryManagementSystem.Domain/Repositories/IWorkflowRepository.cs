using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IWorkflowRepository
    {
        Task<Process> AddProcess(Process process);
        Task<Process> UpdateProcess(Process process);
        Task<Process> GetProcess(int id);
        Task<IQueryable<Process>> GetAllProcess();
        Task<BookRequest> AddBookRequest(BookRequest br);
        Task<BookRequest> UpdateBookRequest(BookRequest br);
        Task<BookRequest> GetBookRequest(int id);
        Task<IQueryable<BookRequest>> GetAllBookRequest();
        Task<WorkflowAction> AddWorkflowAction(WorkflowAction wa);
        Task<IQueryable<WorkflowSequence>> GetAllWorkflowSequence();
        Task<IQueryable<NextStepRules>> GetAllNextStepRules();
        Task<IQueryable<Workflow>> GetAllWorkflow();
        Task<IQueryable<WorkflowAction>> GetAllWorkflowAction();
    }
}
