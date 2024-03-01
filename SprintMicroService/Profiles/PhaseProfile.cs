using AutoMapper;
using SprintMicroService.Entites;
using SprintMicroService.Models;
using SprintMicroService.Models.ModelPhase;

namespace SprintMicroService.Profiles
{
    public class PhaseProfile : Profile
    {
        public PhaseProfile()
        {
            CreateMap<Phase, PhaseDto>();
            CreateMap<PhaseCreationDto, Phase>().ReverseMap();
            CreateMap<PhaseUpdateDto, Phase>().ReverseMap();
            CreateMap<Phase, Phase>();
        }
    }
}