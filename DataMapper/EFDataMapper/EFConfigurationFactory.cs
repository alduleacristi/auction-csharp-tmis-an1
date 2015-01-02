using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.EFDataMapper
{
    public class EFConfigurationFactory:IConfigurationFactory
    {
        private IDictionary<string,int> GetConfigurationFromDatabase()
        {
            IDictionary<string,int> configs = new Dictionary<string,int>();
            
            using(var context = new AuctionModelContainer())
            {
                var confVars = (from conf in context.Configurations
                               select conf).ToList();

                foreach (Configuration c in confVars)
                    configs.Add(c.Key, c.Value);
            }

            return configs;
        }
        public IDictionary<string,int> GetConfigurations()
        {
            return GetConfigurationFromDatabase();
        }
    }
}
