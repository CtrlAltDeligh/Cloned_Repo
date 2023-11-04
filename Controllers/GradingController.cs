using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                var newGrade = new Grading(input.Id);
                
                newGrade.Id = input.Id;
                newGrade.Grade = input.Grade;

                if (input.Grade <= 0 || input.Grade >= 101)
                {
                    return BadRequest("Invalid Grades");
                }


                _gradingRepository.Add(newGrade);
                var response = new Grading()
                {
                    Id = newGrade.Id,
                    Grade = newGrade.Grade,
                  //  Remarks = newGrade.Remarks 

                };
                if (await _gradingRepository.SaveAllChangesAsync())
                {
                    return Ok(response);
                }

                return BadRequest("404 not found!");
            }
            catch (DbUpdateException ex)
            {
                // Handle unique constraint violation (PK duplication)
                return BadRequest("The primary key is already used for another record.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // You can log the exception and return an appropriate error response.
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(GradingDto input)
        {
            var grading = await _gradingRepository.GetById(input.Id);
    
            if (grading == null)
            {
                return NotFound($"Resource with Id {input.Id} not found.");
            }

            // Update the properties if input is valid
            if (input.Grade <= 0 || input.Grade >= 101)
            {
                return BadRequest("Invalid Grade");
            }
    
            grading.Id = input.Id;
            grading.Grade = input.Grade;

            if (await _gradingRepository.SaveAllChangesAsync())
            {
                return Ok("Update successfully");
            }

            return BadRequest("Error updating the resource.");
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