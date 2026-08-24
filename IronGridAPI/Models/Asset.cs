namespace IronGridAPI.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string? AssetSerial { get; set; }
        public string? AssetType { get; set; }
        public Units? Unit { get; set; }
    }
}
