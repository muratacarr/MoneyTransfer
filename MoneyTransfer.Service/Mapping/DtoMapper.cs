using AutoMapper;
using MoneyTransfer.Core.DTOs;
using MoneyTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Service.Mapping
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<AppUserDto, AppUser>().ReverseMap();
        }
    }
}
