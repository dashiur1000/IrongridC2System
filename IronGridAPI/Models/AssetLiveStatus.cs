namespace IronGridAPI.Models
{
    public class AssetLiveStatus
    {
        public int assetId { get; set; }
        public string? assetType { get; set; }
        public string? rawValue { get; set; }
        public string? ProcessedStatus { get; set; }
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }
        public Asset? Asset { get; set; }

    }
}
