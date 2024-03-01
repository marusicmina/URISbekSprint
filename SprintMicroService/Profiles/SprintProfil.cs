using AutoMapper;
using SprintMicroService.Entites;
using SprintMicroService.Models;
using SprintMicroService.Models.ModelSprint;

namespace SprintMicroService.Profiles
{
    public class SprintProfile : Profile
    {
        public SprintProfile()
        {
            CreateMap<Sprint, SprintDto>();
            CreateMap<SprintCreationDto, Sprint>().ReverseMap();
            CreateMap<SprintUpdateDto, Sprint>().ReverseMap();
            CreateMap<Sprint,Sprint>();
        }
    }
}

