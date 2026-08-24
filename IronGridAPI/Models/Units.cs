namespace IronGridAPI.Models
{
    public class Units
    {
        public int Id { get; set; }
        public string UnitName = null!;
        public string Sector = null!;
        public ICollection<Asset>? Assets { get; set; }
    }
}
