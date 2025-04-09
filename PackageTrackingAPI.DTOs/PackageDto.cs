using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingAPI.DTOs
{
    public class PackageDto
    {
        public int PackageID { get; set; }
        public string TrackingNumber { get; set; }
        public int SenderID { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string CurrentStatus { get; set; }
    }
}
