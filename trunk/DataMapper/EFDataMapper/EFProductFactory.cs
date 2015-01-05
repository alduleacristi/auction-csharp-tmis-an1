using DataMapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.EFDataMapper;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DomainModel;
using System.Data.Entity.Validation;

namespace DataMapper.EFDataMapper
{
    class EFProductFactory : IProductFactory
    {
        public void AddProduct(Product product)
        {
            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                if (product.Categories.Count() == 0)
                {
                    throw new ValidationException("The product must have at least one category setted!");
                }

                ICollection<Product> products = this.GetProdctByNameAndDescription(product.Name, product.Description);
                //Console.WriteLine(products.Count());
                foreach (Category category in product.Categories)
                {
                    //Console.WriteLine("Category in product.Categories " + category.IdCategory);
                    foreach (Product p in products)
                    {
                        Product productAux = this.GetProdctById(p.IdProduct);
                        context.Products.Attach(productAux);
                        context.Entry(productAux).Collection(pAux => pAux.Categories).Load();
                        //Console.WriteLine("P in Products " + productAux.IdProduct + " " + productAux.Categories.Count());
                        foreach (Category pcateg in productAux.Categories)
                            {
                                //Console.WriteLine(category.Name);
                                //Console.WriteLine(pcateg.Name);
                                if (category.Name.Equals(pcateg.Name))
                                    throw new DuplicateException("The same product already exists - same name, description and category");
                            }
                    }
                }
                
                foreach (Category categ in product.Categories)
                    context.Categories.Attach(categ);
                context.Products.Add(product);
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    throw new ValidationException("Invalid product's name/description.");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public ICollection<Product> GetProdctByNameAndDescription(String name, String description)
        {
            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                return context.Products.
                    Where(product => product.Name.Equals(name) && product.Description.Equals(description)).
                    ToList();
                return (from product in context.Products
                                where product.Name.Equals(name) && product.Description.Equals(description)
                                select product).ToList();
            }
        }
        public Product GetProdctById(int id)
        {
            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                var productVar = (from product in context.Products
                                where product.IdProduct == id
                                select product).
                                FirstOrDefault();
                return productVar;
            }
        }
        public void UpdateProduct(int id, String newName)
        {

        }
        public void UpdateProductDescription(int id, String description)
        {

        }
        public void DeleteProduct(int id)
        {

        }

        public ICollection<Product> GetAllProductsOfACategory(Category category)
        {
            using (var context = new AuctionModelContainer())
            {
                context.Categories.Attach(category);
                context.Entry(category).Collection(cat => cat.Products).Load();

                ICollection<Product> products = category.Products;
                return products;
            }
        }

        public Auction GetAuctionOfAProduct(Product product)
        {
            using (var context = new AuctionModelContainer())
            {
                var auctionVar = (from auction in context.Auctions
                                  join productSel in context.Products on auction.Product.IdProduct equals product.IdProduct
                                  where productSel.IdProduct == product.IdProduct
                                  select auction).FirstOrDefault();


                 context.Auctions.Attach(auctionVar);
                 context.Entry(auctionVar).Reference(auc => auc.User).Load();

                return auctionVar;
            }
        }
    }
}
