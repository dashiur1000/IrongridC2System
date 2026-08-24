using IronGridAPI.Data;
using System;
using IronGridAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IronGridAPI.Repositories
{
    public class AssetsStatusRepo
    {
        private readonly IronGridDbContext _context;

        public AssetsStatusRepo(IronGridDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AssetLiveStatusDto>> GetAllStatusesAsync()
        {
            return await _context.Assets
                .Join(_context.AssetLiveStatus,
                    asset => asset.Id,
                    status => status.assetId,
                    (asset, status) => new AssetLiveStatusDto
                    {
                        assetId = asset.Id,
                        rawValue = asset.AssetSerial,
                        assetType = asset.AssetType,
                        ProcessedStatus = status.ProcessedStatus,
                        IsVerified = status.IsVerified,
                        LastUpdate = status.LastUpdate
                    }).ToListAsync();
        }

        public async Task<AssetLiveStatusDto> GetStatusByIdAsync(int id)
        {
            return await _context.Assets
                .Where(asset => asset.Id == id)
                .Join(_context.AssetLiveStatus,
                    asset => asset.Id,
                    status => status.assetId,
                    (asset, status) => new AssetLiveStatusDto
                    {
                        assetId = asset.Id,
                        rawValue = asset.AssetSerial,
                        assetType = asset.AssetType,
                        ProcessedStatus = status.ProcessedStatus,
                        IsVerified = status.IsVerified,
                        LastUpdate = status.LastUpdate
                    }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AssetLiveStatusDto>> GetStatusesByProcessedStatusAsync(string statusFilter)
        {
            return await _context.Assets
                .Join(_context.AssetLiveStatus,
                    asset => asset.Id,
                    status => status.assetId,
                    (asset, status) => new { asset, status })
                .Where(x => x.status.ProcessedStatus == statusFilter)
                .Select(x => new AssetLiveStatusDto
                {
                    assetId = x.asset.Id,
                    rawValue = x.asset.AssetSerial,
                    assetType = x.asset.AssetType,
                    ProcessedStatus = x.status.ProcessedStatus,
                    IsVerified = x.status.IsVerified,
                    LastUpdate = x.status.LastUpdate
                }).ToListAsync();
        }
    }
}