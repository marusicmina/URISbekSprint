using AutoMapper;
using SprintMicroService.Entites;
using SprintMicroService.Models.ModelSprint;

namespace SprintMicroService.Profiles
{
    public class SprintConfirmationProfile : Profile
    {
       public SprintConfirmationProfile()
        {
            CreateMap<Sprint, SprintConfirmationDto>();
        }
    }

    }
        
    
