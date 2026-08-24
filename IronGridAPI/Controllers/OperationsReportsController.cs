using IronGridAPI.Dtos;
using IronGridAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IronGridAPI.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class OperationsReportsController : ControllerBase
    {
        private readonly IOperationsReportsRepo _repo;
        public OperationsReportsController(IOperationsReportsRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("critical-assets")]
        public async Task<ActionResult<IEnumerable<CriticalAssetsDto>>> GetCriticalAssets()
        {
            var result = await _repo.GetCriticalAsync();
            return Ok(result);
        }
        [HttpGet("unit/{unitId}/assets")]
        public async Task<ActionResult<IEnumerable<AssetLiveStatusUnitDto>>> GetAllByUnit(int unitId)
        {
            var result = await _repo.GetAssetLiveStatusAsync(unitId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("summary-by-unit")]
        public async Task<ActionResult<IEnumerable<SummaryDto>>> GetAllByUnit()
        {
            var result = await _repo.SummaryByUnitAsync();
            return Ok(result);
        }
    }
}
