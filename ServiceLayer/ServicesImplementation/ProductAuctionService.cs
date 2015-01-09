using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProductAuctionService : IProductAuctionService
    {
        private AuctionLogger logger;
        public ProductAuctionService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public bool closeAuction(User user, Product product)
        {
            logger.logInfo("Try to close a auction");
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductAuctionFactory.closeAuction(user, product);
            }
            catch (EntityDoesNotExistException exc)
            {
                logger.logError(exc);
                throw exc;
            }
            catch (AuctionException exc)
            {
                logger.logError(exc);
                throw exc;
            }
            return true;
        }

        public bool AddProductAuction(User user, Product product, double price, Currency currency)
        {
            logger.logInfo("Try to add a new ProductAuction");
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductAuctionFactory.AddProductAuction(user, product, price, currency);
                return true;
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
            catch (EntityDoesNotExistException entity)
            {
                logger.logError(entity);
                throw entity;
            }
        }


    }
}
