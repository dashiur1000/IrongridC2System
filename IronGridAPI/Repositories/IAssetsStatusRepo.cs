using IronGridAPI.Dtos;

namespace IronGridAPI.Repositories
{
    public interface IAssetsStatusRepo
    {
        Task<IEnumerable<AssetLiveStatusDto>> GetAllStatusesAsync();
        Task<AssetLiveStatusDto> GetStatusByIdAsync(int id);
        Task<IEnumerable<AssetLiveStatusDto>> GetStatusesByProcessedStatusAsync(string statusFilter);
    }
}
