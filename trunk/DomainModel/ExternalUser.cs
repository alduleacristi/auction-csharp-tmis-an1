using Microsoft.Practices.EnterpriseLibrary.Validation;
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
    }
}
