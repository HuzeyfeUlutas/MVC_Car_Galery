using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;
using Car_Galery.Entities;
using Car_Galery.Models;
using Type = Car_Galery.Entities.Type;

namespace Car_Galery.Helpers
{
    public class MapWrapper
    {
        public static void Run()
        {
            SetMappings();
        }

        public static void SetMappings()
        {
            var config = new MapperConfigurationExpression();

            config.CreateMap<Brand, BrandModel>();
            config.CreateMap<BrandModel, Brand>();
            config.CreateMap<Model, ModelModel>();
            config.CreateMap<ModelModel, Model>();
            config.CreateMap<Vehicle, VehicleModel>();
            config.CreateMap<VehicleModel, Vehicle>();
            config.CreateMap<Type, TypeModel>();
            config.CreateMap<TypeModel, Type>();
        }
    }
}