﻿// <auto-generated />
using System;
using InvSys.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InvSys.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InvSys.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a8083892-ecd2-49b9-ad54-1fcfc99b1241",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7f09f932-7f8c-42e3-adbb-527fa1dac321",
                            Email = "admin@psa.gov.ph",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMIN@PSA.GOV.PH",
                            PasswordHash = "AQAAAAIAAYagAAAAEF8VNN2IVZeshaPmCloPjO4tHuYNQ93J+1kataEFq5XBTF3psyEkj1lheJAQk8c1PA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "23d5b2d4-58f6-4e7f-b3a0-e1dedf3b6d0d",
                            TwoFactorEnabled = false,
                            UserName = "admin@psa.gov.ph"
                        });
                });

            modelBuilder.Entity("InvSys.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool?>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("empMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("empName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("empPos")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IsRemoved = false,
                            empName = "YRA B. SIBUG",
                            empPos = "Chief Statistical Specialist"
                        },
                        new
                        {
                            ID = 2,
                            IsRemoved = false,
                            empName = "MERCY LIZA B. TIBAY",
                            empPos = "Supervising Statistical Specialist"
                        },
                        new
                        {
                            ID = 3,
                            IsRemoved = false,
                            empName = "MA. CRISTINA C. CRISOL",
                            empPos = "Statistical Specialist II"
                        },
                        new
                        {
                            ID = 4,
                            IsRemoved = false,
                            empName = "VANESSA A. ABARQUEZ",
                            empPos = "Senior Statistical Specialist"
                        },
                        new
                        {
                            ID = 5,
                            IsRemoved = false,
                            empName = "LYN A. JERUSALEM",
                            empPos = "Statistical Specialist II"
                        },
                        new
                        {
                            ID = 6,
                            IsRemoved = false,
                            empName = "MARIA TERESITA E. FELIX",
                            empPos = "Statistical Specialist II"
                        },
                        new
                        {
                            ID = 7,
                            IsRemoved = false,
                            empName = "GEMALLI I. AGUSTIN",
                            empPos = "Statistical Specialist II"
                        },
                        new
                        {
                            ID = 8,
                            IsRemoved = false,
                            empName = "MARITES T. JUAN",
                            empPos = "Statistical Specialist II"
                        },
                        new
                        {
                            ID = 9,
                            IsRemoved = false,
                            empName = "LOWELL D. SAN ESTEBAN",
                            empPos = "Statistical Specialist II"
                        },
                        new
                        {
                            ID = 10,
                            IsRemoved = false,
                            empName = "ROXANNE P. VILLANUEVA",
                            empPos = "Registration Officer II"
                        },
                        new
                        {
                            ID = 11,
                            IsRemoved = false,
                            empName = "GEMMA DC. SIPPI",
                            empPos = "Statistical Analyst"
                        },
                        new
                        {
                            ID = 12,
                            IsRemoved = false,
                            empName = "CHERRY ANN A. VILLANUEVA",
                            empPos = "Statistical Analyst"
                        },
                        new
                        {
                            ID = 13,
                            IsRemoved = false,
                            empName = "LINO BILOLO JR",
                            empPos = "Registration Officer I"
                        },
                        new
                        {
                            ID = 14,
                            IsRemoved = false,
                            empName = "JOMARI T. PERLAS",
                            empPos = "Registration Officer I"
                        },
                        new
                        {
                            ID = 15,
                            IsRemoved = false,
                            empName = "KIMBERLY JOYCE M. ATOLE",
                            empPos = "Administrative Officer I"
                        },
                        new
                        {
                            ID = 16,
                            IsRemoved = false,
                            empName = "DAN LEONEL T. ANISCOL",
                            empPos = "Registration Officer I"
                        },
                        new
                        {
                            ID = 17,
                            IsRemoved = false,
                            empName = "DANICA O. CABALSA",
                            empPos = "Registration Officer I"
                        },
                        new
                        {
                            ID = 18,
                            IsRemoved = false,
                            empName = "ROSELYND N. LUCAS",
                            empPos = "Accountant I"
                        },
                        new
                        {
                            ID = 19,
                            IsRemoved = false,
                            empName = "ANGELA B. VILLANUEVA",
                            empPos = "Registration Officer I"
                        },
                        new
                        {
                            ID = 20,
                            IsRemoved = false,
                            empName = "MARVIN SA. SAGUN",
                            empPos = "Registration Officer I"
                        },
                        new
                        {
                            ID = 21,
                            IsRemoved = false,
                            empName = "RICHIEGOLD VARCA",
                            empPos = "Administrative Officer I"
                        },
                        new
                        {
                            ID = 22,
                            IsRemoved = false,
                            empName = "MAC-MAC R. ABDULLAH",
                            empPos = "Administrative Assistant III"
                        });
                });

            modelBuilder.Entity("InvSys.Models.Property", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateOnly?>("AssignDate")
                        .HasColumnType("date");

                    b.Property<bool?>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateOnly?>("ReturnDate")
                        .HasColumnType("date");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propArticle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("propBalancePerCardQty")
                        .HasColumnType("int");

                    b.Property<string>("propCustodian")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("propOnHandPerCardQty")
                        .HasColumnType("int");

                    b.Property<string>("propPropertyNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("propShortageOverageQty")
                        .HasColumnType("int");

                    b.Property<double?>("propShortageOverageValue")
                        .HasColumnType("float");

                    b.Property<string>("propStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propUnitOfMeasure")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double?>("propUnitValue")
                        .HasColumnType("float");

                    b.Property<DateOnly?>("propYrAcquired")
                        .HasColumnType("date");

                    b.HasKey("ID");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("InvSys.Models.Supply", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool?>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateOnly?>("IssueDate")
                        .HasColumnType("date");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("supArticle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("supAverageUnitCost")
                        .HasColumnType("float");

                    b.Property<int?>("supBalance")
                        .HasColumnType("int");

                    b.Property<DateOnly>("supBegInvDate")
                        .HasColumnType("date");

                    b.Property<int?>("supBeginningInventoryQty")
                        .HasColumnType("int");

                    b.Property<int?>("supDelivery")
                        .HasColumnType("int");

                    b.Property<string>("supDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("supEndingInventoryQty")
                        .HasColumnType("int");

                    b.Property<int?>("supIssuance")
                        .HasColumnType("int");

                    b.Property<string>("supRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("supStockLetter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("supStockNumber")
                        .HasColumnType("int");

                    b.Property<double?>("supTotalAmount")
                        .HasColumnType("float");

                    b.Property<string>("supUnitType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Supplies");
                });

            modelBuilder.Entity("InvSys.Models.SupplyHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateOnly>("DateModified")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("SupplyID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("SupplyHistories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InvSys.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InvSys.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvSys.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InvSys.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
