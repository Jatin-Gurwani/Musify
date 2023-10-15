using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musify_web.Models;

namespace Musify_web.Controllers
{
    public class CustomerController : Controller
    {
        ProjectDBContext db = new ProjectDBContext();
        

        // GET: Customer
        public ActionResult Index()
        {
            if(Session["Login_id"] != null)
            {
                int login_id = Convert.ToInt32(  Session["Login_id"].ToString());
                var name = db.Customer.Where(x => x.Customer_id.Equals(login_id));
                ViewBag.name = name.ToList();
                ViewBag.album = db.album.ToList().Take(5);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

    public ActionResult Contact()
        {
            if (Session["Login_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Contact", "Home");
            }

        }

    public ActionResult About()
        {
            if (Session["Login_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("About", "Home");
            }
        }

        public ActionResult Search()
        {
            if (Session["Login_id"] != null)
            {
                List<vm_songlist> data = (from s in db.Song
                                          join al in db.album on s.Album_id equals al.Album_id
                                          join a in db.Artist on s.Artist_id equals a.Artist_id
                                          join g in db.Genre on s.Genre_id equals g.Genre_id
                                          select new vm_songlist
                                          {
                                              Song_name = s.Song_name,
                                              Artist_name = a.Artist_name,
                                              Album_name = al.Album_name,
                                              Genre_name = g.Genre_name,
                                              Song_link = s.Song_link


                                          }).ToList();
                ViewBag.data = data.ToList();
                ViewBag.notfound = "";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult Search(String Option, String search)
        {

            if (Option == "Song_name")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  

                List<vm_songlist> data = (from s in db.Song
                                          join al in db.album on s.Album_id equals al.Album_id
                                          join a in db.Artist on s.Artist_id equals a.Artist_id
                                          join g in db.Genre on s.Genre_id equals g.Genre_id
                                          where s.Song_name.Contains(search)
                                          select new vm_songlist
                                          {
                                              Song_name = s.Song_name,
                                              Artist_name = a.Artist_name,
                                              Album_name = al.Album_name,
                                              Genre_name = g.Genre_name,
                                              Song_link = s.Song_link,
                                              Album_id = s.Album_id


                                          }).ToList();
                ViewBag.data = data.ToList();
                return View();
            }
            else if (Option == "Album_name")
            {
                List<vm_songlist> data = (from s in db.Song
                                          join al in db.album on s.Album_id equals al.Album_id
                                          join a in db.Artist on s.Artist_id equals a.Artist_id
                                          join g in db.Genre on s.Genre_id equals g.Genre_id
                                          where al.Album_name.Contains(search)
                                          select new vm_songlist
                                          {
                                              Song_name = s.Song_name,
                                              Artist_name = a.Artist_name,
                                              Album_name = al.Album_name,
                                              Genre_name = g.Genre_name,
                                              Song_link = s.Song_link,
                                              Album_id = s.Album_id


                                          }).ToList();
                ViewBag.data = data.ToList();
                return View();
            }
            else if (Option == "Artist_name")
            {
                List<vm_songlist> data = (from s in db.Song
                                          join al in db.album on s.Album_id equals al.Album_id
                                          join a in db.Artist on s.Artist_id equals a.Artist_id
                                          join g in db.Genre on s.Genre_id equals g.Genre_id
                                          where a.Artist_name.Contains(search)
                                          select new vm_songlist
                                          {
                                              Song_name = s.Song_name,
                                              Artist_name = a.Artist_name,
                                              Album_name = al.Album_name,
                                              Genre_name = g.Genre_name,
                                              Song_link = s.Song_link,
                                              Album_id = s.Album_id


                                          }).ToList();
                ViewBag.data = data.ToList();
                return View();
            }
            else
            {
                ViewBag.notfound = " NO Record Found ";
                return View();
            }
        }

        public ActionResult album_list()
        {
            if (Session["Login_id"] != null)
            {
                var album = db.album.ToList();
                return View(album);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult album_list(String Search)
        {
            var album = db.album.Where(x => x.Album_name.Contains(Search)).ToList();
            return View(album);
        }
        public ActionResult album(string album_id)
        {
            if (Session["Login_id"] != null && album_id != null)
            {
                int a_id = Convert.ToInt32(album_id);
                List<vm_songlist> data = (from s in db.Song
                                          join al in db.album on s.Album_id equals al.Album_id
                                          join a in db.Artist on s.Artist_id equals a.Artist_id
                                          join g in db.Genre on s.Genre_id equals g.Genre_id
                                          where s.Album_id.Equals(a_id)
                                          select new vm_songlist
                                          {
                                              Song_name = s.Song_name,
                                              Artist_name = a.Artist_name,
                                              Album_name = al.Album_name,
                                              Genre_name = g.Genre_name,
                                              Song_link = s.Song_link,
                                              Album_img = al.Album_img


                                          }).ToList();
                ViewBag.data = data.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Account()
        {
            if (Session["Login_id"] != null)
            {
                int login_id = Convert.ToInt32(Session["Login_id"].ToString());
                var User = db.Customer.Where(x => x.Customer_id.Equals(login_id)).ToList();
                Customer customer = db.Customer.Find(login_id);
                ViewBag.editflag = 0;
                
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
       

        public ActionResult Logout()
        {
            Session["Login_id"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login", "Home");
        }

    }
}