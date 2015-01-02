using DataMapper.EFDataMapper;
using DomainModel;
using ServiceLayer;
using ServiceLayer.Common;
using ServiceLayer.ServicesImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionConsole
{
    class Program
    {
        public static void AddUser()
        {
            /*User user = new User();
            user.FirstName = "Gigi";
            user.LastName = "Vasile";
            user.Email = "gligor.vanessa@gmail.com";*/

            IUserService userService = new UserService();
            IRoleService roleService = new RoleService();
            //userService.AddUser(user);
            //userService.UpdateLastName("alduleacristi@yahoo.com", "bau");
            //Role role = roleService.GetRoleByName("XYZ");
            //userService.AddRole(role);
            userService.UpdateEmail("alduleacristi@yahoo.com", "gligor.vanessa2@gmail.com");
        }

        public static void GetConf()
        {
            IConfiguration conf = ConfigurationService.GetInstance();
  
            Console.WriteLine(conf.GetValue(Constants.MAX_NR_OF_STARTED_AUCTION));
            Console.WriteLine(conf.GetValue(Constants.MAX_NR_OF_AUCTION_ASSOCIATE_WITH_CATEGORY));
            Console.WriteLine(conf.GetValue(Constants.NR_OF_DAY_BEFORE_RATING_RESET));
            Console.WriteLine(conf.GetValue(Constants.NR_OF_DAYS_USED_TO_DETERMINE_RATING));
            Console.WriteLine(conf.GetValue(Constants.RATING_THRESH_HOLD_FOR_AUCTION));
        }

        public static void AddAuction()
        {
            Auction auction = new Auction();
            User user = new User();
            Product product = new Product();
            auction.Product = product;

            IAuctionService auctionService = new AuctionService();
            auctionService.AddNewAuction(auction, user);
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello world");
            //RoleService roleService = new RoleService();
            //roleService.AddRole(new Role() { Name="ABCDE"});
            //Console.ReadKey();

            //Console.WriteLine("Add category");
            //CategoryService categoryService = new CategoryService();
            //categoryService.AddCategory(new Category() { Name="cccs", Description=""});
           
            //Console.ReadKey();

            /*Console.WriteLine("Hello world");
            CategoryService categService = new CategoryService();
            Category parent = categService.GetCategoryById(1);*/
            //Category parent = new Category();
            //parent.Name = "Name";
            //categService.AddCategory(new Category() { Name = "Abcd", Description="Description", ParentCategory=parent });



        // roleService.AddRole(new Role() { Name="12345"});

            //roleService.UpdateRole("ABCD", "XYZ");
            //roleService.DropRole("12345");

            //AddUser();
            GetConf();
            AddAuction();
            Console.ReadKey();

            //Console.WriteLine("Update category's name");
            //CategoryService categService = new CategoryService();
            //categService.UpdateCategory(10, "Categ");
            //Console.ReadKey();

            //Console.WriteLine("Update category's description");
            //CategoryService categService = new CategoryService();
            //categService.UpdateCategoryDescription(1, "124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890124567890");
            //Console.ReadKey();

           // Console.WriteLine("Delete Category");
            //CategoryService categService = new CategoryService();
            //categService.DeleteCategory(1);
            //Console.ReadKey();

            /*
            Console.WriteLine("Add Product");
            ProductService productService = new ProductService();
            CategoryService categService = new CategoryService();
            Category categ = categService.GetCategoryById(1);
            ICollection<Category> categs = new HashSet<Category>();
            if(categ!= null)
                categs.Add(categ);
            if (categ != null)
                categs.Add(categ);
            Console.WriteLine("Size: "+categs.Count());
            productService.AddProduct(new Product() { Name = "Product", Description = "Desc" , Categories = categs});
            Console.ReadKey();
             * */
        }
    }
}
