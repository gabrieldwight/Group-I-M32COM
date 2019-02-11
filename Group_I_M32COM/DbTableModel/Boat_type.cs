using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This a table model to register the remote boat class categories
    public class Boat_type
    {
        public int Id { get; set; }
        public string Boat_class_type { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        // Foreign key definition one boat class category can have many sub boat class category
        public List<Sub_boat_type> Sub_Boat_Types { get; set; }

        // Foreign key definition one boat class category can have many boats manufacturer
        public List<Boat> Boats { get; set; }
    }
}
