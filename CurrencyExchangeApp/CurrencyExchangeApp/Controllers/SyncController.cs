using CurrencyExchangeApp.Business;
using CurrencyExchangeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CurrencyExchangeApp.Controllers
{
    public class SyncController : ApiController
    {
        #region Properties
        Lazy<SyncManager> syncManager = new Lazy<SyncManager>();
        #endregion

        #region Public Methods
        [HttpPost]
        public async Task<bool> SyncCurrencies()
        {
            return await syncManager.Value.SyncCurrencies();
        } 
        #endregion
    }
}
