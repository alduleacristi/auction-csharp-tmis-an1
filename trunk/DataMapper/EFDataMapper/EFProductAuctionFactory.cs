using DataMapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.EFDataMapper;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DomainModel;
using System.Data.Entity.Validation;

namespace DataMapper.EFDataMapper
{
    class EFProductAuctionFactory : IProductAuctionFactory
    {
        public void AddProductAuction(User user, Product product, double price, Currency currency)
        {

            if (!this.verifyTheSameUser(product, user))
                throw new ValidationException("The ownwer and the actioneer cannot be the same user");

            if (!this.VerifyAuction(product))
                throw new ValidationException("Cannot add a new auction for the selected product");

            if (!this.IsInvalidAuction(product))
                throw new ValidationException("Cannot add a new auction for the selected product because it is expired");

            if (!this.VerifyUser(user))
                throw new ValidationException("The user is not an actioneer");

            if (!this.VerifyCurrency(product, currency))
                throw new ValidationException("The auction must have the same currency as the product: " + product.Auction.Currency.Name);

            if (!this.VerifyPrice(product, price))
                throw new ValidationException("The price is too low");

            ProductAuction ap = new ProductAuction();
            ap.User = user;
            ap.Price = price;
            ap.Currency = currency;
            ap.Auction = product.Auction;
            ap.Date = DateTime.Now;
            /*if (ap.Currency == null)
                Console.WriteLine("ap.Cur e null");
            if (ap.Auction.Currency == null)
                Console.WriteLine("ap.au.cur e null");
            Console.Write(ap.Currency.Name + " " + ap.Auction.Currency.Name + " ");*/
            
            using (var context = new AuctionModelContainer())
            {
                context.Auctions.Attach(product.Auction);
                context.Currencies.Attach(currency);
                context.Products.Attach(product);
                context.Users.Attach(user);
                context.ProductAuctions.Add(ap);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbUnexpectedValidationException e)
                {
                    Console.WriteLine(e);
                }
                catch (DbEntityValidationException exc)
                {
                    String message = "Invalid fields for ProductAuction object.";
                    IEnumerable<DbEntityValidationResult> errors = exc.EntityValidationErrors;
                    foreach (DbEntityValidationResult error in errors)
                        foreach (var validationError in error.ValidationErrors)
                            message = message + " " + validationError.PropertyName + ". " + validationError.ErrorMessage;

                    throw new ValidationException(message);
                }
            }
        }

        private Boolean verifyTheSameUser(Product product, User user)
        {
            if (product.Auction.User.Equals(user))
                return false;
            return true;
        }

        private Boolean IsInvalidAuction(Product product)
        {
            if (product.Auction.EndDate < DateTime.Today || product.Auction.Finished == true)
                return false;
            return true;
        }
        private Boolean VerifyUser(User user)
        {
            foreach (Role role in user.Roles)
            {
                if (role.Name.Equals("actioneer"))
                    return true;
            }
            return false;
        }
        private Boolean VerifyAuction(Product product)
        {
            if (product == null)
                return false;
            Console.WriteLine("product not null");
            if (product.Auction == null)
                return false;
            Console.WriteLine("auction not null");
            return true;
        }
        private Boolean VerifyCurrency(Product product, Currency currency)
        {
            if (product.Auction.Currency != currency)
                return false;
            return true;
        }
        private Boolean VerifyPrice(Product product, double price)
        {
            //Console.WriteLine("VerifyPrice lower than 0");
            //if (price <= 0)
                //return false;
            //Console.WriteLine("VerifyPrice lower than startPrice");
            if (price <= product.Auction.StartPrice)
                return false;
            Console.WriteLine("VerifyPrice grater than existing if exist");
            if (product.Auction.ProductActions.Count() == 0)
                return true;
            Console.WriteLine("VerifyPrice grater than existing");
            double lastAuction = product.Auction.ProductActions.ElementAt(0).Price;
            DateTime maxDate = product.Auction.ProductActions.ElementAt(0).Date;
            foreach (ProductAuction pa in product.Auction.ProductActions)
            {
                if( pa.Date > maxDate )
                {
                    maxDate = pa.Date;
                    lastAuction = pa.Price;
                }
            }
            if (price <= lastAuction)
                return false;
            return true;
        }
    }
}
