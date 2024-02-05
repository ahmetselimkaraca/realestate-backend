using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Api.DatabaseContext;
using RealEstateApp.Api.DTO.ListingDTO;
using RealEstateApp.Api.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public ListingController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Listings.
                Include(x => x.PropertyType).
                Include(x => x.Status).
                Include(x => x.Currency).
                Include(x => x.RealEstateUser).
                ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            var listingDetailList = new List<ListingDetailDto>();
            result.ForEach(x => listingDetailList.Add(new ListingDetailDto(x)));
            return Ok(listingDetailList);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Listings.
                Include(x => x.PropertyType).
                Include(x => x.Status).
                Include(x => x.Currency).
                Include(x => x.RealEstateUser).
                FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(new ListingAttributeIdDto(result));
        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> ViewById(int id)
        {
            var result = await _context.Listings.
                Include(x => x.PropertyType).
                Include(x => x.Status).
                Include(x => x.Currency).
                Include(x => x.RealEstateUser).
                FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(new ListingDetailDto(result));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewListingDto request)
        {
            int realEstateUserId = int.Parse(User.FindFirst("Id")?.Value);

            var newListing = request.ToListing();
            newListing.RealEstateUserId = realEstateUserId;

            await _context.Listings.AddAsync(newListing);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] NewListingDto request)
        {
            int realEstateUserId = int.Parse(User.FindFirst("Id")?.Value);
            var listing = await _context.Listings.FirstOrDefaultAsync(x => x.Id == request.Id && (User.IsInRole("Admin") || x.RealEstateUserId == realEstateUserId));

            if (listing == null)
            {
                return NotFound();
            }

            listing.Price = request.Price;
            listing.CurrencyId = request.CurrencyId;
            listing.StatusId = request.StatusId;
            listing.PropertyTypeId = request.PropertyTypeId;
            listing.StartDate = request.StartDate;
            listing.EndDate = request.EndDate;
            listing.Image = request.Image;
            listing.Thumbnail = request.Thumbnail;
            listing.Latitude = request.Latitude;
            listing.Longitude = request.Longitude;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            int realEstateUserId = int.Parse(User.FindFirst("Id")?.Value);

            // checking if the user is admin or the owner of the listing 
            var listing = await _context.Listings.FirstOrDefaultAsync(x => x.Id == id && (User.IsInRole("Admin") || x.RealEstateUserId == realEstateUserId));

            if (listing == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}