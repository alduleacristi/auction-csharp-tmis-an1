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
        void AddProductAuction(User user, Product product, double price, Currency currency);
    }
}
