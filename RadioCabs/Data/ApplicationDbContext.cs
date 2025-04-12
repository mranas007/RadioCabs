using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Models;

namespace RadioCabs.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define one-to-many relationship between User and Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete is fine here

            // Define one-to-many relationship between Company and Advertisement
            modelBuilder.Entity<Advertisement>()
              .HasOne(a => a.Company)
              .WithMany(c => c.Advertisements)
              .HasForeignKey(a => a.CompanyId)
              .OnDelete(DeleteBehavior.Cascade);

            // dummy data for Seed.
            modelBuilder.Entity<User>().HasData(
                new User { Id = "user1", UserName = "user1@example.com", NormalizedUserName = "USER1@EXAMPLE.COM", Email = "user1@example.com", NormalizedEmail = "USER1@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User One", UserType = "Company" },
                new User { Id = "user2", UserName = "user2@example.com", NormalizedUserName = "USER2@EXAMPLE.COM", Email = "user2@example.com", NormalizedEmail = "USER2@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Two", UserType = "Company" },
                new User { Id = "user3", UserName = "user3@example.com", NormalizedUserName = "USER3@EXAMPLE.COM", Email = "user3@example.com", NormalizedEmail = "USER3@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Three", UserType = "Company" },
                new User { Id = "user4", UserName = "user4@example.com", NormalizedUserName = "USER4@EXAMPLE.COM", Email = "user4@example.com", NormalizedEmail = "USER4@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Four", UserType = "Company" },
                new User { Id = "user5", UserName = "user5@example.com", NormalizedUserName = "USER5@EXAMPLE.COM", Email = "user5@example.com", NormalizedEmail = "USER5@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Five", UserType = "Company" },
                new User { Id = "user6", UserName = "user6@example.com", NormalizedUserName = "USER6@EXAMPLE.COM", Email = "user6@example.com", NormalizedEmail = "USER6@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Six", UserType = "Company" },
                new User { Id = "user7", UserName = "user7@example.com", NormalizedUserName = "USER7@EXAMPLE.COM", Email = "user7@example.com", NormalizedEmail = "USER7@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Seven", UserType = "Company" },
                new User { Id = "user8", UserName = "user8@example.com", NormalizedUserName = "USER8@EXAMPLE.COM", Email = "user8@example.com", NormalizedEmail = "USER8@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Eight", UserType = "Company" },
                new User { Id = "user9", UserName = "user9@example.com", NormalizedUserName = "USER9@EXAMPLE.COM", Email = "user9@example.com", NormalizedEmail = "USER9@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Nine", UserType = "Company" },
                new User { Id = "user10", UserName = "user10@example.com", NormalizedUserName = "USER10@EXAMPLE.COM", Email = "user10@example.com", NormalizedEmail = "USER10@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Ten", UserType = "Company" },
                new User { Id = "user11", UserName = "user11@example.com", NormalizedUserName = "USER11@EXAMPLE.COM", Email = "user11@example.com", NormalizedEmail = "USER11@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Eleven", UserType = "Driver" },
                new User { Id = "user12", UserName = "user12@example.com", NormalizedUserName = "USER12@EXAMPLE.COM", Email = "user12@example.com", NormalizedEmail = "USER12@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Twelve", UserType = "Driver" },
                new User { Id = "user13", UserName = "user13@example.com", NormalizedUserName = "USER13@EXAMPLE.COM", Email = "user13@example.com", NormalizedEmail = "USER13@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Thirteen", UserType = "Driver" },
                new User { Id = "user14", UserName = "user14@example.com", NormalizedUserName = "USER14@EXAMPLE.COM", Email = "user14@example.com", NormalizedEmail = "USER14@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Fourteen", UserType = "Driver" },
                new User { Id = "user15", UserName = "user15@example.com", NormalizedUserName = "USER15@EXAMPLE.COM", Email = "user15@example.com", NormalizedEmail = "USER15@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Fifteen", UserType = "Driver" },
                new User { Id = "user16", UserName = "user16@example.com", NormalizedUserName = "USER16@EXAMPLE.COM", Email = "user16@example.com", NormalizedEmail = "USER16@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Sixteen", UserType = "Driver" },
                new User { Id = "user17", UserName = "user17@example.com", NormalizedUserName = "USER17@EXAMPLE.COM", Email = "user17@example.com", NormalizedEmail = "USER17@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Seventeen", UserType = "Driver" },
                new User { Id = "user18", UserName = "user18@example.com", NormalizedUserName = "USER18@EXAMPLE.COM", Email = "user18@example.com", NormalizedEmail = "USER18@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Eighteen", UserType = "Driver" },
                new User { Id = "user19", UserName = "user19@example.com", NormalizedUserName = "USER19@EXAMPLE.COM", Email = "user19@example.com", NormalizedEmail = "USER19@EXAMPLE.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAE...", SecurityStamp = "XYZ", ConcurrencyStamp = "XYZ", PhoneNumber = "1234567890", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0, FullName = "User Nineteen", UserType = "Driver" }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, CompanyName = "Tech Solutions", CompanyID = 1234567890, Designation = "IT Services", Address = "123 Tech Street", Membership = "Premium", Description = "Leading IT service provider", UserId = "user1" },
                new Company { Id = 2, CompanyName = "HealthCare Inc.", CompanyID = 2345678901, Designation = "Healthcare Services", Address = "456 Health Ave", Membership = "Basic", Description = "Comprehensive healthcare services", UserId = "user2" },
                new Company { Id = 3, CompanyName = "EduWorld", CompanyID = 3456789012, Designation = "Educational Services", Address = "789 Edu Lane", Membership = "Free", Description = "Innovative educational solutions", UserId = "user3" },
                new Company { Id = 4, CompanyName = "Finance Corp", CompanyID = 4567890123, Designation = "Financial Services", Address = "101 Finance Blvd", Membership = "Premium", Description = "Expert financial services", UserId = "user4" },
                new Company { Id = 5, CompanyName = "RetailMart", CompanyID = 5678901234, Designation = "Retail Services", Address = "202 Retail Road", Membership = "Basic", Description = "Top retail services", UserId = "user5" },
                new Company { Id = 6, CompanyName = "AutoWorks", CompanyID = 6789012345, Designation = "Automotive Services", Address = "303 Auto Drive", Membership = "Free", Description = "Reliable automotive services", UserId = "user6" },
                new Company { Id = 7, CompanyName = "Foodies", CompanyID = 7890123456, Designation = "Food Services", Address = "404 Food Street", Membership = "Premium", Description = "Delicious food services", UserId = "user7" },
                new Company { Id = 8, CompanyName = "TravelCo", CompanyID = 8901234567, Designation = "Travel Services", Address = "505 Travel Blvd", Membership = "Basic", Description = "Exceptional travel services", UserId = "user8" },
                new Company { Id = 9, CompanyName = "MediaHouse", CompanyID = 9012345678, Designation = "Media Services", Address = "606 Media Lane", Membership = "Free", Description = "Creative media services", UserId = "user9" },
                new Company { Id = 10, CompanyName = "BuildIt", CompanyID = 1234567891, Designation = "Construction Services", Address = "707 Build Ave", Membership = "Premium", Description = "Quality construction services", UserId = "user10" }
            );

            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, DriverName = "John Doe", DriverID = "D12345", ContactPerson = "Jane Doe", Address = "123 Driver Street", City = "Tech City", Experience = 5, Description = "Experienced and reliable driver", UserId = "user11" },
                new Driver { Id = 2, DriverName = "Alice Smith", DriverID = "D23456", ContactPerson = "Bob Smith", Address = "456 Driver Ave", City = "Health City", Experience = 3, Description = "Reliable and safe driver", UserId = "user12" },
                new Driver { Id = 3, DriverName = "Michael Johnson", DriverID = "D34567", ContactPerson = "Sarah Johnson", Address = "789 Driver Lane", City = "Edu City", Experience = 7, Description = "Professional and skilled driver", UserId = "user13" },
                new Driver { Id = 4, DriverName = "Emily Davis", DriverID = "D45678", ContactPerson = "David Davis", Address = "101 Driver Blvd", City = "Finance City", Experience = 2, Description = "Skilled and courteous driver", UserId = "user14" },
                new Driver { Id = 5, DriverName = "James Brown", DriverID = "D56789", ContactPerson = "Linda Brown", Address = "202 Driver Road", City = "Retail City", Experience = 4, Description = "Safe and experienced driver", UserId = "user15" },
                new Driver { Id = 6, DriverName = "Patricia Wilson", DriverID = "D67890", ContactPerson = "Robert Wilson", Address = "303 Driver Drive", City = "Auto City", Experience = 6, Description = "Experienced and professional driver", UserId = "user16" },
                new Driver { Id = 7, DriverName = "Christopher Martinez", DriverID = "D78901", ContactPerson = "Laura Martinez", Address = "404 Driver Street", City = "Food City", Experience = 8, Description = "Reliable and professional driver", UserId = "user17" },
                new Driver { Id = 8, DriverName = "Jessica Garcia", DriverID = "D89012", ContactPerson = "Daniel Garcia", Address = "505 Driver Blvd", City = "Travel City", Experience = 1, Description = "Professional and courteous driver", UserId = "user18" },
                new Driver { Id = 9, DriverName = "David Rodriguez", DriverID = "D90123", ContactPerson = "Maria Rodriguez", Address = "606 Driver Lane", City = "Media City", Experience = 9, Description = "Skilled and experienced driver", UserId = "user19" }
            );
            modelBuilder.Entity<Advertisement>().HasData(
                new Advertisement { Id = 1, Title = "Tech Solutions Cab Ad", Designation = "Cab Services", Address = "123 Tech Street", Mobile = "1234567890", Description = "Leading cab service provider", CompanyId = 1 },
                new Advertisement { Id = 2, Title = "HealthCare Inc. Cab Ad", Designation = "Cab Services", Address = "456 Health Ave", Mobile = "2345678901", Description = "Comprehensive cab services", CompanyId = 2 },
                new Advertisement { Id = 3, Title = "EduWorld Cab Ad", Designation = "Cab Services", Address = "789 Edu Lane", Mobile = "3456789012", Description = "Innovative cab solutions", CompanyId = 3 },
                new Advertisement { Id = 4, Title = "Finance Corp Cab Ad", Designation = "Cab Services", Address = "101 Finance Blvd", Mobile = "4567890123", Description = "Expert cab services", CompanyId = 4 },
                new Advertisement { Id = 5, Title = "RetailMart Cab Ad", Designation = "Cab Services", Address = "202 Retail Road", Mobile = "5678901234", Description = "Top cab services", CompanyId = 5 },
                new Advertisement { Id = 6, Title = "AutoWorks Cab Ad", Designation = "Cab Services", Address = "303 Auto Drive", Mobile = "6789012345", Description = "Reliable cab services", CompanyId = 6 },
                new Advertisement { Id = 7, Title = "Foodies Cab Ad", Designation = "Cab Services", Address = "404 Food Street", Mobile = "7890123456", Description = "Delicious cab services", CompanyId = 7 },
                new Advertisement { Id = 8, Title = "TravelCo Cab Ad", Designation = "Cab Services", Address = "505 Travel Blvd", Mobile = "8901234567", Description = "Exceptional cab services", CompanyId = 8 },
                new Advertisement { Id = 9, Title = "MediaHouse Cab Ad", Designation = "Cab Services", Address = "606 Media Lane", Mobile = "9012345678", Description = "Creative cab services", CompanyId = 9 },
                new Advertisement { Id = 10, Title = "BuildIt Cab Ad", Designation = "Cab Services", Address = "707 Build Ave", Mobile = "1234567891", Description = "Quality cab services", CompanyId = 10 }
            );
            
        }
    }
}
