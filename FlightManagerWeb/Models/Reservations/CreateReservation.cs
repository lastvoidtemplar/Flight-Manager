using System.ComponentModel.DataAnnotations;
namespace FlightManagerWeb.Models.Reservations
{
    public class AvailableFlightsData{
        public IEnumerable<Flight> flights {get;set;}
        
        public int CurrentPage { get; set; }
        public int CountOfPages { get; set; }
    }
    public class CreateReservation{
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public int FlightId { get; set; }
        public AvailableFlightsData AvailableFlightsData { get; set; }
       
    }
}