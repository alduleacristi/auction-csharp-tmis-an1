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

namespace UnitTestProject
{
    [TestClass]
    public class UserTests
    {
        private IUserService userService;
        private IRoleService roleService;
        private ICategoryService categoryService;
        private ICurrencyService currencyService;
        private IProductService productService;
        private IAuctionService auctionService;
        private IProductAuctionService productAuctionService;

        private Role role;
        private Product product;

        private void GetRole()
        {
            role = roleService.GetRoleByName("CCCC");
        }

        public UserTests()
        {
            userService = new UserService();
            roleService = new RoleService();
            categoryService = new CategoryService();
            currencyService = new CurrencyService();
            productService = new ProductService();
            auctionService = new AuctionService();
            productAuctionService = new ProductAuctionService();

            GetRole();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddUserNull()
        {
            userService.AddUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddUserWith1CharacterFirstName()
        {
            userService.AddUser(new User { FirstName = "A" });
        }

        [TestMethod]
        public void TestAddUserWith3CharacterFirstName()
        {
            bool ok = userService.AddUser(new User { FirstName = "AAA", LastName = "AAA", Email = "a@b.com" });

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestAddUserWith30CharacterFirstName()
        {
            bool ok = userService.AddUser(new User { FirstName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", LastName = "AAA", Email = "aa@bb.com" });

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddUserWith31CharacterFirstName()
        {
            bool ok = userService.AddUser(new User { FirstName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" });
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddUserWith1CharacterLastName()
        {
            userService.AddUser(new User { LastName = "A" });
        }

        [TestMethod]
        public void TestAddUserWith3CharacterLastName()
        {
            bool ok = userService.AddUser(new User { FirstName = "BBB", LastName = "BBB", Email = "ab@bc.com" });

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestAddUserWith30CharacterLastName()
        {
            bool ok = userService.AddUser(new User { FirstName = "AAA", LastName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", Email = "abc@bcd.com" });

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddUserWith31CharacterLastName()
        {
            bool ok = userService.AddUser(new User { LastName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" });
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestAddUserWithDuplicateEmailException()
        {
            bool ok = userService.AddUser(new User { LastName = "AAA", FirstName = "BBB", Email = "ab@bc.com" });
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddUserWithNullEmailException()
        {
            bool ok = userService.AddUser(new User { LastName = "AAA", FirstName = "BBB", Email = null });
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddUserWithInvalidEmailException()
        {
            bool ok = userService.AddUser(new User { LastName = "AAA", FirstName = "BBB", Email = null });
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateFirstNameThatDoesNotExist()
        {
            userService.UpdateFirstName("x.y@yahoo.com", "X");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateFirstNameWith1Character()
        {
            userService.UpdateFirstName("ab@bc.com", "A");
        }

        [TestMethod]
        public void TestUpdateFirstNameWith3Character()
        {
            bool ok = userService.UpdateFirstName("ab@bc.com", "AAA");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestUpdateFirstNameValid()
        {
            bool ok = userService.UpdateFirstName("ab@bc.com", "ABC");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateLastNameThatDoesNotExist()
        {
            userService.UpdateLastName("x.y@yahoo.com", "Y");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateLastNameWith1Character()
        {
            userService.UpdateLastName("ab@bc.com", "A");
        }

        [TestMethod]
        public void TestUpdateLastNameWith3Character()
        {
            bool ok = userService.UpdateLastName("ab@bc.com", "CCC");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestUpdateLastNameValid()
        {
            bool ok = userService.UpdateLastName("ab@bc.com", "DDD");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateEmailThatDoesNotExist()
        {
            userService.UpdateEmail("x.y@yahoo.com", "Y");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateEmailInvalid()
        {
            userService.UpdateEmail("ab@bc.com", "Y");
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestUpdateEmailvalidDuplicate()
        {
            bool ok = userService.UpdateEmail("ab@bc.com", "aaa@bbb.com");
        }

        [TestMethod]
        public void TestUpdateEmailvalid()
        {
            bool ok = userService.UpdateEmail("ab@bc.com", "de@fg.com");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropUserWhichDoesNotExist()
        {
            userService.DropUser("aaaa");
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyException))]
        public void TestDropUserWhichHasRoleAsigned()
        {
            userService.DropUser("dddd@eeee.com");
        }

        [TestMethod]
        public void TestDropUser()
        {
            userService.DropUser("ab@bc.com");
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestAddRoleToUserWhichDoesNotExist()
        {
            bool ok = userService.AddRoleToUser("x@y.com", role);
        }

        [TestMethod]
        public void TestAddRoleToUser()
        {
            bool ok = userService.AddRoleToUser("a@b.com", role);

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestremoveRoleFromUserWhichDoesNotExist()
        {
            bool ok = userService.RemoveRoleFromUser("x@y.com", role);

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestremoveRoleFromUserWhichDoesNotHaveRoles()
        {
            bool ok = userService.RemoveRoleFromUser("aa@bb.com", role);

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestremoveRoleFromUser()
        {
            bool ok = userService.RemoveRoleFromUser("a@b.com", role);

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestGetUserByEmailWhichDoesNotExist()
        {
            User user = userService.GetUserByEmail("x@y.com");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void TestGetUserByEmailInvalid()
        {
            User user = userService.GetUserByEmail("c");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void TestGetUserByEmail()
        {
            User user = userService.GetUserByEmail("a@b.com");

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestGetUserById()
        {
            User user = userService.GetUserById(1);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestGetUserByIdWhichDoesNotExist()
        {
            User user = userService.GetUserById(99999);

            Assert.IsNull(user);
        }

        [TestMethod]
        public void TestAddNote()
        {
            User user1 = new User();
            user1.FirstName = "AAA";
            user1.LastName = "AAA";
            user1.Email = "aa@aa.a";

            User user2 = userService.GetUserByEmail("aaa@bbb.com");
            Category category = categoryService.GetCategoryByName("category");

            Product product = new Product();
            product.Name = "BBBB";
            product.Description = "BBBB";
            product.Categories.Add(category);

            userService.AddUser(user1);
            productService.AddProduct(product);
            //currencyService.AddCurrency("RON");
            Currency currency = currencyService.getCurrencyById(1);
            Role role = roleService.GetRoleByName(Constants.ACTIONEER);
            userService.AddRoleToUser("aa@aa.a", role);

            auctionService.AddNewAuction(user2, product, currency, 100, DateTime.Now, new DateTime(2015, 10, 18));
            productAuctionService.AddProductAuction(user1, product, 101, currency);
            
            bool ok = userService.AddNoteToUser(user1,user2,10);

            Assert.IsTrue(ok);
        }
    }
}
