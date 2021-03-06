﻿using System;
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
    public partial class MarketsComponent : ComponentBase
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

        protected RadzenGrid<InsertStoredProcDemo.Models.ConData.Market> grid0;

        IEnumerable<InsertStoredProcDemo.Models.ConData.Market> _getMarketsResult;
        protected IEnumerable<InsertStoredProcDemo.Models.ConData.Market> getMarketsResult
        {
            get
            {
                return _getMarketsResult;
            }
            set
            {
                if (!object.Equals(_getMarketsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMarketsResult", NewValue = value, OldValue = _getMarketsResult };
                    _getMarketsResult = value;
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
            var conDataGetMarketsResult = await ConData.GetMarkets();
            getMarketsResult = conDataGetMarketsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddMarket>("Add Market", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddMarketAdoNet>("AddMarketADONet", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(InsertStoredProcDemo.Models.ConData.Market args)
        {
            var dialogResult = await DialogService.OpenAsync<EditMarket>("Edit Market", new Dictionary<string, object>() { {"MarketListID", args.MarketListID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeleteMarketResult = await ConData.DeleteMarket(data.MarketListID);
                if (conDataDeleteMarketResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeleteMarketException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Market");
            }
        }
    }
}
