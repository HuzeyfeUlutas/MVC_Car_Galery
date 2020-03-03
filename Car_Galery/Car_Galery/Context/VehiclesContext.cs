using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Car_Galery.Entities;
using Type = Car_Galery.Entities.Type;

namespace Car_Galery.Context
{
    public class VehiclesContext:DbContext
    {
        public VehiclesContext(): base("VehiclesContext")
        {
            
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<TypeBrand> TypeBrands { get; set; }

        public DbSet<UserRequest> UserRequests { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().ToTable("Brands");
            modelBuilder.Entity<TypeBrand>().ToTable("TypeBrands");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
            modelBuilder.Entity<Model>().ToTable("Models");
            modelBuilder.Entity<Type>().ToTable("Types");
            modelBuilder.Entity<UserRequest>().ToTable("UserRequests");

            modelBuilder.Entity<Model>().HasKey(m=>m.Id)
                .HasMany(m=>m.Vehicles)
                .WithRequired(v=>v.Model)
                .HasForeignKey(v=>v.ModelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type>().HasKey(t=>t.Id)
                .HasMany(t=>t.Vehicles)
                .WithRequired(v=>v.Type)
                .HasForeignKey(v=>v.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type>().HasKey(t=>t.Id)
                .HasMany(t=>t.TypeBrands)
                .WithRequired(ty=>ty.Type)
                .HasForeignKey(ty=>ty.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>().HasKey(b=>b.Id)
                .HasMany(b=>b.Models)
                .WithRequired(m=>m.Brand)
                .HasForeignKey(m=>m.BrandId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>().HasKey(b=> b.Id)
                .HasMany(b=>b.Vehicles)
                .WithRequired(v=>v.Brand)
                .HasForeignKey(v => v.BrandId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>().HasKey(b=>b.Id)
                .HasMany(b=>b.TypeBrands)
                .WithRequired(ty=>ty.Brand)
                .HasForeignKey(ty=>ty.BrandId)
                .WillCascadeOnDelete(false);

        }
    }
}