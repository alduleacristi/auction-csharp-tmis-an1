//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.Categories = new HashSet<Category>();
            this.Products = new HashSet<Product>();
        }
    
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> IdParentCategory { get; set; }
    
        public virtual ICollection<Category> Categories { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
