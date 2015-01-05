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
    public class RoleTests
    {
        MockRepository mockRepo = null;

        public RoleTests()
        {
            mockRepo = new MockRepository();
        }

       /* [TestMethod]
        public void TestAddRole()
        {
            RoleService roleService = new RoleService();
            roleService.AddRole(new Role() { Name="Admin"});
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateException))]
        public void TestGetRoleByNameWithException()
        {
            RoleService roleService = new RoleService();
            roleService.AddRole(new Role { Name = "Admin" });
            Role role = roleService.GetRoleByName("admin");

            Assert.IsNotNull(role);
        }

        [TestMethod]
        public void TestGetRoleByName()
        {
            RoleService roleService = new RoleService();
            roleService.AddRole(new Role { Name = "User" });
            Role role = roleService.GetRoleByName("admin");

            Assert.IsNotNull(role);
        }*/

        [TestMethod]
        public void TestRoleValidation()
        {
            Role role = new Role() { Name = "ABCDE"};
            
            var validationResults = Validation.Validate<Role>(role);
            //Assert.IsTrue(validationResults.IsValid);
        }
    }
}
