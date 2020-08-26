using AutoMapper;
using NetCoreEFDemo.Application.Models;
using NetCoreEFDemo.Application.Entitys;
using NetCoreEFDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreEFDemo.Application
{
    public class BasicProfile : Profile
    {
        public BasicProfile()
        {
            CreateMap<TestDto, Test>();
            CreateMap<Test, TestDto>();
        }
    }
}

