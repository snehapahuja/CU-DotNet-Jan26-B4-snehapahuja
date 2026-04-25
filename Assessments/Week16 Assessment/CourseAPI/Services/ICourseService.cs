using CourseAPI.Models;

namespace CourseAPI.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetByID(int id);
        Task<Course> Update(Course c);
        Task<Course> Create(Course c);
        Task<bool> Delete(int id);
    }
}
