using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;

namespace InsertStoredProcDemo.Pages
{
    public partial class AddMarketComponent
    {
        private async Task MuyikCreateNewMarket()
        {
            try
            {
                await ConData.MuyikAddNewMarket(market.MarketLocation, market.MarketName, market.MarketSizeInHectares);
                UriHelper.NavigateTo("markets", true);
            }
            catch(Exception)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Market Insert Error", "An Error Occurred While Creating The New Market", 7000);
            }
            
        }
    }
}
