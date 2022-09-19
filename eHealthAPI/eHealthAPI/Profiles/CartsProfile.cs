using AutoMapper;

namespace eHealthAPI.Profiles
{
    public class CartsProfile : Profile
    {
        public CartsProfile()
        {
            CreateMap<Models.Domain.Cart, Models.DTO.Cart>()
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id)
                .ReverseMap();
        }
    }
}
