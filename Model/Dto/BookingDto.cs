using System.Collections.Generic;
using System;

namespace WebApplication1.Model.Dto
{

    public class BookingDto
    {

        public long Id { get; set; }
        public long houseId { get; set; }

        //public List<DateTime> BookedDates { get; set; } = new List<DateTime>();

        public string GuestName { get; set; }

        public int price { get; set; }

        public string BookedDates { get; set; }
    }
}
