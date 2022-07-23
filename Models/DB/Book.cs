using System;
using System.Collections.Generic;

#nullable disable

namespace Bookings.Models.DB
{
    public partial class Book
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string BookName { get; set; }
    }
}
