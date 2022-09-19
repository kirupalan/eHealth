using AutoMapper;

namespace eHealthAPI.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Models.Domain.Medicine, Models.DTO.Medicine>()
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id)
                .ReverseMap();
        }
    }
}