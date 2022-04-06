using System.ComponentModel.DataAnnotations;

namespace FlightManagerWeb.Models
{
    public class Passager
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }


        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }


        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }


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


        [Required]
        [Phone]
        public string PhoneNumber { get; set; }


        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; }


        [Required]
        [MaxLength(30)]
         public string TicketType { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}