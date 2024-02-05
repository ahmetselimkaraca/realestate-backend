using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Api.DatabaseContext;
using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTypeController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public PropertyTypeController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.PropertyTypes.ToListAsync();

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
            var result = await _context.PropertyTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(PropertyType propertyType)
        {
            await _context.PropertyTypes.AddAsync(propertyType);
            await _context.SaveChangesAsync();

            return Ok(propertyType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(PropertyType propertyType)
        {
            var existingPropertyType = await _context.PropertyTypes.FirstOrDefaultAsync(x => x.Id == propertyType.Id);
            if (existingPropertyType == null)
            {
                return NotFound();
            }

            existingPropertyType.Description = propertyType.Description;

            await _context.SaveChangesAsync();
            return Ok(existingPropertyType);
        }
    }
}
