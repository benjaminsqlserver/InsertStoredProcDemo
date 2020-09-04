using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using InsertStoredProcDemo.Models.ConData;
using Microsoft.EntityFrameworkCore;

namespace InsertStoredProcDemo.Pages
{
    public partial class EditMarketComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected ConDataService ConData { get; set; }

        [Parameter]
        public dynamic MarketListID { get; set; }

        InsertStoredProcDemo.Models.ConData.Market _market;
        protected InsertStoredProcDemo.Models.ConData.Market market
        {
            get
            {
                return _market;
            }
            set
            {
                if (!object.Equals(_market, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "market", NewValue = value, OldValue = _market };
                    _market = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var conDataGetMarketByMarketListIdResult = await ConData.GetMarketByMarketListId(MarketListID);
            market = conDataGetMarketByMarketListIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(InsertStoredProcDemo.Models.ConData.Market args)
        {
            try
            {
                var conDataUpdateMarketResult = await ConData.UpdateMarket(MarketListID, market);
                DialogService.Close(market);
            }
            catch (System.Exception conDataUpdateMarketException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Market");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
