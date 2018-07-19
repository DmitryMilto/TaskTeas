using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TastTest.Models;

namespace TastTest.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        SoccerContext db = new SoccerContext();

        public ActionResult Index()
        {
            var team = db.Teams.Include(t => t.Players);
            return View(team);

        }
        public ActionResult TeamDetails(int? id)
        {

            Team team = db.Teams.Include(t => t.Players).FirstOrDefault(p => p.Id == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return View();
            }
            Team player = db.Teams.Find(id);
            if (player != null)
            {
                return View(player);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(int? id, Team team)
        {
            if (id != null)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult OtherCreate(int? id)
        {
            Player player = new Player();
            player.Team = db.Teams.Include(p => p.Players).FirstOrDefault(p => p.Id == id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }
        [HttpPost]
        public ActionResult OtherCreate(int? id, Player players)
        {
            players.TeamId = id;
            db.Players.Add(players);
            db.SaveChanges();
            return RedirectToAction("TeamDetails/" + id);
        }

        [HttpGet]
        public ActionResult OtherEdit(int? id, int? ModelId)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Player player = db.Players.Find(id);
            if (player != null)
            {
                return View(player);
            }
            return RedirectToAction("TeamDetails/" + ModelId);
        }
        [HttpPost]
        public ActionResult OtherEdit(int? ModelId, Player player)
        {
            player.TeamId = ModelId;
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TeamDetails/" + player.TeamId);
        }

        [HttpGet]
        public ActionResult OtherDelete(int? id)
        {
            var player = db.Players.Include(p => p.Team).FirstOrDefault(p => p.Id == id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }
        [HttpPost]
        public ActionResult OtherDelete(int id)
        {
            Player team = db.Players.Find(id);
            int? TeamId = team.TeamId;
            if (team == null)
            {
                return HttpNotFound();
            }
            db.Players.Remove(team);
            db.SaveChanges();
            return RedirectToAction("TeamDetails/" + TeamId);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var team = db.Teams.Include(p => p.Players).FirstOrDefault(p => p.Id == id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Team team = db.Teams.Find(id);
            IEnumerable<Player> player = db.Players.Include(p => p.Team).Where(p => p.TeamId == id);
            IEnumerable<PlTeam> plTeam = db.PlTeams.Where(p => p.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            foreach (Player play in player)
                db.Players.Remove(play);
            foreach (PlTeam plteam in plTeam)
                db.PlTeams.Remove(plteam);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}