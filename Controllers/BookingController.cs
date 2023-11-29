using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Model.Dto;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]

    public class BookingController : ControllerBase
    {

        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        //gets all bookings
        [HttpGet]
        [Authorize]
        public List<BookingDto> Get()
        {
            List<Booking> datas = _bookingService.getBookings();
            List<BookingDto> ret = new List<BookingDto>();
            datas.ForEach(data => ret.Add(createBookingDto(data)));
            return ret;
          
        }

        //gets a booking with given id
        [HttpGet("{id}")]
        [Authorize]
        public BookingDto Get(long id)
        {
            Booking data = _bookingService.getBookingById(id);
            return createBookingDto(data);
        }

        // inserts a booking 
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                long houseId = bookingDto.houseId;
                string bookedDates = _bookingService.getBookedDatesByHouseId(houseId);

                if (IsHouseBookedOnDate(bookedDates, bookingDto.BookedDates))
                {
                    return BadRequest("The house is already booked on the specified date.");
                }
                _bookingService.insertBooking(createBooking(bookingDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        private bool IsHouseBookedOnDate(string bookedDates, string bookingDate)
        {
            return bookedDates.Contains(bookingDate);
        }

        // updates a booking with given id
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] BookingDto bookingDto)
        {
            if (bookingDto.Id == 0)
            {
                return BadRequest("Cant update a booking with no id");
            }
            if (bookingDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        _bookingService.updateBooking(createBooking(bookingDto));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromBody] BookingDto bookingDto)
        {
            if (bookingDto.Id == 0)
            {
                return BadRequest("Cant delete a booking with no id");
            }
            if (bookingDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        _bookingService.deleteBooking(createBooking(bookingDto));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
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

