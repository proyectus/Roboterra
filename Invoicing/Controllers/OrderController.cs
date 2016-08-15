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
            TracingJ("Estuve por aqui");
            Order model = new Order();
            model.FirstName = "";
            model.SubTotal = 300;
            model.ShippingCharges = 0;
            model.Tax = 0;
            model.Total = 300;
            model.OrderDetails = new List<OrderDetail>();
            model.OrderDetails.Add(new OrderDetail { Item = 1, ZohoIdProduct = "563816000000069021", Description = "ROBOTERRA Origin Kit for Schools – 1 kit with 4 licenses", Qty = 1, Price = 300, Total = 300 });
            return View(model);
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
            model.OrderDetails.Add(new OrderDetail { Item = 1, ZohoIdProduct = "563816000000069021", Description = "ROBOTERRA Origin Kit for Schools – 1 kit with 4 licenses", Qty = 1, Price = 300, Total = 300 });
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
            if (ModelState.IsValid)
            {
                order.OrderDetails = orderDetails;
                db.Orders.Add(order);
                db.SaveChanges();

                //return RedirectToAction("Index");

                // Saving Contact in Zoho
                TracingJ("Contact Start");
                ZI_Contact zi_contact = PrepareContact(order);
                TracingJ("Passed Contact Prepared");
                JsonResult resp = Save_ZIContact(zi_contact);
                TracingJ("Passed Contact Save");
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                TracingJ("Passed Contact Serialized");
                Result result = serializer.Deserialize<Result>(serializer.Serialize(resp.Data));
                TracingJ("Passed Contact Deserialize");
                JObject responseText = JObject.Parse(result.responseText);
                TracingJ("Passed Contact Parse");
                if (result.success == false)
                {
                    TracingJ("Passed Contact result success");
                    return resp;
                }
                
                // Saving Order header and detail in Zoho
                string contactId = Convert.ToString(responseText["contact"]["contact_id"]);
                List<ZI_InvoiceLineItem> zi_lineItems = PrepareLineItem(orderDetails);
                ZI_Invoice zi_invoice = PrepareInvoice(order, contactId);
                zi_invoice.line_items = zi_lineItems;
                JsonResult resp2 = Save_ZIInvoice(zi_invoice);
                result = serializer.Deserialize<Result>(serializer.Serialize(resp2.Data));
                responseText = JObject.Parse(result.responseText);
                if (result.success == false)
                {
                    return resp;
                }

                // Emailing Invoice
                string invoiceId = Convert.ToString(responseText["invoice"]["invoice_id"]);
                string invoiceNumber = Convert.ToString(responseText["invoice"]["invoice_number"]);
                string invoiceDate = Convert.ToString(responseText["invoice"]["date"]);
                string invoiceEmail = order.Email;
                string invoiceAmount = Convert.ToString(responseText["invoice"]["total"]); 

                ZI_InvoiceEmailInfo zi_email = PrepareEmailInfo(invoiceNumber, invoiceDate, invoiceEmail, invoiceAmount);
                JsonResult resp3 = Email_ZIInvoice(invoiceId, zi_email);
                result = serializer.Deserialize<Result>(serializer.Serialize(resp3.Data));
                responseText = JObject.Parse(result.responseText);
                if (result.success == false)
                {
                    return resp;
                }

                return RedirectToAction("Index");
            }

            return View(order);
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
