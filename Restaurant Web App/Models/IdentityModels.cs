using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Restaurant_Web_App.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual List<PizzaOrder> PizzaOrders { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
        }*/
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<NoIngredient> NoIngredients { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<PizzaOrder> PizzaOrders { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderIngredient> OrderIngredients { get; set; }
        public DbSet<OrderNoIngredient> OrderNoIngredients { get; set; }
        public DbSet<OrderOption> OrderOptions { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Review> Reviews { get; set; }

        //public System.Data.Entity.DbSet<Restaurant_Web_App.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<Restaurant_Web_App.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}