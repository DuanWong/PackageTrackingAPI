using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingAPI.Models
{
    public class TrackingEvent
    {
        public int EventID { get; set; }
        public int PackageID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public Package Package { get; set; }
    }
}
