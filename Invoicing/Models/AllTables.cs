using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invoicing.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Int64 ID { get; set; }
        [Required(ErrorMessage = "The School Name is required")]
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
        public string Mobile { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        [NotMapped]
        public bool ind_FD { get; set; }
        public string FD_FirstName { get; set; }
        public string FD_LastName { get; set; }
        public string FD_Email { get; set; }
        public string FD_Phone { get; set; }
        public string FD_Mobile { get; set; }
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
        [Required(ErrorMessage = "The Tax is required")]        
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
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
}