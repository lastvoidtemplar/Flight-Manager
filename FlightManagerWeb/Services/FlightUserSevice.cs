using FlightManagerWeb.Data;
using FlightManagerWeb.Models;
using FlightManagerWeb.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace FlightManagerWeb.Services
{
    public class FlightUserSevice
    {
        private readonly FlightDbContext _context;
        private readonly UserManager<FlightUser> _userManager;
        private readonly IUserStore<FlightUser> _userStore;
        public FlightUserSevice(FlightDbContext context)
        {
            _context = context;
        }
        public FlightUserSevice(FlightDbContext context,UserManager<FlightUser> userManager,IUserStore<FlightUser> userStore)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
        }
        public async Task<FlightUser> GetFlightYserIdByUsername(string username){
            FlightUser user =_context.Users.FirstOrDefault(user=>user.UserName ==username);
            if(user==null)throw new ArgumentException("Invalid username!");
            return  user;
        }
        public IEnumerable<DisplayFlightUser> IndexAllUsers(int pageNumber,int pageSize,Search search){
            bool isFiltered = search.EmailIsChecked||search.UsernameIsChecked||search.FirstnameIsChecked||search.LastnameIsChecked;
            var filterUsers = _context.Users.Where(user=>
            !isFiltered||(
                (search.EmailIsChecked&&search.SearchText == user.Email)
            ||(search.UsernameIsChecked&&search.SearchText == user.UserName)
            ||(search.FirstnameIsChecked&&search.SearchText == user.Firstname)
            ||(search.LastnameIsChecked &&search.SearchText == user.Lastname))).Skip((pageNumber-1)*pageSize).Take(pageSize);;         
            var displayUsers = filterUsers.Select(user=>new DisplayFlightUser(){
                Id = user.Id,
                SecurityStamp = user.SecurityStamp,
                Username = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                SSN = user.SSN,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            });
            System.Console.WriteLine("display");
            return displayUsers;

        }
        public async Task<int> NumberOfPages(int pageSize,Search search){
         bool isFiltered = search.EmailIsChecked||search.UsernameIsChecked||search.FirstnameIsChecked||search.LastnameIsChecked;
           double usersCount =  _context.Users.Where(user=>
            !isFiltered||(
                (search.EmailIsChecked&&search.SearchText == user.Email)
            ||(search.UsernameIsChecked&&search.SearchText == user.UserName)
            ||(search.FirstnameIsChecked&&search.SearchText == user.Firstname)
            ||(search.LastnameIsChecked &&search.SearchText == user.Lastname))).Count();         
           double countPages = usersCount/pageSize;
           System.Console.WriteLine("here");
           System.Console.WriteLine(countPages);
            return (int)Math.Ceiling(countPages);
        }
        public async Task<IdentityResult> CreateFlightUser(CreateFlightUser user){
            FlightUser flightUser = new FlightUser();
            flightUser.Id = Guid.NewGuid().ToString();
            flightUser.SecurityStamp = Guid.NewGuid().ToString();
            flightUser.UserName = user.Username;
            flightUser.Firstname = user.Firstname;
            flightUser.Lastname = user.Lastname;
            flightUser.SSN = user.SSN;
            flightUser.Email = user.Email;
            flightUser.PhoneNumber = user.PhoneNumber;
            flightUser.Address = user.Address;
             await _userStore.SetUserNameAsync(flightUser, flightUser.UserName, CancellationToken.None);
                var result = await _userManager.CreateAsync(flightUser, user.Password);
                await _userManager.AddToRoleAsync(flightUser, "User");
                return result;
        }
        public async Task<DisplayFlightUser> GetUserById(string id){
            var flightUser = await _context.Users.FindAsync(id);
            var displayUser = new DisplayFlightUser();
            displayUser.Id = flightUser.Id;
            displayUser.SecurityStamp = displayUser.SecurityStamp;
            displayUser.Username = flightUser.UserName;
            displayUser.Firstname = flightUser.Firstname;
            displayUser.Lastname = flightUser.Lastname;
            displayUser.SSN = flightUser.SSN;
            displayUser.Email = flightUser.Email;
            displayUser.PhoneNumber = flightUser.PhoneNumber;
            displayUser.Address = flightUser.Address;
            return displayUser;
        }
        public async Task UpdateUser(DisplayFlightUser user){
              FlightUser flightUser = _context.Users.FirstOrDefault(x=>x.Id==user.Id);
            flightUser.UserName = user.Username;
            flightUser.Firstname = user.Firstname;
            flightUser.Lastname = user.Lastname;
            flightUser.SSN = user.SSN;
            flightUser.Email = user.Email;
            flightUser.PhoneNumber = user.PhoneNumber;
            flightUser.Address = user.Address;
             await  _context.SaveChangesAsync();
        }
        public async Task DeleteUser(string id){
            FlightUser user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}