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
    class CategoryMetadata
    {
        [NotNullValidator(MessageTemplate = "The name of the category cannot be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "The category's name should have between {3} and {30} characters")]
        public String Name
        {
            get;
            set;
        }
        
        [StringLengthValidator(0, RangeBoundaryType.Inclusive, 300, RangeBoundaryType.Inclusive, ErrorMessage = "The category's description should have between {0} and {300} characters")]
        public String Description
        {
            get;
            set;
        }

        internal static void Validate(Category category, ValidationResults results)
        {
            if (true)//some business-logic derived condition
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", category, "ValidateMethod", "error", null)
                    );
            }
        }
    }
}
