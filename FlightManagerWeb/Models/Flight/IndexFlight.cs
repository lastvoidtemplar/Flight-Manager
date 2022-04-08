using FlightManagerWeb.Models.Users;

namespace FlightManagerWeb.Models
{
    public class IndexFlight
    {
        public IEnumerable<Flight> Flights { get; set; }
         public string Search {get;set;}
        public int CurrentPage { get; set; }
        public int CountOfPages { get; set; }
    }
}