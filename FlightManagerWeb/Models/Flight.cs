using System.ComponentModel.DataAnnotations.Schema;
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
        private DateTime? departureTimeAndDate= null;
        [Required]
        public DateTime DepartureTimeAndDate { get{return DepartureTimeAndDate;} 
        set{
            departureTimeAndDate = value;
            if(ArrivalTimeAndDate!=null)Duration = ArrivalTimeAndDate-value;
            } 
        }
        private DateTime? arrivalTimeAndDate= null;
        [Required]
        public DateTime? ArrivalTimeAndDate { 
            get {return arrivalTimeAndDate;} 
            set{
                if(DepartureTimeAndDate.CompareTo(value)<1)
                {
                    throw new ArgumentException("ArrivalTimeAndDate must be greater than DepartureTimeAndDate");
                }
                arrivalTimeAndDate = value;
                if(DepartureTimeAndDate!=null)Duration = value-DepartureTimeAndDate;
            } 
        }
        [NotMappedAttribute]
        public TimeSpan? Duration { get; set; }
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
        public int PlaneCapacity{ get; set; }
        public int ReservationId { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
    }
}
