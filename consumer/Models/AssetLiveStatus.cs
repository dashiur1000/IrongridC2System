using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer.Models
{
    public class AssetLiveStatus
    {
        public int assetId { get; set; }
        public string? assetType { get; set; }
        public string? rawValue { get; set; }
        public string? ProcessedStatus { get; set; }
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
