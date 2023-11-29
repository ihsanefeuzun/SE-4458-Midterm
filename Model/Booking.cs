using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    [Table("Booking")]
    [Index(nameof(Id), IsUnique = true)]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        
        public long houseId { get; set; }

        //[Required]
        //public List<DateTime> BookedDates { get; set; }

        [Required]
        [StringLength(20)]
        public String GuestName { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public String BookedDates { get; set; }
    }
}
