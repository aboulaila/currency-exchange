using CurrencyExchangeApp.Models;
using CurrencyExchangeApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchangeApp.Business
{
    public class ExchangeManager
    {
        #region Properties
        private Lazy<CurrencyRepository> _currencyRepository;
        #endregion

        #region Ctor
        public ExchangeManager()
        {
            _currencyRepository = new Lazy<CurrencyRepository>(() => new CurrencyRepository("CurrencyDB"));
        }
        #endregion

        #region Public Methods
        public IEnumerable<Currency> GetCurrencies()
        {
            var currencies = _currencyRepository.Value.SelectAllCurrencies();
            return currencies;
        }

        public CurrencyRate GetCurrencyRate(String code)
        {
            var currency = _currencyRepository.Value.SelectRateByCriteria(code).FirstOrDefault();
            this.ConvertToUSD(ref currency);
            return currency;
        }

        public ConversionPair ConvertCurrencies(ConversionPair pair)
        {
            var firstCurrency = _currencyRepository.Value.SelectRateByCriteria(pair.FirstRate.CurrencyCode).FirstOrDefault();
            var secondCurrency = _currencyRepository.Value.SelectRateByCriteria(pair.SecondRate.CurrencyCode).FirstOrDefault();

            Decimal conversionRate;
            if (pair.FirstRate.Value > 0) {
                conversionRate = firstCurrency.Rate / secondCurrency.Rate;
                pair.SecondRate.Value = pair.FirstRate.Value / conversionRate;
            }
            else
            {
                conversionRate = secondCurrency.Rate / firstCurrency.Rate;
                pair.FirstRate.Value = pair.SecondRate.Value / conversionRate;
            }
            return pair;
        }

        public IEnumerable<CurrencyRate> GetChartData(String code)
        {
            return _currencyRepository.Value.SelectAllRatesByCode(code).Select(c => {
                this.ConvertToUSD(ref c);
                return c;
            });
        }
        #endregion

        #region Private Methods
        private void ConvertToUSD(ref CurrencyRate rate)
        {
            var usdRateToEUR = _currencyRepository.Value.SelectRateByCriteria("usd").FirstOrDefault();
            if (usdRateToEUR != null)
            {
                rate.Rate = rate.Rate / usdRateToEUR.Rate;
            }
        }
        #endregion
    }
}