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
    //5 - owner
    //7 - actioneer
    //public void AddProductAuction(User user, Product product, double price, Currency currency)
    [TestClass]
    public class AuctionProductTests
    {
        [TestMethod]
        public void TestAddProductAuctionSameUser()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            Auction auction = auctionService.GetAuctionById(1);
            Role role = roleService.GetRoleByName("actioneer");
            userService.AddRoleToUser("a@a.a", role);
            User user = userService.GetUserById(5);
            Product product = productService.GetProductById(1);
            Double price = 100;
            Currency currency = currencyService.getCurrencyById(1);

            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The ownwer and the actioneer cannot be the same user", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionPrice0()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Product product = productService.GetProductById(1);
            Double price = 0;
            Currency currency = currencyService.getCurrencyById(1);

            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The price is too low", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionPriceMinus1()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Product product = productService.GetProductById(1);
            Double price = -1;
            Currency currency = currencyService.getCurrencyById(1);

            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The price is too low", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionPriceLowerThanStartPice()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Product product = productService.GetProductById(1);
            Double price = 1;
            Currency currency = currencyService.getCurrencyById(1);

            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The price is too low", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionPriceAsStartPice()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Product product = productService.GetProductById(1);
            Double price = 100;
            Currency currency = currencyService.getCurrencyById(1);

            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The price is too low", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionPriceHigherThanStartPice()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Product product = productService.GetProductById(1);
            Double price = 101;
            Currency currency = currencyService.getCurrencyById(1);

            Assert.IsNotNull(currency.IdCurrency);
            Assert.IsNotNull(product.Auction.Currency.IdCurrency);

            Assert.AreEqual(currency.IdCurrency, 1);
            Assert.AreEqual(product.Auction.Currency.IdCurrency, 1);
            
            Boolean result = false;
            try
            {
                result = productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void TestAddProductAuctionPriceTheSameAsTheLastAuction()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Product product = productService.GetProductById(1);
            Double price = 101;
            Currency currency = currencyService.getCurrencyById(1);

            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The price is too low", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionDifferentCurrency()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Product product = productService.GetProductById(1);
            Double price = 100;
            Currency currency = currencyService.getCurrencyById(2);

            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The auction must have the same currency as the product: " + product.Auction.Currency.Name, exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionNullProduct()
        {
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Double price = 200;
            Currency currency = currencyService.getCurrencyById(2);

            try
            {
                productAuctionService.AddProductAuction(user, null, price, currency);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Product is null", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionNullUser()
        {
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Double price = 200;
            Currency currency = currencyService.getCurrencyById(2);

            try
            {
                productAuctionService.AddProductAuction(null, new Product(), price, currency);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("User is null", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductAuctionOwner()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            
            Product product = productService.GetProductById(1);
            Double price = 200;
            Currency currency = currencyService.getCurrencyById(1);

            Role role = roleService.GetRoleByName("owner");
            userService.AddRoleToUser("ab@bc.com", role);

            User user = userService.GetUserById(3);
            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The user is not actioneer", exc.Message);
            }
        }

        [TestMethod]
        public void TestAddProductNullCurrency()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();


            Product product = productService.GetProductById(1);
            Double price = 200;
            Currency currency = currencyService.getCurrencyById(1);

            Role role = roleService.GetRoleByName("owner");
            userService.AddRoleToUser("ab@bc.com", role);

            User user = userService.GetUserById(3);
            try
            {
                productAuctionService.AddProductAuction(user, product, price, null);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Currency is null", exc.Message);
            }
        }


        [TestMethod]
        public void TestAddProductAuctionNullAuction()
        {
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(7);
            Double price = 101;
            Currency currency = currencyService.getCurrencyById(1);

            Boolean result = false;
            try
            {
                result = productAuctionService.AddProductAuction(user, new Product(), price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Cannot add a new auction for the selected product", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestAddProductAuctionUserWithNoRole()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();


            Product product = productService.GetProductById(1);
            Double price = 200;
            Currency currency = currencyService.getCurrencyById(1);

            User user = userService.GetUserById(2);
            try
            {
                productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("The user is not actioneer", exc.Message);
            }
        }
    }
}
