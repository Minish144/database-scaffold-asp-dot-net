using System;

namespace database_scaffold_asp_dot_net.Models
{
    public class BusinessTrip
    {
        public int Id { get; set; }
        public int EmpolyeeID { get; set; }
        public string City { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
    }
}