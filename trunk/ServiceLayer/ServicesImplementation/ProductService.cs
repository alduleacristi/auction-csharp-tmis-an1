using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ServiceLayer
{
    public class ProductService : IProductService
    {
        private AuctionLogger logger;
        public ProductService()
        {
            logger = AuctionLogger.GetInstance();
        }
        public bool AddProduct(Product product)
        {
            logger.logInfo("Try to add a new product in db.");
            if (product == null)
            {
                ValidationException e = new ValidationException("Product is null");
                logger.logError(e);
                throw e;
            }
            if (product.Categories == null)
            {
                ValidationException e = new ValidationException("Product's categories are null");
                logger.logError(e);
                throw e;
            }
            CategoryService categoryService = new CategoryService();
            foreach (Category categ in product.Categories)
            {
                if (categoryService.GetCategoryById(categ.IdCategory) == null)
                {
                    EntityDoesNotExistException e = new EntityDoesNotExistException("One or more categories of the product do not exist");
                    logger.logError(e);
                    throw e;
                }
            }
            var validationResults = Validation.Validate<Product>(product);
            if (product.Name != null)
            {
                if (!validationResults.IsValid)
                {
                    ValidationException e = new ValidationException("Invalid product's name/description.");
                    logger.logError(e);
                    throw e;
                }
                else
                {
                    try
                    {
                        DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.AddProduct(product);
                    }
                    catch (DuplicateException duplicateException)
                    {
                        logger.logError(duplicateException);
                        throw duplicateException;
                    }
                }
            }
            else
            {
                ValidationException e = new ValidationException("Invalid product's name - null.");
                logger.logError(e);
                throw e;
            }
            
            logger.logInfo("Product added successfully!");
            return true;
        }
        public ICollection<Product> GetProductsByNameAndDescription(String name, String description)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetProdctByNameAndDescription(name, description);
        }
        public Product GetProductById(int id)
        {
            logger.logInfo("Try to get product by id from the db.");
            return DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetProdctById(id);
        }
        public bool UpdateProduct(int id, String newName)
        {
            logger.logInfo("Try to update product whit the id " + id);

            Product product = this.GetProductById(id);
            if (product == null)
            {
                EntityDoesNotExistException e = new EntityDoesNotExistException("Product is null");
                logger.logError(e);
                throw e;
            }
            if (product.Name != newName)
            {
                String oldName = product.Name;
                product.Name = newName;
                var validationResults = Validation.Validate<Product>(product);
                if (newName != null)
                {
                    if (validationResults.IsValid)
                    {
                        product.Name = oldName;
                        try
                        {
                            DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.UpdateProduct(product, newName);
                        }
                        catch (DuplicateException duplicateException)
                        {
                            logger.logError(duplicateException);
                            throw duplicateException;
                        }
                        catch (EntityDoesNotExistException entityDoesNotExist)
                        {
                            logger.logError(entityDoesNotExist);
                            throw entityDoesNotExist;
                        }
                    }
                    else
                    {
                        ValidationException e = new ValidationException("Invalid product's name.");
                        logger.logError(e);
                        throw e;
                    }
                }
                else
                {
                    ValidationException e = new ValidationException("Invalid product's name.");
                    logger.logError(e);
                    throw e;
                }
            }
            else logger.logInfo("Product has the same name");
            logger.logInfo("Product successfully updated");
            return true;
        }
        public void UpdateProductDescription(int id, String description)
        {
            logger.logInfo("Try to update product whit the id " + id);
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.UpdateProductDescription(id, description);
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
            catch (EntityDoesNotExistException entityDoesNotExist)
            {
                logger.logError(entityDoesNotExist);
                throw entityDoesNotExist;
            }
            logger.logInfo("Product successfully updated");
        }
        public void DeleteProduct(int id)
        {
            logger.logInfo("Try to delete product whit the id " + id);
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.DeleteProduct(id);
            }
            catch (EntityDoesNotExistException entityDoesNotExist)
            {
                logger.logError(entityDoesNotExist);
                throw entityDoesNotExist;
            }
            catch (DependencyException dependencyException)
            {
                logger.logError(dependencyException);
                throw dependencyException;
            }
            logger.logInfo("Product successfully deleted!");
        }

        public ICollection<Product> GetAllProductsOfACategory(Category category)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetAllProductsOfACategory(category);
        }


        public Auction GetAuctionOfAProduct(Product product)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetAuctionOfAProduct(product);
        }
    }
}
