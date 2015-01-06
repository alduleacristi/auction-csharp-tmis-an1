﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IProductFactory
    {
        void AddProduct(Product product);
        ICollection<Product> GetProdctByNameAndDescription(String name, String description);
        Product GetProdctById(int id);
        void UpdateProduct(Product product, String newName);
        void UpdateProductDescription(Product product, String description);
        void DeleteProduct(int id);
        ICollection<Product> GetAllProductsOfACategory(Category category);
        Auction GetAuctionOfAProduct(Product product);
    }
}