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
    public class ProductAuctionMetadata
    {
        [RangeValidator(1.0, RangeBoundaryType.Inclusive, 1.0, RangeBoundaryType.Ignore, MessageTemplate = "Minimum price: {3}")]
        public double Price { get; set; }
        public System.DateTime Date { get; set; }
       
       [SelfValidation]
        internal static void Validate(ProductAuction productAuction, ValidationResults results)
        {
            //Console.Write()
            //Console.WriteLine(productAuction.Currency.Name + " " + productAuction.Auction.Currency.Name);
            if (!productAuction.Currency.Name.Equals(productAuction.Auction.Currency.Name))
                results.AddResult(new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("the same currency is required", productAuction, "ValidatePrice", "error", null));

            if (productAuction.Price <= 0)
                results.AddResult(new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("price is negative or zero", productAuction, "ValidatePrice", "error", null));

            if (productAuction.Price <= productAuction.Auction.StartPrice)
                results.AddResult(new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("price is lower than start auction price", productAuction, "ValidatePrice", "error", null));

            if (productAuction.Date != DateTime.Today)
                results.AddResult(new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("date must be today", productAuction, "ValidateDate", "error", null));
        }
    }
}
