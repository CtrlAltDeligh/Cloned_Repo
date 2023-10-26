using Microsoft.EntityFrameworkCore;
using Student.Web.Api.Models;

namespace Student.Web.Api.Data;

public class GradingRepository : IGradingRepository
{
    private readonly StudentDataContext _gradingContext;
    public GradingRepository(StudentDataContext gradingContext)
    {
        _gradingContext = gradingContext;

    }
    public void Add(Grading newT)
    {
        _gradingContext.Add(newT);
    }

    public void Delete(Grading item)
    {
        _gradingContext.Remove(item);
    }

    public async Task<bool> SaveAllChangesAsync()
    {
        return await _gradingContext.SaveChangesAsync() > 0;
    }

    public async void Update<K>(K id, Grading input)
    {
        // Get the grading
        var theGrading = await _gradingContext.Grading.FindAsync(id);
        theGrading.Id = input.Id;
        theGrading.Grade = input.Grade;
        if (theGrading.Grade >= 75)
        {
            theGrading.Remarks = "Passed";
        }

        theGrading.Remarks = "Failed";
    }

    public async Task<List<Grading>> GetAllAsync()
    {
        return await _gradingContext.Grading.ToListAsync();
    }

    public async Task<Grading?> GetById(int id)
    {
        return await _gradingContext.Grading.FirstOrDefaultAsync(x => x.Id == id);
    }

}