using AutoMapper;
using EventEaseAPI.DTOs.Booking;
using EventEaseAPI.DTOs.Client;
using EventEaseAPI.DTOs.Event;
using EventEaseAPI.DTOs.EventType;
using EventEaseAPI.DTOs.Venue;

namespace EventEaseAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Client
            CreateMap<Client, ClientReadDto>();
            CreateMap<ClientCreateDto, Client>();
            CreateMap<ClientUpdateDto, Client>();

            // Event
            CreateMap<Event, EventReadDto>()
                .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Venue.VenueName))
                .ForMember(dest => dest.EventTypeName, opt => opt.MapFrom(src => src.EventType.EventTypeName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive ? "Active" : "Inactive"));
                
            CreateMap<EventCreateDto, Event>();

            CreateMap<EventUpdateDto, Event>();


            // Venue
            CreateMap<Venue, VenueReadDto>()
                .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.IsActive ? "Available" : "Booked"));
            CreateMap<VenueCreateDto, Venue>();
            CreateMap<VenueUpdateDto, Venue>();

            // EventType
            CreateMap<EventType, EventTypeReadDto>();
            CreateMap<EventTypeCreateDto, EventType>();
            CreateMap<EventTypeUpdateDto, EventType>();

            // Booking
            CreateMap<Booking, BookingReadDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.ClientName))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.EventName))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Event.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Event.EndTime))
                .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Event.Venue.VenueName))
                .ForMember(dest => dest.EventTypeName, opt => opt.MapFrom(src => src.Event.EventType.EventTypeName))
                .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.IsActive ? "Active" : "Inctive"));

            CreateMap<BookingCreateDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();
        }
    }

}
