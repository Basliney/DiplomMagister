using DiplomMagister.Classes;
using DiplomMagister.Classes.Client;
using DiplomMagister.Classes.DTOs;
using DiplomMagister.Classes.Tests;
using JWT_Example_ASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiplomMagister.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<ProfileSettings> ProfileSettings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagDTO> TagDTO { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<BasicQuestion> BasicQuestions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
