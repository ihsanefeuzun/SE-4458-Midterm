using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Source.Db;
using System.Linq;

namespace WebApplication1.Source.Svc
{
    public class BookingService : IBookingService
    {
        private HouseContext _context;
        private readonly IMemoryCache _memoryCache;

        public BookingService(HouseContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public Booking getBookingById(long id)
        {
            BookingAccess access = new BookingAccess(_context);
            return access.getBooking(id);
        }

        public List<Booking> getBookings()
        {
            BookingAccess access = new BookingAccess(_context);
            return access.getAllBookings().ToList();
        }

        public int insertBooking(Booking booking)
        {
            BookingAccess access = new BookingAccess(_context);
            return access.insertBooking(booking);
        }

        public Booking getBookingsbyGuestName(string guestName)
        {
            BookingAccess access = new BookingAccess(_context);
            return access.getBookingbyGuestName(guestName);
        }

        public string getBookedDatesByHouseId(long houseId)
        {
            var bookedDates = _context.Bookings
                .Where(b => b.houseId == houseId)
                .Select(b => b.BookedDates)
                .ToList();

            string bookedDatesString = string.Join(",", bookedDates);

            return bookedDatesString;
        }

        public void updateBooking(Booking booking)
        {
            BookingAccess access = new BookingAccess(_context);
            access.updateBooking(booking);
        }

        public void deleteBooking(Booking booking)
        {
            BookingAccess access = new BookingAccess(_context);
            access.deleteBooking(booking);
        }

    }
}
