using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [HasSelfValidation]
    public class RoleMetadata
    {
        [NotNullValidator(MessageTemplate = "The name of the role can not be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {30} letters")]
        public String Name
        {
            get;
            set;
        }

        internal static void Validate(Role role, ValidationResults results)
        {
            if (true)//some business-logic derived condition
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", role, "ValidateMethod", "error", null)
                    );
            }
        }
    }
}
