using IronGridAPI.Data;
using IronGridAPI.Models;
using IronGridAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IronGridAPI.Repositories
{
    public class AssetsRepo : IAssetsRepo
    {
        private readonly IronGridDbContext _dbContext;
        public AssetsRepo(IronGridDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AssetDto> GetBYIdAsync(int id)
        {
            var result = _dbContext.Assets.FirstOrDefault(a => a.Id == id);
            if (result == null)
            {
                return null;
            }
            var dto = new AssetDto
            {
                Id = id,
                UnitId = result.UnitId,
                AssetSerial = result.AssetSerial,
                AssetType = result.AssetType,
            };
            return dto;
        }
        public async Task<Units> AddUnitAsync(UnitToCreateDto unit)
        {
            var newUnit = new Units
            {
                Sector = unit.Sector,
                UnitName = unit.UnitName,
            };
            var created = _dbContext.Units.Add(newUnit);
            return newUnit;
        }
        public async Task<UnitDto> GetUnitBYIdAsync(int id)
        {
            var result = _dbContext.Units.FirstOrDefault(a => a.Id == id);
            if (result == null)
            {
                return null;
            }
            var dto = new UnitDto
            {
                Id = id,
                Sector  = result.Sector,
                UnitName = result.UnitName
            };
            return dto;
        }
        public async Task<Asset> updateAssetAsync(int id, AssetToUpdate assetToUpdate)
        {
            var result = _dbContext.Assets.FirstOrDefault(a => a.Id == id);
            if ( result == null )
            {
                return null;
            }
            result.AssetSerial = assetToUpdate.AssetSerial;
            result.AssetType = assetToUpdate.AssetType;
            await _dbContext.SaveChangesAsync();
            return result;
        }
        public async Task deleteAsset(int id)
        {
            var asset = await _dbContext.Assets.FindAsync(id);
            if (asset != null)
            {
                _dbContext.Assets.Remove(asset);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
