using System.Numerics;
using FlightManagerWeb.Data;
using FlightManagerWeb.Models;

namespace FlightManagerWeb.Services
{
    public class FlightService
    {
        private readonly FlightDbContext _context;
        public FlightService(FlightDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Flight> IndexAllFlights(int pageNumber,int pageSize,string search){
            var filterUsers = _context.Flights
            .Where(flight=>
                search==""||((search == flight.LocationFrom)
            ||(search == flight.LocationTo)
            ||(search == flight.DepartureTimeAndDate.Date.ToString())
            ||(search==flight.PlaneType)
            ||(search==flight.PlaneNumber)
            ||(search==flight.PilotName)
            ||(search==flight.PlaneCapacity.ToString())
            ||(search==flight.CapacityBusinessClass.ToString())))
            .Skip((pageNumber-1)*pageSize).Take(pageSize);;         
            return filterUsers;

        }
        public async Task<int> NumberOfPages(int pageSize,string search){
           double usersCount =  _context.Flights.Where(flight=>
               search==""||((search == flight.LocationFrom)
            ||(search == flight.LocationTo)
            ||(search == flight.DepartureTimeAndDate.Date.ToString())
            ||(search==flight.PlaneType)
            ||(search==flight.PlaneNumber)
            ||(search==flight.PilotName)
            ||(search==flight.PlaneCapacity.ToString())
            ||(search==flight.CapacityBusinessClass.ToString()))).Count();         
           double countPages = usersCount/pageSize;
            return (int)Math.Ceiling(countPages);
        }
         public async Task CreateFlight(Flight flight){
             System.Console.WriteLine("ADDINF");
            await  _context.Flights.AddAsync(flight);
            System.Console.WriteLine(await _context.SaveChangesAsync()==1);
        }
        public async Task<Flight> GetFlightById(int id){
            var flight = await _context.Flights.FindAsync(id);
            return flight;
        }
        public async Task UpdateFlight(Flight flight){
               _context.Flights.Update(flight);

             await  _context.SaveChangesAsync();
        }
        public async Task DeleteFlight(int id){
Flight flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
    }
}