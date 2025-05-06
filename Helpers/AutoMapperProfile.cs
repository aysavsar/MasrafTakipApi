using AutoMapper;
using MasrafTakipApi.DTOs;
using MasrafTakipApi.Entities;

namespace MasrafTakipApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserDto>();

            // ExpenseRequest mappings
            CreateMap<ExpenseRequestCreateDto, ExpenseRequest>();
            CreateMap<ExpenseRequestUpdateDto, ExpenseRequest>();
            CreateMap<ExpenseRequest, ExpenseRequestDto>();
        }
    }
}