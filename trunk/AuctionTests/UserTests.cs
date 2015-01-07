using DataMapper.Exceptions;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTests
{
    [TestClass]
    public class UserTests
    {
        private IUserService userService;

        public UserTests()
        {
            userService = new UserService();
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
            bool ok = userService.AddUser(new User { FirstName = "AAA",LastName = "AAA",Email = "a@b.com" });

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

        ////

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

      /*  [TestMethod]
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
        }*/
    }
}
