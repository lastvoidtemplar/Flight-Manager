using FlightManagerWeb.Data;
using FlightManagerWeb.Models;

namespace FlightManagerWeb.Services
{
    public class FlightUserSevice
    {
        private readonly FlightDbContext _context;
        public FlightUserSevice(FlightDbContext context)
        {
            _context = context;
        }
        public FlightUser GetFlightYserIdByUsername(string username){
            FlightUser user =_context.Users.FirstOrDefault(user=>user.UserName ==username);
            if(user==null)throw new ArgumentException("Invalid username!");
            return  user;
        }
    }
}