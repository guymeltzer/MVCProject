using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Models.ViewModels;
using Models.Validations;
namespace Models.Models
{
    public class User
    {
        public int UserID { get; set; }

        //[CheckIfUserExist(ErrorMessage = "User Name already exists! please try again")]
        [Required(ErrorMessage ="Username is required!")]
        [MinLength(6,ErrorMessage ="Username must be at least 6 characters long!"), MaxLength(15, ErrorMessage = "Username must have a maxiunm of 20 characters!")]
        [DisplayName("User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is required!")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long!"), MaxLength(20, ErrorMessage = "Password must have a maximum of 20 characters!")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Confirm password is required!")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Confirm Password must be at least 5 characters long!"), MaxLength(20, ErrorMessage = "Confirm Password must have a maximum of 20 characters!")]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="First Name is required!")]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters long!"), MaxLength(20, ErrorMessage = "First Name must have a maximum of 20 characters")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters long!"), MaxLength(20, ErrorMessage = "Last Name must have a maxium of 20 characters!")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Address field is required!")]
        [MaxLength(50, ErrorMessage ="Address must be between 1 and 50 characters!")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Email is required!")]
        [MaxLength(50, ErrorMessage ="Email field must be between 13 and 50 characters")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Email must be a in a valid email format!")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Birth Date is required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Birth")]
        public DateTime BirthDate { get; set; }
    }
}
