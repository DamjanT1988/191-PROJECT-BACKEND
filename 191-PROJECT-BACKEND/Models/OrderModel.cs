using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace _191_PROJECT_BACKEND.Models
{
    public class OrderModel
    {
            //data fields
            public int Id { get; set; }

        //user input and object properties
        //[Required]
        [Display(Name = "Order information:")]
        public string? order { get; set; }
        [Display(Name = "Customer email:")]
        public string? email { get; set; }
        [Display(Name = "Customer telephone number:")]
        public string? telephone { get; set; }
        [Display(Name = "Company name:")]
        public string? company_name { get; set; }
        [Display(Name = "Company organisation number:")]
        public string? company_org { get; set; }
        [Display(Name = "Company adress:")]
        public string? company_adress { get; set; }
        [Display(Name = "Company contact name:")]
        public string? contact_name { get; set; }
        [Display(Name = "Status of the order:")]
        public string? status { get; set; }
        [Display(Name = "Internal notes:")]
        public string? internal_note { get; set; }
    }
}
