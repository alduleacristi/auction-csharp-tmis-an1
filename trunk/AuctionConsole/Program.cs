using DataMapper.EFDataMapper;
using DomainModel;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionConsole
{
    class Program
    {
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

            Console.WriteLine("Hello world");
            CategoryService categService = new CategoryService();
            Category parent = categService.GetCategoryById(1);
            //Category parent = new Category();
            //parent.Name = "Name";
            categService.AddCategory(new Category() { Name = "Abcd", Description="Description", ParentCategory=parent });
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
