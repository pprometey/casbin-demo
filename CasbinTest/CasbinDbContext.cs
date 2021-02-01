using CasbinTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasbinTest
{
	public class CasbinDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }

        public CasbinDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("CasbinTest");
 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User { Id = 1, UserName = "Alice" },
                    new User { Id = 2, UserName = "Bob" },
                    new User { Id = 3, UserName = "Piter" }
                });

            modelBuilder.Entity<Article>().HasData(
                new Article[]
                {
                    new Article { Id=1, Name="A", Description="A", Content="A", OwnerId=1},
                    new Article { Id=2, Name="A1", Description="A1", Content="A1", OwnerId=1},
                    new Article { Id=3, Name="A2", Description="A2", Content="A2", OwnerId=1},
                    new Article { Id=4, Name="B", Description="B", Content="B", OwnerId=2},
                    new Article { Id=5, Name="B1", Description="B1", Content="B1", OwnerId=2},
                    new Article { Id=6, Name="B2", Description="B2", Content="B2", OwnerId=2},
                    new Article { Id=7, Name="C", Description="C", Content="C", OwnerId=3},
                    new Article { Id=8, Name="C1", Description="C1", Content="C1", OwnerId=3}
                });
        }
    }
}
