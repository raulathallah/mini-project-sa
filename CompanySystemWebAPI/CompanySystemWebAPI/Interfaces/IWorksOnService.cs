using CompanySystemWebAPI.Dtos.ProjectsDto;
using CompanySystemWebAPI.Dtos.WorksOnDto;
using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IWorksOnService
    {
        Task<Workson> Create(WorksOnAddDto wo);
        Task<Workson> Update(int projNo, int empNo, WorksOnUpdateDto wo);
        Task<Workson> Delete(int projNo, int empNo);
        Task<List<Workson>> GetWorksons(int pageNumber, int perPage);
        Task<Workson> GetWorkson(int projNo, int empNo);

        // k.Produce a report of the total hours worked by each female employee, arranged by department number and alphabetically by employee surname within each department.
        Task<List<object>> GetTotalHoursWorkedFemale();

        // k.Retrieve the total hours worked for each employee
        Task<List<object>> GetTotalHoursWorked();
    }
}
