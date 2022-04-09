using System.Net;
using FlightManagerWeb.Data;
using FlightManagerWeb.Models;
using FlightManagerWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagerWeb.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightService flightSevice;
        public FlightController(FlightDbContext context)
        {
            flightSevice = new FlightService(context);
        }
        const int pageSize = 10;
        [Authorize(Roles= "User")]
        public async Task<IActionResult> Index(IndexFlight model)
        {
            System.Console.WriteLine(model?.CountOfPages);
            if (model == null)
            {
                model = new IndexFlight();
                model.CurrentPage = 1;
                model.CountOfPages = await flightSevice.NumberOfPages(pageSize, model.Search);
                model.Search = "";
            }
            if (model.Search == null) model.Search = "";
            if (model.CurrentPage <= 0)
            {

                model.CurrentPage = 1;
            }
            if (model.CountOfPages == 0) model.CountOfPages = await flightSevice.NumberOfPages(pageSize, model.Search);
            model.Flights = flightSevice.IndexAllFlights(model.CurrentPage, pageSize, model.Search);
            return View(model);
        } 
        [Authorize(Roles= "Admin")]
        public IActionResult Create()
        {
            Flight model = new Flight();
            return View(model);
        }
             
        [Authorize(Roles= "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight model)
        {
                try
                {
                    await flightSevice.CreateFlight(model);
                }
                catch (System.Exception ex)
                {
                     ModelState.AddModelError(string.Empty, ex.Message);
                }
                        return LocalRedirect("~/Flight/Index");
        }
        [Authorize(Roles= "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await flightSevice.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }
        [Authorize(Roles= "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Flight flight)
        {
          
                try
                {
                    await flightSevice.UpdateFlight(flight);
                }
                catch (System.Exception ex)
                {
                    return NotFound();
                }
            return LocalRedirect("~/Flight/Index");
        }
                
        [Authorize(Roles= "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await flightSevice.DeleteFlight(id);
                }
                catch (System.Exception ex)
                {
                    return NotFound(ex.Message);
                }


            }

            return LocalRedirect("~/Flight/Index");
        }
    }
}