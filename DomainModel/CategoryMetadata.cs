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
    public class CategoryMetadata
    {
        [NotNullValidator(MessageTemplate = "The name of the category cannot be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "The category's name should have between {3} and {30} characters")]
        public String Name
        {
            get;
            set;
        }
        
        [StringLengthValidator(0, RangeBoundaryType.Inclusive, 200, RangeBoundaryType.Inclusive, ErrorMessage = "The category's description should have between {0} and {300} characters")]
        public String Description
        {
            get;
            set;
        }

        internal static void Validate(Category category, ValidationResults results)
        {
            if ( category.Name.Length < 3 || category.Name.Length > 30 || category.Description.Length > 200)//some business-logic derived condition
            {
                results.AddResult
                    (
                        new ValidationResult("Invalid category's name/description.", category, "ValidateMethod", "error", null)
                    );
            }
        }
    }
}
