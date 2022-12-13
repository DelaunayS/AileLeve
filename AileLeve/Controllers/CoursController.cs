using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Controllers
{
    public class CoursController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListeCours()
        {
            return View();
        }
    }
}