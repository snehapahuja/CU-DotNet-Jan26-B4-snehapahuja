using CourseAPI.Models;
using CourseAPI.Repository;

namespace CourseAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _courseRepo;

        public CourseService(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<Course> Create(Course c)
        {
            return await _courseRepo.Create(c);
        }

        public async Task<bool> Delete(int id)
        {
            return await _courseRepo.Delete(id);
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _courseRepo.GetAll();
        }

        public async Task<Course> GetByID(int id)
        {
            return await _courseRepo.GetByID(id);
        }

        public async Task<Course> Update(Course c)
        {
            return await _courseRepo.Update(c);
        }
    }
}
