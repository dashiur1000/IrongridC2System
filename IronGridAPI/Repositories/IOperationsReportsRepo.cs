using IronGridAPI.Dtos;

namespace IronGridAPI.Repositories
{
    public interface IOperationsReportsRepo
    {
        Task<IEnumerable<CriticalAssetsDto>> GetCriticalAsync();
        Task<IEnumerable<AssetLiveStatusUnitDto>> GetAssetLiveStatusAsync(int unitId);
        Task<IEnumerable<SummaryDto>> SummaryByUnitAsync();
    }
}
