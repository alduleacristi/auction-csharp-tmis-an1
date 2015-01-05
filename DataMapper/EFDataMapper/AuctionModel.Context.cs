﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AuctionModelContainer : DbContext
    {
        public AuctionModelContainer()
            : base("name=AuctionModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<ProductAuction> ProductAuctions { get; set; }
    
        public virtual ObjectResult<categories_FindChildren_Result> categories_FindChildren(Nullable<int> parent_id)
        {
            var parent_idParameter = parent_id.HasValue ?
                new ObjectParameter("parent_id", parent_id) :
                new ObjectParameter("parent_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<categories_FindChildren_Result>("categories_FindChildren", parent_idParameter);
        }
    
        public virtual ObjectResult<categories_GetCatParentsNew_Result> categories_GetCatParentsNew(Nullable<int> lCategoryID)
        {
            var lCategoryIDParameter = lCategoryID.HasValue ?
                new ObjectParameter("lCategoryID", lCategoryID) :
                new ObjectParameter("lCategoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<categories_GetCatParentsNew_Result>("categories_GetCatParentsNew", lCategoryIDParameter);
        }
    }
}
