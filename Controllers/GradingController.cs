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
            input.Remarks = null;
            newGrade.Id = input.Id;
            newGrade.Grade = input.Grade;
            if (input.Grade <= 0 || input.Grade >= 101)
            {
                return BadRequest("Invalid Grades");
            }

            if (input.Remarks == null)
            {
                
                input.Remarks = CalculateRemarks();
            }
            newGrade.Remarks = CalculateRemarks();
            

             string CalculateRemarks()
            {
                
                return newGrade.Grade >= 75 ? "Passed" : "Failed";
            }
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
            if (input.Grade <= 0 || input.Grade >= 101)
            {
                return BadRequest("Invalid Grade");
            }

            var grading = await _gradingRepository.GetById(input.Id);

            if (grading == null)
            {
                return NotFound("Resource not found.");
            }

            grading.Id = input.Id;
            grading.Grade = input.Grade;

            // Calculate remarks based on the input grade
            grading.Remarks = CalculateRemarks(grading.Grade);

            if (await _gradingRepository.SaveAllChangesAsync())
            {
                return Ok("Update successfully");
            }

            return BadRequest("Error");
        }

        private string CalculateRemarks(int grade)
        {
            return grade >= 75 ? "Passed" : "Failed";
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var grading = await _gradingRepository.GetById(id);

            if (grading == null) return BadRequest("Error");
            _gradingRepository.Delete(grading);
            if (await _gradingRepository.SaveAllChangesAsync())
            {
                return Ok($"Grade with subject id {grading.Id} is succesfully deleted.");
            }


            return BadRequest($"{id} is not found.");
        }


    }

}