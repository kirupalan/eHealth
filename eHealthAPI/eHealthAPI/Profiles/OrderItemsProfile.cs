using AutoMapper;

namespace eHealthAPI.Profiles
{
    public class OrderItemsProfile : Profile
    {
        public OrderItemsProfile()
        {
            CreateMap<Models.Domain.OrderItem, Models.DTO.OrderItem>()
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id)
                .ReverseMap();
        }
    }
}