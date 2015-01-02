using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.EFDataMapper
{
    class EFDataMapperFactory:IDataMapperFactory
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
    }
}
