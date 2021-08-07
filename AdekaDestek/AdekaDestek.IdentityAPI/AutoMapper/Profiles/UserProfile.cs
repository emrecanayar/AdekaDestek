using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using AdekaDestek.Entities.Dtos;
using AutoMapper;

namespace AdekaDestek.IdentityAPI.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateForApiDto>();
            CreateMap<UserUpdateForApiDto, User>();
        }
    }
}
