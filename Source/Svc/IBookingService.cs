using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Source.Svc
{
    public interface IBookingService
    {
        public List<Booking> getBookings();
        public Booking getBookingById(long id);
        public int insertBooking(Booking booking);
        public void updateBooking(Booking booking);

        public string getBookedDatesByHouseId(long houseId);

        public void deleteBooking(Booking booking);
    }
}
