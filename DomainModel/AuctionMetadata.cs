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
    public class AuctionMetadata
     {
         internal static void Validate(Auction auction, ValidationResults results)
         {
             if (auction.StartPrice > 10000000 || auction.StartPrice < 0.1)//some business-logic derived condition
             {
                 results.AddResult
                     (
                         new ValidationResult("In a auction start price must be between 0.1 and 10000000", auction, "ValidateMethod", "error", null)
                     );
             }

             if (auction.BeginDate > auction.EndDate)//some business-logic derived condition
             {
                 results.AddResult
                     (
                         new ValidationResult("In a auction begin date can not be grater than end date", auction, "ValidateMethod", "error", null)
                     );
             }
         }
    }
}
