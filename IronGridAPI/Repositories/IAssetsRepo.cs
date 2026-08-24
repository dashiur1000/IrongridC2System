using IronGridAPI.Dtos;
using IronGridAPI.Models;

namespace IronGridAPI.Repositories
{
    public interface IAssetsRepo
    {
        Task<AssetDto> GetBYIdAsync(int id);
        Task<Units> AddUnitAsync(UnitToCreateDto unit);
        Task<UnitDto> GetUnitBYIdAsync(int id);
        Task<Asset> updateAssetAsync(int id, AssetToUpdate assetToUpdate);
        Task deleteAsset(int id);
    }
}
