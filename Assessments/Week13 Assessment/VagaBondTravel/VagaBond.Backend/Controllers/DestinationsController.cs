using Microsoft.AspNetCore.Mvc;
using VagaBond.Backend.Models;
using VagaBond.Backend.Services;

namespace VagaBond.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _service;

        public DestinationsController(IDestinationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Destination destination)
        {
            if(destination.Rating<1 || destination.Rating>5)
            {
                throw new Filters.Invalidrating("Rating must be between 1 and 5.");
            }
            await _service.AddAsync(destination);
            return CreatedAtAction(nameof(GetById),
                new { id = destination.DestinationId },
                destination);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Destination destination)
        {
            if (id != destination.DestinationId)
                return BadRequest();

            await _service.UpdateAsync(destination);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}