using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Car_Galery.Entities;
using Car_Galery.Models;
using Car_Galery.Models.ViewModels;
using Type = Car_Galery.Entities.Type;

namespace Car_Galery.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Vehicle, VehicleModel>();
                config.CreateMap<Model, ModelModel>();
                config.CreateMap<VehicleModel, Vehicle>();
                config.CreateMap<Type, TypeModel>();
                config.CreateMap<TypeModel, Type>();
                config.CreateMap<Brand, BrandModel>();
                config.CreateMap<BrandModel, Brand>();
                config.CreateMap<Brand, BrandModelsModel>()
                    .ForMember(dest => dest.BrandId, opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.BrandName, opts => opts.MapFrom(src => src.Name));
                config.CreateMap<Vehicle, VehicleModalViewModel>()
                    .ForMember(dest => dest.TypeName, opts => opts.MapFrom(src => src.Type.Name))
                    .ForMember(dest => dest.BrandName, opts => opts.MapFrom(src => src.Brand.Name))
                    .ForMember(dest => dest.ModelName, opts => opts.MapFrom(src => src.Model.Name));
            });
        }
    }
}