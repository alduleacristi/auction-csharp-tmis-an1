using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DomainModel
{
    public partial class AuctionModelContainer
    {
        public void setLazyFalse()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
