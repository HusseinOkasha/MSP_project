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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult signup()
        {

            return View();
        }
        public ActionResult login() {
            return View();  

        }
        public ActionResult Home() {

            return View();
        }
        public ActionResult insert()
        {

            return View();
        }
        public RedirectToRouteResult signup_submit()
        {
            NameValueCollection nvc = Request.Form;
            string User_name=nvc["user_name"] ;
            string password = nvc["password"]; ;
            string phone = nvc["phone"]; ;
            string location = nvc["address"]; ;
            string mail = nvc["mail"]; ;
            string[] data = new string []{ User_name,mail, password, phone, location} ;
            try
            {
                DataBaseHandler.Insert("users", data);
                return RedirectToAction("Home", "User");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("signup", "User");
            }
        }
        public RedirectToRouteResult login_submit()
        {
            NameValueCollection nvc = Request.Form;
            string mail = nvc["mail"];
            string password = nvc["password"]; 
            string[] data = new string[] { mail, password};
            DataTable table =  DataBaseHandler.Select("users", "where email=" + mail+ " and password=" + password);
            if(table.Rows.Count == 0)
            {
                return RedirectToAction("Home","User");
            }
            else
            {
                return RedirectToAction("Home","User");
            }
        }
        public ActionResult SearchResult(string str = "")
        {
            DataTable phtable = new DataTable();
            if (str != "where ID in ()")
            {
                phtable = DataBaseHandler.Select("pharmacies", str);
            }
            ViewBag.table = phtable;
            return View();
        }
        public RedirectToRouteResult searchaction()
        {
            string value = Request.QueryString["searchvalue"];
            Console.WriteLine(value);
            DataTable table = DataBaseHandler.Select("drug", "where name_drug='" + value + "'");
            string pharmacis = "where ID in (";
            for(int i = 0; i< table.Rows.Count; i++)
            {
                pharmacis += table.Rows[i][3];
                if(i != table.Rows.Count - 1)
                {
                    pharmacis += ",";
                }
            }
            pharmacis += ")";
            return RedirectToAction("SearchResult", "User", new { @str = pharmacis });
        }
        public RedirectToRouteResult insert_submit()
        {

            string value = Request.QueryString["AddValue"];
            
        }
    }
}