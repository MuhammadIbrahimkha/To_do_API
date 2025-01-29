using AutoMapper;
using To_do_API.Models.Domain;
using To_do_API.Models.DTOs;

namespace To_do_API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Tasks, TaskDTO>().ReverseMap();
            CreateMap<PostTaskRequestDTO, Tasks>().ReverseMap();
            CreateMap<PutTaskRequestDTO, Tasks>().ReverseMap();
        }
    }
}
