using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchangeApp
{
    public static class GlobalConstants
    {
        public static class CurrencyProcedures
        {
            public static string Insert = "USP_CURRENCY_INSERT";
            public static string InsertMany = "USP_CURRENCY_INSERTMANY";
            public static string DeleteAll = "USP_Currency_DeleteAll";
            public static string SelectAll = "USP_CURRENCY_SELECTALL";
        }

        public static class RateProcedures
        {
            public static string Insert = "USP_CURRENCYRATE_INSERT";
            public static string InsertMany = "USP_CURRENCYRATE_INSERTMANY";
            public static string SelectByCriteria = "USP_CURRENCYRATE_SelectByCriteria";
            public static string SelectAllByCode = "USP_CURRENCYRATE_SelectAllByCode";
            
        }
    }
}