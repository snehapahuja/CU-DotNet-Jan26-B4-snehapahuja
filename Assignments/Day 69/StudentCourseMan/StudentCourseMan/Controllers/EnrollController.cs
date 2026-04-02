using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseMan.Data;
using StudentCourseMan.Models;
using StudentCourseMan.DTOs;

[Route("api/[controller]")]
[ApiController]
public class EnrollController : ControllerBase
{
    private readonly AppDbContext _context;

    public EnrollController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Enroll([FromBody] EnrollDto dto)
    {
        var studentExists = await _context.Students.AnyAsync(s => s.Id == dto.StudentId);
        if (!studentExists)
            return NotFound("Student not found");

        var courseExists = await _context.Courses.AnyAsync(c => c.Id == dto.CourseId);
        if (!courseExists)
            return NotFound("Course not found");

        var exists = await _context.StudentCourses
            .AnyAsync(sc => sc.StudentId == dto.StudentId && sc.CourseId == dto.CourseId);

        if (exists)
            return BadRequest("Already enrolled");

        var enrollment = new StudentCourse
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId
        };

        _context.StudentCourses.Add(enrollment);
        await _context.SaveChangesAsync();

        return Ok("Enrolled successfully");
    }
}