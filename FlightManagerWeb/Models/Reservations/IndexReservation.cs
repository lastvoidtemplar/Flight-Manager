namespace FlightManagerWeb.Models.Reservations
{
    public class IndexReservation
    {
   
        public IEnumerable<Reservation> Reservations { get; set; }
         public string Email {get;set;}
        public int CurrentPage { get; set; }
        public int CountOfPages { get; set; }
    }
}