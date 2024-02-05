using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Api.DatabaseContext;
using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public CurrencyController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Currencies.ToListAsync();

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
            var result = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Currency currency)
        {
            await _context.Currencies.AddAsync(currency);
            await _context.SaveChangesAsync();

            return Ok(currency);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Currency currency)
        {
            var existingCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == currency.Id);
            if (existingCurrency == null)
            {
                return NotFound();
            }

            existingCurrency.Name = currency.Name;

            await _context.SaveChangesAsync();
            return Ok(currency);
        }


    }
}
