using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // The properties to create Boat table in the database 
    public class Boat
    {
        public int Id { get; set; }
        public string Boat_name { get; set; }
        public string Boat_top_speed { get; set; }
        public string Boat_weight { get; set; }
        public string Boat_description { get; set; }
        public int Boat_media_type { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        // Foreign key for boat categories and Sub boat categories types
        public Boat_type Boat_Types { get; set; }
        public Sub_boat_type Sub_Boat_Types { get; set; }

        // Foreign key definition one boat can have many boat media
        public List<Boat_media> Boat_Medias { get; set; }
    }
}
