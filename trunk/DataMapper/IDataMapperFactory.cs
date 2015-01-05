using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IDataMapperFactory
    {
        IRoleFactory RoleFactory
        {
            get;
        }

        ICategoryFactory CategoryFactory
        {
            get;
        }

        IProductFactory ProductFactory
        {
            get;
        }

        IUserFactory UserFactory
        {
            get;
        }

        IConfigurationFactory ConfigurationFactory
        {
            get;
        }

        IAuctionFactory AuctionFactory
        {
            get;
        }


        IProductAuctionFactory ProductAuctionFactory
        {
            get;
        }

        ICurrencyFactory CurrencyFactory
        {
            get;
        }
    }
}
