using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WhatToDrink.Models;
using WhatToDrink.Models.AccountViewModels;
using WhatToDrink.Services;
using WhatToDrink.Data;
using WhatToDrink.Models.BeerViewModels;
using Microsoft.EntityFrameworkCore;

namespace WhatToDrink.Controllers
{


    public class BeerController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationDbContext context;

        public BeerController(ApplicationDbContext ctx, UserManager<ApplicationUser> user)
        {
            _userManager = user;
            context = ctx;
        }

        [HttpGet]
        public IActionResult Choose()
        {
            ChooseSeason model = new ChooseSeason(context);
            return View(model);
        }
      
      

        [HttpPost]
        public IActionResult GetBeersBySeason([FromRoute]int id)
        {
            var beersBySeason = context.Beer.OrderBy(s => s.Name.ToUpper()).Where(s => s.SeasonId == id).ToList();
            return Json(beersBySeason);
        }

        public IActionResult Index()
        {
            ChooseSeason model = new ChooseSeason(context);
            model.Beers = context.Beer.OrderBy(s => s.Name.ToUpper());

            return View(model);

        }

        [HttpPost]
        public IActionResult GetBeersByFeelings([FromRoute]int id)
        {
            var beersByFeelings = context.Beer.OrderBy(s => s.Name.ToUpper()).Where(s => s.StyleId == id).ToList();
            return Json(beersByFeelings);
        }


        [HttpPost]
        public IActionResult GetBeersByDay([FromRoute]int id)
        {
            var beersByDay = context.Beer.OrderBy(s => s.Name.ToUpper()).Where(s => s.ABVId == id).ToList();
            return Json(beersByDay);
        }
    }

}