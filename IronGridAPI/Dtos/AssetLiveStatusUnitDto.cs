namespace IronGridAPI.Dtos
{
    public class AssetLiveStatusUnitDto
    {
        public int? assetId { get; set; }
        public string? assetSerial { get; set; }
        public string? assetType { get; set; }
        public string? ProcessedStatus { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
