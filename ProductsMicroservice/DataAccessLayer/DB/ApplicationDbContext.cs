using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;


namespace DatabaseLayer.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().HasData(new CategoryEntity { Id = new Guid("312d245c-cb9f-4dad-b7e3-848baecc43fd"), Name="Test",Products=new List<ProductEntity>() });
        }
    }
    
}
