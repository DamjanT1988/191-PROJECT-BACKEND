using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace _191_PROJECT_BACKEND.Models
{
    public class ProductModel
    {
            //data fields
            public int Id { get; set; }

        //user input and object properties
        //[Required]
        [Display(Name = "Title:")]
        public string? Product_title { get; set; }
        [Display(Name = "EAN number:")]
        public string? Ean_number { get; set; }
        [Display(Name = "Description:")]
        public string? Product_description { get; set; }
        [Display(Name = "Price (SEK):")]
        public double? Price { get; set; }
        [Display(Name = "In storage:")]
        public int? Amount_storage { get; set; }
        [Display(Name = "Earliest expiration:")]
        public string? Expiration_date { get; set; }
        [Display(Name = "Category:")]
        public int? Category { get; set; }
        [Display(Name = "Made in Sweden?")]
        public bool? IsSwedish { get; set; }
        [Display(Name = "Image stored at:")]
        public string? Image_path { get; set; }
 
            //not stored in DB, but shown in UI
            [NotMapped]
            [Display(Name = "Image file")]
            public IFormFile? Image_file { get; set; }
    }
}
