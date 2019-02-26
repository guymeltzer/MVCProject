using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repositories;

namespace SecondHandStoreASPNET_Project.Validations
{
   

    public class CusVal : ValidationAttribute
    {

        private readonly string _userName;
        public CusVal(string userName)
        {
            this._userName = userName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var property = validationContext.ObjectType.GetProperty(_userName);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _userName)
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            // at this stage you have "value" and "otherValue" pointing
            // to the value of the property on which this attribute
            // is applied and the value of the other property respectively
            // => you could do some checks
            if (!object.Equals(value, otherValue))
            {
                // here we are verifying whether the 2 values are equal
                // but you could do any custom validation you like
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }

    public class CheckIfUserExists : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            using (var _dbManager = new DbManager())
            {
                var user = _dbManager.UserRepository.GetUserByID((int)value);
                bool status;
                if (HttpContext.Current.User.Identity.Name == user.Username)
                    status = false;
                else
                {
                    status = true;

                }
                return status;
            }


        }
    }
}