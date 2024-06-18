using InvSys.Components.Account;
using InvSys.Models;
using InvSys.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvSys.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
        public DbSet<Property> Properties { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyHistory> SupplyHistories { get; set; }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            ////Pushed Data into Employee Database
            modelBuilder.Entity<Employee>().HasData(
                new Employee { ID = 1, empName = "YRA B. SIBUG", empPos = SD.Role_CSS, IsRemoved = false },
                new Employee { ID = 2, empName = "MERCY LIZA B. TIBAY", empPos = SD.Role_SSS, IsRemoved = false },
                new Employee { ID = 3, empName = "MA. CRISTINA C. CRISOL", empPos = SD.Role_SS2, IsRemoved = false },
                new Employee { ID = 4, empName = "VANESSA A. ABARQUEZ", empPos = SD.Role_SSAS, IsRemoved = false },
                new Employee { ID = 5, empName = "LYN A. JERUSALEM", empPos = SD.Role_SS2, IsRemoved = false },
                new Employee { ID = 6, empName = "MARIA TERESITA E. FELIX", empPos = SD.Role_SS2, IsRemoved = false },
                new Employee { ID = 7, empName = "GEMALLI I. AGUSTIN", empPos = SD.Role_SS2, IsRemoved = false },
                new Employee { ID = 8, empName = "MARITES T. JUAN", empPos = SD.Role_SS2, IsRemoved = false },
                new Employee { ID = 9, empName = "LOWELL D. SAN ESTEBAN", empPos = SD.Role_SS2, IsRemoved = false },
                new Employee { ID = 10, empName = "ROXANNE P. VILLANUEVA", empPos = SD.Role_RO2, IsRemoved = false },
                new Employee { ID = 11, empName = "GEMMA DC. SIPPI", empPos = SD.Role_SA, IsRemoved = false },
                new Employee { ID = 12, empName = "CHERRY ANN A. VILLANUEVA", empPos = SD.Role_SA, IsRemoved = false },
                new Employee { ID = 13, empName = "LINO BILOLO JR", empPos = SD.Role_RO1, IsRemoved = false },
                new Employee { ID = 14, empName = "JOMARI T. PERLAS", empPos = SD.Role_RO1, IsRemoved = false },
                new Employee { ID = 15, empName = "KIMBERLY JOYCE M. ATOLE", empPos = SD.Role_AO1, IsRemoved = false },
                new Employee { ID = 16, empName = "DAN LEONEL T. ANISCOL", empPos = SD.Role_RO1, IsRemoved = false },
                new Employee { ID = 17, empName = "DANICA O. CABALSA", empPos = SD.Role_RO1, IsRemoved = false },
                new Employee { ID = 18, empName = "ROSELYND N. LUCAS", empPos = SD.Role_ACC1, IsRemoved = false },
                new Employee { ID = 19, empName = "ANGELA B. VILLANUEVA", empPos = SD.Role_RO1, IsRemoved = false },
                new Employee { ID = 20, empName = "MARVIN SA. SAGUN", empPos = SD.Role_RO1, IsRemoved = false },
                new Employee { ID = 21, empName = "RICHIEGOLD VARCA", empPos = SD.Role_AO1, IsRemoved = false },
                new Employee { ID = 22, empName = "MAC-MAC R. ABDULLAH", empPos = SD.Role_AO3, IsRemoved = false }
                );



            var passwordHasher = new PasswordHasher<IdentityUser>();

            //Pushed Data into Product Database
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin@psa.gov.ph",
                    NormalizedUserName = "ADMIN@PSA.GOV.PH",
                    Email = "admin@psa.gov.ph",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin123!")
                });
        }
    }
}
