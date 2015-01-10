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
            if (product == null)
                throw new EntityDoesNotExistException("Product is null");

            if (user == null)
                throw new EntityDoesNotExistException("User is null");

            if (currency == null)
                throw new EntityDoesNotExistException("Currency is null");

            ProductAuction ap = new ProductAuction();
            ap.User = user;
            ap.Price = price;
            ap.Currency = currency;
            ap.Auction = product.Auction;
            ap.Date = DateTime.Now;
            var validationResults = Validation.Validate<ProductAuction>(ap);

            if (!this.VerifyAuction(product))
                throw new ValidationException("Cannot add a new auction for the selected product");
            
            if (this.verifyTheSameUser(product, user))
                throw new ValidationException("The ownwer and the actioneer cannot be the same user");

            if (!this.IsInvalidAuction(product))
                throw new ValidationException("Cannot add a new auction for the selected product because it is expired");

            if (!this.VerifyUser(user))
                throw new ValidationException("The user is not actioneer");

            if (!this.VerifyCurrency(product, currency))
                throw new ValidationException("The auction must have the same currency as the product: " + product.Auction.Currency.Name);

            if (!this.VerifyPrice(product, price))
                throw new ValidationException("The price is too low");

            /*if (ap.Currency == null)
                Console.WriteLine("ap.Cur e null");
            if (ap.Auction.Currency == null)
                Console.WriteLine("ap.au.cur e null");
            Console.Write(ap.Currency.Name + " " + ap.Auction.Currency.Name + " ");*/
            
            using (var context = new AuctionModelContainer())
            {
                context.Auctions.Attach(product.Auction);
                //context.Currencies.Attach(ap.Currency);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //context.Currencies.Attach(currency);
                context.Products.Attach(product);
                context.Users.Attach(user);
                context.ProductAuctions.Add(ap);
                
                context.SaveChanges();
            }
        }

        private Boolean verifyTheSameUser(Product product, User user)
        {
            if (product.Auction.User.Email.Equals(user.Email))
                return true;
            return false;
        }

        private Boolean IsInvalidAuction(Product product)
        {
            if (product.Auction.EndDate <= DateTime.Today)
            {
                using (var context = new AuctionModelContainer())
                {
                    product.Auction.Finished = true;
                    Auction auction = product.Auction;
                    context.Auctions.Attach(auction);
                    var entry = context.Entry(auction);
                    entry.Property(r => r.Finished).IsModified = true;
                    context.SaveChanges();
                }
                return false;
            }
            if (product.Auction.Finished == true)
                return false;

            return true;
        }

        private ICollection<Role> GetAllRolesOfAUser(User user)
        {
                using (var context = new AuctionModelContainer())
                {
                    var roleVar = (from role in context.Roles
                                   select role).ToList();

                    for (int i = 0; i < roleVar.Count; i++)
                    {
                        ICollection<User> users = roleVar.ElementAt(i).Users;
                        bool ok = false;
                        foreach (User userFor in users)
                            if (userFor.Email.Equals(user.Email))
                                ok = true;
                        if (!ok)
                        {
                            roleVar.Remove(roleVar.ElementAt(i));
                            i--;
                        }
                    }

                    return roleVar;
            }
        }

        private Boolean VerifyUser(User user)
        {
            ICollection<Role> roles = this.GetAllRolesOfAUser(user);

            foreach (Role role in roles)
            {
                if (role.Name.Equals("actioneer"))
                    return true;
            }
            return false;
        }
        private Boolean VerifyAuction(Product product)
        {
            if (product.Auction == null)
                return false;
            return true;
        }
        private Boolean VerifyCurrency(Product product, Currency currency)
        {
            if (product.Auction.Currency.IdCurrency != currency.IdCurrency)
                return false;
            return true;
        }
        private Boolean VerifyPrice(Product product, double price)
        {
            //Console.WriteLine("VerifyPrice lower than 0");
            if (price <= 0)
                return false;
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
                if( pa.Date >= maxDate )
                {
                    maxDate = pa.Date;
                    lastAuction = pa.Price;
                }
            }
            if (price <= lastAuction)
                return false;
            return true;
        }

        public bool closeAuction(User user, Product product)
        {
            if (user == null)
                throw new EntityDoesNotExistException("User is null");

            if (product == null)
                throw new EntityDoesNotExistException("Product is null");

            if (product.Auction == null)
                throw new EntityDoesNotExistException("Auction is null");

            if (!user.Email.Equals(product.Auction.User.Email))
                throw new AuctionException("You are not allowed to close the auction - you are not the owner!");

            if (product.Auction.Finished == true)
                throw new AuctionException("Auction already closed");

            using (var context = new AuctionModelContainer())
            {
                product.Auction.Finished = true;
                Auction auction = product.Auction;
                context.Auctions.Attach(auction);
                var entry = context.Entry(auction);
                entry.Property(r => r.Finished).IsModified = true;
                context.SaveChanges();
            }

            return true;
        }
    }
}
