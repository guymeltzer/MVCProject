using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModels.VideoViewModel
{
    public class VideoViewModel
    {
        [DisplayName("Published By")]
        public User Owner  { get; set; }

        [DisplayName("Movie Title")]
        [Required(ErrorMessage = "Title is required, field cannot be empty!")]
        [MaxLength(30, ErrorMessage = "Title must a maximum of 30 characters!")]
        public string Title { get; set; }

        [DisplayName("A Short Description")]
        [Required(ErrorMessage = "Short Description is required, field cannot be empty!")]
        [MaxLength(50, ErrorMessage = "Short Description must have a maximum of 50 characters")]
        public string ShortDescription { get; set; }

        [DisplayName("A Long Description")]
        [Required(ErrorMessage = "Long Description is required, field cannot be empty!")]
        [DataType(DataType.MultilineText)]
        [MaxLength(100, ErrorMessage = "Long Description must have a maximum of 100 characters")]
        public string LongDescription { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price is required, field cannot be empty!")]
        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [DisplayName("Year Of Release")]
        [Required(ErrorMessage = "Release Year is required, field cannot be empty!")]
        public int ReleaseYear { get; set; }

        public virtual HttpPostedFileBase Image1 { get; set; }
        public virtual HttpPostedFileBase Image2 { get; set; }
        public virtual HttpPostedFileBase Image3 { get; set; }


    }
}
