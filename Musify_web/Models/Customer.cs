using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musify_web.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int Customer_id { get; set; }

        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$" , ErrorMessage ="Enter Proper Name")]
        public string Customer_name { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a vaild Email ")]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@+[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage ="Invaild Email")]
        public string Customer_Email { get; set; }

        [Display(Name = "Phone Number ")]
        [RegularExpression(@"[0-9]{9,10}", ErrorMessage = "Enter A proper number ")]
        [Required(ErrorMessage ="Please Enter your number")]
        public String Customer_Mobile_number { get; set; }


        [Display(Name = " Date Of Birth")]
        [DataType(DataType.Date)]
        public String Customer_DOB { get; set; }

       
        public String Account_DOC { get; set; }
        [Display(Name = " Password ")]
        [DataType(DataType.Password)]
        [Required]
        public string Account_Password { get; set; }
        [NotMapped]
        [Compare("Account_Password", ErrorMessage = "Password Doesn't match  with above Feild")]
        [Display(Name = "Re-Enter Password")]
        [DataType(DataType.Password)]
        public String Reenter_password { get; set; }
        public String Role { get; set; }


    }
}