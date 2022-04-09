namespace FlightManagerWeb.Models.Reservations
{
    public class EditResservation
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FlightInfo { get; set; }
         public PassagerData PassagerData { get; set; }
        
    }
        public class PassagerData{
         public  List<Passager> Passagers { get; set; }
         public int PassagerCount {get;set;}
         public int CurrentPassager { get; set; }
    }
}