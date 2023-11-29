using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Model;

namespace WebApplication1.Source.Db
{
    public class BookingAccess
    {

        private HouseContext _context;

        public BookingAccess(HouseContext context)
        {
            _context = context;
        }
        public Booking getBooking(long id)
        {
            return _context.Bookings.FirstOrDefault(s => s.Id == id);
        }

        public Booking getBookingbyGuestName(string guestName)
        {
            return _context.Bookings.FirstOrDefault(s => s.GuestName == guestName);
        }

        public int insertBooking(Booking data)
        {
            // validateData(data);
            _context.Bookings.Add(data);
            return _context.SaveChanges();

        }
        /*
        private void validateData(House data)
        {
            if (String.IsNullOrEmpty(data.Address))
            {
                throw new Exception("Address cannot be null");
            }
        }
        */
        public int deleteBooking(Booking booking)
        {
            
            _context.Bookings.Remove(booking);
            return _context.SaveChanges();
        }
        public IEnumerable<Booking> getAllBookings()
        {
            return _context.Bookings;
        }

        public void updateBooking(Booking booking)
        {
            // Implementation to update a student in the database
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }
        /*
        private static List<Booking> sampleBookings = new List<Booking>(new Booking[]
        {
            new Booking() { Id=1, houseId=1, GuestName = "John", price = 1000 }
        });

        public List<Booking> getBookings()
        {
            return sampleBookings;
        }

        public Booking getBookingById(long id)
        {
            return sampleBookings.Find(c => c.Id == id);
        }

        public void insertBooking(Booking booking)
        {
            sampleBookings.Add(booking);
        }

        public int deleteBooking(long id)
        {
            return sampleBookings.RemoveAll(c => c.Id == id);
        }

        public void updateBooking(Booking booking)
        {
            Booking bookingEx = getBookingById(booking.Id);
            if (bookingEx != null)
            {
                bookingEx.GuestName = booking.GuestName;
                bookingEx.price = booking.price;
            }
        }

        public List<Booking> searchBooking(Booking booking)
        {
            List<Booking> ret = getBookings().FindAll(c => c.houseId.Equals(booking.houseId));

            return ret;
        }

        public void performBooking(Booking booking)
        {
            Booking bookingEx = getBookingById(booking.Id);
            if (bookingEx != null)
            {
                bookingEx.GuestName = booking.GuestName;
                bookingEx.price = booking.price;
            }
        }
        */

    }
}
