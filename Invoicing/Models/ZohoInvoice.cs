using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoicing.Models
{
    public class ZI_ContactCustomFields
    {
        public int index { get; set; }
        public string value { get; set; }
    }

    public class ZI_ContactDefaultTemplate
    {
        public string invoice_template_id { get; set; }
        public string estimate_template_id { get; set; }
        public string creditnote_template_id { get; set; }
        public string invoice_email_template_id { get; set; }
        public string estimate_email_template_id { get; set; }
        public string creditnote_email_template_id { get; set; }
    }

    public class ZI_ContactPerson
    {
        public string salutation { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public bool is_primary_contact { get; set; }
    }
    public class ZI_ContactAddress
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip  { get; set; }
        public string country  { get; set; }
        public string fax  { get; set; }
    }

    public class ZI_Contact
    {
        public string contact_name { get; set; }
        public string company_name { get; set; }
        public int payment_terms { get; set; }
        public string payment_terms_label { get; set; } 
        public string currency_id { get; set; }
        public string website { get; set; }
        public List<ZI_ContactCustomFields> custom_fields { get; set; }
        public ZI_ContactAddress billing_address { get; set; }
        public ZI_ContactAddress shipping_address { get; set; }
        public List<ZI_ContactPerson> contact_persons { get; set; }
        public ZI_ContactDefaultTemplate default_templates { get; set; }
        public string notes { get; set; }
    }

    public class ZI_Invoice
    {
      public string customer_id { get; set; }
      public List<ZI_InvoiceContact> contact_persons { get; set; }
      public string invoice_number { get; set; }
      public string reference_number { get; set; }
      public string template_id { get; set; }
      public string date { get; set; }
      public int payment_terms { get; set; }
      public string payment_terms_label { get; set; }
      public string due_date { get; set; }
      public decimal discount { get; set; }
      public bool is_discount_before_tax { get; set; }
      public string discount_type { get; set; }
      public bool is_inclusive_tax { get; set; }
      public decimal exchange_rate { get; set; }
//      public string recurring_invoice_id { get; set; }
//      public string invoiced_estimate_id { get; set; }
      public string salesperson_name { get; set; }
      public List<ZI_InvoiceCustomField> custom_fields { get; set; }
      public List<ZI_InvoiceLineItem> line_items { get; set; }
      public ZI_InvoicePaymentOption payment_options { get; set; } 
      public bool allow_partial_payments { get; set; }
      public string custom_body { get; set; }
      public string custom_subject { get; set; }
      public string notes { get; set; }
      public string terms { get; set; }
      public decimal shipping_charge { get; set; }
      public decimal adjustment { get; set; }
      public string adjustment_description { get; set; }
      public string reason { get; set; }
    }

    public class ZI_InvoiceContact
    {
        public string ID { get; set; }
    }

    public class ZI_InvoiceCustomField
    {
        public int index { get; set; }
        public string value { get; set; }
    }

    public class ZI_InvoiceLineItem
    {
        public string item_id { get; set; }
        public string project_id  { get; set; }
        public string expense_id { get; set; }
        public string name { get; set; }
        public string description  { get; set; }
        public int item_order { get; set; }
        public decimal rate  { get; set; }
        public string unit { get; set; }
        public decimal quantity { get; set; }
        public decimal discount { get; set; }
        public string tax_id { get; set; }
    }

    public class ZI_InvoicePaymentOption
    {
        public List<ZI_InvoicePaymentGateway> payment_gateways { get; set; }
    }
    public class ZI_InvoicePaymentGateway
    {
        public string gateway_name { get; set; }
        public string additional_field1 { get; set; }
    }

    public class ZI_InvoiceAddress
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string fax { get; set; }
    }

    public class ZI_InvoiceEmailInfo
    {
        public List<string> to_mail_ids { get; set; }
        public List<string> cc_mail_ids { get; set; }
        //public string subject { get; set; }
        //public string body { get; set; }
        //public bool send_from_org_email_id { get; set; }
    }

    public class ZI_TaxesList
    {
        public string tax_id { get; set; }
        public string tax_name { get; set; }
        public double tax_percentage { get; set; }
        public string tax_type { get; set; }
    }

}