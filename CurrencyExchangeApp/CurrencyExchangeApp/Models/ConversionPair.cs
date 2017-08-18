using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchangeApp.Models
{
    public class ConversionPair
    {
        public CurrencyRate FirstRate { get; set; }
        public CurrencyRate SecondRate { get; set; }
    }
}