using CurrencyExchangeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeApp.Repository
{
    public interface ICurrencyRepository
    {
        List<Currency> SelectAllCurrencies();
        IList<CurrencyRate> SelectRateByCriteria(String code, DateTime? date);
        IList<CurrencyRate> SelectAllRatesByCode(String code);
        void InsertCurrency(Currency currency);
        void InsertManyCurrencies(IEnumerable<Currency> currencies);
        void InsertRate(CurrencyRate rate);
        void InsertManyRates(IEnumerable<CurrencyRate> rates);
    }
}
