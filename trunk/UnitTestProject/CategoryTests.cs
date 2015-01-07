using DataMapper.EFDataMapper;
using DataMapper.Exceptions;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void AddCategoryNullName()
        {
            Category category = new Category();
            category.Name = null;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name - null.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }
        [TestMethod]
        public void AddCategoryOneCharacterName()
        {
            Category category = new Category();
            category.Name = "o";
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }
        [TestMethod]
        public void AddCategoryThreeCharacterName()
        {
            Category category = new Category();
            category.Name = "thr";
            category.Description = "";
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void AddCategoryThirtyCharacterName()
        {
            Category category = new Category();
            category.Name = "123456789012345678901234567890";
            category.Description = "";
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddCategoryThirtyOneCharacterName()
        {
            Category category = new Category();
            category.Name = "1234567890123456789012345678901";
            category.Description = "";
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddCategoryNullDescription()
        {
            Category category = new Category();
            category.Name = "category";
            category.Description = null;

            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddCategoryZeroCharactesDescription()
        {
            Category category = new Category();
            category.Name = "category";
            category.Description = "";

            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddCategoryThirtyCharactesDescription()
        {
            Category category = new Category();
            category.Name = "category1";
            category.Description = "123456789012345678901234567890";

            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddCategoryTwoHundredCharactesDescription()
        {
            Category category = new Category();
            category.Name = "category2";
            category.Description = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";

            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AddCategoryTwoHundredAndOneCharactesDescription()
        {
            Category category = new Category();
            category.Name = "category3";
            category.Description = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901";

            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddCategoryNullParent()
        {
            Category category = new Category();
            category.Name = "NullParentName";
            category.Description = "NullParentDescription";
            category.ParentCategory = null;

            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AddCategoryNotNullParent()
        {
            Category category = new Category();
            category.Name = "NotNullParentName";
            category.Description = "NotNullParentDescription";
            CategoryService categoryService = new CategoryService();
            category.ParentCategory = categoryService.GetCategoryById(1);

            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AddCategoryNotNullInexistentParent()
        {
            Category category = new Category();
            category.Name = "NotNullInexistentParentName";
            category.Description = "NotNullInexistentParentDescription";
            CategoryService categoryService = new CategoryService();
            Category categoryParent = new Category();
            categoryParent.IdCategory = 1000;
            category.ParentCategory = categoryParent;

            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Parent does not exists!", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddCategoryDuplicationExceptionRoot()
        {
            Category category = new Category();
            category.Name = "thr";
            category.Description = "";
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("You can not add two categories with the same name (" + category.Name + ").", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddCategoryDuplicationExceptionNotRoot()
        {
            Category category = new Category();
            category.Name = "NotNullParentName";
            category.Description = "NotNullParentDescription";
            CategoryService categoryService = new CategoryService();
            category.ParentCategory = categoryService.GetCategoryById(1);
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("You can not add two categories with the same name (" + category.Name + ").", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddCategoryNull()
        {
            Category category = null;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.AddCategory(category);
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("Category is null", e.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void GetCategoryById()
        {
            String name = "thr";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            Assert.IsNotNull(category);
            Assert.AreEqual(category.Name, name);
        }

        [TestMethod]
        public void GetInexistentCategoryById()
        {
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(100);
            Assert.IsNull(category);
        }

        [TestMethod]
        public void GetCategoryByName()
        {
            String name = "thr";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryByName(name);
            Assert.IsNotNull(category);
            Assert.AreEqual(category.Name, name);
        }

        [TestMethod]
        public void GetCategoryIncexistentByName()
        {
            String name = "redtcvyhbnk";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryByName(name);
            Assert.IsNull(category);
        }

        [TestMethod]
        public void UpdateCategoryNameNull()
        {
            String name = null;
            int id = 1;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name.", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateCategoryNameOneCharacter()
        {
            String name = "o";
            int id = 1;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategoryNameThreeCharacters()
        {
            String name = "abc";
            int id = 1;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
            Category categ = categoryService.GetCategoryById(1);
            Assert.AreEqual(categ.Name, name);
        }

        [TestMethod]
        public void UpdateCategoryNameThirtyCharacters()
        {
            String name = "abcdefghijabcdefghijabcdefghij";
            int id = 1;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
            Category categ = categoryService.GetCategoryById(1);
            Assert.AreEqual(categ.Name, name);
        }

        [TestMethod]
        public void UpdateCategoryNameThirtyOneCharacters()
        {
            String name = "abcdefghijabcdefghijabcdefghija";
            int id = 1;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's name.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategoryInexistentName()
        {
            String name = "categ";
            int id = 100;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Category is null", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategoryDuplicateRootName()
        {
            String name = "category";
            int id = 1;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("You can not add two categories with the same name (" + name + ").", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategoryDuplicateHName()
        {
            String name = "HCateg";
            int id = 7;
            CategoryService categoryService = new CategoryService();
            Category parent = categoryService.GetCategoryById(1);
            Category category = new Category() { Name = "HCateg", Description = "", ParentCategory = parent };
            categoryService.AddCategory(category);
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("You can not add two categories with the same name (" + name + ").", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategorySameName()
        {
            String name = "category";
            int id = 3;
            CategoryService categoryService = new CategoryService();
            Boolean result = true;
            try
            {
                result = categoryService.UpdateCategory(id, name);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
            Category category = categoryService.GetCategoryById(id);
            Assert.AreEqual(category.Name, name);
        }

        [TestMethod]
        public void UpdateCategoryDescriptionNull()
        {
            String description = null;
            int id = 1;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategoryDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategoryDescriptionZeroCharacters()
        {
            String description = "";
            int id = 4;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategoryDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void UpdateCategoryDescriptionThirtyCharacters()
        {
            String description = "abcdefghijabcdefghijabcdefghij";
            int id = 4;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategoryDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void UpdateCategoryDescriptionTwoHundredCharacters()
        {
            String description = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            int id = 4;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategoryDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void UpdateCategoryDescriptionTwoHundredAndOneCharacters()
        {
            String description = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901";
            int id = 4;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategoryDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid category's description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategoryDescriptionInexistent()
        {
            String description = "abc";
            int id = 100;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategoryDescription(id, description);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Category is null", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateCategorySameDescription()
        {
            String description = "NullParentDescription";
            int id = 6;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.UpdateCategoryDescription(id, description);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
            Category category = categoryService.GetCategoryById(id);
            Assert.AreEqual(description, category.Description);
        }

        [TestMethod]
        public void deleteCategoryInexistent()
        {
            int id = 100;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            try
            {
                result = categoryService.DeleteCategory(id);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Category does not exists!", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void deleteCategoryExistent()
        {
            int id = 8;
            CategoryService categoryService = new CategoryService();
            Boolean result = false;
            result = categoryService.DeleteCategory(id);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void deleteCategoryDependency()
        {
            int id = 7;
            CategoryService categoryService = new CategoryService();
            ProductService productService = new ProductService();
            Category category = categoryService.GetCategoryById(id);
            Product product = new Product();
            product.Name = "Product";
            product.Description = "Description";
            product.Categories.Add(category);
            productService.AddProduct(product);
            Boolean result = false;
            try
            {
                result = categoryService.DeleteCategory(id);
            }
            catch (DependencyException exc)
            {
                Assert.AreEqual("The category has products. It cannot be deleted!", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestCategoryChildrenInexistentCategory()
        {
            int id = 100;
            CategoryService categoryService = new CategoryService();
            ICollection<Category> categories = categoryService.getChildren(id);
            Assert.AreEqual(categories.Count(), 0);
        }

        [TestMethod]
        public void TestCategoryParentsInexistentCategory()
        {
            int id = 100;
            CategoryService categoryService = new CategoryService();
            ICollection<Category> categories = categoryService.getParents(id);
            Assert.AreEqual(categories.Count(), 0);
        }

        [TestMethod]
        public void TestCategoryParentsCategory()
        {
            int id = 1;
            CategoryService categoryService = new CategoryService();
            ICollection<Category> categories = categoryService.getParents(id);
            Assert.AreEqual(categories.Count(), 1);
        }

        [TestMethod]
        public void GetCategorysForAProduct()
        {
            ProductService productService = new ProductService();
            Product product = productService.GetProductById(1);
            CategoryService categoryService = new CategoryService();
            ICollection<Category> categs = categoryService.GetCategorysForAProduct(product);
            Assert.AreEqual(categs.Count(), 1);
        }

        [TestMethod]
        public void TestCategoryChildrenCategory()
        {
            int id = 1;
            CategoryService categoryService = new CategoryService();
            ICollection<Category> categories = categoryService.getChildren(id);
            Assert.AreEqual(categories.Count(), 1);
        }
    }
}
