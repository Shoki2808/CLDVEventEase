//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.JSInterop;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using Radzen;
//using Radzen.Blazor;

//namespace EventEaseBooking.Components.Pages
//{
//    public partial class AddVenue
//    {
//        [Inject]
//        protected IJSRuntime JSRuntime { get; set; }

//        [Inject]
//        protected NavigationManager NavigationManager { get; set; }

//        [Inject]
//        protected DialogService DialogService { get; set; }

//        [Inject]
//        protected TooltipService TooltipService { get; set; }

//        [Inject]
//        protected ContextMenuService ContextMenuService { get; set; }

//        [Inject]
//        protected NotificationService NotificationService { get; set; }
//        [Inject]
//        public EventEaseService EventEaseService { get; set; }

//        protected override async Task OnInitializedAsync()
//        {
//            venue = new EventEaseBooking.Models.EventEase.Venue();

//            eventTypesForEventTypeID = await EventEaseService.GetEventTypes();
//        }
//        protected bool errorVisible;
//        protected EventEaseBooking.Models.EventEase.Venue venue;

//        protected IEnumerable<EventEaseBooking.Models.EventEase.EventType> eventTypesForEventTypeID;

//        protected async Task FormSubmit()
//        {
//            try
//            {
//                await EventEaseService.CreateVenue(venue);
//                DialogService.Close(venue);
//            }
//            catch (Exception ex)
//            {
//                errorVisible = true;
//            }
//        }

//        protected async Task CancelButtonClick(MouseEventArgs args)
//        {
//            DialogService.Close(null);
//        }
//    }
//}