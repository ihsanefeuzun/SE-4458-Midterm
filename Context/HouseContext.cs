using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Context
{
    public class HouseContext : DbContext
    {
        public HouseContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<House> Houses{ get; set; }
        public DbSet<Booking> Bookings { get; set; }

        
    }
}
