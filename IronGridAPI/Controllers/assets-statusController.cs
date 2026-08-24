using IronGridAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IronGridAPI.Controllers
{
    [ApiController]
    [Route("api/assets-status")]
    public class assetsStatusController : ControllerBase
    {
        private readonly AssetsStatusRepo _repository;

        public assetsStatusController(AssetsStatusRepo repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _repository.GetAllStatusesAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var assetStatus = await _repository.GetStatusByIdAsync(id);
            if (assetStatus == null) return NotFound();

            return Ok(assetStatus);
        }
        [HttpGet("status")]
        public async Task<ActionResult> GetByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                var filtered = await _repository.GetStatusesByProcessedStatusAsync(status);
                return Ok(filtered);
            }

            var all = await _repository.GetAllStatusesAsync();
            return Ok(all);
        }

        
    }
}
