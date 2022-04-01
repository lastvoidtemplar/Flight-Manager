using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightManagerWeb.Models
{
    public class Reservation
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


        [Required]
        [MaxLength(30)]
        public string EGN { get; set; }


        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }


        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; }


        [Required]
        [MaxLength(30)]
        public string TicketType { get; set; }


        [ForeignKey("Flight")]
        public int FlightId { get; set; }
    }
}
