using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models.Validations
{
    public class CheckIfUserExist : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var isValid = HttpContext.Current.User.Identity.Name;

            return (isValid != (string)value);

        }
    }
}