using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchangeApp.Models
{
    public class CurrencyRate
    {
        public CurrencyRate()
        {

        }
        public CurrencyRate(String code, String rate, String date)
        {
            this.Rate = Convert.ToDecimal(rate);
            this.Date = Convert.ToDateTime(date);
            this.CurrencyCode = code;
        }
        public Decimal Rate { get; set; }
        public DateTime Date { get; set; }
        public String CurrencyCode { get; set; }
        public Decimal Value { get; set; }
    }
}