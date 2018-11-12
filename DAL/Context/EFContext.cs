using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .WithOne(c => c.Sinavs).HasForeignKey<TestSinav>(x=>x.SinavId);

            builder.Entity<Sinav>()
                .HasOne(c => c.KlasikSinav)
                .WithOne(c => c.Sinavs).HasForeignKey<KlasikSinav>(x=>x.SinavId);

            builder.Entity<KlasikSinav>()
                .HasMany(c => c.KlasikSinavSorulars)
                .WithOne(e => e.KlasikSinav).HasForeignKey(x=>x.KlasikSinavId);

             builder.Entity<TestSinav>()
                .HasMany(c => c.TestSinavSorulars)
                .WithOne(e => e.TestSinav).HasForeignKey(x=>x.TestSinavId);

            builder.Entity<TestSinavSorular>()
                .HasMany(c => c.TestSinavSoruSiklari)
                .WithOne(c => c.TestSinavSorular).HasForeignKey(c=>c.TestSinavSorularId);
        }
    }
}
