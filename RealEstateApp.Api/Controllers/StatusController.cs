using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Api.DatabaseContext;
using RealEstateApp.Api.DTO.StatusDTO;
using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public StatusController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Statuses.ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Statuses.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Status status)
        {
            await _context.Statuses.AddAsync(status);
            await _context.SaveChangesAsync();

            return Ok(status);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Status status)
        {
            var existingStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.Id == status.Id);

            if (existingStatus == null)
            {
                return NotFound();
            }

            existingStatus.Description = status.Description;

            await _context.SaveChangesAsync();
            return Ok(existingStatus);
        }
    }
}
