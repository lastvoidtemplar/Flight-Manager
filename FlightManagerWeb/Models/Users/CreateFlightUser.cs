using System.ComponentModel.DataAnnotations;

namespace FlightManagerWeb.Models.Users
{
    public class CreateFlightUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [StringLength(30, ErrorMessage = "Firstname must contain less than 30 characters")]
        public string Firstname { get; set; }
        [StringLength(30, ErrorMessage = "Lastname must contain less than 30 characters")]
        public string Lastname { get; set; }
        private string ssn = "";
        [StringLength(10, MinimumLength = 10, ErrorMessage = "SSN must be 10 character long!")]
        public string SSN
        {
            get { return ssn; }
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