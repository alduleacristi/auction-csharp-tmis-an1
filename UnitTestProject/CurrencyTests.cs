using DataMapper.EFDataMapper;
using DataMapper.Exceptions;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UnitTestProject
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod]
        public void AddCurrency()
        {
            String name = "EURO";
            CurrencyService currencyService = new CurrencyService();
            Boolean result = currencyService.AddCurrency(name);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void GetCurrencyById()
        {
            int id = 2;
            CurrencyService currencyService = new CurrencyService();
            Currency currency = currencyService.getCurrencyById(id);
            Assert.AreEqual(currency.Name, "EURO");
        }
    }
}
