﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace _191_PROJECT_BACKEND.Models
{
    public class ProductModel
    {
            //data fields
            public int Id { get; set; }
            public DateTime Date { get; set; }

            //user input and object properties
            [Required]
            public string? Product_title { get; set; }
            public string? Ean_number { get; set; }
            public string? Product_description { get; set; }
            public double? Price { get; set; }
            public int? Amount_storage { get; set; }
            public string? Expiration_date { get; set; }
            public int? Category { get; set; }
            public bool? IsSwedish { get; set; }
    }
}