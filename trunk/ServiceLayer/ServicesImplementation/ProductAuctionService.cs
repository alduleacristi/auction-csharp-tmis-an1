using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServicesImplementation
{
    public class ProductAuctionService : IProductAuctionService
    {
        private AuctionLogger logger;
        public ProductAuctionService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public void AddProductAuction(User user, Product product, double price, Currency currency)
        {
            logger.logInfo("Try to add a new ProductAuction");
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductAuctionFactory.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
        }


    }
}
