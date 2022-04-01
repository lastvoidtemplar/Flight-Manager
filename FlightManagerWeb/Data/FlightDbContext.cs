using FlightManagerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightManagerWeb.Data
{
    public class FlightDbContext : IdentityDbContext<FlightUser, IdentityRole, string>
    {
        public DbSet<TestRegex> testRegices {get;set;}
        public FlightDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}