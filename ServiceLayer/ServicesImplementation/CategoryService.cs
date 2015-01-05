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
    public class CategoryService:ICategoryService
    {
        private AuctionLogger logger;
        public CategoryService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public bool AddCategory(Category category)
        {
            logger.logInfo("Try to add a new category in db.");
            if (category == null)
            {
                ValidationException e = new ValidationException("Category is null");
                logger.logError(e);
                throw e;
            }
            var validationResults = Validation.Validate<Category>(category);
            if (category.Name != null)
            {
                if (!validationResults.IsValid)
                {
                    ValidationException e = new ValidationException("Invalid category's name/description.");
                    logger.logError(e);
                   foreach (ValidationResult vr in validationResults)
                        Console.WriteLine(vr.Message);
                    throw e;
                }
                else
                { 
                    try
                    {
                        DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.AddCategory(category);
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
            }
            else
            {
                ValidationException e = new ValidationException("Invalid category's name - null.");
                logger.logError(e);
                throw e;
            }
            return true;
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

        public bool UpdateCategory(int id, String newName)
        {
            logger.logInfo("Try to update category whit the id " + id);
            Category category = this.GetCategoryById(id);
            if (category == null)
            {
                EntityDoesNotExistException e = new EntityDoesNotExistException("Category is null");
                logger.logError(e);
                throw e;
            }
            if (category.Name != newName)
            {
                String oldName = category.Name;
                category.Name = newName;
                var validationResults = Validation.Validate<Category>(category);
                if (newName != null)
                {
                    if (validationResults.IsValid)
                    {
                        try
                        {
                            category.Name = oldName;
                            DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.UpdateCategory(category,newName);
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
                        ValidationException e = new ValidationException("Invalid category's name.");
                        logger.logError(e);
                        foreach (ValidationResult vr in validationResults)
                            Console.WriteLine(vr.Message);
                        throw e;
                    }
                }
                else
                {
                    ValidationException e = new ValidationException("Invalid category's name.");
                    logger.logError(e);
                    throw e;
                }
                logger.logInfo("Category successfully updated");
            }
            else logger.logInfo("Category has the same name");
            return true;
        }

        public bool UpdateCategoryDescription(int id, String description)
        {
             logger.logInfo("Try to update category whit the id " + id);
             Category category = this.GetCategoryById(id);
             if (category == null)
             {
                 EntityDoesNotExistException e = new EntityDoesNotExistException("Category is null");
                 logger.logError(e);
                 throw e;
             }
             String oldDescription = category.Description;
             category.Description = description;
             var validationResults = Validation.Validate<Category>(category);
             if (validationResults.IsValid)
             {
                 category.Description = oldDescription;
                 if (category.Description != description)
                 {
                     try
                     {
                         DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.UpdateCategoryDescription(category, description);
                     }
                     catch (EntityDoesNotExistException entityDoesNotExist)
                     {
                         logger.logError(entityDoesNotExist);
                         throw entityDoesNotExist;
                     }
                     logger.logInfo("Category successfully updated");
                 }
             }
             else
             {
                 ValidationException e = new ValidationException("Invalid category's description.");
                 logger.logError(e);
                 foreach (ValidationResult vr in validationResults)
                     Console.WriteLine(vr.Message);
                 throw e;
             }
             return true;
        }

        public bool DeleteCategory(int id)
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
            return true;
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
