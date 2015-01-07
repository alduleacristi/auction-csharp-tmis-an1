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
    public class ProductTests
    {
        [TestMethod]
        public void AddProductNullName()
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
        public void AddProductOneCharacterName()
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
        public void AddProductThreeCharacterName()
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
        public void AddProductThirtyCharactersName()
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
        public void AddProductThirtyOneCharactersName()
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
        public void AddProductNullDescription()
        {
            Product product = new Product();
            product.Name = "Product";
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
        public void AddProductZeroCharactesDescription()
        {
            Product product = new Product();
            product.Name = "Product";
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
        public void AddProductThirtyCharactesDescription()
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
        public void AddProductTwoHundredCharactesDescription()
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
        public void AddProductTwoHundredAndOneCharactersDescription()
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
        public void AddProductExistent()
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

        [TestMethod]
        public void UpdateProductThirtyCharactersName()
        {
            String name = "123456789012345678901234567890";
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

        [TestMethod]
        public void UpdateProductThirtyOneCharactersName()
        {
            String name = "1234567890123456789012345678901";
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
        public void UpdateProductDuplicateName()
        {
            Product product = new Product();
            product.Name = "Product1";
            product.Description = "Description";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(7);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            productService.AddProduct(product);

            String name = "123456789012345678901234567890";
            int id = 7;

            Boolean result = false;
            try
            {
                result = productService.UpdateProduct(id, name);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("The same product already exists - same name, description and category", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateProductNull()
        {
            String name = "Product";
            int id = 100;
            ProductService productService = new ProductService();

            Boolean result = false;
            try
            {
                result = productService.UpdateProduct(id, name);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Product is null", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateProductSameName()
        {
            String name = "product";
            int id = 5;
            ProductService productService = new ProductService();

            Boolean result = false;
            try
            {
                result = productService.UpdateProduct(id, name);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
            Product product = productService.GetProductById(id);
            Assert.AreEqual(product.Name, name);
        }

        [TestMethod]
        public void UpdateProductDescriptionNull()
        {
            String description = null;
            int id = 1;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProductDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's description.", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateProductDescriptionZeroCharacters()
        {
            String description = "";
            int id = 7;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProductDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void UpdateProductDescriptionThirtyCharacters()
        {
            String description = "123456789012345678901234567890";
            int id = 7;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProductDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void UpdateProductDescriptionTwoHundredCharacters()
        {
            String description = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            int id = 7;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProductDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void UpdateProductDescriptionTwoHundredAndOneCharacters()
        {
            String description = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901";
            int id = 7;
            ProductService ProductService = new ProductService();
            Boolean result = false;
            try
            {
                result = ProductService.UpdateProductDescription(id, description);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Invalid product's description.", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateProductNullDescription()
        {
            String description = "Product";
            int id = 100;
            ProductService productService = new ProductService();

            Boolean result = false;
            try
            {
                result = productService.UpdateProductDescription(id, description);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Product is null", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateProductSameDescription()
        {
            String description = "abcdefghijabcdefghijabcdefghij";
            int id = 5;
            ProductService productService = new ProductService();

            Boolean result = false;
            try
            {
                result = productService.UpdateProductDescription(id, description);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(true, result);
            Product product = productService.GetProductById(id);
            Assert.AreEqual(product.Description, description);
        }

        [TestMethod]
        public void AddProductNull()
        {
            Product product = null;
            ProductService productService = new ProductService();

            try
            {
                productService.AddProduct(product);
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("Product is null", e.Message);
            }
        }

        [TestMethod]
        public void UpdateProductDuplicateDescription()
        {
            Product product = new Product();
            product.Name = "product";
            product.Description = "Description";
            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCategoryById(1);
            product.Categories.Add(category);
            ProductService productService = new ProductService();
            productService.AddProduct(product);

            String desc = "abcdefghijabcdefghijabcdefghij";
            int id = 8;

            Boolean result = false;
            try
            {
                result = productService.UpdateProductDescription(id, desc);
            }
            catch (DuplicateException exc)
            {
                Assert.AreEqual("The same product already exists - same name, description and category", exc.Message);
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void DeleteProductInexistent()
        {
            int id = 100;
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.DeleteProduct(id);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Product does not exists!", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void DeleteProductExistent()
        {
            int id = 8;
            ProductService productService = new ProductService();
            Boolean result = false;
            result = productService.DeleteProduct(id);
            Assert.AreEqual(result, true);
        }
        /*
                [TestMethod]
                public void deleteExistentProductDependency()
                {
                    int id = 9;
                    ProductService productService = new ProductService();
                    CategoryService categoryService = new CategoryService();
                    Product p = new Product();
                    p.Name = "Prod";
                    p.Description = "Desc";
                    p.Categories.Add(categoryService.GetCategoryById(1));
                    p.Auction = new Auction();
                    Boolean result = false;
                    productService.AddProduct(p);
                    try
                    {
                        result = productService.DeleteProduct(id);
                    }
                    catch (DependencyException e)
                    {
                        Assert.AreEqual("The product has auctions. It cannot be deleted!", e.Message);
                    }
                    Assert.AreEqual(result, false);
                }*/

        [TestMethod]
        public void GetProductsOfACategory()
        {
            CategoryService categoryService = new CategoryService();
            ProductService productService = new ProductService();
            Category category = categoryService.GetCategoryById(7);
            ICollection<Product> products = productService.GetAllProductsOfACategory(category);
            Assert.AreEqual(products.Count(), 2);
        }

        [TestMethod]
        public void GetAuctionOfAProduct()
        {
            ProductService productService = new ProductService();
            Product product = productService.GetProductById(1);
            Auction auction = productService.GetAuctionOfAProduct(product);
            Assert.IsNull(auction);
        }

        [TestMethod]
        public void GetProductExistentById()
        {
            ProductService productService = new ProductService();
            Product product = productService.GetProductById(1);
            Assert.AreEqual(product.Description, "Description");
        }

        [TestMethod]
        public void GetProductInexistentById()
        {
            ProductService productService = new ProductService();
            Product product = productService.GetProductById(100);
            Assert.IsNull(product);
        }

        [TestMethod]
        public void GetProductByNameAndDescription()
        {
            ProductService productService = new ProductService();
            ICollection<Product> products = productService.GetProductsByNameAndDescription("Product", "");
            Assert.AreEqual(products.Count(), 1);
        }

        [TestMethod]
        public void GetProductByNameAndDescriptionZero()
        {
            ProductService productService = new ProductService();
            ICollection<Product> products = productService.GetProductsByNameAndDescription("", "");
            Assert.AreEqual(products.Count(), 0);
        }

        [TestMethod]
        public void GetProductByNameAndDescriptionNull()
        {
            ProductService productService = new ProductService();
            ICollection<Product> products = productService.GetProductsByNameAndDescription(null, null);
            Assert.AreEqual(products.Count(), 0);
        }
    }
}
