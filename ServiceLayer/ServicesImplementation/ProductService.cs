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
    public class ProductService : IProductService
    {
       private AuctionLogger logger;
        public ProductService()
        {
            logger = AuctionLogger.GetInstance();
        }
        public void AddProduct(Product product)
        {
            logger.logInfo("Try to add a new product in db.");

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.AddProduct(product);
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
            catch (DuplicateException duplicateException)
            {
                logger.logError(duplicateException);
                throw duplicateException;
            }
            logger.logInfo("Product added successfully!");
        }
        public ICollection<Product> GetProductsByNameAndDescription(String name, String description)
        {
            return null;
        }
        public Product GetProductById(int id)
        {
            return null;
        }
        public void UpdateProduct(int id, String newName)
        {

        }
        public void UpdateProductDescription(int id, String description)
        {

        }
        public void DeleteProduct(int id)
        {

        }
    }
}
