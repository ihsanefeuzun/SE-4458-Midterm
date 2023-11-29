using System;
using System.Collections.Generic;

namespace WebApplication1.Model.Dto
{
    public class QueryWithPagingDto
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
    public class HouseDto
    {

        public long Id { get; set; }
        public string Name { get; set; }

        //public List<DateTime> BookedDates { get; set; } = new List<DateTime>();

        public string OwnerName { get; set; }

        public string Address { get; set; }

        public int NumberofPeople { get; set; }

        public string Amenities { get; set; }

    }
    public class HouseResultDto : APIResultDto
    {
        public long Id { get; set; }
        public string InnerExceptionMessage { get; set; }

    }

    public class APIResultDto
    {
        public APIResultDto()
        {
            Status = "SUCCESS";
        }
        public string Status { get; set; }
        public string Message { get; set; }

    }
}
