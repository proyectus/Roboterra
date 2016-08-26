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
            TracingJ("Passed Save_ZIContact");
            string URL = baseURL + "/contacts";
            string serialize = JsonConvert.SerializeObject(contact);

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
            TracingJ("Passed Save_ZIInvoice");
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

        public JsonResult Get_ZIInvoice(string invoiceID)
        {
            TracingJ("Passed Get_ZIInvoice");
            string URL = baseURL + "/invoices/" + invoiceID ;            

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string response = "";
                try
                {
                    response = wc.DownloadString( URL + "?authtoken=" + token + "&organization_id=" + organization_id + "&print=false&accept=json");
                }
                catch (WebException exception)
                {
                    return Json(new { success = false, responseText = "Email Sending Error: " + GetWebErrorDescription(exception) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = "Email Sending Error: " + ex.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = response }, JsonRequestBehavior.AllowGet);
            }            
        }

        public JsonResult Email_ZIInvoice(string invoiceID, ZI_InvoiceEmailInfo emailInfo)
        {
            TracingJ("Passed Email_ZIInvoice");
            string URL = baseURL + "/invoices/" + invoiceID +"/email";
            string serialize = JsonConvert.SerializeObject(emailInfo);

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string response = "";
                try
                {
                    TracingJ("Email_ZIInvoice Before UploadString");
                    response = wc.UploadString(URL, "POST", "authtoken=" + token + "&organization_id=" + organization_id + "&send_customer_statement=false&send_attachment=true&JSONString=" + System.Web.HttpUtility.UrlEncode(serialize));
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

        public ZI_InvoiceEmailInfo PrepareEmailInfo(string invoiceEmail, string invoiceEmailFD)
        {
            TracingJ("Passed PrepareEmailInfo");
            ZI_InvoiceEmailInfo einfo = new ZI_InvoiceEmailInfo();

            einfo.to_mail_ids = new List<string>();
            einfo.to_mail_ids.Add(invoiceEmail);
            einfo.to_mail_ids.Add(invoiceEmailFD);
            einfo.cc_mail_ids = new List<string>();
            einfo.cc_mail_ids.Add("derek.capo@roboterra.com");
            einfo.cc_mail_ids.Add("vendor@roboterra.com");
            //einfo.subject = "Invoice from Zillium Inc (Invoice#: INV-00001)";
            //einfo.body = body;
            //einfo.send_from_org_email_id = true;

            return einfo;
        }

        public ZI_Contact PrepareContact(Order order)
        {
            TracingJ("Passed PrepareContact");
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
                company_name = order.SchoolName,
                payment_terms = 15,
                payment_terms_label = "Net 15",
                currency_id = "563816000000005001", // Dolar
                website = "",
                custom_fields = new List<ZI_ContactCustomFields>(),
                shipping_address = new ZI_ContactAddress()
                {
                    address = order.ShippingAddress,
                    city = order.ShippingCity,
                    state = order.ShippingState,
                    zip = order.ShippingZip,
                    country = order.ShippingCountry,
                    fax = order.FirstName + " " + order.LastName + " - " + order.SchoolName
                },
                billing_address = new ZI_ContactAddress()
                {
                    address = order.FD_ShippingAddress,
                    city = order.FD_ShippingCity,
                    state = order.FD_ShippingState,
                    zip = order.FD_ShippingZip,
                    country = order.FD_ShippingCountry,
                    fax = order.FD_FirstName + " " + order.FD_LastName
                },
                contact_persons = contactPerson,
                default_templates = defaulttemplates,
                notes = ""
            };

            return contact;
        }

        public ZI_Invoice PrepareInvoice(Order order, string customer_id)
        {
            TracingJ("Passed PrepareInvoice");
            ZI_InvoiceAddress billing_Address = new ZI_InvoiceAddress()
            {
                address = order.FD_ShippingAddress,
                city = order.FD_ShippingCity,
                state = order.FD_ShippingState,
                zip = order.FD_ShippingZip,
                country = order.FD_ShippingCountry,
                fax = ""
            };

            ZI_InvoiceAddress shipping_Address = new ZI_InvoiceAddress()
            {
                address = order.ShippingAddress,
                city = order.ShippingCity,
                state = order.ShippingState,
                zip = order.ShippingZip,
                country = order.ShippingCountry,
                fax = ""
            };

            ZI_InvoicePaymentOption payment_options = new ZI_InvoicePaymentOption()
            {
                payment_gateways = new List<ZI_InvoicePaymentGateway>()
                {
                    new ZI_InvoicePaymentGateway() {
                        gateway_name = "stripe",
                        additional_field1 =  ""
                    }                                        
                }
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
                notes = "Thank you for choosing ROBOTERRA we are excited to be your partner in STEM education. If you have any questions please don't hesitate to contact us or your sales representative.",
                terms = @"ROBOTERRA's Customer satisfaction is our top priority. Care and expertise have gone into the construction of this product; every detail has been tested multiple times prior to shipping. If, however, you find any faults or defects in the product, please contact us to find out if you qualify for a product replacement or refund." + '\n' + '\n' + "Payment of this invoice is acceptance of our Terms and Conditions which are located at http://www.roboterra.com/terms which may be updated without notice." + '\n' + '\n' + "ROBOTERRA Inc. currently does not offer phone support. Technical support is available at support@roboterra.com",
                shipping_charge = order.ShippingCharges,
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

        public List<ZI_InvoiceLineItem> PrepareLineItem(List<OrderDetail> orderDetails, string State)
        {
            string taxID = "";
            if (State == "CA") { taxID = "563816000000069071";  }

            TracingJ("Passed PrepareLineItem");
            List<ZI_InvoiceLineItem> list = new List<ZI_InvoiceLineItem>();
                
            list.Add(
            new ZI_InvoiceLineItem{
                item_id = orderDetails[0].ZohoIdProduct,
                item_order = orderDetails[0].Item,
                name = orderDetails[0].Name,
                description = "",
                quantity = orderDetails[0].Qty,
                rate = orderDetails[0].Price,
                unit = "",
                project_id = "",
                expense_id = "",
                discount = 0,
                tax_id = taxID
            });

            return list;
        }


        public JsonResult Get_ZITaxesList()
        {
            string URL = baseURL + "/settings/taxes";
            string response = "";

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

                try
                {
                    response = wc.DownloadString(URL + "?authtoken=" + token + "&organization_id=" + organization_id);
                }
                catch (WebException exception)
                {
                    return Json(new { success = false, responseText = "Zoho Contact creation Error: " + GetWebErrorDescription(exception) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, responseText = "Zoho Contact creation Error: " + ex.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = response }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Get_ZITax(string taxID)
        {
            TracingJ("Passed GetZITax");
            string URL = baseURL + "/settings/taxes/" + taxID;
            string response = "";

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

                try
                {
                    response = wc.DownloadString(URL + "?authtoken=" + token + "&organization_id=" + organization_id);
                }
                catch (WebException exception)
                {
                    return Json(new { success = false, responseText = "Zoho Contact creation Error: " + GetWebErrorDescription(exception) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, responseText = "Zoho Contact creation Error: " + ex.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = response }, JsonRequestBehavior.AllowGet);
            }
        }
        /////
    }
}