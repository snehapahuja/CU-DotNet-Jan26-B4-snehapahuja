using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseAPI.Models;
using CourseAPI.Services;
using CourseAPI.DTO;
using CourseAPI.Common;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IValidator<CreateDto> _createValidator;
        private readonly IValidator<GetByIDDto> _getByIdValidator;
        private readonly IValidator<UpdateDto> _updateValidator;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(
            ICourseService courseService,
            IValidator<CreateDto> createValidator,
            IValidator<GetByIDDto> getByIdValidator,
            IValidator<UpdateDto> updateValidator,
            ILogger<CoursesController> logger)
        {
            _courseService = courseService;
            _createValidator = createValidator;
            _getByIdValidator = getByIdValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<APIResponse<IEnumerable<Course>>>> GetCourse()
        {
            _logger.LogInformation("Fetching all courses");
            var courses = await _courseService.GetAll();
            return Ok(new APIResponse<IEnumerable<Course>>
            {
                Success = true,
                Message = "Courses retrieved successfully",
                Data = courses
            });
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<Course>>> GetCourse(int id)
        {
            try
            {
                _logger.LogInformation("Fetching course with Id {CourseId}", id);
                var getByIdDto = new GetByIDDto { Id = id };
                var validationResult = await _getByIdValidator.ValidateAsync(getByIdDto);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validation failed for GetCourse with Id {CourseId}: {Errors}", id, validationResult.Errors);
                    return BadRequest(new APIResponse<Course>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Data = null
                    });
                }

                var course = await _courseService.GetByID(id);
                if (course == null)
                {
                    _logger.LogWarning("Course not found with Id {CourseId}", id);
                    return NotFound(new APIResponse<Course>
                    {
                        Success = false,
                        Message = "Course not found",
                        Data = null
                    });
                }

                return Ok(new APIResponse<Course>
                {
                    Success = true,
                    Message = "Course retrieved successfully",
                    Data = course
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving course with Id {CourseId}", id);
                return NotFound(new APIResponse<Course>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the course",
                    Data = null
                });
            }
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse<Course>>> PutCourse(int id, UpdateDto updateDto)
        {
            _logger.LogInformation("Updating course with Id {CourseId}", id);
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed while updating course with Id {CourseId}: {Errors}", id, validationResult.Errors);
                return BadRequest(new APIResponse<Course>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = null
                });
            }

            var course = new Course
            {
                Id = id,
                Title = updateDto.Title,
                Price = updateDto.Price,
                Summary = updateDto.Summary
            };

            var updated = await _courseService.Update(course);
            if (updated == null)
            {
                _logger.LogWarning("Course not found for update with Id {CourseId}", id);
                return NotFound(new APIResponse<Course>
                {
                    Success = false,
                    Message = "Course not found",
                    Data = null
                });
            }

            return Ok(new APIResponse<Course>
            {
                Success = true,
                Message = "Course updated successfully",
                Data = updated
            });
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<APIResponse<Course>>> PostCourse(CreateDto createDto)
        {
            _logger.LogInformation("Creating a new course");
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed while creating course: {Errors}", validationResult.Errors);
                return BadRequest(new APIResponse<Course>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = null
                });
            }

            var course = new Course
            {
                Title = createDto.Title,
                Price = createDto.Price,
                Summary = createDto.Summary
            };

            var created = await _courseService.Create(course);
            _logger.LogInformation("Course created successfully with Id {CourseId}", created.Id);
            return CreatedAtAction(nameof(GetCourse), new { id = created.Id }, new APIResponse<Course>
            {
                Success = true,
                Message = "Course created successfully",
                Data = created
            });
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse<string>>> DeleteCourse(int id)
        {
            _logger.LogInformation("Deleting course with Id {CourseId}", id);
            var deleted = await _courseService.Delete(id);
            if (!deleted)
            {
                _logger.LogWarning("Course not found for delete with Id {CourseId}", id);
                return NotFound(new APIResponse<string>
                {
                    Success = false,
                    Message = "Course not found",
                    Data = null
                });
            }

            return Ok(new APIResponse<string>
            {
                Success = true,
                Message = "Course deleted successfully",
                Data = null
            });
        }
    }
}
