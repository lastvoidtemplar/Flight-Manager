using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FlightManagerWeb.Models
{
    public class FlightUser:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        /// <summary>
        /// Only numbers and length 10
        /// </summary>
        /// <value></value>
        // [RegularExpression("^[0-9]{10}$")]
        public string SSN { get; set;}
        // [RegularExpression("/^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$/")]
        public string Email { get; set; }
    }
}