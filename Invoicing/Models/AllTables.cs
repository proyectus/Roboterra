using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invoicing.Models
{
    public class Parameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None), Key]
        public int ID { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Int64 ID { get; set; }
        [Required(ErrorMessage = "The School Name is required")]
        public Int64 SchoolID { get; set; }
        [MinLength(3, ErrorMessage = "The SchoolName can have a min of {1} characters")]
        [MaxLength(60, ErrorMessage = "The School Name can have a max of {1} characters")]
        public string SchoolName { get; set; }
        [Required(ErrorMessage = "The First Name is required")]
        [MinLength(3, ErrorMessage = "The First Name can have a min of {1} characters")]
        [MaxLength(100, ErrorMessage = "The First Name can have a max of {1} characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name is required")]
        [MinLength(3, ErrorMessage = "The Last Name can have a min of {1} characters")]
        [MaxLength(60, ErrorMessage = "The Last Name can have a max of {1} characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Extension { get; set; }
        public string Mobile { get; set; }
        public string OccupationTitle { get; set; }
        [Required(ErrorMessage = "The Organization Type is required")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Please select an Organization Type")]
        public string OrganizationType { get; set; }
        [Required(ErrorMessage = "The Organization State is required")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Please select a State")]
        public string SchoolState { get; set; }
        [Required(ErrorMessage = "The Organization City is required")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Please select a City")]
        public string SchoolCity { get; set; }
        public string OfficeClassRoomLocation { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        [NotMapped]
        public bool ind_FD { get; set; }
        public string FD_OccupationTitle { get; set; }
        public string FD_FirstName { get; set; }
        public string FD_LastName { get; set; }
        public string FD_Email { get; set; }
        public string FD_Phone { get; set; }
        public string FD_Extension { get; set; }
        public string FD_Mobile { get; set; }
        public string FD_OfficeLocation { get; set; }
        public string FD_ShippingAddress { get; set; }
        public string FD_ShippingCountry { get; set; }
        public string FD_ShippingCity { get; set; }
        public string FD_ShippingState { get; set; }
        public string FD_ShippingZip { get; set; }        
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept our Terms and Conditions")]
        public bool AcceptTC { get; set; }
        public decimal SubTotal { get; set; }
        [Required(ErrorMessage = "The Shipping Charge is required")]        
        public decimal ShippingCharges { get; set; }
        public decimal TaxPercentage { get; set; }
        [Required(ErrorMessage = "The Tax is required")]        
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        [MaxLength(1)]
        public string PaymentMethod { get; set; }  // P=Will Send Purchase Order, C=Purchase with Credit Card Today
        public string ZohoInvoiceID { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        [Key]
        public Int64 ID { get; set; }
        public virtual Order Order { get; set; }
        [Key]
        public int Item { get; set; }
        public string ZohoIdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "The Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }

    public class Tracing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Int64 ID { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }

    }

    public class SchoolType
    {
        [Key]
        public string Type { get; set; }	
    }

    public class Ubigeo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Int64 ID { get; set; }
        public string zip { get; set; }
        public string state	{ get; set; }
        public string city	{ get; set; }
        public string lat	{ get; set; }
        public string lng { get; set; }
    }

    public class StateTable
    {
        [Key]
        public string State { get; set; }
    }

    public class CityTable
    {
        [Column(Order = 0), Key]
        public string State { get; set; }
        [Column(Order = 1), Key]
        public string City { get; set; }
        public bool New { get; set; }	
    }

    public class School
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Int64 ID { get; set; }
        public string DistrictName{ get; set; }	
        public string SchoolName{ get; set; }	
        public string PrincipalName{ get; set; }	
        public string FirstName	{ get; set; }
        public string LastName	{ get; set; }
        public string EmailAddress	{ get; set; }
        public string Phone	{ get; set; }
        public string Fax	{ get; set; }
        public string LocationAddress	{ get; set; }
        [MaxLength(50)]
        public string LocationCity	{ get; set; }
        [MaxLength(50)]
        public string LocationState	{ get; set; }
        public string LocationZip { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
    }

    public class School2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Int64 ID { get; set; }
        public string DistrictName { get; set; }
        public string SchoolName { get; set; }
        public string PrincipalName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZip { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
    }

}