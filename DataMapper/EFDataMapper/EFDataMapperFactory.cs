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
    }
}
