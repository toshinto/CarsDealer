using CarsDealer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsDealer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Car>()
                .HasOne(c => c.User)
                .WithMany(u => u.Cars)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            base.OnModelCreating(builder);
        }

        public DbSet<Car> Cars { get; set; }
    }
}
