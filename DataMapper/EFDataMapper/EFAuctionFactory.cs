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
        public void AddNewAuction(Auction auction,User user)
        {
            using(var context=new AuctionModelContainer())
            {
                context.Auctions.Add(auction);

                context.SaveChanges();
            }
        }
    }
}
