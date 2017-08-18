using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchangeApp.Models
{
    public class Currency
    {
        public Currency(String code, String rate, String time)
        {
            this.Code = code;
            this.Rate = Convert.ToDecimal(rate);
            this.Time = Convert.ToDateTime(time);
        }
        public String Code { get; set; }
        public Decimal Rate { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}