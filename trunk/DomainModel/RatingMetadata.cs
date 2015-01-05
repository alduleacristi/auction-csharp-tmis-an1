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
    public class RatingMetadata
    {
        [RangeValidator(1,RangeBoundaryType.Inclusive,10,RangeBoundaryType.Inclusive,MessageTemplate="The note must be between 1 and 10")]
        public int Grade
        {
            get;
            set;
        }

        internal static void Validate(Rating role, ValidationResults results)
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
