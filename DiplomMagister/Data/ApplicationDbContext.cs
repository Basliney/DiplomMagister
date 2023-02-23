using DiplomMagister.Classes;
using DiplomMagister.Classes.Client;
using JWT_Example_ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomMagister.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=MagisterDb;Username=postgres;Password=3365241Tt");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
