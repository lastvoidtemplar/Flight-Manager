namespace FlightManagerWeb.Models.Users
{
    public class Search {
        public string SearchText { get; set; }
        public bool EmailIsChecked { get; set; }
        public bool UsernameIsChecked { get; set; }
        public bool FirstnameIsChecked { get; set; }
        public bool LastnameIsChecked { get; set; }
    }
    public class IndexViewUser
    {
        public IEnumerable<DisplayFlightUser> Users{ get; set; }
        public Search Search {get;set;}
        public int CurrentPage { get; set; }
        public int CountOfPages { get; set; }
    }
}