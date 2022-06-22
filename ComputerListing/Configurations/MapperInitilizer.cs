using AutoMapper;
using ComputerListing.Data;
using ComputerListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Computer, ComputerDTO>().ReverseMap();
            CreateMap<Computer, CreateComputerDTO>().ReverseMap();

            CreateMap<Accessory, AccessoryDTO>().ReverseMap();
            CreateMap<Accessory, CreateAccessoryDTO>().ReverseMap();

            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
