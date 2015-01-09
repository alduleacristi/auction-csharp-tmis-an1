﻿using DataMapper.EFDataMapper;
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
            User user = userService.GetUserById(1);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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

            User user = userService.GetUserById(3);
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
            userService.AddRoleToUser("dddd@eeee.com", role);

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
            userService.AddRoleToUser("aaa@bbb.com", role);

            User user = userService.GetUserById(2);
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

            User user = userService.GetUserById(3);
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

        [TestMethod]
        public void deleteExistentProductDependency()
        {

            int id = 1;
            ProductService productService = new ProductService();
            Boolean result = false;
            try
            {
                result = productService.DeleteProduct(id);
            }
            catch (DependencyException e)
            {
                Assert.AreEqual("The product has auctions. It cannot be deleted!", e.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestFinishAuctionNullUser()
        {
            ProductService productService = new ProductService();
            ProductAuctionService productAuctionService = new ProductAuctionService();

            Product product = productService.GetProductById(1);
            Boolean result = false;

            try
            {
                productAuctionService.closeAuction(null, product);
            }
            catch(EntityDoesNotExistException exc)
            {
                Assert.AreEqual("User is null", exc.Message);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestFinishAuctionNullProduct()
        {
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();

            User user = userService.GetUserById(1);
            Boolean result = false;

            try
            {
                productAuctionService.closeAuction(user, null);
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Product is null", exc.Message);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestFinishAuctionNullAuction()
        {
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();

            User user = userService.GetUserById(1);
            Boolean result = false;

            try
            {
                productAuctionService.closeAuction(user, new Product());
            }
            catch (EntityDoesNotExistException exc)
            {
                Assert.AreEqual("Auction is null", exc.Message);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestFinishAuctionNotTheSameUser()
        {
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            ProductService productService = new ProductService();

            User user = userService.GetUserById(2);
            Product product = productService.GetProductById(1);
            Boolean result = false;

            try
            {
                productAuctionService.closeAuction(user, product);
            }
            catch (AuctionException exc)
            {
                Assert.AreEqual("You are not allowed to close the auction - you are not the owner!", exc.Message);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestFinishAuction()
        {
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            ProductService productService = new ProductService();

            User user = userService.GetUserById(1);
            Product product = productService.GetProductById(1);
            Boolean result = false;

            try
            {
                result = productAuctionService.closeAuction(user, product);
            }
            catch (AuctionException exc)
            {
                Assert.AreEqual("", exc.Message);
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFinishAuctionAlreadyFinished()
        {
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            ProductService productService = new ProductService();

            User user = userService.GetUserById(1);
            Product product = productService.GetProductById(1);
            Boolean result = false;

            try
            {
                result = productAuctionService.closeAuction(user, product);
            }
            catch (AuctionException exc)
            {
                Assert.AreEqual("Auction already closed", exc.Message);
            }
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TestAddProductAuctionInvalidDate()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(3);
            Product product = productService.GetProductById(1);
            Double price = 101;
            Currency currency = currencyService.getCurrencyById(1);

            product.Auction.EndDate = DateTime.Today;

            Boolean result = false;
            try
            {
                result = productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (ValidationException exc)
            {
                Assert.AreEqual("Cannot add a new auction for the selected product because it is expired", exc.Message);
            }
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void AddProductAuctionAlreadyFinished()
        {
            AuctionService auctionService = new AuctionService();
            ProductService productService = new ProductService();
            CurrencyService currencyService = new CurrencyService();
            UserService userService = new UserService();
            ProductAuctionService productAuctionService = new ProductAuctionService();
            RoleService roleService = new RoleService();

            User user = userService.GetUserById(3);
            Product product = productService.GetProductById(1);
            Double price = 101;
            Currency currency = currencyService.getCurrencyById(1);
            Boolean result = false;

            try
            {
                result = productAuctionService.AddProductAuction(user, product, price, currency);
            }
            catch (AuctionException exc)
            {
                Assert.AreEqual("Cannot add a new auction for the selected product because it is expired", exc.Message);
            }
            Assert.IsFalse(result);
        }

        
    }
}