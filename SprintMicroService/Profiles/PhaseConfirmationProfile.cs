using AutoMapper;
using SprintMicroService.Entites;
using SprintMicroService.Models.ModelPhase;

namespace SprintMicroService.Profiles
{
    public class PhaseConfirmationProfile : Profile
    {
        public PhaseConfirmationProfile()
        {
            CreateMap<Phase, PhaseConfirmationDto>();
        }
    }
}
