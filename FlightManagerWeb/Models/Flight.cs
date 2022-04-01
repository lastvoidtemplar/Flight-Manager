using System.ComponentModel.DataAnnotations;

namespace FlightManagerWeb.Models
{
    public class Flight
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(30)]
        public string LocationFrom { get; set; }


        [Required]
        [MaxLength(30)]
        public string LocationTo { get; set; }


        public DateTime DepartureTimeAndDate { get; set; }


        public DateTime ArrivalTimeAndDate { get; set; }


        [Required]
        [MaxLength(30)]
        public string PlaneType { get; set; }


        [Required]
        [MaxLength(30)]
        public string PlaneNumber { get; set; }


        [Required]
        [MaxLength(30)]
        public string PilotName { get; set; }


        [Range(0, 600)]
        public int CapacityBusinessClass { get; set; }


        [Range(0, 600)]
        public int CapacityFirstClass { get; set; }


        public List<Reservation>? reservations { get; set; }
    }
}
