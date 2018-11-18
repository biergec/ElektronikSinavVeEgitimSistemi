using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using EntityLayer.Sinav;
using EntityLayer.Login;
using EntityLayer.Sinav;

namespace DAL.Context
{
    public class EfContext : IdentityDbContext<AppUser>
    {
        public EfContext(DbContextOptions<EfContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //public DbSet<Product> Product { get; set; }

        public DbSet<Sinav> Sinavs { get; set; }
        public DbSet<KlasikSinav> KlasikSinavs { get; set; }
        public DbSet<TestSinav> TestSinavs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Sinav>()
                .HasOne(c => c.TestSinav)
                .WithOne(c => c.Sinavs).HasForeignKey<TestSinav>(x=>x.SinavId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Sinav>()
                .HasOne(c => c.KlasikSinav)
                .WithOne(c => c.Sinavs).HasForeignKey<KlasikSinav>(x=>x.SinavId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<KlasikSinav>()
                .HasMany(c => c.KlasikSinavSorulars)
                .WithOne(e => e.KlasikSinav).HasForeignKey(x=>x.KlasikSinavId).OnDelete(DeleteBehavior.Cascade);

             builder.Entity<TestSinav>()
                .HasMany(c => c.TestSinavSorulars)
                .WithOne(e => e.TestSinav).HasForeignKey(x=>x.TestSinavId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TestSinavSorular>()
                .HasMany(c => c.TestSinavSoruSiklari)
                .WithOne(c => c.TestSinavSorular).HasForeignKey(c=>c.TestSinavSorularId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
