using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musify_web.Models;
using System.IO;
using System.Net;
using System.Data.Entity;
using System.Data;

namespace Musify_web.Controllers
{
    public class AdminController : Controller
    {
        ProjectDBContext db = new ProjectDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            if( Session["Role"] != null )
                {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    var user = db.Customer.ToList();
                    return View(user);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }

            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }

            
        }
        [HttpPost]
        public ActionResult Index(string Search)
        {
            var result = db.Customer.Where(x => x.Customer_Mobile_number.Contains(Search));
            return View(result.ToList());
        }

        public ActionResult Artist_display()
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    var result = db.Artist.ToList();
                    return View(result);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }
            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        }
        [HttpPost]
        public ActionResult Artist_display(string Search)
        {
            var result = db.Artist.Where(x => x.Artist_name.Contains(Search)).ToList();
            return View(result);
        }

        public ActionResult Artist_Create()
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    var result = db.Artist.ToList();

                    ViewBag.msg = "";
                    return View();
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }
            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        

        }

        [HttpPost]
        public ActionResult Artist_Create(Artist Artist)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Artist.Add(Artist);
                    db.SaveChanges();

                    var ArtistList = db.Artist.ToList();
                    ViewBag.Artist = ArtistList;
                   
                    return RedirectToAction("Song_create");
                }
                else
                {
                    ViewBag.Msg = "error in Creating Artist";
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                ViewBag.Msg = s;
            }
            return View(Artist);
        }
        public ActionResult Artist_delete(int? id)
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Artist artist = db.Artist.Find(id);
                    if (artist == null)
                    {
                        return RedirectToAction("noaccess403", "home");
                    }
                    return View(artist);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }

            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        }

        [HttpPost, ActionName("Artist_delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Deleteartist(int id)
        {
            Artist artist = db.Artist.Find(id);
            db.Artist.Remove(artist);
            db.SaveChanges();
            return RedirectToAction("Artist_display");
        }
        public ActionResult Artist_edit(int? id)
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    ViewBag.alert = "";
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Artist artist = db.Artist.Find(id);
                    if (artist == null)
                    {
                        return RedirectToAction("noaccess403", "home");
                    }
                    return View(artist);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }

            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Artist_edit([Bind(Include = "Artist_id,Artist_name")] Artist artist)
        {
            try
            {


                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.alert = "Edit done";
                return RedirectToAction("Artist_display", "Admin");


            }
            catch (Exception ex)
            {
                string s = Convert.ToString(ex);
                ViewBag.alert = s;
                return View(artist);
            }

        }


        [HttpGet]
        public ActionResult Album_create()
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    var albumlist = db.album.ToList();
                    return View();
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }
            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }

        }
        [HttpPost]
        public ActionResult Album_create(album album)
        {



            try
            {
                if (ModelState.IsValid)
                {

                    string FileExtension = Path.GetExtension(album.ImageFile.FileName);
                    string Filename = album.Album_id.ToString() + "-" + album.Album_name.ToString() + "-" + FileExtension;
                    album.Album_img = "../Image/" + Filename;
                    Filename = Path.Combine(Server.MapPath("../Image/"), Filename);
                    album.ImageFile.SaveAs(Filename);


                    db.album.Add(album);
                    db.SaveChanges();

                    var albumList = db.album.ToList();
                    ViewBag.albumlist = albumList;
                    
                    return RedirectToAction("Song_create");
                }
                else
                {
                    ViewBag.msg = "Error Creating Album";
                }
            }
            catch (Exception ex)
            {
                string s = ex.InnerException.ToString();
                ViewBag.Msg = s;
            }
            return View(album);

        }
        public ActionResult Album_display()
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    var albumlist = db.album.ToList();
                    return View(albumlist);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }
            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }

        }
        [HttpPost]
        public ActionResult Album_display(string Search)
        {
            var result = db.album.Where(x => x.Album_name.Contains(Search)).ToList();
            return View(result);
        }

        public ActionResult album_delete(int? id)
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    album album = db.album.Find(id);
                    if (album == null)
                    {
                        return RedirectToAction("noaccess403", "home");
                    }
                    return View(album);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }

            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        }

        [HttpPost, ActionName("album_delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletealbum(int id)
        {
            album album = db.album.Find(id);
            db.album.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Album_display");
        }


        [HttpGet]
        public ActionResult Song_create()
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    var Songlist = db.Song.ToList();
                    ViewBag.albumList = db.album.ToList();
                    ViewBag.artistList = db.Artist.ToList();
                    ViewBag.genreList = db.Genre.ToList();
                    return View();
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }
            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        }

        [HttpPost]
        public ActionResult Song_create(Song sing)
        {
            try
            {



                ViewBag.albumList = db.album.ToList();
                ViewBag.artistList = db.Artist.ToList();
                ViewBag.genreList = db.Genre.ToList();
                if (ModelState.IsValid)
                {
                    string FileExtension = Path.GetExtension(sing.Song_file.FileName);
                    string Filename = sing.Song_id.ToString() + "-" + sing.Song_name.ToString() + "-" + FileExtension;
                    sing.Song_link = "~/Music/" + Filename;
                    Filename = Path.Combine(Server.MapPath("~/Music/"), Filename);
                    sing.Song_file.SaveAs(Filename);


                    db.Song.Add(sing);
                    db.SaveChanges();

                    var songList = db.Song.ToList();
                    ViewBag.songlist = songList;
                    return RedirectToAction("Song_display");
                }
                else
                {
                    ViewBag.Msg = "error Creating Music";
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                ViewBag.Msg = s;
            }
            return View("Song_create", sing);

            
        }

        public ActionResult Song_display()
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
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
                                                  Song_link = s.Song_link,
                                                  Album_img = al.Album_img,
                                                  Song_id = s.Song_id

                                              }).ToList();




                    ViewBag.data = data.ToList();


                    var songlist = db.Song.ToList();
                    return View();
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }
            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        }
        [HttpPost]
        public ActionResult Song_display(String Option, String search)
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
                                              Album_img = al.Album_img,
                                              Song_id = s.Song_id


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
                                              Album_img = al.Album_img,
                                              Song_id = s.Song_id


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
                                              Album_img = al.Album_img,
                                              Song_id = s.Song_id


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
        public ActionResult Song_delete(int? id)
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Song Song = db.Song.Find(id);
                    if (Song == null)
                    {
                        return RedirectToAction("noaccess403", "home");
                    }
                    return View(Song);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }

            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        }

        [HttpPost, ActionName("Song_delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Song(int id)
        {
            Song Song = db.Song.Find(id);
            db.Song.Remove(Song);
            db.SaveChanges();
            return RedirectToAction("Song_display");
        }

        public ActionResult User_edit(int? id)
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    ViewBag.alert = "";
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Customer customer = db.Customer.Find(id);
                    if (customer == null)
                    {
                        return RedirectToAction("noaccess403", "home");
                    }
                    return View(customer);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }

            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_edit([Bind(Include = "Customer_id,Customer_name,Customer_Email,Customer_Mobile_number,Customer_DOB,Account_DOC,Account_Password,Role,Reenter_password")] Customer customer)
        {
            try
            {
                
                
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.alert = "Edit done";
                    return RedirectToAction("Index", "Admin");
                

            }
            catch (Exception ex)
            {
                string s = Convert.ToString(ex);
                ViewBag.alert = s;
                return View(customer);
            }
           
        }

        public ActionResult User_delete(int? id)
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();
                if (role.Equals("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Customer Customer = db.Customer.Find(id);
                    if (Customer == null)
                    {
                        return RedirectToAction("noaccess403", "home");
                    }
                    return View(Customer);
                }
                else
                {
                    return RedirectToAction("noaccess403", "home");
                }

            }
            else
            {
                return RedirectToAction("noaccess403", "home");
            }
        }

        [HttpPost, ActionName("User_delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer Customer = db.Customer.Find(id);
            db.Customer.Remove(Customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session["Login_id"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}