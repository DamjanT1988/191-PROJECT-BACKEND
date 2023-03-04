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
        public string? order { get; set; }
        public string? email { get; set; }
        public string? telephone { get; set; }
        public string? company_name { get; set; }
        public string? company_org { get; set; }
        public string? company_adress { get; set; }
        public string? contact_name { get; set; }
        public string? status { get; set; }
        public string? internal_note { get; set; }
    }
}
