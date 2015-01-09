using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IProductAuctionFactory
    {
        void AddProductAuction(User user, Product product, double price, Currency currency);

        bool closeAuction(User user, Product product);
    }
}
