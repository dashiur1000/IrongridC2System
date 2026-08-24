using IronGridAPI.Data;
using IronGridAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IronGridAPI.Repositories
{
    public class OperationsReportsRepo : IOperationsReportsRepo
    {
        private readonly IronGridDbContext _context;
        public OperationsReportsRepo(IronGridDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CriticalAssetsDto>> GetCriticalAsync()
        {
            return await _context.AssetLiveStatus
                .Where(a => (a.ProcessedStatus == "Warning" || a.IsVerified == false))
                .Include(a => a.Asset)
                .ThenInclude(b => b.Unit)
                .Select(a => new CriticalAssetsDto
                {
                    assetId = a.assetId,
                    assetSerial = a.rawValue,
                    assetType = a.assetType,
                    unitName = a.Asset.Unit.UnitName.ToString(),
                    sector = a.Asset.Unit.Sector,
                    ProcessedStatus = a.ProcessedStatus,
                    IsVerified = a.IsVerified,
                    LastUpdate = a.LastUpdate
                }).ToListAsync();
        }
        public async Task<IEnumerable<AssetLiveStatusUnitDto>> GetAssetLiveStatusAsync(int unitId)
        {
            var result = _context.Units.FirstOrDefault(a => a.Id == unitId);
            if (result == null)
            {
                return null;
            }
            return await _context.AssetLiveStatus
                .Where(a => a.Asset.UnitId == unitId)
                .Select(a => new AssetLiveStatusUnitDto
                {
                    assetId = a.assetId,
                    assetSerial = a.rawValue,
                    assetType = a.assetType,
                    ProcessedStatus = a.ProcessedStatus,
                    IsVerified = a.IsVerified,
                    LastUpdate = a.LastUpdate
                }).ToListAsync();
        }
        public async Task<IEnumerable<SummaryDto>> SummaryByUnitAsync()
        {
            return await _context.AssetLiveStatus
                .Include(a => a.Asset)
                .ThenInclude(a => a.Unit)
                .Select(a => new SummaryDto
                {
                    unitId = a.Asset.Unit.Id,
                    unitName = a.Asset.Unit.UnitName,
                    sector = a.Asset.Unit.Sector,
                }).ToListAsync();
        }
    }
}
