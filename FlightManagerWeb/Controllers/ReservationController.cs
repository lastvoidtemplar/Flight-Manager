using FlightManagerWeb.Data;
using FlightManagerWeb.Models;
using FlightManagerWeb.Models.Reservations;
using FlightManagerWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
namespace FlightManagerWeb.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationService reservationService;
        private readonly EmailService emailService;
        public ReservationController(FlightDbContext context)
        {
            reservationService = new ReservationService(context);
            emailService = new EmailService(context);
        }
        const int pageSize = 10;
        [Authorize(Roles="User")]
        public async Task<IActionResult> Index(IndexReservation model)
        {
            if (model == null)
            {
                model = new IndexReservation();
                model.CurrentPage = 1;
                model.CountOfPages = await reservationService.NumberOfPages(pageSize, model.Email);
                model.Email = "";
            }
            if (model.Email == null) model.Email = "";
            if (model.CurrentPage <= 0)
            {
                model.CurrentPage = 1;
            }
            if (model.CountOfPages == 0) model.CountOfPages = await reservationService.NumberOfPages(pageSize, model.Email);
            model.Reservations = reservationService.IndexAllReservations(model.CurrentPage, pageSize, model.Email);
            return View(model);
        }
        public IActionResult Create(CreateReservation model)
        {
            if (model == null) model = new CreateReservation();
            if (model.Email == null) model.Email = "";
            if (model.AvailableFlightsData == null)
            {
                model.AvailableFlightsData = new AvailableFlightsData();
                model.AvailableFlightsData.CurrentPage = 1;
                model.AvailableFlightsData.CountOfPages = reservationService.AvailableFlightsPageCount(pageSize);
            }
            model.AvailableFlightsData.flights = reservationService.AvailableFlights(model.AvailableFlightsData.CurrentPage, pageSize);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Save(CreateReservation model)
        {
            try
            {
                System.Console.WriteLine("contr try");
                string id = await reservationService.CreateReservation(model.Email, model.FlightId);
                return LocalRedirect($"~/Reservation/Edit/{id}");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return LocalRedirect("~/Flight/Index");
        }
        public async Task<IActionResult> Edit(string id)
        {
            EditResservation model = new EditResservation();
            model.Id = id;
            if (model.Email == null || model.Email == "")
            {
                model.Email = reservationService.GetEmailOFReservationById(model.Id);
                model.FlightInfo = reservationService.GetFlightInfoOFReservationById(model.Id);
                model.PassagerData = new PassagerData();
                model.PassagerData.CurrentPassager = 1;
                model.PassagerData.PassagerCount = reservationService.PassangersReservationCount(pageSize, model.Id);
            }
            model.PassagerData.Passagers = reservationService.PassangersReservation(model.PassagerData.CurrentPassager, pageSize, model.Id);
            return View(model);
        }
        public async Task<IActionResult> SendEmail(string id)
        {
            
                emailService.SendReservationConfirmation(id);
                return LocalRedirect($"~/");
        
            return LocalRedirect("~/Flight/Index");
        }
        public IActionResult CreatePassager(string id)
        {
            Passager model = new Passager();
            model.ReservationId = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePassager(Passager model)
        {
            try
            {
                reservationService.CreatePassager(model);
                return LocalRedirect($"~/Reservation/Edit/{model.ReservationId}");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
                return LocalRedirect($"~/Reservation/Edit/{model.ReservationId}");
        }
        public async Task<IActionResult> ReservationConfirmation(string id)
        {
            return View(id);
        }
        public async Task<IActionResult> Comfirm(string id)
        {
            try
            {
                reservationService.ComfirmReservation(id);
                return LocalRedirect("~/");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return LocalRedirect("~/");
        }
                [Authorize(Roles= "User")]
         public async Task<IActionResult> Details(string id)
        {
            EditResservation model = new EditResservation();
            model.Id = id;
            if (model.Email == null || model.Email == "")
            {
                model.Email = reservationService.GetEmailOFReservationById(model.Id);
                model.FlightInfo = reservationService.GetFlightInfoOFReservationById(model.Id);
                model.PassagerData = new PassagerData();
                model.PassagerData.CurrentPassager = 1;
                model.PassagerData.PassagerCount = reservationService.PassangersReservationCount(pageSize, model.Id);
            }
            model.PassagerData.Passagers = reservationService.PassangersReservation(model.PassagerData.CurrentPassager, pageSize, model.Id);
            return View(model);
        }
    }

}