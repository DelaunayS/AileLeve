using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace AileLeve.Controllers
{

    [Authorize(Roles = "Admin")]
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

        public IActionResult SupprimerNotif(int id)
        {
            Notification notification = dal.ObtenirNotifications().Where(p => p.Id == id).FirstOrDefault();
            dal.SupprimerNotification(notification);

            return Redirect("/Notification");
        }
    }
}
