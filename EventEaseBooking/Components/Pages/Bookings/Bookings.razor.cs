
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.JSInterop;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using Radzen;
//using Radzen.Blazor;
//using EventEaseBooking.Models;

//namespace EventEaseBooking.Components.Pages.Bookings
//{
//    public partial class Bookings
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
//        //public EventEaseService EventEaseService { get; set; }

//        //protected IEnumerable<EventEaseBooking.Models.EventEase.Booking> bookings;

//        protected RadzenDataGrid<Booking> grid0;

//        protected string search = "";

//        //protected async Task Search(ChangeEventArgs args)
//        //{
//        //    search = $"{args.Value}";

//        //    await grid0.GoToPage(0);

//        //    bookings = await EventEaseService.GetBookings(new Query { Filter = $@"i => i.Status.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Event,Client" });
//        //}
//        //protected override async Task OnInitializedAsync()
//        //{
//        //    bookings = await EventEaseService.GetBookings(new Query { Filter = $@"i => i.Status.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Event,Client" });
//        //}

//        //protected async Task AddButtonClick(MouseEventArgs args)
//        //{
//        //    await DialogService.OpenAsync<AddBooking>("Add Booking", null);
//        //    await grid0.Reload();
//        //}

//        protected async Task EditRow(DataGridRowMouseEventArgs<EventEaseBooking.Models.EventEase.Booking> args)
//        {
//            await DialogService.OpenAsync<EditBooking>("Edit Booking", new Dictionary<string, object> { { "BookingId", args.Data.BookingId } });
//        }

//        protected async Task GridDeleteButtonClick(MouseEventArgs args, EventEaseBooking.Models.EventEase.Booking booking)
//        {
//            try
//            {
//                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
//                {
//                    var deleteResult = await EventEaseService.DeleteBooking(booking.BookingId);

//                    if (deleteResult != null)
//                    {
//                        await grid0.Reload();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                NotificationService.Notify(new NotificationMessage
//                {
//                    Severity = NotificationSeverity.Error,
//                    Summary = $"Error",
//                    Detail = $"Unable to delete Booking"
//                });
//            }
//        }
//    }
//}
