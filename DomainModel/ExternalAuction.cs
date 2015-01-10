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
    [MetadataType(typeof(AuctionMetadata))]
    public partial class Auction
    {
        [SelfValidation]
        public void CustomValidate(ValidationResults results)
        {
            // Call the CategoryMetadata class validation code
            AuctionMetadata.Validate(this, results);
        }
    }
}
