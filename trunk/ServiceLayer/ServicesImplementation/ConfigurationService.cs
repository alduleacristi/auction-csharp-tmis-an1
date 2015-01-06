using DataMapper;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ConfigurationService:IConfiguration
    {
        private static ConfigurationService instance;
        private IDictionary<string, int> configs;
        public static ConfigurationService GetInstance()
        {
            if (instance == null)
                instance = new ConfigurationService();

            return instance;
        }

        private void VerifyAndSetDefault()
        {
            if(!configs.ContainsKey(Constants.MAX_NR_OF_AUCTION_ASSOCIATE_WITH_CATEGORY))
                configs.Add(Constants.MAX_NR_OF_AUCTION_ASSOCIATE_WITH_CATEGORY,30);

            if (!configs.ContainsKey(Constants.MAX_NR_OF_STARTED_AUCTION))
                configs.Add(Constants.MAX_NR_OF_STARTED_AUCTION, 90);

            if (!configs.ContainsKey(Constants.NR_OF_DAY_BEFORE_RATING_RESET))
                configs.Add(Constants.NR_OF_DAY_BEFORE_RATING_RESET, 30);

            if (!configs.ContainsKey(Constants.NR_OF_DAYS_USED_TO_DETERMINE_RATING))
                configs.Add(Constants.NR_OF_DAYS_USED_TO_DETERMINE_RATING, 30);

            if (!configs.ContainsKey(Constants.RATING_THRESH_HOLD_FOR_AUCTION))
                configs.Add(Constants.RATING_THRESH_HOLD_FOR_AUCTION, 4);
        }
        private ConfigurationService()
        {
            configs = DataMapperFactoryMethod.GetCurrentFactory().ConfigurationFactory.GetConfigurations();
            VerifyAndSetDefault();
        }

        public int GetValue(string key)
        {
            int value;
            configs.TryGetValue(key,out value);

            return value;
        }
    }
}
