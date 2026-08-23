using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace producer.Models
{
    public class LiveStatus
    {
        public int assetId { get; set; }
        public string? assetType { get; set; }
        public string? rawValue { get; set; }
        public DateTime timestamp { get; set; }
    }
}
