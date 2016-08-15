using Invoicing.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.IO;

namespace Invoicing.Controllers
{
    public class ZohoInvoiceController : BaseController
    {
        string token = "24a6157d1f9908427104d4ab3b24ebc7";
        string organization_id = "43466580";
        string baseURL = "https://invoice.zoho.com/api/v3";

        public JsonResult Save_ZIContact(ZI_Contact contact)
        {
            string URL = baseURL + "/contacts";
            string serialize = JsonConvert.SerializeObject(contact);
            TracingJ("Passed Save_ZIContact - Save Serialization");
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string response = "";
                try
                {
                    TracingJ("Passed Save Save_ZIContact - Before UploadString");
                    response = wc.UploadString(URL, "POST", "authtoken=" + token + "&organization_id=" + organization_id + "&JSONString=" + System.Web.HttpUtility.UrlEncode(serialize));
                    TracingJ("Passed Save Save_ZIContact - Before UploadString: " + response.ToString());
                }
                catch (WebException exception)
                {
                    TracingJ("Save_ZIContact WebException" + GetWebErrorDescription(exception));
                    return Json(new { success = false, responseText = "Zoho Contact creation Error: " + GetWebErrorDescription(exception) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    TracingJ("Save_ZIContact Exception" + ex.ToString());
                    return Json(new { success = false, responseText = "Zoho Contact creation Error: " + ex.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = response }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Save_ZIInvoice(ZI_Invoice invoice)
        {
            string URL = baseURL + "/invoices";
            string serialize = JsonConvert.SerializeObject(invoice);

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string response = "";
                try
                {
                    TracingJ("Save_ZIInvoice Before UploadString");
                    response = wc.UploadString(URL, "POST", "authtoken=" + token + "&organization_id=" + organization_id + "&send=false&ignore_auto_number_generation=false&JSONString=" +  System.Web.HttpUtility.UrlEncode(serialize));
                    TracingJ("Save_ZIInvoice After UploadString");
                }
                catch (WebException exception)
                {
                    TracingJ("Save_ZIInvoice WebException" + GetWebErrorDescription(exception));
                    return Json(new { success = false, responseText = "Zoho Invoice creation Error: " + GetWebErrorDescription(exception) }, JsonRequestBehavior.AllowGet);
                }   
                catch (Exception ex){
                    TracingJ("Save_ZIInvoice Exception" + ex.ToString());
                    return Json(new { success = false, responseText = "Zoho Invoice creation Error: " + ex.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = response }, JsonRequestBehavior.AllowGet);
            }            
        }

        public JsonResult Email_ZIInvoice(string invoiceID, ZI_InvoiceEmailInfo emailInfo)
        {
            string URL = baseURL + "/invoices/" + invoiceID +"/email";
            string serialize = JsonConvert.SerializeObject(emailInfo);

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string response = "";
                try
                {
                    TracingJ("Email_ZIInvoice Before UploadString");
                    response = wc.UploadString(URL, "POST", "authtoken=" + token + "&organization_id=" + organization_id + "&send=false&ignore_auto_number_generation=false&JSONString=" + System.Web.HttpUtility.UrlEncode(serialize));
                    TracingJ("Email_ZIInvoice After UploadString");
                }
                catch (WebException exception)
                {
                    TracingJ("Email_ZIInvoice WebException" + GetWebErrorDescription(exception));
                    return Json(new { success = false, responseText = "Email Sending Error: " + GetWebErrorDescription(exception) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    TracingJ("Email_ZIInvoice Exception" + ex.ToString());
                    return Json(new { success = false, responseText = "Email Sending Error: " + ex.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = response }, JsonRequestBehavior.AllowGet);
            }            
        }

        public ZI_InvoiceEmailInfo PrepareEmailInfo(string invoiceNumber, string invoiceDate, string invoiceEmail, string invoiceAmount)
        {            
            string body = String.Format( @"Dear Customer,         
                        <br><br><br><br>
                        Thanks for your business.         
                        <br><br><br><br>
                        The invoice {0} is attached with this email. You can choose the easy way out and <a href= https://invoice.zoho.com/SecurePayment?CInvoiceID=b9800228e011ae86abe71227bdacb3c68e1af685f647dcaed747812e0b9314635e55ac6223925675b371fcbd2d5ae3dc  >pay online for this invoice.</a>         
                        <br><br>Here's an overview of the invoice for your reference.         
                        <br><br><br><br>Invoice Overview:         
                        <br><br>Invoice # : {0}         
                        <br><br>Date : {1}         
                        <br><br>Amount : ${2}         
                        <br><br><br><br>It was great working with you. Looking forward to working with you again.
                        <br><br><br>\nRegards<br>\nZillium Inc<br>\n", invoiceNumber, invoiceDate, invoiceAmount);

            ZI_InvoiceEmailInfo einfo = new ZI_InvoiceEmailInfo();

            einfo.to_mail_ids = new List<string>();
            einfo.to_mail_ids.Add(invoiceEmail);
            einfo.cc_mail_ids = new List<string>();
            einfo.to_mail_ids.Add("jramirez@proyectus.com");
            einfo.subject = "Invoice from Zillium Inc (Invoice#: INV-00001)";
            einfo.body = body;
            einfo.send_from_org_email_id = true;

            return einfo;
        }

        public ZI_Contact PrepareContact(Order order)
        {

            ZI_ContactDefaultTemplate defaulttemplates = new ZI_ContactDefaultTemplate()
            {
                invoice_template_id = "563816000000025001",
                estimate_template_id = "",
                creditnote_template_id = "",
                invoice_email_template_id = "",
                estimate_email_template_id = "",
                creditnote_email_template_id = ""
            };

            List<ZI_ContactPerson> contactPerson = new List<ZI_ContactPerson>();
            contactPerson.Add(new ZI_ContactPerson
            { 
                    first_name = order.FirstName,
                    last_name = order.LastName,
                    email = order.Email,
                    is_primary_contact = true,
                    phone = order.Phone,
                    mobile = order.Mobile,
                    salutation = ""
            });

            ZI_Contact contact = new ZI_Contact
            {
                contact_name = String.Format("{0} {1}", order.FirstName, order.LastName),
                company_name = "",
                payment_terms = 15,
                payment_terms_label = "Net 15",
                currency_id = "563816000000005001", // Dolar
                website = "",
                custom_fields = new List<ZI_ContactCustomFields>(),
                billing_address = new ZI_ContactAddress()
                {
                    address = order.ShippingAddress,
                    city = order.ShippingCity,
                    state = order.ShippingState,
                    zip = order.ShippingZip,
                    country = order.ShippingCountry,
                    fax = ""
                },
                shipping_address = new ZI_ContactAddress()
                {
                    address = order.FD_ShippingAddress,
                    city = order.FD_ShippingCity,
                    state = order.FD_ShippingState,
                    zip = order.FD_ShippingZip,
                    country = order.FD_ShippingCountry,
                    fax = ""
                },
                contact_persons = contactPerson,
                default_templates = defaulttemplates,
                notes = ""
            };

            return contact;
        }

        public ZI_Invoice PrepareInvoice(Order order, string customer_id)
        {
            ZI_InvoicePaymentOption payment_options = new ZI_InvoicePaymentOption()
            {
                payment_gateways = new List<ZI_InvoicePaymentGateway>()
            };

            ZI_Invoice model = new ZI_Invoice()
            {
                customer_id = customer_id,
                invoice_number = "",
                reference_number = "",
                template_id = "563816000000025001",
                date = DateTime.Today.ToString("yyyy-MM-dd"),
                payment_terms = 15,
                payment_terms_label = "Net 15",
                due_date = DateTime.Today.ToString("yyyy-MM-dd"),
                discount = 0,
                is_discount_before_tax = true,
                discount_type = "item_level",
                is_inclusive_tax = false,
                exchange_rate = 1,
//                recurring_invoice_id = "",
//                invoiced_estimate_id = "563816000000096093",
                salesperson_name = "",
                allow_partial_payments = true,
                custom_body = "",
                custom_subject = "",
                notes = "Thanks for your business.",
                terms = "Terms and conditions apply",
                shipping_charge = 0,
                adjustment = 0,
                adjustment_description = "",
                reason = "",
                contact_persons = new List<ZI_InvoiceContact>(),
                custom_fields = new List<ZI_InvoiceCustomField>(),
                line_items = new List<ZI_InvoiceLineItem>(),
                payment_options = payment_options
            };


            

            return model;
        }

        public List<ZI_InvoiceLineItem> PrepareLineItem(List<OrderDetail> orderDetails)
        {
            List<ZI_InvoiceLineItem> list = new List<ZI_InvoiceLineItem>();
                
            list.Add(
            new ZI_InvoiceLineItem{
                item_id = orderDetails[0].ZohoIdProduct,
                item_order = orderDetails[0].Item,
                name = orderDetails[0].Description,
                description = "",
                quantity = orderDetails[0].Qty,
                rate = orderDetails[0].Price,
                unit = 0,
                project_id = "",
                expense_id = "",
                discount = 0,
                tax_id = ""
            });

            return list;
        }

    }
}