namespace IronGridAPI.Dtos
{
    public class SummaryDto
    {
        public int unitId {  get; set; }
        public string unitName { get; set; }
        public string sector {  get; set; }
        public int totalAssets { get; set; }
        public int stableAssets { get; set; }
        public int warningAssets { get; set; }
        public int unverifiedAssets { get; set; }
    }
}
