﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    [DbContext(typeof(EfContext))]
    partial class EfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.GirilenKlasikSinavKayit", b =>
                {
                    b.Property<Guid>("GirilenKlasikSinavKayitId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("OgrenciSinavPuani");

                    b.Property<Guid>("SuresiBaslamisSinavlarId");

                    b.HasKey("GirilenKlasikSinavKayitId");

                    b.HasIndex("SuresiBaslamisSinavlarId");

                    b.ToTable("GirilenKlasikSinavKayits");
                });

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.GirilenTestSinavSonuclari", b =>
                {
                    b.Property<Guid>("GirilenTestSinavSonuclariId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("SinavPuani");

                    b.Property<Guid>("SuresiBaslamisSinavlarId");

                    b.HasKey("GirilenTestSinavSonuclariId");

                    b.HasIndex("SuresiBaslamisSinavlarId")
                        .IsUnique();

                    b.ToTable("GirilenTestSinavSonuclaris");
                });

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.KlasikSinavSinavSoruCevap", b =>
                {
                    b.Property<Guid>("KlasikSinavSinavSoruCevapId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GirilenKlasikSinavKayitId");

                    b.Property<string>("SoruCevapText");

                    b.Property<string>("SoruText");

                    b.HasKey("KlasikSinavSinavSoruCevapId");

                    b.HasIndex("GirilenKlasikSinavKayitId");

                    b.ToTable("KlasikSinavSinavSoruCevaps");
                });

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.SuresiBaslamisSinavlar", b =>
                {
                    b.Property<Guid>("SuresiBaslamisSinavlarId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("OgrenciId");

                    b.Property<DateTime>("OgrenciSinavaBaslamaZamani");

                    b.Property<DateTime>("OgrenciSinaviBitirmeZamani");

                    b.Property<Guid>("SinavId");

                    b.HasKey("SuresiBaslamisSinavlarId");

                    b.HasIndex("SinavId")
                        .IsUnique();

                    b.ToTable("SuresiBaslamisSinavlars");
                });

            modelBuilder.Entity("EntityLayer.CanliYayin.CanliYayin", b =>
                {
                    b.Property<Guid>("CanliYayinId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanliYayinAktifMi");

                    b.Property<DateTime?>("CanliYayinBaslamaZamani");

                    b.Property<DateTime?>("CanliYayinBitisZamani");

                    b.Property<Guid>("CanliYayinDersId");

                    b.Property<Guid>("CanliYayinSahibi");

                    b.Property<string>("CanliYayinYayinId");

                    b.HasKey("CanliYayinId");

                    b.ToTable("CanliYayins");
                });

            modelBuilder.Entity("EntityLayer.CanliYayin.CanliYayinaKatilanlar", b =>
                {
                    b.Property<Guid>("CanliYayinaKatilanlarId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CanliYayinAyrilmaZamani");

                    b.Property<Guid>("CanliYayinId");

                    b.Property<DateTime>("CanliYayinKatilmaZamani");

                    b.Property<Guid>("CanliYayinaKatilanKisiId");

                    b.HasKey("CanliYayinaKatilanlarId");

                    b.HasIndex("CanliYayinId");

                    b.ToTable("CanliYayinaKatilanlars");
                });

            modelBuilder.Entity("EntityLayer.CanliYayin.CanliYayinDokumanlari", b =>
                {
                    b.Property<Guid>("CanliYayinDokumanlariId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CanliYayinId");

                    b.Property<string>("DokumanAdi");

                    b.Property<DateTime>("DokumanEklenmeTarihi");

                    b.Property<string>("DokumanKayitPath");

                    b.HasKey("CanliYayinDokumanlariId");

                    b.HasIndex("CanliYayinId");

                    b.ToTable("CanliYayinDokumanlaris");
                });

            modelBuilder.Entity("EntityLayer.Ders.Dersler", b =>
                {
                    b.Property<Guid>("DerslerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DersAdi");

                    b.Property<DateTime>("DersEklenmeTarihi");

                    b.Property<string>("DersKayitAnahtari");

                    b.Property<double>("DersKodu");

                    b.HasKey("DerslerId");

                    b.ToTable("Derslers");
                });

            modelBuilder.Entity("EntityLayer.Ders.KayitliDerslerim", b =>
                {
                    b.Property<Guid>("KayitliDerslerimId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DersKayitTarihi");

                    b.Property<Guid>("DerslerId");

                    b.Property<Guid>("OgrenciId");

                    b.HasKey("KayitliDerslerimId");

                    b.HasIndex("DerslerId");

                    b.ToTable("KayitliDerslerims");
                });

            modelBuilder.Entity("EntityLayer.Login.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Ad");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("EgitimGorduguKurumAdi");

                    b.Property<string>("EgitimGorduguKurumBolum");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("KurumOgrenciNumarasi");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Soyad");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EntityLayer.Sinav.KlasikSinav", b =>
                {
                    b.Property<Guid>("KlasikSinavId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("SinavId");

                    b.HasKey("KlasikSinavId");

                    b.HasIndex("SinavId")
                        .IsUnique();

                    b.ToTable("KlasikSinavs");
                });

            modelBuilder.Entity("EntityLayer.Sinav.KlasikSinavSorular", b =>
                {
                    b.Property<Guid>("KlasikSinavSorularId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("KlasikSinavId");

                    b.Property<string>("SoruMetni");

                    b.HasKey("KlasikSinavSorularId");

                    b.HasIndex("KlasikSinavId");

                    b.ToTable("KlasikSinavSorulars");
                });

            modelBuilder.Entity("EntityLayer.Sinav.Sinav", b =>
                {
                    b.Property<Guid>("SinavId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DerslerId");

                    b.Property<bool>("SinavAktiflikDurumu");

                    b.Property<DateTime>("SinavEklenmeTarihi");

                    b.Property<Guid>("SinavSahibi");

                    b.Property<int>("SinavSuresiDakika");

                    b.Property<int>("SinavTuru");

                    b.HasKey("SinavId");

                    b.HasIndex("DerslerId");

                    b.ToTable("Sinavs");
                });

            modelBuilder.Entity("EntityLayer.Sinav.TestSinav", b =>
                {
                    b.Property<Guid>("TestSinavId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("SinavId");

                    b.HasKey("TestSinavId");

                    b.HasIndex("SinavId")
                        .IsUnique();

                    b.ToTable("TestSinavs");
                });

            modelBuilder.Entity("EntityLayer.Sinav.TestSinavSorular", b =>
                {
                    b.Property<Guid>("TestSinavSorularId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SoruCevabi");

                    b.Property<Guid>("TestSinavId");

                    b.Property<string>("TestSinavSorusuMetni");

                    b.HasKey("TestSinavSorularId");

                    b.HasIndex("TestSinavId");

                    b.ToTable("TestSinavSorulars");
                });

            modelBuilder.Entity("EntityLayer.Sinav.TestSinavSoruSiklari", b =>
                {
                    b.Property<Guid>("TestSinavSoruSiklariId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SoruSikMetni");

                    b.Property<int>("SoruSikki");

                    b.Property<Guid>("TestSinavSorularId");

                    b.HasKey("TestSinavSoruSiklariId");

                    b.HasIndex("TestSinavSorularId");

                    b.ToTable("TestSinavSoruSiklaris");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.GirilenKlasikSinavKayit", b =>
                {
                    b.HasOne("EntityLayer.BaslayanSinavlar.SuresiBaslamisSinavlar", "SuresiBaslamisSinavlar")
                        .WithMany("GirilenKlasikSinavKayits")
                        .HasForeignKey("SuresiBaslamisSinavlarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.GirilenTestSinavSonuclari", b =>
                {
                    b.HasOne("EntityLayer.BaslayanSinavlar.SuresiBaslamisSinavlar", "SuresiBaslamisSinavlar")
                        .WithOne("GirilenTestSinavSonuclaris")
                        .HasForeignKey("EntityLayer.BaslayanSinavlar.GirilenTestSinavSonuclari", "SuresiBaslamisSinavlarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.KlasikSinavSinavSoruCevap", b =>
                {
                    b.HasOne("EntityLayer.BaslayanSinavlar.GirilenKlasikSinavKayit", "GirilenKlasikSinavKayit")
                        .WithMany("KlasikSinavSinavSoruCevaps")
                        .HasForeignKey("GirilenKlasikSinavKayitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.BaslayanSinavlar.SuresiBaslamisSinavlar", b =>
                {
                    b.HasOne("EntityLayer.Sinav.Sinav", "Sinav")
                        .WithOne("SuresiBaslamisSinavlar")
                        .HasForeignKey("EntityLayer.BaslayanSinavlar.SuresiBaslamisSinavlar", "SinavId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.CanliYayin.CanliYayinaKatilanlar", b =>
                {
                    b.HasOne("EntityLayer.CanliYayin.CanliYayin", "CanliYayin")
                        .WithMany("CanliYayinaKatilanlar")
                        .HasForeignKey("CanliYayinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.CanliYayin.CanliYayinDokumanlari", b =>
                {
                    b.HasOne("EntityLayer.CanliYayin.CanliYayin", "CanliYayin")
                        .WithMany("CanliYayinDokumanlari")
                        .HasForeignKey("CanliYayinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.Ders.KayitliDerslerim", b =>
                {
                    b.HasOne("EntityLayer.Ders.Dersler", "Dersler")
                        .WithMany("KayitliDerslerim")
                        .HasForeignKey("DerslerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.Sinav.KlasikSinav", b =>
                {
                    b.HasOne("EntityLayer.Sinav.Sinav", "Sinavs")
                        .WithOne("KlasikSinav")
                        .HasForeignKey("EntityLayer.Sinav.KlasikSinav", "SinavId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.Sinav.KlasikSinavSorular", b =>
                {
                    b.HasOne("EntityLayer.Sinav.KlasikSinav", "KlasikSinav")
                        .WithMany("KlasikSinavSorulars")
                        .HasForeignKey("KlasikSinavId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.Sinav.Sinav", b =>
                {
                    b.HasOne("EntityLayer.Ders.Dersler", "Dersler")
                        .WithMany("Sinav")
                        .HasForeignKey("DerslerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.Sinav.TestSinav", b =>
                {
                    b.HasOne("EntityLayer.Sinav.Sinav", "Sinavs")
                        .WithOne("TestSinav")
                        .HasForeignKey("EntityLayer.Sinav.TestSinav", "SinavId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.Sinav.TestSinavSorular", b =>
                {
                    b.HasOne("EntityLayer.Sinav.TestSinav", "TestSinav")
                        .WithMany("TestSinavSorulars")
                        .HasForeignKey("TestSinavId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EntityLayer.Sinav.TestSinavSoruSiklari", b =>
                {
                    b.HasOne("EntityLayer.Sinav.TestSinavSorular", "TestSinavSorular")
                        .WithMany("TestSinavSoruSiklari")
                        .HasForeignKey("TestSinavSorularId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EntityLayer.Login.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EntityLayer.Login.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EntityLayer.Login.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EntityLayer.Login.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
