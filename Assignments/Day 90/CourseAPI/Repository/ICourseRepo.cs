using CourseAPI.DTO;
using CourseAPI.Models;
using System.Collections;

namespace CourseAPI.Repository
{
    public interface ICourseRepo
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetByID(int id);
        Task<Course> Update(Course c);
        Task<Course> Create(Course c);
        Task<bool> Delete(int id);

    }
}
