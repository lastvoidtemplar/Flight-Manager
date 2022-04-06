using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FlightManagerWeb.Models
{
    public class FlightUser : IdentityUser<string>
    {
        [Required]
        [StringLength(30, ErrorMessage = "Firstname must contain less than 30 characters")]
        public string Firstname { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Lastname must contain less than 30 characters")]
        public string Lastname { get; set; }
        /// <summary>
        /// Only numbers and length 10
        /// </summary>
        /// <value></value>
        private string ssn = "";
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "SSN must be 10 character long!")]
        public string SSN
        {
            get { return ssn;}
            set
            {
                if (!value.All(Char.IsDigit)) throw new ArgumentException("SSN must contain only digits");
                ssn = value;
            }
        }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}