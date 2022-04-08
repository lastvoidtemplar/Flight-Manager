using System;
using FlightManagerWeb.Data;
using FlightManagerWeb.Models;
using FlightManagerWeb.Models.Users;
using FlightManagerWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagerWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly FlightUserSevice userSevice;
        public UsersController(FlightDbContext context, UserManager<FlightUser> userManager, IUserStore<FlightUser> userStore)
        {
            userSevice = new FlightUserSevice(context, userManager, userStore);
        }
        const int pageSize = 10;
        public async Task<IActionResult> Index(IndexViewUser model){
            System.Console.WriteLine(model?.CountOfPages);
            if(model==null)
            {
                model = new IndexViewUser();
                 model.CurrentPage = 1;
                model.CountOfPages = await userSevice.NumberOfPages(pageSize,model.Search);
                model.Search = new Search();
                
            }
            if(model.Search==null)
            {
                 model.Search = new Search();
            }
            if(model.CurrentPage <=0){
                
                 model.CurrentPage = 1;
            }
            if(model.CountOfPages==0)model.CountOfPages = await userSevice.NumberOfPages(pageSize,model.Search);
            model.Users = userSevice.IndexAllUsers(model.CurrentPage, pageSize,model.Search);
            return View(model);
        }
        public  IActionResult Create()
        {
            CreateFlightUser model = new CreateFlightUser();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFlightUser model)
        {
            if (ModelState.IsValid)
            {
                var result = await userSevice.CreateFlightUser(model);

                if (result.Succeeded)
                {
                    return LocalRedirect("~/Users/Index");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
         public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userSevice.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
                [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DisplayFlightUser editModel)
        {
            
                try
                {
                     await userSevice.UpdateUser(editModel);
                }
                catch (System.Exception ex)
                {
                     return NotFound();
                }

            return LocalRedirect("~/Users/Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     await userSevice.DeleteUser(id);
                }
                catch (System.Exception ex)
                {
                     return NotFound();
                }
               
  
            }

            return LocalRedirect("~/Users/Index");
        }
    }
}