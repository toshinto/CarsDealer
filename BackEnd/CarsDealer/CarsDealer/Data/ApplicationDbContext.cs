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

            builder.Entity<Offer>(entity =>
            {
                entity.HasOne(d => d.Sender)
                      .WithMany(p => p.SenderOffers)
                      .HasForeignKey(k => k.SenderId)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Receiver)
                      .WithMany(p => p.ReceiverOffers)
                      .HasForeignKey(k => k.ReceiverId)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Car)
                      .WithMany(p => p.Offers)
                      .HasForeignKey(k => k.CarId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.User)
                      .WithMany(p => p.Notifications)
                      .HasForeignKey(k => k.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
      
            });


            base.OnModelCreating(builder);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
