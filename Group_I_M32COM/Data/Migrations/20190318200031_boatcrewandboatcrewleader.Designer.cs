﻿// <auto-generated />
using System;
using Group_I_M32COM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Group_I_M32COM.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190318200031_boatcrewandboatcrewleader")]
    partial class boatcrewandboatcrewleader
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Boat_TypesId");

                    b.Property<string>("Boat_description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Boat_media_type");

                    b.Property<string>("Boat_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Boat_top_speed")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Boat_weight")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("Created_At");

                    b.Property<int?>("Sub_Boat_TypesId");

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("Boat_TypesId");

                    b.HasIndex("Sub_Boat_TypesId");

                    b.ToTable("Boats");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat_crew", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Boat_crew_address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Boat_crew_allocation");

                    b.Property<string>("Boat_crew_logo");

                    b.Property<string>("Boat_crew_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Boat_crew_phone");

                    b.Property<DateTime?>("Created_At");

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.ToTable("Boat_Crews");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat_crew_leader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Created_At");

                    b.Property<DateTime?>("Updated_At");

                    b.Property<string>("User_Id");

                    b.Property<int?>("boat_CrewId");

                    b.HasKey("Id");

                    b.HasIndex("boat_CrewId");

                    b.ToTable("Boat_crew_leader");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat_media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoatId");

                    b.Property<string>("Boat_media_url");

                    b.Property<DateTime?>("Created_At");

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("BoatId");

                    b.ToTable("Boat_Medias");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat_media_type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Boat_media_type_name");

                    b.Property<DateTime?>("Created_At");

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.ToTable("Boat_Media_Types");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat_type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Boat_class_type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("Created_At");

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.ToTable("Boat_Types");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Boat_TypesId");

                    b.Property<DateTime?>("Created_At");

                    b.Property<DateTime?>("Event_End_date")
                        .IsRequired();

                    b.Property<DateTime?>("Event_Start_date")
                        .IsRequired();

                    b.Property<int?>("Event_TypesId");

                    b.Property<string>("Event_description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Event_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("Boat_TypesId");

                    b.HasIndex("Event_TypesId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Event_participation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Created_At");

                    b.Property<int?>("EventId");

                    b.Property<DateTime?>("Updated_At");

                    b.Property<int?>("boat_CrewId");

                    b.Property<int>("points_awarded");

                    b.Property<string>("position");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("boat_CrewId");

                    b.ToTable("Event_Participations");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Event_type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Created_At");

                    b.Property<string>("Event_type_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.ToTable("Event_Types");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Members", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Boat_CrewId");

                    b.Property<DateTime?>("Created_At");

                    b.Property<string>("Member_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("Boat_CrewId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Sub_boat_type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Boat_TypesId");

                    b.Property<DateTime?>("Created_At");

                    b.Property<string>("Sub_boat_class_type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("Boat_TypesId");

                    b.ToTable("Sub_Boat_Types");
                });

            modelBuilder.Entity("Group_I_M32COM.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

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

            modelBuilder.Entity("Group_I_M32COM.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<DateTime?>("Created_At");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("Last_Login");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<bool>("Login_Status");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostalCode");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("Updated_At");

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

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat", b =>
                {
                    b.HasOne("Group_I_M32COM.DbTableModel.Boat_type", "Boat_Types")
                        .WithMany("Boats")
                        .HasForeignKey("Boat_TypesId");

                    b.HasOne("Group_I_M32COM.DbTableModel.Sub_boat_type", "Sub_Boat_Types")
                        .WithMany("Boats")
                        .HasForeignKey("Sub_Boat_TypesId");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat_crew_leader", b =>
                {
                    b.HasOne("Group_I_M32COM.DbTableModel.Boat_crew", "boat_Crew")
                        .WithMany("boat_Crew_Leaders")
                        .HasForeignKey("boat_CrewId");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Boat_media", b =>
                {
                    b.HasOne("Group_I_M32COM.DbTableModel.Boat", "Boat")
                        .WithMany("Boat_Medias")
                        .HasForeignKey("BoatId");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Event", b =>
                {
                    b.HasOne("Group_I_M32COM.DbTableModel.Boat_type", "Boat_Types")
                        .WithMany("Events")
                        .HasForeignKey("Boat_TypesId");

                    b.HasOne("Group_I_M32COM.DbTableModel.Event_type", "Event_Types")
                        .WithMany("Events")
                        .HasForeignKey("Event_TypesId");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Event_participation", b =>
                {
                    b.HasOne("Group_I_M32COM.DbTableModel.Event", "Event")
                        .WithMany("Event_Participations")
                        .HasForeignKey("EventId");

                    b.HasOne("Group_I_M32COM.DbTableModel.Boat_crew", "boat_Crew")
                        .WithMany("Event_Participations")
                        .HasForeignKey("boat_CrewId");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Members", b =>
                {
                    b.HasOne("Group_I_M32COM.DbTableModel.Boat_crew", "Boat_Crew")
                        .WithMany("Members")
                        .HasForeignKey("Boat_CrewId");
                });

            modelBuilder.Entity("Group_I_M32COM.DbTableModel.Sub_boat_type", b =>
                {
                    b.HasOne("Group_I_M32COM.DbTableModel.Boat_type", "Boat_Types")
                        .WithMany("Sub_Boat_Types")
                        .HasForeignKey("Boat_TypesId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Group_I_M32COM.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Group_I_M32COM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Group_I_M32COM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Group_I_M32COM.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Group_I_M32COM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Group_I_M32COM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
