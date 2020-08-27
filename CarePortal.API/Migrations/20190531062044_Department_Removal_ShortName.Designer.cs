﻿// <auto-generated />
using System;
using CarePortal.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarePortal.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190531062044_Department_Removal_ShortName")]
    partial class Department_Removal_ShortName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarePortal.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<int>("IsApproved");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Picture");

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

            modelBuilder.Entity("CarePortal.Data.Models.Calendar", b =>
                {
                    b.Property<int>("CalendarId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUser_DoctorId");

                    b.Property<string>("ApplicationUser_PatientId");

                    b.Property<DateTimeOffset>("AppointmentTime")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("CalendarId");

                    b.HasIndex("ApplicationUser_DoctorId");

                    b.HasIndex("ApplicationUser_PatientId");

                    b.ToTable("Calendar");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FeedId");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Conversation", b =>
                {
                    b.Property<int>("ConversationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUser_DoctorId");

                    b.Property<string>("ApplicationUser_PatientId");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MessageType")
                        .HasColumnType("int");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.HasKey("ConversationId");

                    b.HasIndex("ApplicationUser_DoctorId");

                    b.HasIndex("ApplicationUser_PatientId");

                    b.ToTable("Conversation");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Feed", b =>
                {
                    b.Property<int>("FeedId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DepartmentId");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FeedId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Feed");
                });

            modelBuilder.Entity("CarePortal.Data.Models.FeedResponse", b =>
                {
                    b.Property<int>("FeedResponseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("FeedId");

                    b.Property<int>("Response")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FeedResponseId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("FeedId");

                    b.ToTable("FeedResponse");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Finance", b =>
                {
                    b.Property<int>("FinanceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("CustomerProfileId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DefaultPaymentProfile")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FinanceId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Finance");
                });

            modelBuilder.Entity("CarePortal.Data.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Expiry")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<DateTimeOffset>("TransactionDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("CarePortal.Data.Models.UserDepartment", b =>
                {
                    b.Property<int>("UserDepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("DepartmentId");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserDepartmentId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("UserDepartment");
                });

            modelBuilder.Entity("CarePortal.Data.ViewModels.CommentsViewModel", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments");

                    b.Property<int>("FeedId");

                    b.Property<string>("ProfilePic");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("CommentId");

                    b.HasIndex("FeedId");

                    b.ToTable("CommentsViewModel");
                });

            modelBuilder.Entity("CarePortal.Data.ViewModels.ConversationViewModel", b =>
                {
                    b.Property<int>("ConversationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<string>("DoctorId");

                    b.Property<string>("DoctorName");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Message");

                    b.Property<int>("MessageType");

                    b.Property<string>("PatientId");

                    b.Property<string>("PatientName");

                    b.Property<string>("Question");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.HasKey("ConversationId");

                    b.ToTable("ConversationViewModel");
                });

            modelBuilder.Entity("CarePortal.Data.ViewModels.FeedViewModel", b =>
                {
                    b.Property<int>("FeedId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data");

                    b.Property<int>("DepartmentId");

                    b.Property<int>("Dislikes");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("LastName");

                    b.Property<int>("Likes");

                    b.Property<string>("Picture");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<string>("UserId");

                    b.HasKey("FeedId");

                    b.ToTable("FeedViewModel");
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

            modelBuilder.Entity("CarePortal.Data.Models.Calendar", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser_Doctor")
                        .WithMany()
                        .HasForeignKey("ApplicationUser_DoctorId");

                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser_Patient")
                        .WithMany()
                        .HasForeignKey("ApplicationUser_PatientId");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Conversation", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser_Doctor")
                        .WithMany()
                        .HasForeignKey("ApplicationUser_DoctorId");

                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser_Patient")
                        .WithMany()
                        .HasForeignKey("ApplicationUser_PatientId");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Feed", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("CarePortal.Data.Models.FeedResponse", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("CarePortal.Data.Models.Feed", "Feed")
                        .WithMany()
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CarePortal.Data.Models.Finance", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("CarePortal.Data.Models.Subscription", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("CarePortal.Data.Models.UserDepartment", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("CarePortal.Data.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CarePortal.Data.ViewModels.CommentsViewModel", b =>
                {
                    b.HasOne("CarePortal.Data.ViewModels.FeedViewModel")
                        .WithMany("CommentsViewModel")
                        .HasForeignKey("FeedId")
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
                    b.HasOne("CarePortal.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser")
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

                    b.HasOne("CarePortal.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarePortal.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
