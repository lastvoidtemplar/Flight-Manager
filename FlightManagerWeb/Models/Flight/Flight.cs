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
        private DateTime departureTimeAndDate = DateTime.Now;
        [Required]
        public DateTime DepartureTimeAndDate
        {
            get { return departureTimeAndDate; }
            set
            {
                departureTimeAndDate = value;
                if (ArrivalTimeAndDate != null) Duration = ArrivalTimeAndDate - value;
            }
        }
        private DateTime arrivalTimeAndDate = DateTime.Now;
        [Required]
        public DateTime ArrivalTimeAndDate
        {
            get { return arrivalTimeAndDate; }
            set
            {
                if (DepartureTimeAndDate >= (value))
                {
                    throw new ArgumentException("ArrivalTimeAndDate must be greater than DepartureTimeAndDate");
                }
                arrivalTimeAndDate = value;
                if (DepartureTimeAndDate != null) Duration = value - DepartureTimeAndDate;
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
        private int planeCapacity = 1;
        [Range(0, 600)]
        public int PlaneCapacity
        {
            get { return planeCapacity; }
            set
            {
                if (CapacityBusinessClass > value) throw new ArgumentException("Business class capacity must be less than plane capacity");
                planeCapacity = value;
            }
        }
        private int capacityBusinessClass = 0;
        [Range(0, 600)]
        public int CapacityBusinessClass
        {
            get { return capacityBusinessClass; }
            set
            {
                if (value > planeCapacity) throw new ArgumentException("Business class capacity must be less than plane capacity");
                capacityBusinessClass = value;
            }
        }



        public virtual List<Reservation> Reservations { get; set; }
    }
}
