﻿using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        [SelfValidation]
        public void CustomValidate(ValidationResults results)
        {
            // Call the ProductMetadata class validation code
            UserMetadata.Validate(this, results);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            User user = (User) obj;
            if (this.Email.Equals(user.Email) && this.FirstName.Equals(user.FirstName) && this.LastName.Equals(user.LastName))
                return true;

            return false;
        }
    }
}