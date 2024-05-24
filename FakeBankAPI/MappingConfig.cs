using AutoMapper;
using FakeBankAPI.Models;
using FakeBankAPI.Models.DTOs;

namespace FakeBankAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        { 
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<Account, AccountCreateDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
        }
    }
}
