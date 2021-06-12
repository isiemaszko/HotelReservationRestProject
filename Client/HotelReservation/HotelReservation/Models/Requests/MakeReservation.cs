using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Models.Requests
{
    class MakeReservation
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<int> rooms { get; set; }
        public int ownersId { get; set; }
        public string notes { get; set; }
    }
}
