using Student.Web.Api.Models;

namespace Student.Web.Api.Data
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetById(int id);
    }
}