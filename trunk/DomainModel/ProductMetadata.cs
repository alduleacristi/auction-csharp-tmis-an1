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
    public class ProductMetadata
    {
        [NotNullValidator(MessageTemplate = "The name of the product cannot be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "The product's name should have between {3} and {30} characters")]
        public String Name
        {
            get;
            set;
        }

        [StringLengthValidator(0, RangeBoundaryType.Inclusive, 200, RangeBoundaryType.Inclusive, ErrorMessage = "The product's description should have between {0} and {200} characters")]
        public String Description
        {
            get;
            set;
        }

        internal static void Validate(Product product, ValidationResults results)
        {
            if (product.Name.Length < 3 || product.Name.Length > 30 || product.Description.Length > 200)//some business-logic derived condition
            {
                results.AddResult
                    (
                        new ValidationResult("Invalid product's name/description.", product, "ValidateMethod", "error", null)
                    );
            }
        }
    }
}
