using Microsoft.AspNetCore.Mvc;
using Student.Web.Api.Data;
using Student.Web.Api.Dto;
using Student.Web.Api.Models;

namespace Student.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradingController : ControllerBase
    {
        private readonly ILogger<GradingController> _logger;
        private readonly IGradingRepository _gradingRepository;


        public GradingController(ILogger<GradingController> logger,
            IGradingRepository gradingRepository
        )
        {
            _logger = logger;
            _gradingRepository = gradingRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            
            var gradings = await _gradingRepository.GetAllAsync();
            if (gradings == null || !gradings.Any())
            {
                // Database is empty, return "empty"
                return Ok("Empty");
            }
            _logger.LogInformation("Getting all list..");
            return Ok(gradings);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(GradingDto input)
        {
            var newGrade = new Grading(input.Id);
            newGrade.Id = input.Id;
            newGrade.Grade = input.Grade;
            if (newGrade.Grade >= 75)
            {
                newGrade.Remarks = "Passed.";
            }

            newGrade.Remarks = "Failed";


            _gradingRepository.Add(newGrade);

            if (await _gradingRepository.SaveAllChangesAsync())
            {
                return Ok(input);
            }

            return BadRequest("404 not found!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(GradingDto input)
        {
            var grading = await _gradingRepository.GetById(input.Id);
            grading.Id = input.Id;
            grading.Grade = input.Grade;
            if (grading.Grade >= 75)
            {
                grading.Remarks = "Passed.";
                
            }

            grading.Remarks = "Failed";
            


            if (await _gradingRepository.SaveAllChangesAsync())
            {
                return Ok("Update successfully");
            }

            return BadRequest("Error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var grading = await _gradingRepository.GetById(id);

            if (grading == null) return BadRequest("Error");
            _gradingRepository.Delete(grading);
            if (await _gradingRepository.SaveAllChangesAsync())
            {
                return Ok($"Subject with id {grading.Id} is succesfully deleted.");
            }


            return BadRequest("May Error");
        }


    }

}