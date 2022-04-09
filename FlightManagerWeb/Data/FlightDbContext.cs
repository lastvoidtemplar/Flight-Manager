using FlightManagerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightManagerWeb.Data
{
    public class FlightDbContext : IdentityDbContext<FlightUser, IdentityRole, string>
    {
        public DbSet<Flight> Flights {get;set;}
        public DbSet<Reservation> Reservations {get;set;}
        public DbSet<Passager> Passagers {get;set;}

        public FlightDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
             base.OnModelCreating(builder);
            builder.Entity<Reservation>()
            .HasOne<Flight>(reservation => reservation.Flight)
            .WithMany(flight=>flight.Reservations)
            .HasForeignKey(reservation=>reservation.FlightId);
            builder.Entity<Passager>()
            .HasOne<Reservation>(passager=>passager.Reservation)
            .WithMany(reservation=>reservation.Passagers)
            .HasForeignKey(passager=>passager.ReservationId);
            builder.Entity<FlightUser>() .HasIndex(u => u.UserName).IsUnique();
            builder.Entity<FlightUser>() .HasIndex(u => u.SSN).IsUnique();
            builder.Entity<IdentityRole>() .HasIndex(u => u.Name).IsUnique();
            builder.Entity<Flight>() .HasIndex(u => u.PlaneNumber).IsUnique();
            builder.Entity<Passager>() .HasIndex(u => u.SSN).IsUnique();
            builder.Entity<Passager>().HasCheckConstraint("CK_TicketTypes","TicketType in ('Normal','Business')");
            builder.Entity<IdentityRole>().HasCheckConstraint("CK_IdentityRoles","Name in ('Admin','User')");
            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                new IdentityRole(){
                    Id=adminRoleId,
                    Name = "Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole(){
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName="USER"
                }
            });
            FlightUser adminUser = new FlightUser();
            adminUser.Id = Guid.NewGuid().ToString();
            adminUser.UserName = "admin";
            adminUser.Firstname = "Admin";
            adminUser.Lastname = "Admin";
            adminUser.NormalizedUserName="ADMIN";
            adminUser.SecurityStamp = Guid.NewGuid().ToString();;
            adminUser.SSN = "1234567890";
            adminUser.Email = "admin123@flight.com";
            adminUser.PhoneNumber = "1234567890";
            adminUser.Address = "AdminAddress";
            PasswordHasher<FlightUser> hasher = new PasswordHasher<FlightUser>();
            adminUser.PasswordHash =  hasher.HashPassword(adminUser,"admin123");
            builder.Entity<FlightUser>().HasData(adminUser);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>(){
                RoleId = adminRoleId,
                UserId = adminUser.Id
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>(){
                RoleId = userRoleId,
                UserId = adminUser.Id
            });
        }
    }
}