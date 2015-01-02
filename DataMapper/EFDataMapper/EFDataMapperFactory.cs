using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.EFDataMapper
{
    public class EFDataMapperFactory:IDataMapperFactory
    {
        public IRoleFactory RoleFactory
        {
            get
            {
                return new EFRoleFactory();
            }
        }

        public ICategoryFactory CategoryFactory
        {
            get
            {
                return new EFCategoryFactory();
            }
        }

        public IProductFactory ProductFactory
        {
            get
            {
                return new EFProductFactory();
            }
        }







 public IUserFactory UserFactory
        {
            get
            {
                return new EFUserFactory();
            }
        }

        public IConfigurationFactory ConfigurationFactory
        {
            get
            {
                return new EFConfigurationFactory();
            }
        }

        public IAuctionFactory AuctionFactory
        {
            get
            {
                return new EFAuctionFactory();
            }
        }    }
}
