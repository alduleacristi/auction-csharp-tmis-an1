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
    public class CategoryService:ICategoryService
    {
        private AuctionLogger logger;
        public CategoryService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public void AddCategory(Category category)
        {
            logger.logInfo("Try to add a new category in db.");

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.AddCategory(category);
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
            catch (EntityDoesNotExistException entityDoesNotExist)
            {
                logger.logError(entityDoesNotExist);
                throw entityDoesNotExist;
            }
            logger.logInfo("Category added successfully!");
        }

        public Category GetCategoryByName(String name)
        {
            logger.logInfo("Try to get category by name from the db.");
            return DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.GetCategoryByName(name);
        }

        public Category GetCategoryById(int id)
        {
            logger.logInfo("Try to get category by id from the db.");
            return DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.GetCategoryById(id);
        }

        public void UpdateCategory(int id, String newName)
        {
            logger.logInfo("Try to update category whit the id " + id);
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.UpdateCategory(id, newName);
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
            catch (EntityDoesNotExistException entityDoesNotExist)
            {
                logger.logError(entityDoesNotExist);
                throw entityDoesNotExist;
            }
            logger.logInfo("Category successfully updated");
        }

        public void UpdateCategoryDescription(int id, String description)
        {
             logger.logInfo("Try to update category whit the id " + id);
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.UpdateCategoryDescription(id, description);
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
            logger.logInfo("Category successfully updated");
        }

        public void DeleteCategory(int id)
        {
            logger.logInfo("Try to delete category whit the id " + id);
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.DeleteCategory(id);
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
            logger.logInfo("Category successfully deleted!");
        }

        public ICollection<Category> getChildren(int idCategory)
        {
            logger.logInfo("Get children hierarchy for category "+idCategory);
            return DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.getChildren(idCategory);
        }
        public ICollection<Category> getParents(int idCategory)
        {
            logger.logInfo("Get parents hierarchy for category " + idCategory);
            return DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.getParents(idCategory);
        }

        public ICollection<Category> GetCategorysForAProduct(Product product)
        {
            logger.logInfo("Get all category for product " + product.Name);
            return DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.GetAllCategoryForAProduct(product);
        }
    }
}
