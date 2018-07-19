using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TastTest.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace TastTest.Controllers
{
    public class HomeController : Controller
    {
        SoccerContext db = new SoccerContext();
        public ActionResult Index()
        {
            var team = db.Teams.Include(t => t.Players);
            return View(team);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(int id)
        {
            Team team = db.Teams.Include(t => t.Players).FirstOrDefault(p => p.Id == id);
            IEnumerable<PlTeam> plTeam = db.PlTeams.Where(p => p.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            team.SumAmount = plTeam.Count();
            return View(team);
        }
        [HttpGet]
        public async Task<ActionResult> Results(int id, string UserId)
        {
            Team team = db.Teams.Include(t => t.Players).FirstOrDefault(p => p.Id == id);
            await UserManager.SendEmailAsync(UserId, "Мероприятие","Спасибо что зарегестировались на мероприятие: "+ team.Name + ", которое пройдет "+ team.Dates+" в " + team.Times+ "<br> Ждем Вас!! <br><br><br><br> P.S.: не отвечайте на данное сообщение, оно было отправлено с сервера!");
            PlTeam plTeam = new PlTeam();
            plTeam.TeamId = id;
            plTeam.UserId = UserId;
            db.PlTeams.Add(plTeam);
            db.SaveChanges();
            return View();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}