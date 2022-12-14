using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AileLeve.Controllers
{
    public class NotificationController : Controller
    {
        private Dal dal = new Dal();
        public IActionResult Index()
        {
            NotificationViewModel nvm = new NotificationViewModel
            {
                NotificationsListe = dal.ObtenirNotifications()
            };

            return View(nvm);
        }

    }
}
