using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musify_web.Models;
using System.IO;
using System.Configuration;

namespace Musify_web.Controllers
{
    public class HomeController : Controller
    {
        ProjectDBContext db = new ProjectDBContext();
        public ActionResult Index()
        {
            ViewBag.album = db.album.ToList().Take(5);
            return View(ViewBag.album);
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

        public ActionResult Register()
        {
            var result = db.Customer.ToList();
            ViewBag.Msg = "";
            return View();

        }

        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            try
            {
                cust.Account_DOC = DateTime.Now.ToString();
                cust.Role = "Cust";
                if (ModelState.IsValid)
                {
                    db.Customer.Add(cust);
                    db.SaveChanges();

                    var custList = db.Artist.ToList();
                    ViewBag.Register = custList;
                    ViewBag.Msg = "Registered Successfully . You can now Login from ";
                }
            }
            catch (Exception ex)
            {
                string s = ex.InnerException.ToString();
                ViewBag.Msg = s;
            }
            return View(cust);

        }

        public ActionResult Login()
        {
            var login = db.Customer.ToList();
            
            ViewBag.msg = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login( Customer check)
        {

            try
            {
                
                
                    var cust = db.Customer.ToList();
                    String password = check.Account_Password.ToString();
                    String number = check.Customer_Mobile_number.ToString();
                    foreach (var item in cust)
                    {

                        if (item.Customer_Mobile_number.ToString().Equals(number))
                        {
                            if (item.Account_Password.ToString().Equals(password))
                            {
                                if (item.Role.ToString().Equals("Admin"))
                                {
                                    Session["Login_id"] = item.Customer_id.ToString();
                                    Session["Role"] = item.Role.ToString();
                                    return RedirectToAction("Index", "Admin");
                                }
                                else
                                {
                                    Session["Login_id"] = item.Customer_id.ToString();
                                    Session["Role"] = item.Role.ToString();
                                    return RedirectToAction("Index", "Customer");
                                }
                            }
                            else
                            {
                                ViewBag.msg = "incorrect password ";
                                return View();
                            }
                        }

                    }
                    ViewBag.msg = " You seem to be new user please Sign Up ";
                    return View();



                
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                ViewBag.Msg = s;

            }
            return View();
        }

        public ActionResult noaccess403()
        {
            return View();
        }
        


       

        
        
       
        
        

       
        

        
        
    }
        

       


        

    }
