using AutoMapper;

namespace eHealthAPI.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Models.Domain.User, Models.DTO.User>()
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id)
                .ReverseMap();
        }
    }
}
