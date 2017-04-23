using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodForFriends.Entity;

namespace FoodForFriends.Controllers
{
    public class UserMenusController : Controller
    {
        private FoodForFriendsDBEntities db = new FoodForFriendsDBEntities();

        // GET: UserMenus
        [Authorize]
        public ActionResult Index()
        { 
            var userMenus = db.UserMenus.Include(u => u.AspNetUser).Include(u => u.Menu);
            if (User.Identity.Name != "ajanthan@q2soft.lk")
            {
                userMenus= db.UserMenus.Include(u => u.AspNetUser).Include(u => u.Menu).Where(e=>e.AspNetUser.UserName== User.Identity.Name);
            }
            ViewBag.UserEmail = User.Identity.Name;
            return View(userMenus.ToList());
        }

        // GET: UserMenus/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMenu userMenu = db.UserMenus.Find(id);
            if (userMenu == null)
            {
                return HttpNotFound();
            }
            return View(userMenu);
        }

        // GET: UserMenus/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.Identity.Name != "ajanthan@q2soft.lk")
            {
                var userMenus = db.UserMenus.Include(u => u.AspNetUser).Include(u => u.Menu).Where(e => e.AspNetUser.UserName == User.Identity.Name);
                if (userMenus.Count() == 1)
                    RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.MenuID = new SelectList(db.Menus, "ID", "MenuName");
            return View();
        }

        // POST: UserMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,MenuID,UserID")] UserMenu userMenu)
        {
            if (User.Identity.Name != "ajanthan@q2soft.lk")
            {
                var userMenus = db.UserMenus.Include(u => u.AspNetUser).Include(u => u.Menu).Where(e => e.AspNetUser.UserName == User.Identity.Name);
                if (userMenus.Count() == 1)
                    RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.UserMenus.Add(userMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", userMenu.UserID);
            ViewBag.MenuID = new SelectList(db.Menus, "ID", "MenuName", userMenu.MenuID);
            return View(userMenu);
        }

        // GET: UserMenus/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMenu userMenu = db.UserMenus.Find(id);
            if (userMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", userMenu.UserID);
            ViewBag.MenuID = new SelectList(db.Menus, "ID", "MenuName", userMenu.MenuID);
            return View(userMenu);
        }

        // POST: UserMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,MenuID,UserID")] UserMenu userMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", userMenu.UserID);
            ViewBag.MenuID = new SelectList(db.Menus, "ID", "MenuName", userMenu.MenuID);
            return View(userMenu);
        }

        // GET: UserMenus/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (User.Identity.Name != "ajanthan@q2soft.lk")
            {
                var userMenus = db.UserMenus.Include(u => u.AspNetUser).Include(u => u.Menu).Where(e => e.AspNetUser.UserName == User.Identity.Name);
                if (userMenus.Count() == 2)
                    RedirectToAction("Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMenu userMenu = db.UserMenus.Find(id);
            if (userMenu == null)
            {
                return HttpNotFound();
            }
            return View(userMenu);
        }

        // POST: UserMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {      
                  if (User.Identity.Name != "ajanthan@q2soft.lk")
            {
                var userMenus = db.UserMenus.Include(u => u.AspNetUser).Include(u => u.Menu).Where(e => e.AspNetUser.UserName == User.Identity.Name);
                if (userMenus.Count() == 2)
                    RedirectToAction("Index");
            }

            UserMenu userMenu = db.UserMenus.Find(id);
            db.UserMenus.Remove(userMenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
