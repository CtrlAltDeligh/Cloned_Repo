using Student.Web.Api.Models;

namespace Student.Web.Api.Data;


    public interface IGradingRepository : IRepository<Grading>
    {
        Task<List<Grading>> GetAllAsync();
        Task<Grading?> GetById(int id);
    }





