using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Model.Dto;
using WebApplication1.Model;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers.v2
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        //gets all bookings
        [HttpGet]
        public List<BookingDto> Get()
        {
            List<Booking> datas = _bookingService.getBookings();
            List<BookingDto> ret = new List<BookingDto>();
            datas.ForEach(data => ret.Add(createBookingDto(data)));
            return ret;

        }

        //gets a booking with given id
        [HttpGet("{id}")]
        public BookingDto Get(long id)
        {
            Booking data = _bookingService.getBookingById(id);
            return createBookingDto(data);
        }

        private BookingDto createBookingDto(Booking booking)
        {
            BookingDto dto = new BookingDto()
            {
                Id = booking.Id,
                houseId = booking.houseId,
                GuestName = booking.GuestName,
                price = booking.price,
                BookedDates = booking.BookedDates
            };
            return dto;
        }

        private Booking createBooking(BookingDto dto)
        {
            Booking booking = new Booking()
            {
                Id = dto.Id,
                houseId = dto.houseId,
                GuestName = dto.GuestName,
                price = dto.price,
                BookedDates = dto.BookedDates
            };
            return booking;
        }
    }
}
