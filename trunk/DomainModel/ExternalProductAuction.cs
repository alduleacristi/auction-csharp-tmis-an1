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
    [MetadataType(typeof(ProductAuctionMetadata))]
    public partial class ProductAuction
    {
        [SelfValidation]
        public void CustomValidate(ValidationResults results)
        {
            // Call the ProductAuctionMetadata class validation code
            
            //validarea currency-ului
            //validarea pretului
            ProductAuctionMetadata.Validate(this, results);
        }
    }
}
