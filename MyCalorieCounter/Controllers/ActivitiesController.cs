using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
