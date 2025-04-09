using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingAPI.DTOs
{
    public class AlertDto
    {
        public int AlertID { get; set; }
        public int UserID { get; set; }
        public int PackageID { get; set; }
        public string Message { get; set; }
        public string Timestamp { get; set; }
    }
}
