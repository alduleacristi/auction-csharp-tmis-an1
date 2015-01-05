using DataMapper.EFDataMapper;
using DataMapper.Exceptions;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void AddNullNameProduct()
        {
            Product product = new Product();
            product.Name = null;
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's name - null.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddOneCharacterNameProduct()
        {
            Product product = new Product();
            product.Name = "o";
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddThreeCharacterNameProduct()
        {
            Product product = new Product();
            product.Name = "thr";
            product.Description = "";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddThirtyCharactersNameProduct()
        {
            Product product = new Product();
            product.Name = "123456789012345678901234567890";
            product.Description = "";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddThirtyOneCharactersNameProduct()
        {
            Product product = new Product();
            product.Name = "1234567890123456789012345678901";
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddNullDescriptionProduct()
        {
            Product product = new Product();
            product.Name = "product";
            product.Description = null;

            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddZeroCharactesDescriptionProduct()
        {
            Product product = new Product();
            product.Name = "product";
            product.Description = "";

            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddThirtyCharactesDescriptionProduct()
        {
            Product product = new Product();
            product.Name = "product";
            product.Description = "abcdefghijabcdefghijabcdefghij";

            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddTwoHundredCharactesDescriptionProduct()
        {
            Product product = new Product();
            product.Name = "product";
            product.Description = "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij";

            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void AddTwoHundredAndOneCharactersDescriptionProduct()
        {
            Product product = new Product();
            product.Name = "product";
            product.Description = "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghija";

            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's name/description.", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddProductWithNullCategories()
        {
            Product product = new Product();
            product.Name = "prod";
            product.Description = "";
            product.Categories = null;

            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Product's categories are null", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddProductWithNullCategory()
        {
            Product product = new Product();
            product.Name = "prod";
            product.Description = "";

            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The product must have at least one category setted!", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddProductWithInexistentCategory()
        {
            Product product = new Product();
            product.Name = "prod";
            product.Description = "";
            Category category = new Category();
            product.Categories.Add(category);

            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("One or more categories of the product do not exist", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddExistentProduct()
        {
            Product product = new Product();
            product.Name = "thr";
            product.Description = "";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.AddProduct(product);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("The same product already exists - same name, description and category", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void UpdateProductNameNull()
        {
            String name = null;
            int id = 1;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProduct(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's name.", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateProductOneCharacterName()
        {
            String name = "o";
            int id = 1;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProduct(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's name.", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateProductThreeCharactersName()
        {
            String name = "pro";
            int id = 1;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProduct(id, name);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
        }
    }
}
