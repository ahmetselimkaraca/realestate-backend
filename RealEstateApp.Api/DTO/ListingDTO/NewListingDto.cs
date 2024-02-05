using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.DTO.ListingDTO
{
    public class NewListingDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Image { get; set; }
        public string Thumbnail { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int PropertyTypeId { get; set; }
        public int StatusId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Price { get; set; }

        public Listing ToListing()
        {
            return new Listing
            {
                Id = 0,
                StartDate = StartDate,
                EndDate = EndDate,
                Image = Image,
                Thumbnail = Thumbnail,
                Latitude = Latitude,
                Longitude = Longitude,
                PropertyTypeId = PropertyTypeId,
                StatusId = StatusId,
                CurrencyId = CurrencyId,
                Price = Price,

            };
        }
    }
}
