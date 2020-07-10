
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MisaWebApi.Entities;
using MisaWebApi.Models;

namespace MisaWebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserEntity>();
            CreateMap<RegisterModel, UserEntity>();

        }
    }
}
