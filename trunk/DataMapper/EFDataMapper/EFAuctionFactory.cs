using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.EFDataMapper
{
    public class EFAuctionFactory:IAuctionFactory
    {
        public void AddNewAuction(Auction auction)
        {
            using(var context=new AuctionModelContainer())
            {
                context.Users.Attach(auction.User);
                context.Currencies.Attach(auction.Currency);
                context.Products.Attach(auction.Product);
                context.Auctions.Add(auction);

                context.SaveChanges();
            }
        }

        public Currency GetCurrencyByName(String currencyName)
        {
            using (var context = new AuctionModelContainer())
            {
                var currencyVar = (from currency in context.Currencies
                                where currency.Name == currencyName
                                select currency).FirstOrDefault();
                return currencyVar;
            }
        }

        public int GetNumberOfActiveAuctionsStartedByUser(User user)
        {
            using(var context = new AuctionModelContainer())
            {
                int nr = (from auction in context.Auctions
                          where auction.User.Email.Equals(user.Email)
                          select auction).Count();

                return nr;
            }
        }        public Auction GetAuctionById(int id)
        {
            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                var auctionVar = (from auction in context.Auctions
                               where auction.IdAuction.Equals(id)
                               select auction).FirstOrDefault();
                context.Auctions.Attach(auctionVar);
                //context.Entry(auctionVar).Collection(pAux => pAux.Categories).Load();
                context.Entry(auctionVar).Reference(pAux => pAux.Product).Load();
                return auctionVar;
            }
        }    }
}
