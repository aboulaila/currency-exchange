using CurrencyExchangeApp.Business;
using CurrencyExchangeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CurrencyExchangeApp.Controllers
{
    public class ExchangeController : ApiController
    {
        #region Properties
        Lazy<ExchangeManager> exchangeManager = new Lazy<ExchangeManager>();
        #endregion

        [HttpGet]
        public IEnumerable<Currency> GetCurrencies()
        {
            return exchangeManager.Value.GetCurrencies();
        }

        [HttpGet]
        public CurrencyRate GetCurrencyRate(String currencyCode)
        {
            return exchangeManager.Value.GetCurrencyRate(currencyCode);
        }

        [HttpPost]
        public ConversionPair ConvertCurrencies(ConversionPair pair)
        {
            return exchangeManager.Value.ConvertCurrencies(pair);
        }

        [HttpGet]
        public IEnumerable<CurrencyRate> GetChartData(String currencyCode)
        {
            return exchangeManager.Value.GetChartData(currencyCode);
        }
    }
}
