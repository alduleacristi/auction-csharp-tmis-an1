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
    
    public partial class Currency
    {
        public Currency()
        {
            this.Auctions = new HashSet<Auction>();
            this.ProductActions = new HashSet<ProductAuction>();
        }
    
        public int IdCurrency { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<ProductAuction> ProductActions { get; set; }
    }
}