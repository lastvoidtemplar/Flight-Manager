using FlightManagerWeb.Data;
using FlightManagerWeb.Models;

namespace FlightManagerWeb.Services
{
    public class ReservationService
    {
        private readonly FlightDbContext _context;
        public ReservationService(FlightDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Reservation> IndexAllReservations(int pageNumber, int pageSize, string email)
        {
            var filterUsers = _context.Reservations
            .Where(reservation =>reservation.Confirmed
                &&(email == "" || (email == reservation.Email)))
            .Skip((pageNumber - 1) * pageSize).Take(pageSize); ;
            return filterUsers;
        }
        public async Task<int> NumberOfPages(int pageSize, string email)
        {
            double usersCount = _context.Reservations.Where(reservation =>
                 email == "" || (email == reservation.Email)).Count();
            double countPages = usersCount / pageSize;
            return (int)Math.Ceiling(countPages);
        }
        public  IEnumerable<Flight> AvailableFlights(int pageNumber, int pageSize)
        {
            var now = DateTime.Now;
            int skip = (pageNumber - 1) * pageSize;
            var flights = _context.Flights.Where(flight => flight.DepartureTimeAndDate > now);
            return flights.Skip(skip).Take(pageSize);
        }
        public int AvailableFlightsPageCount(int pageSize)
        {

            double flightsCount = _context.Flights.Where(flight => flight.DepartureTimeAndDate > DateTime.Now).Count();
            double countPages = flightsCount / pageSize;
            return (int)Math.Ceiling(countPages);
        }
        public async Task<string> CreateReservation(string email,int flightId){
            Reservation reservation = new Reservation();
            reservation.Id = Guid.NewGuid().ToString();
            reservation.DateAndTimeReservation = DateTime.Now;
            reservation.Confirmed = false;
            reservation.Email = email;
            reservation.FlightId = flightId;
            await _context.Reservations.AddAsync(reservation);
            if(await _context.SaveChangesAsync()==1){
                return reservation.Id;
            }
            throw new Exception("Coould not save!");
        }
        public string GetEmailOFReservationById(string id){
            return _context.Reservations.Find(id).Email;
        }
        public string GetFlightInfoOFReservationById(string id){
            Flight flight = _context.Flights.Find(_context.Reservations.Find(id).FlightId);
            return $"{flight.LocationFrom}->{flight.LocationTo} in {flight.DepartureTimeAndDate} that will last {flight.Duration}";
        }
        public  List<Passager> PassangersReservation(int pageNumber,int pageSize,string id)
        {
            int skip = (pageNumber - 1) * pageSize;
            var passangers = _context.Passagers.Where(passanger => passanger.ReservationId ==id);
            return passangers.Skip(skip).Take(pageSize).ToList();
        }
        public int PassangersReservationCount(int pageSize,string id)
        {
            
            double passangers= _context.Passagers.Where(passanger=>passanger.ReservationId==id).Count();
            return (int)Math.Ceiling(passangers/pageSize);
        }
        public void CreatePassager(Passager passager){
            _context.Passagers.Add(passager);
            _context.SaveChanges();
        }
    }
}