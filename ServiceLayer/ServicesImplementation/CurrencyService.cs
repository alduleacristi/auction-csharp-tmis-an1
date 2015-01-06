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
    public class CurrencyService : ICurrencyService
    {
       private AuctionLogger logger;
        public CurrencyService()
        {
            logger = AuctionLogger.GetInstance();
        }
        public Currency getCurrencyById(int id)
        {
            logger.logInfo("Try to get currency by id from the db.");
            return DataMapperFactoryMethod.GetCurrentFactory().CurrencyFactory.getCurrencyById(id);
        }

        public bool AddCurrency(String name)
        {
            logger.logInfo("Try to add a new currency "+name);
            return DataMapperFactoryMethod.GetCurrentFactory().CurrencyFactory.AddCurrency(name);
        }
    }
}
