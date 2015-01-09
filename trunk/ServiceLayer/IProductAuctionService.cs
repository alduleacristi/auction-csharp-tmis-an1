using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IProductAuctionService
    {
        bool AddProductAuction(User user, Product product, double price, Currency currency);
        bool closeAuction(User user, Product product);
    }
}
