﻿using DataMapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.EFDataMapper;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DomainModel;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace DataMapper.EFDataMapper
{
    class EFCategoryFactory : ICategoryFactory
    {

        public void AddCategory(Category category)
        {
            using (var context = new AuctionModelContainer())
            {
                if (category.ParentCategory != null)
                    context.Categories.Attach(category.ParentCategory);
                if (category.ParentCategory != null)
                {
                     Category parent = this.GetCategoryById(category.ParentCategory.IdCategory);
                     if(parent == null)
                    {
                        throw new EntityDoesNotExistException("Parent does not exists!");
                    }
                }

                Category auxCateg = this.GetCategoryByName(category.Name);
                if (category.ParentCategory != null)
                {
                    Console.WriteLine("verify parent h");
                    if (this.exists(category.ParentCategory.IdCategory, category.Name))
                        throw new DuplicateException("You can not add two categories with the same name (" + category.Name + ").");
                }
                else
                {
                    Console.WriteLine("verify in roots");
                    if (this.verifyNameInRoots(category.Name))
                       throw new DuplicateException("You can not add two categories with the same name (" + category.Name + ").");
                }
            
                 context.Categories.Add(category);
                    try
                    {
                        context.SaveChanges();
                    }catch(DbEntityValidationException exc)
                    {
                        throw new ValidationException("Invalid category's name/description.");
                    }
                }
            }

        private Boolean exists(int idCategory, String name)
        {
            Console.WriteLine(idCategory);
            ICollection<Category> categs = this.getChildren(idCategory);
            ICollection<Category> parents = this.getParents(idCategory);
            foreach (Category categ in parents)
                categs.Add(categ);
            foreach (Category categ in categs)
            {
                Console.WriteLine(categ.Name+" "+name);
                if (categ.Name.Equals(name))
                    return true;
            }
            return false;
        }

        private ICollection<Category> getRoots()
        {
            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                return (from category in context.Categories
                                where category.IdParentCategory == null
                                select category).ToList();
            }
        }

        private Boolean verifyNameInRoots(String name)
        {
            ICollection<Category> categs = this.getRoots();
            foreach(Category categ in categs)
                if (categ.Name.Equals(name))
                    return true;
            return false;
        }

        public Category GetCategoryByName(String name)
        {
            using(var context = new AuctionModelContainer())
            {
                var categVar = (from category in context.Categories where category.Name.Equals(name)
                                  select category).FirstOrDefault();
                return categVar;
            }
        }

        public Category GetCategoryById(int id)
        {
            using(var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                var categVar = (from category in context.Categories where category.IdCategory == id 
                                select category).FirstOrDefault();
                return categVar;
            }
        }

        public void UpdateCategory(int id, String newName)
        {
            Category category = this.GetCategoryById(id);
            if (category == null)
            {
                throw new EntityDoesNotExistException("Category does not exists!");
            }

            if (!category.Name.Equals(newName))
            {
                Category auxCateg = this.GetCategoryByName(newName);
                if (this.exists(category.ParentCategory.IdCategory, newName))
                    throw new DuplicateException("You can not add two categories with the same name (" + newName + ").");
                category.Name = newName;
                using (var context = new AuctionModelContainer())
                {
                    context.Categories.Attach(category);
                    var entry = context.Entry(category);
                    entry.Property(r => r.Name).IsModified = true;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException exc)
                    {
                        throw new ValidationException("Invalid category's name.");
                    }
                }
            }
        }
        public void UpdateCategoryDescription(int id, String description)
        {
            Category category = this.GetCategoryById(id);
            if (category == null)
            {
                throw new EntityDoesNotExistException("Category does not exists!");
            }

            //if (!description.Equals(null) && !category.Description.Equals(null))
            {
                //if (!category.Description.Equals(description))
                {
                    category.Description = description;
                    using (var context = new AuctionModelContainer())
                    {
                        context.Categories.Attach(category);
                        var entry = context.Entry(category);
                        entry.Property(r => r.Description).IsModified = true;

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbEntityValidationException exc)
                        {
                            throw new ValidationException("Invalid category's description.");
                        }
                    }
                }
            }
        }

        public void DeleteCategory(int id)
        {
            Category category = this.GetCategoryById(id);
            if (category == null)
            {
                throw new EntityDoesNotExistException("Category does not exists!");
            }
            
            using (var context = new AuctionModelContainer())
            {

                context.Categories.Attach(category);
                context.Entry(category).Collection(categ => categ.Products).Load();
                if (category.Products.Count() > 0)
                {
                    throw new DependencyException("The category has products. It cannot be deleted!");
                }
                context.Categories.Attach(category);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public ICollection<Category> getChildren(int idCategory)
        {
             using(var context = new AuctionModelContainer())
             {
                var clientIdParameter = new SqlParameter("@parent_id", idCategory);

                return context.Database
                    .SqlQuery<Category>("dbo.categories_FindChildren @parent_id", clientIdParameter)
                    .ToList();
             }
        }
        public ICollection<Category> getParents(int idCategory)
        {
            using(var context = new AuctionModelContainer())
             {
                var clientIdParameter = new SqlParameter("@lCategoryID", idCategory);

                return context.Database
                    .SqlQuery<Category>("dbo.categories_GetCatParentsNew @lCategoryID", clientIdParameter)
                    .ToList();
             }
        }

        public ICollection<Category> GetAllCategoryForAProduct(Product product)
        {
            using(var context = new AuctionModelContainer())
            {
                context.Products.Attach(product);
                context.Entry(product).Collection(prod => prod.Categories).Load();

                return product.Categories;
            }
        }
    }
}