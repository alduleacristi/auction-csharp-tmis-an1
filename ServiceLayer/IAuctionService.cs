using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IAuctionService
    {
        void AddNewAuction(User user,Product product,Currency currency,double startPrice,DateTime startDate,DateTime endDate);
        Currency GetCurrencyByName(String currencyName);
        int GetNumberOfActiveAuctionsStartedByUser(User user);
        Auction GetAuctionById(int id);
    }
}
