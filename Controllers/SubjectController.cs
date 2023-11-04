using Microsoft.AspNetCore.Mvc;
using Student.Web.Api.Data;
using Student.Web.Api.Dto;
using Student.Web.Api.Models;

namespace Student.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private ILogger<SubjectController> _logger;
        private readonly ISubjectRepository _subjectRepository;


        public SubjectController(ILogger<SubjectController> logger,
            ISubjectRepository subjectRepository
        )
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            
            var subjects = await _subjectRepository.GetAllAsync();
            if (subjects == null || !subjects.Any())
            {
                // Database is empty, return "empty"
                return Ok("Empty");
            }
            _logger.LogInformation("Getting all list");
            return Ok(subjects);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(SubjectDto input)
        {
            var newSubject = new Subject(input.id);
            newSubject.Id = input.id;
            newSubject.Code = input.Code;
            newSubject.Title = input.Title;


            _subjectRepository.Add(newSubject);

            if (await _subjectRepository.SaveAllChangesAsync())
            {
                return Ok(input);
            }

            return BadRequest("404 not found!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SubjectDto input)
        {
            var subject = await _subjectRepository.GetById(id);
            
            subject.Code = input.Code;
            subject.Title = input.Title;


            if (await _subjectRepository.SaveAllChangesAsync())
            {
                return Ok("Update successfully");
            }

            return BadRequest("Error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectRepository.GetById(id);
          

            if (subject != null)
            {
                _subjectRepository.Delete(subject);
                if (await _subjectRepository.SaveAllChangesAsync())
                {
                    return Ok($"Subject with id {subject.Id} is succesfully deleted.");
                }
            }


            return BadRequest($"Subject with id {id} is not found.");
        }


    }

}