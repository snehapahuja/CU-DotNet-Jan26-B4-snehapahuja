using AutoMapper;
using CourseAPI.DTO;
using CourseAPI.Models;

namespace CourseAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDto, Course>();
            CreateMap<Course, CreateDto>();

            CreateMap<UpdateDto, Course>();
            CreateMap<Course, UpdateDto>();
            
            CreateMap<GetAlldto, Course>();
            CreateMap<Course, GetAlldto>();
            
            CreateMap<GetByIDDto, Course>();
            CreateMap<Course, GetByIDDto>();
        }
    }
}
