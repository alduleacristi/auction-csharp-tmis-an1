using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IAuctionFactory
    {
        void AddNewAuction(Auction auction);
        Currency GetCurrencyByName(String currencyName);
        int GetNumberOfActiveAuctionsStartedByUser(User user);
        Auction GetAuctionById(int id);
    }
}
