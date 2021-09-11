﻿// <auto-generated />
using System;
using CEI_MVC_CORE_Proj.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CEI_MVC_CORE_Proj.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200405201113_correctSpelling")]
    partial class correctSpelling
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfilePictureLink")
                        .HasMaxLength(100);

                    b.Property<string>("SecurityStamp");

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

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.PaymentMethod", b =>
                {
                    b.Property<int>("FK_ProductId");

                    b.Property<int>("Method");

                    b.HasKey("FK_ProductId");

                    b.ToTable("Product_PaymentMethod");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("FK_CategoryId");

                    b.Property<string>("FK_VendorId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("OfferPrice");

                    b.Property<double>("Price");

                    b.Property<long>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("FK_CategoryId");

                    b.HasIndex("FK_VendorId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.ProductImage", b =>
                {
                    b.Property<int>("FK_ProductId");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("FK_ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.ProductTagRel", b =>
                {
                    b.Property<int>("FK_ProductId");

                    b.Property<int>("FK_TagId");

                    b.HasKey("FK_ProductId", "FK_TagId");

                    b.HasIndex("FK_TagId");

                    b.ToTable("Product_Tag");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.RequestToAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FK_UserId");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("FK_UserId");

                    b.ToTable("RequestToAdmin");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("FK_CustomerId");

                    b.Property<int>("FK_ProductId");

                    b.Property<int>("PaymentMethod");

                    b.HasKey("Id");

                    b.HasIndex("FK_CustomerId");

                    b.HasIndex("FK_ProductId");

                    b.ToTable("Transaction");
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
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

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

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.PaymentMethod", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.Product", "Product")
                        .WithMany("PaymentMethod")
                        .HasForeignKey("FK_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.Product", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("FK_CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CEI_MVC_CORE_Proj.Models.AppUser", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("FK_VendorId");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.ProductImage", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("FK_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.ProductTagRel", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.Product", "Product")
                        .WithMany("ProductTagRels")
                        .HasForeignKey("FK_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CEI_MVC_CORE_Proj.Models.Tag", "Tag")
                        .WithMany("ProductTagRels")
                        .HasForeignKey("FK_TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.RequestToAdmin", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.AppUser", "User")
                        .WithMany("Requests")
                        .HasForeignKey("FK_UserId");
                });

            modelBuilder.Entity("CEI_MVC_CORE_Proj.Models.Transaction", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.AppUser", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("FK_CustomerId");

                    b.HasOne("CEI_MVC_CORE_Proj.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("FK_ProductId")
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
                    b.HasOne("CEI_MVC_CORE_Proj.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.AppUser")
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

                    b.HasOne("CEI_MVC_CORE_Proj.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CEI_MVC_CORE_Proj.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}