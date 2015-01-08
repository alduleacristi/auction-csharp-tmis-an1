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
    public class RoleTests
    {
        private IRoleService roleService;
        private IUserService userService;

        public RoleTests()
        {
            roleService = new RoleService();
            userService = new UserService();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddRoleNull()
        {
            roleService.AddRole(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddRoleWith1Character()
        {
            roleService.AddRole(new Role { Name = "A" });
        }

        [TestMethod]
        public void TestAddRoleWith3Character()
        {
            bool ok = roleService.AddRole(new Role { Name = "AAA" });

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestAddRoleWith30Character()
        {
            bool ok = roleService.AddRole(new Role { Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" });

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestAddRoleWith31Character()
        {
            bool ok = roleService.AddRole(new Role { Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" });
        }

        [TestMethod]
        public void TestAddValidRole()
        {
            bool ok = roleService.AddRole(new Role { Name = "AAAA" });

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestAddDuplicateRole()
        {
            roleService.AddRole(new Role { Name = "AAAA" });
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdateRoleWhichDoesNotExist()
        {
            roleService.UpdateRole("U", "X");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateRoleToANameWith1Character()
        {
            roleService.UpdateRole("AAA", "B");
        }

        [TestMethod]
        public void TestUpdateRoleToANameWith3Character()
        {
            bool ok = roleService.UpdateRole("AAA", "BBB");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestUpdateRoleToANameWith30Character()
        {
            bool ok = roleService.UpdateRole("BBB", "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestUpdateRoleToANameWith31Character()
        {
            bool ok = roleService.UpdateRole("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB", "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
        }

        [TestMethod]
        public void TestUpdateToAValidRole()
        {
            bool ok = roleService.UpdateRole("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB", "CCCC");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestUpdateToADuplicateRole()
        {
            bool ok = roleService.UpdateRole("CCCC", "AAAA");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestGetARoleThatExist()
        {
            bool ok = roleService.AddRole(new Role() { Name = "DDDD" });
            Role role = roleService.GetRoleByName("DDDD");

            Assert.IsNotNull(role);
            Assert.AreEqual("DDDD", role.Name);
        }

        [TestMethod]
        public void TestGetARoleThatDoesNotExist()
        {
            Role role = roleService.GetRoleByName("U");

            Assert.IsNull(role);
        }

        [TestMethod]
        public void TestDropRole()
        {
            bool ok = roleService.AddRole(new Role() { Name = "EEEE" });
            ok = roleService.DropRole("EEEE");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestDropRoleWhichDoesNotExist()
        {
            bool ok = roleService.DropRole("X");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void TestGetRolesFromAnUser()
        {
            User user = new User();
            user.FirstName = "AAA";
            user.LastName = "BBB";
            user.Email = "aaa@bbb.com";

            userService.AddUser(user);

            ICollection<Role> roles = roleService.GetRolesFromAnUser(userService.GetUserByEmail("aaa@bbb.com"));
            Assert.AreEqual(0, roles.Count);
        }

        [TestMethod]
        public void TestGetRolesFromAnUserWithRoles()
        {
            User user = new User();
            user.FirstName = "AAA";
            user.LastName = "BBB";
            user.Email = "dddd@eeee.com";

            userService.AddUser(user);

            Role role = new Role();
            role.Name = "actioneer";
            roleService.AddRole(role);

            userService.AddRoleToUser("dddd@eeee.com", role);

            ICollection<Role> roles = roleService.GetRolesFromAnUser(userService.GetUserByEmail("dddd@eeee.com"));
            Assert.AreEqual(1, roles.Count);
        }
    }
}
