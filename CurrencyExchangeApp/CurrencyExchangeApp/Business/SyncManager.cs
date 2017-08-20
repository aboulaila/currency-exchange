using CurrencyExchangeApp.Models;
using CurrencyExchangeApp.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace CurrencyExchangeApp.Business
{
    public class SyncManager
    {
        #region Properties
        private Lazy<CurrencyRepository> _currencyRepository; 
        #endregion

        #region Ctor
        public SyncManager()
        {
            _currencyRepository = new Lazy<CurrencyRepository>(() => new CurrencyRepository("CurrencyDB"));
        } 
        #endregion

        #region Public Methods
        public async Task<bool> SyncCurrencies()
        {
            var currencies = await this.GetCurrencies();

            if (currencies != null && currencies.Any())
            {
                _currencyRepository.Value.DeleteAll();

                #region InsertCurrencies
                _currencyRepository.Value.InsertManyCurrencies(currencies.Keys.Select(k => new Currency(k.ToString().ToLower(), k.ToString())));
                #endregion

                #region InsertRates
                foreach (var key in currencies.Keys)
                {
                    _currencyRepository.Value.InsertManyRates(currencies[key]);
                }
                #endregion
            }
            return true;
        }
        #endregion

        #region Private Methods
        private async Task<Dictionary<String, List<CurrencyRate>>> GetCurrencies()
        {
            String dataProviderUri = ConfigurationManager.AppSettings.Get("DataProvider");
            XNamespace xmlNamespace = ConfigurationManager.AppSettings.Get("XMLNamespace");

            Dictionary<String, List<CurrencyRate>> currencies = new Dictionary<String, List<CurrencyRate>>();

            XDocument xmlDoc = await this.GetXMLDocument(dataProviderUri);

            foreach (var time in xmlDoc.Descendants(xmlNamespace + "Cube").Where(d => d.Attribute("time") != null))
            {
                foreach (var currency in time.Descendants(xmlNamespace + "Cube").Where(d => d.Attribute("currency") != null))
                {
                    var c = (string)currency.Attribute("currency");
                    var r = (string)currency.Attribute("rate");
                    var t = (string)time.Attribute("time");

                    if (currencies.ContainsKey(c))
                    {
                        currencies[c].Add(new CurrencyRate(c.ToLower(), r, t));
                    }
                    else
                    {
                        currencies.Add(c, new List<CurrencyRate>()
                        {
                            new CurrencyRate(c.ToLower(), r, t)
                        });
                    }
                }
            }
            return currencies;
        }

        private async Task<XDocument> GetXMLDocument(String uri)
        {
            return await Task.Run(() =>
            {
                return XDocument.Load(uri);
            });
        }
        #endregion
    }
}