using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class RoleService:IRoleService
    {
        private AuctionLogger logger;

        public RoleService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public void AddRole(Role role)
        {
            logger.logInfo("Try to add a new role.");

            try
            { 
                DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.AddRole(role);
            }
            catch(ValidationException validationException)
            {
                Console.WriteLine("Exceptie de validare");
                logger.logError(validationException);
            }
        }

        public Role GetRoleByName(String name)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.GetRoleByName(name);
        }
    }
}
