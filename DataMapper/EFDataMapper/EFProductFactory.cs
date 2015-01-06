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
                foreach (Category category in product.Categories)
                {
                    foreach (Product p in products)
                    {
                        Product productAux = this.GetProdctById(p.IdProduct);
                        context.Products.Attach(productAux);
                        context.Entry(productAux).Collection(pAux => pAux.Categories).Load();
                        foreach (Category pcateg in productAux.Categories)
                            {
                                if (category.Name.Equals(pcateg.Name))
                                    throw new DuplicateException("The same product already exists - same name, description and category");
                            }
                    }
                }
                
                foreach (Category categ in product.Categories)
                    context.Categories.Attach(categ);
                context.Products.Add(product);
                context.SaveChanges();
                
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
            }
        }
        public Product GetProdctById(int id)
        {
            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                var prodVar = (from product in context.Products
                                where product.IdProduct == id
                                select product).FirstOrDefault();
                if (prodVar != null)
                {
                    context.Products.Attach(prodVar);
                    context.Entry(prodVar).Collection(pAux => pAux.Categories).Load();
                    //context.Entry(auctionVar).Collection(pAux => pAux.Categories).Load();
                    context.Entry(prodVar).Reference(pAux => pAux.Auction).Load();
                    if (prodVar.Auction != null)
                    {
                        context.Entry(prodVar.Auction).Collection(p => p.ProductActions).Load();
                        context.Entry(prodVar.Auction).Reference(c => c.Currency).Load();
                    }
                }
                return prodVar;
            }
        }
        public void UpdateProduct(Product product, String newName)
        {
                using (var context = new AuctionModelContainer())
                {
                    Console.WriteLine("Verify duplication");
                    ICollection<Product> products = this.GetProdctByNameAndDescription(newName, product.Description);
                    foreach (Category category in product.Categories)
                    {
                        foreach (Product p in products)
                        {
                            Product productAux = this.GetProdctById(p.IdProduct);
                            context.Products.Attach(productAux);
                            context.Entry(productAux).Collection(pAux => pAux.Categories).Load();
                            foreach (Category pcateg in productAux.Categories)
                            {
                                Console.WriteLine(category.Name + " " + pcateg.Name);
                                if (category.Name.Equals(pcateg.Name))
                                {
                                    Console.WriteLine("Duplication exception");
                                    throw new DuplicateException("The same product already exists - same name, description and category");
                                }
                            }
                        }
                    }
                    product.Name = newName;
                    context.Products.Attach(product);
                    var entry = context.Entry(product);
                    entry.Property(r => r.Name).IsModified = true;
                    context.SaveChanges();
                }
        }
        public void UpdateProductDescription(Product product, String description)
        {
                using (var context = new AuctionModelContainer())
                {
                    ICollection<Product> products = this.GetProdctByNameAndDescription(product.Name, description);
                    foreach (Category category in product.Categories)
                    {
                        foreach (Product p in products)
                        {
                            Product productAux = this.GetProdctById(p.IdProduct);
                            context.Products.Attach(productAux);
                            context.Entry(productAux).Collection(pAux => pAux.Categories).Load();
                            foreach (Category pcateg in productAux.Categories)
                            {
                                if (category.Name.Equals(pcateg.Name))
                                    throw new DuplicateException("The same product already exists - same name, description and category");
                            }
                        }
                    }

                    product.Description = description;
                    context.Products.Attach(product);
                    var entry = context.Entry(product);
                    entry.Property(r => r.Description).IsModified = true;
                    context.SaveChanges();
                }
        }
        public void DeleteProduct(int id)
        {
            Product product = this.GetProdctById(id);
            if (product == null)
            {
                throw new EntityDoesNotExistException("Product does not exists!");
            }

            using (var context = new AuctionModelContainer())
            {
                context.Products.Attach(product);
                if(product.Auction!=null)
                    context.Entry(product).Collection(prod => prod.Auction.ProductActions).Load();
                if (product.Auction != null)
                {
                    if (product.Auction.ProductActions.Count() > 0)
                    {
                        throw new DependencyException("The product has auctions. It cannot be deleted!");
                    }
                }
                context.Products.Attach(product);
                context.Products.Remove(product);
                context.SaveChanges();
            }
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
