using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class UserMetadata
    {
        [NotNullValidator(MessageTemplate = "First name of the user can not be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "First name should have between {3} and {30} letters")]
        public String FirstName
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Last name of the user can not be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Last name should have between {3} and {30} letters")]
        public String LastName
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "The email of the user can not be null")]
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",ErrorMessage="Invalid email")]
        public String Email
        {
            get;
            set;
        }

        internal static void Validate(User user, ValidationResults results)
        {
            if (true)//some business-logic derived condition
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", user, "ValidateMethod", "error", null)
                    );
            }
        }
    }
}
