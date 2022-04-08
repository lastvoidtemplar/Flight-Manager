using System.ComponentModel.DataAnnotations;

namespace FlightManagerWeb.Models
{
    public class DisplayFlightUser
    {
        public string Id { get; set; }
        public string SecurityStamp{get;set;}
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string SSN{ get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}