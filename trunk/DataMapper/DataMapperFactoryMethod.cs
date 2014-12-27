using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DataMapper.EFDataMapper;

namespace DataMapper
{
    public static class DataMapperFactoryMethod
    {
        private static readonly string factoryType;

        static DataMapperFactoryMethod()
        {
            factoryType = ConfigurationManager.AppSettings["factoryType"];
        }

        public static IDataMapperFactory GetCurrentFactory()
        {
            switch (factoryType.Trim().ToLower())
            {
                case "ef": return new EFDataMapperFactory();
                default:
                    throw new NotImplementedException("cannot find mapper: " + factoryType);
            }
        }
    }
}
