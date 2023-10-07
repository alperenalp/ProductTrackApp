using AutoMapper;
using ProductTrackApp.Business.DTOs.Responses;
using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.Services.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
           //User
           CreateMap<User, UserValidateResponse>().ReverseMap();

        }
    }
}
