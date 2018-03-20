using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Retail_Bankingteam03.Models;
using System.Data.Entity.Core.Objects;

namespace Retail_Bankingteam03.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Banking
        DB03TMS155_1718Entities db = new DB03TMS155_1718Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCustomer()
        {
            State_List();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomer(CustomerDetails_3 c , FormCollection f)
        {
            //c.State_3 = f["State_Name_3"];
            // c.City_3 = f["City_Name_3"];
            int state_id = Convert.ToInt32(c.State_3);
            int city_id = Convert.ToInt32(c.City_3);
            var temp = db.Select_State_3.Where(a=>a.State_ID_3==state_id).ToList();
            if (temp != null)
            {
                foreach (var l in temp)
                {
                    c.State_3 = l.State_Name_3;
                }
            }
            var temp1 = db.Select_City_3.Where(x => x.City_ID_3 ==city_id ).ToList();
            if (temp1 != null)
            {
                foreach (var l in temp1)
                {
                    c.City_3 = l.City_Name_3;
                }
            }
            if (ModelState.IsValid)
            {
                ObjectParameter op = new ObjectParameter("CustomerID_3", 0);
                db.create_customer_3(c.CustomerSSID_3, c.CustomerName_3, c.Age_3, c.CustomerAddress_3, c.State_3, c.City_3, op);
                db.SaveChanges();
                ModelState.Clear();
                if (Convert.ToInt32(op.Value) == 0)
                {
                    Response.Write("<script>alert('Registration Unsuccessful!!! Try Re-Registering')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Registration Successful!!! The Customer ID is : " + op.Value + "')</script>");
                    State_List();
                    return View();
                }
            }
            State_List();
            return View();
        } 
        public ActionResult SearchUpdate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SearchUpdate(FormCollection f)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    long id = Convert.ToInt64(f["txtcustid"]);
                    if (id == 0)
                    {
                        TempData["AlertMessage"] = 86;
                        return RedirectToAction("SearchUpdate");
                    }
                    else
                    {
                        Session["custid"] = f["txtcustid"];
                        return RedirectToAction("CustomerUpdate");

                    }
                }
                catch (Exception ex)
                {
                    TempData["AlertMessage"] = 86;
                    return RedirectToAction("SearchUpdate");
                }

            }
            return View();
        }
        public ActionResult CustomerUpdate()
        {
            Validation_update b = new Validation_update();
            CustomerDetails_3 o = new CustomerDetails_3();
            try
            {
               
                int i = Convert.ToInt32(Session["custid"]);
                o = db.CustomerDetails_3.Find(i);
                b.CustomerID_3 = o.CustomerID_3;
                b.OldCustomerName_3 = o.CustomerName_3;
                b.OldAge_3 = o.Age_3;
                b.OldCustomerAddress_3 = o.CustomerAddress_3;
                b.Message_3 = o.Message_3;
                b.Status_3 = o.Status_3;
                b.CustomerSSID_3 = o.CustomerSSID_3;
                if (o == null)
                {
                    TempData["AlertMessage"] = 6;
                    return RedirectToAction("SearchUpdate");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Enter valid Account Id')</script>");
            }


            return View(b);
        }
        [HttpPost]
        public ActionResult CustomerUpdate( FormCollection p)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    string str1 = p["txtnewname"];
                    string str2 = p["txtnewaddrss"];
                    if (str1.Length == 0 || str2.Length == 0)
                    {
                        TempData["AlertMessage"] = 66;
                        return RedirectToAction("CustomerUpdate");
                    }
                        int str3 = Convert.ToInt32(p["txtnewage"]);
                        db.UpdateCustomer(Convert.ToInt32(Session["custid"]), str1, str2, str3);
                        ModelState.Clear();
                        db.SaveChanges();
                        Response.Write("<script>alert('Successfully updated')</script>");

                    
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Please enter all the details')</script>");
                }
               
            }
            return View();
        }
        public ActionResult SearchDelete()
        {
            return View();
        }
            
        [HttpPost]
        public ActionResult SearchDelete(FormCollection f)
        {
            try
            {
                long id = Convert.ToInt64(f["txtcustid"]);
                if (id == 0)
                {
                    TempData["AlertMessage"] = 77;
                    return RedirectToAction("SearchDelete");
                }
                else
                {
                    Session["custid"] = f["txtcustid"];
                    return RedirectToAction("CustomerDelete");
                }
            }
            catch(Exception ex)
            {
                TempData["AlertMessage"] = 77;
                return RedirectToAction("SearchDelete");
            }
        }
        public ActionResult CustomerDelete(CustomerDetails_3 c)
        {
            try
            {
                CustomerDetails_3 o = new CustomerDetails_3();
                int i = Convert.ToInt32(Session["custid"]);
                o = db.CustomerDetails_3.Find(i);
                return View(o);
            }
            catch(Exception ex)
            {
                TempData["AlertMessage"] = 99;
                return RedirectToAction("CustomerDelete");
            }
            
        }
        [HttpPost]
        public ActionResult CustomerDelete()
        {
            //if (ModelState.IsValid)
            //{
                db.DeleteCustomer(Convert.ToInt32(Session["custid"]));
                ModelState.Clear();
                db.SaveChanges();
                Response.Write("<script>alert('Deactivated Successfully')</script>");
            //}
            return View();
        }

        public ActionResult CustomerStatus(CustomerDetails_3 c)
        {
            ObjectResult o = db.View_Status_3();
            return View(o);
        }

        public ActionResult View_Cus_Details(int id)
        {
            View_CusDetails_3_Result1 s = new View_CusDetails_3_Result1();
            ObjectResult o = db.View_CusDetails_3(id);
            //foreach (View_CusDetails_3_Result temp in o)
            //View_Customer_name_Result s = new View_Customer_name_Result();
            //ObjectResult o = db.View_Customer_name(id);
            foreach(View_CusDetails_3_Result1 temp in o)
            {
                s.CustomerID_3 = temp.CustomerID_3;
                s.CustomerName_3 = temp.CustomerName_3;
                s.Age_3 = temp.Age_3;
                s.CustomerAddress_3 = temp.CustomerAddress_3;
                s.State_3 = temp.State_3;
                s.City_3 = temp.City_3;
            }
            return View(s);
        }
        [HttpPost]
        public ActionResult View_Cus_Details()
        {
            return RedirectToAction("CustomerStatus");
        }
        public ActionResult SearchCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchCustomer(FormCollection f)
        {
            if (f["CheckMethod"] == "CustomerID")
            {
                try
                {
                    TempData["id"] = f["CustomerID"];
                    CustomerDetails_3 p = db.CustomerDetails_3.Find(Convert.ToInt64(f["CustomerID"]));
                    if (p == null)
                    {
                        TempData["AlertMessage"] = 98;
                        return RedirectToAction("SearchCustomer");
                    }
                    else
                    {
                        return RedirectToAction("SearchCustomer1");
                    }
                }
                catch(Exception ex)
                {
                    TempData["AlertMessage"] = 98;
                    return RedirectToAction("SearchCustomer");
                }
            }
            else if (f["CheckMethod"] == "CustomerSSID")
            {
                try
                {
                    TempData["id"] = f["CustomerSSID"];
                    long id1 = Convert.ToInt64(f["CustomerSSID"]);
                    ObjectParameter d = new ObjectParameter("cusid", 0);
                    long cusid = Convert.ToInt64(db.sp_findcustid(id1,d));
                    if(cusid==0)
                    {
                        TempData["AlertMessage"] = 100;
                        return RedirectToAction("SearchCustomer");
                    }
                    return RedirectToAction("SearchCustomer1");
                }
                catch(Exception ex)
                {

                    TempData["AlertMessage"] = 100;
                    return RedirectToAction("SearchCustomer");
                }
            }
            else
            {
                Response.Write("<script>alert('Select the Account Selection type!!')</script>");
                return View();
            }
            
            return View();

        }
        public ActionResult SearchCustomer1()
        {
            List<CustomerSearch_final_Result> customerlist = new List<CustomerSearch_final_Result>();
            
            customerlist = db.CustomerSearch_final(Convert.ToInt32(TempData["id"])).ToList();
            if (customerlist == null)
            {
                TempData["AlertMessage"] = 109;
                return RedirectToAction("SearchCustomer1");
            }
            else
            {
                return View(customerlist);
            }

        }


        //CODE FOR DDL STATE AND CITY
        public void State_List()
        {

            var state_3 = db.Select_State_3.ToList();
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = "--Select Country--", Value = "0" });

            foreach (var m in state_3)
            {


                lst.Add(new SelectListItem { Text = m.State_Name_3, Value = m.State_ID_3.ToString() });
                ViewBag.state_3 = lst;

            }
            
        }

        public JsonResult getCity(int id)
        {
            var city = db.Select_City_3.Where(x => x.State_ID_3 == id).ToList();
            List<SelectListItem> licity = new List<SelectListItem>();

            licity.Add(new SelectListItem { Text = "--Select City--", Value = "0" });
            if (city != null)
            {
                foreach (var l in city)
                {
                    licity.Add(new SelectListItem { Text = l.City_Name_3, Value = l.City_ID_3.ToString() });
                }
            }
            return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
    }
}
    
