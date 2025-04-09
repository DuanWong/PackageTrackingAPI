using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingAPI.DTOs
{
    public class TrackingEventDto
    {
        public int EventID { get; set; }
        public int PackageID { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string Timestamp { get; set; }
    }
}
