using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.Extensions.Configuration;

namespace InsertStoredProcDemo.Pages
{
    public partial class AddMarketAdoNetComponent
    {
        private async Task MuyikCreateNewMarketADONet()
        {
            try
            {
                var myConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conDataConnection"];
                await ConData.MuyikAddMarketADONet(myConnectionString, market.MarketLocation, market.MarketName, market.MarketSizeInHectares);
                UriHelper.NavigateTo("markets", true);
            }
            catch (Exception)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Market Insert Error", "An Error Occurred While Creating The New Market", 7000);
            }
        }
    }
}
