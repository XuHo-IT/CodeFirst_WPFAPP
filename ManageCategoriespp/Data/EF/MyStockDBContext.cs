using ManageCategoriesApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCategoriesApp.Data.EF
{
    public class MyStockDBContext : DbContext
    {
        public MyStockDBContext()
        { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builer = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builer.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStockDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(category => category.CategoryName)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Beverages"},
                new Category {CategoryID = 2, CategoryName = "Condiments"},
                new Category { CategoryID = 3, CategoryName = "Confections" }
            );
        }
    }
}