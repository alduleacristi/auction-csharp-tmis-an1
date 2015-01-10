using DataMapper.Exceptions;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTests
{
    [TestClass]
    public class AuctionTests
    {
        private IAuctionService auctionService;
        private IUserService userService;
        private IProductService productService;
        private ICurrencyService currencyService;
        private ICategoryService categoryService;
        private IRoleService roleService;
        private IProductAuctionService productAuctionService;

        public AuctionTests()
        {
            auctionService = new AuctionService();
            userService = new UserService();
            productService = new ProductService();
            currencyService = new CurrencyService();
            categoryService = new CategoryService();
            roleService = new RoleService();
            productAuctionService = new ProductAuctionService();
        }

        [TestMethod]
        public void TestAddLicitation()
        {
            User user1 = new User();
            user1.FirstName = "AAA";
            user1.LastName = "AAA";
            user1.Email = "a@a.a";

            User user2 = new User();
            user2.FirstName = "BBB";
            user2.LastName = "BBB";
            user2.Email = "bau@bau.b";

            Role role1 = new Role();
            role1.Name = Constants.OWNER;
            Role role2 = new Role();
            role2.Name = Constants.ACTIONEER;

            Category category = new Category();
            category.Name = "category";
            category.Description = "AAAAA";
            categoryService.AddCategory(category);

            Product product = new Product();
            product.Name = "AAA";
            product.Description = "AAAAAAAA";
            product.Categories.Add(category);

            userService.AddUser(user1);
            userService.AddUser(user2);

            productService.AddProduct(product);
            currencyService.AddCurrency("RON");
            Currency currency = currencyService.GetCurrencyByName("RON");
            roleService.AddRole(role1);
            roleService.AddRole(role2);
            userService.AddRoleToUser("a@a.a", role1);
            userService.AddRoleToUser("bau@bau.b", role2);

            auctionService.AddNewAuction(user1, product, currency, 100, DateTime.Now, DateTime.Now);
            productAuctionService.AddProductAuction(user2, product, 200, currency);
            userService.AddNoteToUser(user2, user1, 7);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddAuctionWithNegativeStartPrice()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA7";
            product.Description = "AAAAAAAA4";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, -1.0, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddAuctionWithStartPriceZero()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA8";
            product.Description = "AAAAAAAA8";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 0, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        public void TestAddAuctionWithStartPriceZeroOne()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA9";
            product.Description = "AAAAAAAA9";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 0.1, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        public void TestAddAuctionWithStartPriceOneBillion()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA10";
            product.Description = "AAAAAAAA10";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 1000000, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddAuctionWithStartPriceTwoBillions()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA11";
            product.Description = "AAAAAAAA11";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 200000000, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddAuctionWithInvalidDates()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA12";
            product.Description = "AAAAAAAA12";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 1000, new DateTime(2015,01,10), new DateTime(2014,10,10));
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestAddLicitationWithUserThatDoesNotExist()
        {
            User user = userService.GetUserById(5);
            Product product = productService.GetProductById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            auctionService.AddNewAuction(user, product, currency, 100, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        public void TestAddNewAuctionByAUserWithGoodRating()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA2";
            product.Description = "AAAAAAAA2";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 100, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        public void TestAddNewAuctionByAUserWithBadRating()
        {
            User user1 = userService.GetUserById(1);
            User user2 = new User();
            user2.FirstName = "CCC";
            user2.LastName = "CCC";
            user2.Email = "cau@cau.c";
            userService.AddUser(user2);
            userService.AddRoleToUser("cau@cau.c", roleService.GetRoleByName(Constants.ACTIONEER));

            Category category = categoryService.GetCategoryById(1);
            Product product = new Product();
            product.Name = "AAA3";
            product.Description = "AAAAAAAA3";
            product.Categories.Add(category);
            productService.AddProduct(product);

            Currency currency = currencyService.GetCurrencyByName("RON");
            auctionService.AddNewAuction(user1, product, currency, 100, DateTime.Now, DateTime.Now);
            
            productAuctionService.AddProductAuction(user2, product, 500, currency);
            userService.AddNoteToUser(user2, user1, 1);

            product = new Product();
            product.Name = "AAA4";
            product.Description = "AAAAAAAA4";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user1, product, currency, 100, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        public void TestAddNewAuctionByAUserAfterResetRating()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA5";
            product.Description = "AAAAAAAA5";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 100, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(AuctionException))]
        public void TestAddMaxNumberOfAuctionOnCategory()
        {
            User user = userService.GetUserById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            Category category = categoryService.GetCategoryById(1);

            Product product = new Product();
            product.Name = "AAA6";
            product.Description = "AAAAAAAA6";
            product.Categories.Add(category);
            productService.AddProduct(product);

            auctionService.AddNewAuction(user, product, currency, 100, DateTime.Now, DateTime.Now);
        }

        

        [TestMethod]
        [ExpectedException(typeof(AuctionException))]
        public void TestAddLicitationWithUserThatIsNotOwner()
        {
            User user = userService.GetUserById(1);
            userService.RemoveRoleFromUser(user.Email, roleService.GetRoleByName(Constants.OWNER));

            Product product = productService.GetProductById(1);
            Currency currency = currencyService.GetCurrencyByName("RON");

            auctionService.AddNewAuction(user, product, currency, 100, DateTime.Now, DateTime.Now);
        }

        [TestMethod]
        public void TestGetNumberOfActiveLicitation()
        {
            int nr = auctionService.GetNumberOfActiveAuctionsStartedByUser(userService.GetUserById(1));

            Assert.AreEqual(7, nr);
        }

        [TestMethod]
        public void TestGetAuctionByAValidId()
        {
            Auction auction = auctionService.GetAuctionById(1);

            Assert.IsNotNull(auction);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestGetAuctionByAInvalidId()
        {
            Auction auction = auctionService.GetAuctionById(1000000);
        }
    }
}
