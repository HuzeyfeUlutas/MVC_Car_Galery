using System.Collections.Generic;
using Car_Galery.Entities;

namespace Car_Galery.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Car_Galery.Context.VehiclesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Car_Galery.Context.VehiclesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            List<Vehicle> vehicleList = new List<Vehicle>
            {
                new Vehicle(){Id = 1,Name = "BMW X6", ImageUrl = "~/Images/VehicleImages/carimage1.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = false,Rented = false, BrandId = 1, ModelId = 1, TypeId = 1},
                new Vehicle(){Id = 2,Name = "BMW 3.20", ImageUrl = "~/Images/VehicleImages/carimage1.png",Year = "2019",Km = 20000,Color = "Black",Price = 250000,Fuel = "Benzin",Transmission = "Manuel",Rentable = false,Rented = false, BrandId = 1, ModelId = 2, TypeId = 2},
                new Vehicle(){Id = 3,Name = "Mercedes ", ImageUrl = "~/Images/VehicleImages/carimage1.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = false,Rented = false, BrandId = 3, ModelId = 4, TypeId = 2},
                new Vehicle(){Id = 4,Name = "Audi A6", ImageUrl = "~/Images/VehicleImages/carimage3.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = false,Rented = false, BrandId = 4, ModelId = 5, TypeId = 2},
                new Vehicle(){Id = 5,Name = "Volkswagen Amarok", ImageUrl = "~/Images/VehicleImages/carimage2.png",Year = "2015",Km = 45000,Color = "Grey",Price = 100000,Fuel = "Dizel",Transmission = "Automatic",Rentable = true,Rented = false, BrandId = 2, ModelId = 3, TypeId = 1},
                new Vehicle(){Id = 6,Name = "BMW X6", ImageUrl = "~/Images/VehicleImages/carimage2.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = false,Rented = false, BrandId = 1, ModelId = 1, TypeId = 1},
                new Vehicle(){Id = 7,Name = "BMW X6", ImageUrl = "~/Images/VehicleImages/carimage1.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = true,Rented = false, BrandId = 1, ModelId = 1, TypeId = 1},
                new Vehicle(){Id = 8,Name = "BMW X6", ImageUrl = "~/Images/VehicleImages/carimage3.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = false,Rented = false, BrandId = 1, ModelId = 1, TypeId = 1},
                new Vehicle(){Id = 9,Name = "BMW X6", ImageUrl = "~/Images/VehicleImages/carimage1.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = false,Rented = false, BrandId = 1, ModelId = 1, TypeId = 1},
                new Vehicle(){Id = 10,Name = "BMW X6", ImageUrl = "~/Images/VehicleImages/carimage2.png",Year = "2018",Km = 15000,Color = "Red",Price = 1500000,Fuel = "Dizel",Transmission = "Automatic",Rentable = false,Rented = false, BrandId = 1, ModelId = 1, TypeId = 1}
            };

            List<Brand> brandList = new List<Brand>
            {
                new Brand(){Id = 1,Name = "BMW",BrandImgUrl = "",TypeBrands = new List<TypeBrand>(),Vehicles = new List<Vehicle>(),Models = new List<Model>()},
                new Brand(){Id = 2,Name = "Volkswagen",BrandImgUrl = "",TypeBrands = new List<TypeBrand>(),Vehicles = new List<Vehicle>(),Models = new List<Model>()},
                new Brand(){Id = 3,Name = "Mercedes",BrandImgUrl = "",TypeBrands = new List<TypeBrand>(),Vehicles = new List<Vehicle>(),Models = new List<Model>()},
                new Brand(){Id = 4,Name = "Audi",BrandImgUrl = "",TypeBrands = new List<TypeBrand>(),Vehicles = new List<Vehicle>(),Models = new List<Model>()}
            };

            List<Model> modelList = new List<Model>
            {
                new Model(){Id = 1,Name = "X6", BrandId = 1,Vehicles = new List<Vehicle>()},
                new Model(){Id = 2,Name = "320", BrandId = 1,Vehicles = new List<Vehicle>()},
                new Model(){Id = 3,Name = "Amarok", BrandId = 2,Vehicles = new List<Vehicle>()},
                new Model(){Id = 4,Name = "AMG", BrandId = 3,Vehicles = new List<Vehicle>()},
                new Model(){Id = 5,Name = "A6", BrandId = 4,Vehicles = new List<Vehicle>()}
            };

            List<Entities.Type> typeList = new List<Entities.Type>
            {
                new Entities.Type(){Id = 1,Name = "SUV",TypeBrands = new List<TypeBrand>(),Vehicles = new List<Vehicle>()},
                new Entities.Type(){Id = 2,Name = "Otomobil",TypeBrands = new List<TypeBrand>(),Vehicles = new List<Vehicle>()}
            };

            List<TypeBrand> typeBrandList = new List<TypeBrand>
            {
                new TypeBrand(){Id = 1,BrandId = 1,TypeId = 1},
                new TypeBrand(){Id = 2,BrandId = 1,TypeId = 2},
                new TypeBrand(){Id = 3,BrandId = 2,TypeId = 1},
                new TypeBrand(){Id = 4,BrandId = 3,TypeId = 2},
                new TypeBrand(){Id = 5,BrandId = 4,TypeId = 2}
            };

            foreach (var brand in brandList)
            {
                List<TypeBrand> typeBrands = typeBrandList.Where(tb => tb.BrandId == brand.Id).ToList();
                foreach (TypeBrand typeBrand in typeBrands)
                {
                    brand.TypeBrands.Add(typeBrand);
                }
            }

            foreach (var type in typeList)
            {
                List<TypeBrand> typeBrands = typeBrandList.Where(tb => tb.TypeId == type.Id).ToList();
                foreach (TypeBrand typeBrand in typeBrands)
                {
                    type.TypeBrands.Add(typeBrand);
                }
            }

            foreach (Vehicle vehicle in vehicleList)
            {
                context.Vehicles.AddOrUpdate(v=>v.Name,new Vehicle
                {
                    Name = vehicle.Name,
                    ImageUrl = vehicle.ImageUrl,
                    Year = vehicle.Year,
                    Km = vehicle.Km,
                    Color = vehicle.Color,
                    Price = vehicle.Price,
                    Fuel = vehicle.Fuel,
                    Transmission = vehicle.Transmission,
                    Rentable = vehicle.Rentable,
                    Rented = vehicle.Rented,
                    BrandId = vehicle.BrandId,
                    ModelId = vehicle.BrandId,
                    TypeId = vehicle.TypeId
                    
                });
            }
        }
    }
}
