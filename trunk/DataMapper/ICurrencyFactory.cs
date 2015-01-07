using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface ICurrencyFactory
    {
        Currency getCurrencyById(int id);
        bool AddCurrency(String name);
        Currency GetCurrencyByName(String name);
    }
}
