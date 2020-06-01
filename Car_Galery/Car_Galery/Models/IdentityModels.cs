using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Car_Galery.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Car_Galery.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int Balance { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            
            userIdentity.AddClaim(new Claim("Balance",this.Balance.ToString()));

            
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("VehiclesContext", throwIfV1Schema: false)
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
            base.OnModelCreating(modelBuilder);

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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}