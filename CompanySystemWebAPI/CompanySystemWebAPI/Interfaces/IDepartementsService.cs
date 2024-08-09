using CompanySystemWebAPI.Dtos.DepartementsDto;
using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IDepartementsService
    {
        Task<Departement> Create(DepartementsAddDto dept);
        Task<Departement> Update(int id, DepartementsAddDto dept);
        Task<Departement> Delete(int id);
        Task<List<Departement>> GetDepartements(int pageNumber, int perPage);
        Task<Departement> GetDepartement(int id);
    }
}
