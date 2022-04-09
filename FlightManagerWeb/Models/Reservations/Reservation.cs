using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightManagerWeb.Models
{
    public class Reservation
    {
        public string Id { get; set; }
        public DateTime DateAndTimeReservation{ get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool Confirmed { get; set; }
        public int FlightId { get; set; }
        public virtual Flight Flight {get;set;}
        public virtual List<Passager> Passagers { get; set; }
    }
}
