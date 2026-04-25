using CourseAPI.Data;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Repository
{
    public class CourseRepo : ICourseRepo
    {
        private readonly CourseAPIContext _context;

        public CourseRepo(CourseAPIContext context)
        {
            _context = context;
        }

        public async Task<Course> Create(Course c)
        {
            await _context.Courses.AddAsync(c);
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<bool> Delete(int id)
        {
            var cour = await _context.Courses.FindAsync(id);
            if (cour == null)
            {
                return false;
            }

            _context.Courses.Remove(cour);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Course>> GetAll() => 
               await _context.Courses.ToListAsync();

        public async Task<Course> GetByID(int id) => await _context.Courses.FindAsync(id) ?? throw new Exception("Null Found");

        public async Task<Course> Update(Course c)
        {
            var cour = await _context.Courses.FindAsync(c.Id);
            if (cour == null)
            {
                return null;
            }

            cour.Title = c.Title;
            cour.Price = c.Price;
            cour.Summary = c.Summary;

            await _context.SaveChangesAsync();
            return cour;

        }
    }
}
