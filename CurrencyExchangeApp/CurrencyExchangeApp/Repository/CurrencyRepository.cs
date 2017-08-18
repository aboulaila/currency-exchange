using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.Database;
using System.Data.SqlClient;
using System.Configuration;
using CurrencyExchangeApp.Models;
using System.Data;

namespace CurrencyExchangeApp.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private IDbConnection _connection;
        private String _connectionString;

        public IDbConnection DB
        {
            get
            {
                if (this._connection == null)
                {
                    this._connection = new SqlConnection(GetConnectionString(this._connectionString));
                }
                return this._connection;
            }
        }

        #region Ctor
        public CurrencyRepository(String connectionString)
        {
            this._connectionString = connectionString;
        }
        #endregion

        #region Public Methods
        public List<Currency> SelectAllCurrencies()
        {
            return this.DB.Query<Currency>(GlobalConstants.CurrencyProcedures.SelectAll).ToList();
        }

        public IList<CurrencyRate> SelectRateByCriteria(String code)
        {
            return this.DB.Query<CurrencyRate>(GlobalConstants.RateProcedures.SelectByCriteria, new { Code = code });
        }

        public void InsertCurrency(Currency currency)
        {
            this.DB.Execute(GlobalConstants.CurrencyProcedures.Insert, currency);
        }

        public void InsertManyCurrencies(IEnumerable<Currency> currencies)
        {
            this.DB.Execute(GlobalConstants.CurrencyProcedures.InsertMany, currencies);
        }

        public void InsertRate(CurrencyRate rate)
        {
            this.DB.Execute(GlobalConstants.RateProcedures.Insert, rate);
        }

        public void InsertManyRates(IEnumerable<CurrencyRate> rates)
        {
            this.DB.Execute(GlobalConstants.RateProcedures.InsertMany, rates);
        }

        public void DeleteAll()
        {
            this.DB.Execute(GlobalConstants.CurrencyProcedures.DeleteAll);
        }
        #endregion

        #region Private Methods
        private static string GetConnectionString(String cs)
        {
            string result = cs;
            var connectionString = ConfigurationManager.ConnectionStrings[cs];
            if (connectionString != null)
            {
                result = connectionString.ConnectionString;
            }
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Invalid connection string or connection string name: '{0}'", cs));
            }
        }
        #endregion
    }
}