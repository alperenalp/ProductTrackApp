using AutoMapper;
using ProductTrackApp.Business.DTOs.Requests;
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

            //Product
            CreateMap<Product, ProductDisplayResponse>().ReverseMap();
            CreateMap<Product, CreateNewProductRequest>().ReverseMap();
            CreateMap<Product, UpdateProductRequest>().ReverseMap();

            //Order
            CreateMap<Order, AddNewOrderRequest>().ReverseMap();
        }
    }
}
