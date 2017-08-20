using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchangeApp.Models
{
    public class Currency
    {
        public String Code { get; set; }
        public String Title { get; set; }

        public Currency(String code, String title)
        {
            this.Code = code;
            this.Title = title;
        }
    }
}