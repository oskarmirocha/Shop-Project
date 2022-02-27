using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class AppDbContext :IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Singer_Song>().HasKey(sg => new
                         {
                             sg.SingerId,
                             sg.SongId
                         });
            
            modelBuilder.Entity<Singer_Song>().HasOne(g => g.Song).WithMany(sg => sg.Singers_Songs).HasForeignKey(g => g.SongId);

            modelBuilder.Entity<Singer_Song>().HasOne(g => g.Singer).WithMany(sg => sg.Singers_Songs).HasForeignKey(g => g.SingerId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Singer> Singers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Singer_Song> Singers_Songs { get; set; }
        public DbSet<RecordLabel> RecordLabels { get; set; }
        public DbSet<Producer> Producers { get; set; }


        //Orders related table
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }


    }
}
