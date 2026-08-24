using IronGridAPI.Dtos;
using IronGridAPI.Models;
using IronGridAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IronGridAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class assetsController :ControllerBase
    {
        private readonly AssetsRepo _repo;

        public assetsController(AssetsRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDto>> GetById(int id)
        {
            var result = await _repo.GetBYIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("units")]
        public async Task<ActionResult> CreateUnit(UnitToCreateDto unit)
        {
            var result = await _repo.AddUnitAsync(unit);
            return Created("created", unit);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AssetDto>> updateAsset(int id, AssetToUpdate asset)
        {
            var result = await _repo.updateAssetAsync(id, asset);
            if(result == null)
            {
                return NotFound();
            }
            var newAsset = new AssetDto
            {
                Id = id,
                UnitId = result.UnitId,
                AssetSerial = result.AssetSerial,
                AssetType = result.AssetType
            };
            return Ok(newAsset);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsset(int id)
        {
            var result = _repo.deleteAsset(id);
            if(!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
