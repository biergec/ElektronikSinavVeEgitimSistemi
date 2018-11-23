using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.Ders;
using EntityLayer.Sinav;
using EntityLayer.Login;

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
        public DbSet<TestSinavSorular> TestSinavSorulars { get; set; }
        public DbSet<KlasikSinavSorular> KlasikSinavSorulars { get; set; }
        public DbSet<TestSinavSoruSiklari> TestSinavSoruSiklaris { get; set; }
        public DbSet<Dersler> Derslers { get; set; }
        public DbSet<SuresiBaslamisSinavlar> SuresiBaslamisSinavlars { get; set; }
        public DbSet<KayitliDerslerim> KayitliDerslerims { get; set; }
        public DbSet<GirilenKlasikSinavKayit> GirilenKlasikSinavKayits { get; set; }
        public DbSet<KlasikSinavSinavSoruCevap> KlasikSinavSinavSoruCevaps { get; set; }
        public DbSet<GirilenTestSinavSonuclari> GirilenTestSinavSonuclaris { get; set; }

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

            builder.Entity<Dersler>()
                .HasMany(x => x.Sinav)
                .WithOne(x => x.Dersler).HasForeignKey(x=>x.DerslerId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Sinav>()
                .HasOne(c => c.SuresiBaslamisSinavlar)
                .WithOne(c => c.Sinav).HasForeignKey<SuresiBaslamisSinavlar>(x=>x.SinavId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Dersler>()
                .HasMany(x => x.KayitliDerslerim)
                .WithOne(x => x.Dersler).HasForeignKey(x => x.DerslerId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SuresiBaslamisSinavlar>()
                .HasMany(x => x.GirilenKlasikSinavKayits)
                .WithOne(x => x.SuresiBaslamisSinavlar).HasForeignKey(x => x.SuresiBaslamisSinavlarId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.Entity<GirilenKlasikSinavKayit>()
                .HasMany(x => x.KlasikSinavSinavSoruCevaps)
                .WithOne(x => x.GirilenKlasikSinavKayit).HasForeignKey(x => x.GirilenKlasikSinavKayitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SuresiBaslamisSinavlar>()
                .HasOne(x => x.GirilenTestSinavSonuclaris)
                .WithOne(x => x.SuresiBaslamisSinavlar).HasForeignKey<GirilenTestSinavSonuclari>(x => x.SuresiBaslamisSinavlarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
