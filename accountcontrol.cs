using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Retail_Bankingteam03.Models;
using System.Data.Entity.Core.Objects;

namespace Retail_Bankingteam03.Controllers
{

    public class AccountController : Controller
    {
        // GET: Account
        DB03TMS155_1718Entities db = new DB03TMS155_1718Entities();



        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateAccount()
        {

            return View();

        }
        [HttpPost]
        public ActionResult CreateAccount(AccountDetails_3 acc)
        {
            try {
                if (ModelState.IsValid)
                {

             

                    ObjectParameter d = new ObjectParameter("id", 0);
                    db.accountcheckteam3(acc.CustomerID_3, d);
                    if (Convert.ToInt64(d.Value) == 0)
                    {
                        Response.Write("<script>alert('invalid customer_ID')</script>");
                        return View();
                    }
                    else
                    {

                        ObjectParameter s = new ObjectParameter("accountid", 0);
                        db.createaccountteam3(acc.CustomerID_3, acc.AccountType_3, acc.Balance_3, s);

                        long accid = Convert.ToInt64(s.Value);
                        // string bal = Convert.ToString(acc.AccountType_3);

                        if (accid == 0)
                        {


                            Response.Write("<script>alert(' user already have an account" + acc.AccountType_3 + "')</script>");


                        }
                        else
                        {

                            Response.Write("<script>alert('Successfully created a account with account_ID=" + accid + "')</script>");
                        }
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                }



            }
            catch(Exception e)
            {
                Response.Write("<script>alert('Please enter Custromer_ID')</script>");
            }
            return View("CreateAccount");
        }
        public ActionResult AccountDelete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AccountDelete(AccountDetails_3 acc)
        {
            //try {

                ObjectParameter d = new ObjectParameter("id", 0);
                db.accountcheckdeleteteam3(Convert.ToInt64(acc.AccountID_3), d);
                //long acid = db.AccountDetails_3.Find(Convert.ToInt64(AccountID_3));
                if (Convert.ToInt64(d.Value) > 0)
                {
                    ObjectParameter t = new ObjectParameter("type", 0);
                    db.ReturnType(t, Convert.ToInt64(acc.AccountID_3));
                    string A = Convert.ToString(t.Value);

                    //ObjectParameter t = new ObjectParameter("type", 0);
                    //db.ReturnType(t, Convert.ToInt64(acc.AccountID_3));
                    //string A = Convert.ToString(t.Value);
                    if (Convert.ToString(acc.AccountType_3).Equals(A))
                    {
                        //    ObjectParameter sat = new ObjectParameter("status", 0);
                        //    string sats = Convert.ToString(sat.Value);


                        //    if (sats == "INACTIVE")
                        //    {


                        //        Response.Write("<script>alert('account has been  already deleted ')</script>");
                        //    }
                        ObjectParameter s = new ObjectParameter("balance", 0);
                        ObjectParameter sat = new ObjectParameter("status", 0);
                        string sats = Convert.ToString(sat.Value);
                        db.DeleteAccountteam3(Convert.ToInt64(acc.AccountID_3), Convert.ToString(acc.AccountType_3), s, sat);
                        long bal = Convert.ToInt64(s.Value);

                        if (sats == "INACTIVE")
                        {


                            Response.Write("<script>alert('account has been  already deleted ')</script>");
                        }
                        else
                        {
                            if (bal > 0)


                            {

                                TempData["AlertMessage"] = 52;

                                return RedirectToAction("searchwithdraw", "Transaction");
                            }




                            else
                            {
                                //db.DeleteAccountteam3(Convert.ToInt64(acc.AccountID_3), Convert.ToString(acc.AccountType_3), s, sat);

                                Response.Write("<script>alert('account has been deleted successfully')</script>");

                            }

                        }
                    }




                    else
                    {
                        Response.Write("<script>alert('invalid Account_type')</script>");

                    }

                    }


                    //else
                    //{
                    //        Response.Write("<script>alert('invalid Account_ID')</script>");
                    //    }


                
                else
                {
                    Response.Write("<script>alert('invalid Account_ID')</script>");
                }



         //   }
            //catch (Exception e)
            //{
            //   // if (Convert.ToInt64(d.Value) > 0)
            //        Response.Write("<script>alert(' Please  enterAccount_ID and  AccountType')</script>");
            //    //  Response.Write("<script>alert(' Please enter Account_ID')</script>");
            //}



         
            return View();
        }           
            
        
        public ActionResult SearchAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchAccount(FormCollection f)
        {

            if (f["CheckMethod"] == "AccountID")
            {
                TempData["id"] = f["AccountID"];
                return RedirectToAction("SearchAccount1");
            }
            else if (f["CheckMethod"] == "CustomerID")
            {
                TempData["id"] = f["CustomerID"];
                return RedirectToAction("SearchAccount1");
            }
            else
            {
                Response.Write("<script>alert('Select the Account Selection type!!')</script>");
                return View();
            }

            return View();


        }



                //if (a.CustomerID_3 == 0 || a.CustomerID_3 == null)
                //{
                //    TempData["id"] = a.AccountID_3;
                //}
                //else if (a.AccountID_3 == 0 || a.AccountID_3 == null)
                //{
                //    TempData["id"] = a.CustomerID_3;
                //}
                //else if (a.CustomerID_3 == 0 && a.AccountID_3 == 0)
                //{
                //    Response.Write("<script>alert('Enter either Customer Id or Account ID')</script>");
                //}
                //return RedirectToAction("SearchAccount1");
            

        public ActionResult SearchAccount1(AccountDetails_3 a)
        {
            List<SearchAccount_fnl_Result> acclist = new List<SearchAccount_fnl_Result>();
            acclist = db.SearchAccount_fnl(Convert.ToInt32(TempData["id"])).ToList();
            return View(acclist);
        }
        
        public ActionResult AccountStatement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AccountStatement(AccountDetails_3 acc,FormCollection f)
        {
            try {
                long id = Convert.ToInt64(Session["Account_ID"]);
                AccountDetails_3 abc = db.AccountDetails_3.Find(id);
                //if (abc == null)
                //{
                //    TempData["AlertMessage"] = 145;
                //    return RedirectToAction("AccountStatement");
                //}
                //else
                //{

                Session["AccountID"] = f["AccountID"];
                Session["TransactionNumber"] = f["TransactionNumber"];
                Session["StartDate"] = f["StartDate"];
                Session["EndDate"] = f["EndDate"];
                ObjectParameter d = new ObjectParameter("id", 0);
                db.SP_Accountidcheckintrans_3(Convert.ToInt64(Session["AccountID"]), d);
                if (Convert.ToInt64(d.Value) == 0)
                {
                    Response.Write("<script>alert('invalid Account_ID')</script>");
                    return View();
                }

                if (f["CheckMethod"] == "number")
                {
                    return RedirectToAction("TransactionByNumber");
                }
                else if (f["CheckMethod"] == "date")
                {
                    return RedirectToAction("Viewtransbydate");
                }
                else
                {
                    Response.Write("<script>alert('Select the Account Statement type!!')</script>");
                    //return View();
                }
            }
            catch(Exception e)
            {
                Response.Write("<script>alert('Enter Account_ID')</script>");
            }
            //}
            return View();
        }

        public ActionResult TransactionByNumber(TransactionDetails_3 t)
        {
            int a = Convert.ToInt32(Session["AccountID"]);
            int n = Convert.ToInt32(Session["TransactionNumber"]);
            return View(db.transaction_by_number(a, n).ToList());
        }
        public ActionResult Viewtransbydate()
        {
           List<sp_viewtransbydate_3_Result> vas = new List<sp_viewtransbydate_3_Result>();
            long ID = Convert.ToInt64(Session["AccountID"]);
            DateTime start = Convert.ToDateTime(Session["StartDate"]);
            DateTime end = Convert.ToDateTime(Session["EndDate"]);
           vas= db.sp_viewtransbydate_3(ID, start, end).ToList();
            return View(vas);
        }

    }


}


































