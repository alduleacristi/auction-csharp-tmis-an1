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

        public AuctionTests()
        {
            auctionService = new AuctionService();
            userService = new UserService();
            productService = new ProductService();
            currencyService = new CurrencyService();
            categoryService = new CategoryService();
            roleService = new RoleService();
        }

        [TestMethod]
        public void AddLicitation()
        {
            User user1 = new User();
            user1.FirstName = "AAA";
            user1.LastName = "AAA";
            user1.Email = "a@a.a";

            Role role = new Role();
            role.Name = Constants.OWNER;

            Category category = new Category();
            category.Name = "category";
            category.Description = "AAAAA";
            categoryService.AddCategory(category);

            Product product = new Product();
            product.Name = "AAA";
            product.Description = "AAAAAAAA";
            product.Categories.Add(category);

            userService.AddUser(user1);
            productService.AddProduct(product);
            currencyService.AddCurrency("RON");
            Currency currency = currencyService.GetCurrencyByName("RON");
            roleService.AddRole(role);
            userService.AddRoleToUser("a@a.a", role);

            auctionService.AddNewAuction(user1, product, currency, 100, DateTime.Now, DateTime.Now);
        }
    }
}
