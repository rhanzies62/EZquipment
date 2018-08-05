using AutoMapper;
using Ems.Domain;
using Ems.Domain.DTO;
//using Ems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Services.Config
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                #region Example
                cfg.CreateMap<Company, CompanyDto>();
                cfg.CreateMap<CompanyDto, Company>();

                cfg.CreateMap<AddressDto, Address>();
                cfg.CreateMap<Address, AddressDto>();

                cfg.CreateMap<PhoneDto, Phone>();
                cfg.CreateMap<Phone, PhoneDto>();

                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();

                cfg.CreateMap<UserAccountDto, UserAccount>();
                cfg.CreateMap<UserAccount, UserAccountDto>();
                #endregion
            });
        }
    }
}
