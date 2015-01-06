using DataMapper.Exceptions;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.EFDataMapper
{
    public class EFCurrencyFactory: ICurrencyFactory
    {
        public Currency getCurrencyById(int id)
        {
            using (var context = new AuctionModelContainer())
            {
                var currencyVar = (from currency in context.Currencies
                                where currency.IdCurrency == id
                                select currency).FirstOrDefault();
                return currencyVar;
            }
        }

        public bool AddCurrency(String name)
        {
            Currency currency = new Currency();
            currency.Name = name;
            using (var context = new AuctionModelContainer())
            {
                context.Currencies.Add(currency);
                context.SaveChanges();
            }
            return true;
        }
    }
}
