using System;

namespace database_scaffold_asp_dot_net.Models
{
    public class PersonalData
    {
        public int Id { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
    }
}