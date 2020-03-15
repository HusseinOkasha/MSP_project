
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using WebApplication1.Models;
using System.Data;


namespace WebApplication1.Controllers
{
    public class PharmacyController : Controller
    {
        // GET: Pharmacy
        public ActionResult Home()
        {
            return View();
        }
        public RedirectToRouteResult Add_Drug() {
            string Name = Request.QueryString["Drug_Name"];
            string Price = Request.QueryString["Drug_Price"];
            int Quantity = Request.QueryString["Drug_quantity"];
            string [] data = { Name, Price, Quantity };
            DataBaseHandler.Insert( "Pharmacy" , data);
            return RedirectToAction("Home", "Pharmacy");



        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult signup()
        {
            return View();
        }
        public RedirectToRouteResult signup_submit() {

            NameValueCollection nvc = Request.Form;
            string name = nvc["phar_name"];
            string password = nvc["password"]; ;
            string phone = nvc["phone"]; ;
            string location = nvc["address"]; ;
            string mail = nvc["mail"]; ;
            string[] data = new string[] { name, mail, password, phone, location };
            try
            {
                DataBaseHandler.Insert("pharmacies", data);
                return RedirectToAction("Home", "Pharmacy");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("signup", "Pharmacy");
            }
        }
        public RedirectToRouteResult login_submit () {

            NameValueCollection nvc = Request.Form;
            string mail = nvc["mail"];
            string password = nvc["password"];
            string[] data = new string[] { mail, password };
            DataTable table = DataBaseHandler.Select("Pharmacy", "where email=" + mail + " and password=" + password);
            if (table.Rows.Count == 0)
            {
                return RedirectToAction("login", "Pharmacy");
            }
            else
            {
                return RedirectToAction("Home", "Pharmacy");
            }

        }

    }
}