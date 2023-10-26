using Microsoft.EntityFrameworkCore;
using Student.Web.Api.Models;

namespace Student.Web.Api.Data;

public class SubjectRepository : ISubjectRepository
{
    private readonly StudentDataContext _subjectContext;
    public SubjectRepository(StudentDataContext subjectContext)
    {
       _subjectContext = subjectContext;

    }
    public void Add(Subject newT)
    {
        _subjectContext.Add(newT);
    }

    public void Delete(Subject item)
    {
        _subjectContext.Remove(item);
    }

    public async Task<bool> SaveAllChangesAsync()
    {
        return await _subjectContext.SaveChangesAsync() > 0;
    }

    public async void Update<K>(K id, Subject input)
    {
        // Get the subjects
        var theSubject = await _subjectContext.Subjects.FindAsync(id);
        theSubject.Id = input.Id;
        theSubject.Code = input.Code;
        theSubject.Title = input.Title;
    }

    public async Task<List<Subject>> GetAllAsync()
    {
        return await _subjectContext.Subjects.ToListAsync();
    }

    public async Task<Subject?> GetById(int id)
    {
        return await _subjectContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
    }

}
