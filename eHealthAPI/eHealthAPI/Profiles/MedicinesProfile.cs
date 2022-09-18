using AutoMapper;

namespace eHealthAPI.Profiles
{
    public class MedicinesProfile : Profile
    {
        public MedicinesProfile()
        {
            CreateMap<Models.Domain.Medicine, Models.DTO.Medicine>()
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id)
                .ReverseMap();
        }
    }
}
