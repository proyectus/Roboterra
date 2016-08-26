using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Invoicing.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace Invoicing.Controllers
{
    public class OrderController : ZohoInvoiceController
    {
        private MyDbContext db = new MyDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult Confirmation()
        {
            ViewBag.x = "INV-01-MC";
            return View();
        }


        // GET: Orders/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            //TracingJ("Estuve por aqui");
            Order model = new Order();
            model.FirstName = "";
            model.LastName= "";
            model.OrganizationType = "Other";
            model.SubTotal = 300;
            model.ShippingCharges = 0;
            model.Tax = 0;
            model.Total = 300;
            model.OrderDetails = new List<OrderDetail>();
            model.OrderDetails.Add(new OrderDetail { Item = 1, ZohoIdProduct = "563816000000069021", Name = "ROBOTERRA Origin Kit for Schools – 1 kit with 4 licenses", Qty = 1, Price = 300, Total = 300 });
            model.OrganizationType = "-1";
            model.PaymentMethod = "P";
            model.ShippingCharges = Convert.ToDecimal(db.Parameters.Find(1).Value);

            ViewBag.OrganizationTypeDDDW = new SelectList(
            new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "- Please select a Type -", Value = "-1" },
                    new SelectListItem { Selected = false, Text = "School District", Value = "School District" },
                    new SelectListItem { Selected = false, Text = "Public School", Value = "Public School" },
                    new SelectListItem { Selected = false, Text = "Charter School", Value = "Charter School" },
                    new SelectListItem { Selected = false, Text = "Private School", Value = "Private School" },
                    new SelectListItem { Selected = false, Text = "Other", Value = "Other" },
                }, "Value" , "Text", "-1");

            ViewBag.OrganizationNameDDDW = new SelectList( 
            new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "- Please select an Organization -", Value = "-1" }
                }, "Value" , "Text", "-1");


            ViewBag.StateDDDW = new SelectList(
            new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "- Please select a State -", Value = "-1" }
                }, "Value", "Text", "-1");


            ViewBag.CityDDDW = new SelectList(
            new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "- Please select a City -", Value = "-1" }
                }, "Value", "Text", "-1");
            
            return View(model);
        }

        [HttpGet]
        public JsonResult _GetStates(string type)
        {
            List<string> stateDistinct = new List<string>();
            if (type == "Other") { 
                stateDistinct = (from x in db.Schools select x.LocationState).Distinct().ToList();
            }
            else
            {
                stateDistinct = (from x in db.Schools where x.Type == type select x.LocationState).Distinct().ToList();
            }
            stateDistinct = stateDistinct.OrderBy(x => x).ToList();
            List<string> optionList = (from sd in stateDistinct select "<option value='" + sd + "'>" + sd + "</option>").ToList();
            optionList.Insert(0, "<option value='-1'>- Please select a State -</option>");

            string combindedString = string.Join(" ", optionList.ToArray());
            return Json(combindedString, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult _GetCities(string type, string state)
        {
            List<string> cityDistinct = new List<string>();
            if (type == "Other")
            {
                cityDistinct = (from x in db.Schools where x.LocationState == state select x.LocationCity).Distinct().ToList();
            }
            else
            {
                cityDistinct = (from x in db.Schools where x.Type == type && x.LocationState == state select x.LocationCity).Distinct().ToList();
            }
            cityDistinct = cityDistinct.OrderBy(x => x).ToList();
            List<string> optionList = (from sd in cityDistinct select "<option value='" + sd + "'>" + sd + "</option>").ToList();
            optionList.Insert(0, "<option value='-1'>- Please select a City -</option>");

            string combindedString = string.Join(" ", optionList.ToArray());
            return Json(combindedString, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult _GetSchools(string type, string state, string city)
        {

            var schoolDistinct = db.Schools.Where(x => x.Type == type && x.LocationState == state && x.LocationCity == city).Select(x => new { x.ID, x.SchoolName, x.DistrictName }).Distinct();
            var schoolOnlyName = schoolDistinct.Select(x => new { x.SchoolName }).Distinct();
            schoolDistinct = schoolDistinct.OrderBy(x => x.SchoolName).ThenBy(x => x.DistrictName);
            List<string> optionList = new List<string>();
            if (schoolDistinct.Count() != schoolOnlyName.Count()) { 
                optionList = (from sd in schoolDistinct select "<option value='" + sd.ID + "'>" + sd.SchoolName + " (" + sd.DistrictName + ")" + "</option>").ToList();
            }
            else
            {
                optionList = (from sd in schoolDistinct select "<option value='" + sd.ID + "'>" + sd.SchoolName + "</option>").ToList();
            }
            optionList.Insert(0, "<option value='-1'>- Please select a School -</option>");
            string combindedString = string.Join(" ", optionList.ToArray());
            return Json(combindedString, JsonRequestBehavior.AllowGet);            
        }

        [HttpGet]
        public JsonResult _GetSchoolInfo(Int32 ID)
        {
            School school = db.Schools.Find(ID);
            return Json(school, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult _GetSchoolAddress(string type, string state, string city)
        {

            List<string> schoolDistinct = (from x in db.Schools where x.Type == type && x.LocationState == state && x.LocationCity == city select x.SchoolName).Distinct().ToList();
            schoolDistinct = schoolDistinct.OrderBy(x => x).ToList();
            List<string> optionList = (from sd in schoolDistinct select "<option value='" + sd + "'>" + sd + "</option>").ToList();
            optionList.Insert(0, "<option value='-1'>- Please select a School -</option>");
            string combindedString = string.Join(" ", optionList.ToArray());

            return Json(combindedString, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult _GetShippingRate(int quantity)
        {
            decimal shipping5Rate = Convert.ToDecimal(db.Parameters.Find(1).Value);
            decimal shippingplusRate = Convert.ToDecimal(db.Parameters.Find(2).Value);

            decimal shipping5 = 0;
            decimal shippingplus = 0;

            if (quantity > 5) {
                shipping5 = Math.Round(5 * shipping5Rate, 2);
                shippingplus = Math.Round((quantity - 5) * shippingplusRate, 2); 
            }
            else
            {
                shipping5 = Math.Round(quantity * shipping5Rate, 2);
                shippingplus = 0;
            }

            decimal shippingTotal = shipping5 + shippingplus;
            return Json(shippingTotal, JsonRequestBehavior.AllowGet);            
        }


        [HttpGet]
        public JsonResult _GetTax(string state, decimal amount)
        {
            decimal taxPercent = 0;
            if (state == "CA") {                 
                string taxID = db.Parameters.Find(3).Value;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                JsonResult resp = Get_ZITax(taxID);
                Result result = serializer.Deserialize<Result>(serializer.Serialize(resp.Data));
                JObject responseText = JObject.Parse(result.responseText);
                if (result.success == false)
                {
                    return resp;
                }

                List<ZI_TaxesList> taxList = new List<ZI_TaxesList>();
                taxPercent = Convert.ToDecimal(responseText["tax"]["tax_percentage"]);
            }
            return Json(new { taxPercent = taxPercent, tax = Math.Round( taxPercent * amount / 100, 2) } , JsonRequestBehavior.AllowGet);            
        }

        [HttpGet]
        public ActionResult CreateVM()
        {
            Order model = new Order();
            model.FirstName = "";
            model.SubTotal = 300;
            model.ShippingCharges = 0;
            model.Tax = 0;
            model.Total = 300;
            model.OrderDetails = new List<OrderDetail>();
            model.OrderDetails.Add(new OrderDetail { Item = 1, ZohoIdProduct = "563816000000069021", Name = "ROBOTERRA Origin Kit for Schools – 1 kit with 4 licenses", Qty = 1, Price = 300, Total = 300 });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public class Result
        {
            public bool success;
            public string responseText;
        }

        // POST: Orders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "ID,FirstName,LastName,Email,Phone,Mobile,ShippingAddress,ShippingCountry,ShippingCity,ShippingState,ShippingZip,FD_FirstName,FD_LastName,FD_Email,FD_Phone,FD_Mobile,FD_ShippingAddress,FD_ShippingCountry,FD_ShippingCity,FD_ShippingState,FD_ShippingZip")]
        public ActionResult Create(Order order, List<OrderDetail> orderDetails)
        {

            // Save Order
            order.OrderDetails = orderDetails;
            db.Orders.Add(order);
            db.SaveChanges();


            // Saving Contact in Zoho
            ZI_Contact zi_contact = PrepareContact(order);
            JsonResult resp = Save_ZIContact(zi_contact);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Result result = serializer.Deserialize<Result>(serializer.Serialize(resp.Data));
            JObject responseText = JObject.Parse(result.responseText);
            if (result.success == false)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
                
            // Saving Invoice in Zoho
            string contactId = Convert.ToString(responseText["contact"]["contact_id"]);
            List<ZI_InvoiceLineItem> zi_lineItems = PrepareLineItem(orderDetails, order.ShippingState);
            ZI_Invoice zi_invoice = PrepareInvoice(order, contactId);
            zi_invoice.line_items = zi_lineItems;
            JsonResult resp2 = Save_ZIInvoice(zi_invoice);
            result = serializer.Deserialize<Result>(serializer.Serialize(resp2.Data));
            responseText = JObject.Parse(result.responseText);
            if (result.success == false)
            {
                return Json(resp2, JsonRequestBehavior.AllowGet);
            }

            // Useful variables
            string invoiceId = Convert.ToString(responseText["invoice"]["invoice_id"]);
            string invoiceNumber = Convert.ToString(responseText["invoice"]["invoice_number"]);
            string invoiceDate = Convert.ToString(responseText["invoice"]["date"]);
            string invoiceEmail = order.Email;
            string invoiceAmount = Convert.ToString(responseText["invoice"]["total"]); 

            // Emailing Invoice
            ZI_InvoiceEmailInfo zi_email = PrepareEmailInfo(order.Email, order.FD_Email);
            JsonResult resp3 = Email_ZIInvoice(invoiceId, zi_email);
            result = serializer.Deserialize<Result>(serializer.Serialize(resp3.Data));
            responseText = JObject.Parse(result.responseText);
            if (result.success == false)
            {
                return Json(resp3, JsonRequestBehavior.AllowGet);
            }

            // Linking the Invoice ID created
            order.ZohoInvoiceID = invoiceId;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();


            // Getting ID for payment
            if (order.PaymentMethod == "C") { 
                JsonResult resp4 = Get_ZIInvoice(invoiceId);
                result = serializer.Deserialize<Result>(serializer.Serialize(resp4.Data));
                responseText = JObject.Parse(result.responseText);
                if (result.success == false)
                {
                    return Json(resp4, JsonRequestBehavior.AllowGet);
                }

                return Redirect(Convert.ToString(responseText["invoice"]["invoice_url"]));
            }
            else { 
                //return RedirectToAction("Index");
                ViewBag.InvoiceNumber = invoiceNumber;
                return View("Confirmation");
            }
            
        }

        public ActionResult GetTaxList()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            JsonResult resp = Get_ZITaxesList();
            Result result = serializer.Deserialize<Result>(serializer.Serialize(resp.Data));
            JObject responseText = JObject.Parse(result.responseText);
            if (result.success == false)
            {
                return resp;
            }

            List<ZI_TaxesList> taxList = new List<ZI_TaxesList>();
            foreach (var rec in responseText["taxes"])
            {
                taxList.Add( 
                        new ZI_TaxesList {
                        tax_id = Convert.ToString(rec["tax_id"]),
                        tax_name = Convert.ToString(rec["tax_name"]),
                        tax_percentage = Convert.ToDouble(rec["tax_percentage"]),
                        tax_type = Convert.ToString(rec["tax_type"])
                        }                    
                );
            }

            return Json(taxList, JsonRequestBehavior.AllowGet);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // POST: Orders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,Phone,Mobile,ShippingAddress,ShippingCountry,ShippingCity,ShippingState,ShippingZip,FD_FirstName,FD_LastName,FD_Email,FD_Phone,FD_Mobile,FD_ShippingAddress,FD_ShippingCountry,FD_ShippingCity,FD_ShippingState,FD_ShippingZip")] Order Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Order Order = db.Orders.Find(id);
            db.Orders.Remove(Order);
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
