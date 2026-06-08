using AutoMapper;
using Backend.Domain;
using Backend.DTOs.Auth;
using Backend.DTOs.Users;

namespace Backend.Mapper;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserRequest, User>()
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(
                dest => dest.IsActive,
                opt => opt.MapFrom(_ => true));

        CreateMap<User, UserDto>();

        CreateMap<User, UserDetailsDto>();
    }
}
