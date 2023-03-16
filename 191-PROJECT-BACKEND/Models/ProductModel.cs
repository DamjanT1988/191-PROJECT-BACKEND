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
        [Display(Name = "Product ID number:")]
        public int Id { get; set; }

        //user input and object properties
        [Required]
        [Display(Name = "Title:")]
        public string? Product_title { get; set; }

        //[Required]
        [Display(Name = "EAN number:")]
        public string? Ean_number { get; set; }

        //[Required]
        [Display(Name = "Description:")]
        public string? Product_description { get; set; }

        //[Required]
        [Display(Name = "Price (SEK):")]
        public double? Price { get; set; }

        //[Required]
        [Display(Name = "In storage:")]
        public int? Amount_storage { get; set; }

        //[Required]
        [Display(Name = "Earliest expiration:")]
        public string? Expiration_date { get; set; }

        //[Required]
        [Display(Name = "Category:")]
        public int? Category { get; set; }

        //[Required]
        [Display(Name = "Made in Sweden?")]
        public bool? IsSwedish { get; set; }
        [Display(Name = "Image file name:")]
        public string? Image_path { get; set; }
 
            //not stored in DB, but shown in UI
            [NotMapped]
            [Display(Name = "Image file")]
            public IFormFile? Image_file { get; set; }
    }
}
