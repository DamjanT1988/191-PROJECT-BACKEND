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
        public string? Order { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Company_name { get; set; }
        public string? Company_org { get; set; }
        public string? Company_adress { get; set; }
        public string? Contact_name { get; set; }
        public string? Status { get; set; }
        public string? Internal_note { get; set; }
    }
}
