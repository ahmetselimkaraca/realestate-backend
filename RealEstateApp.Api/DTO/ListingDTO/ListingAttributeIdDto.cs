using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.DTO.ListingDTO
{
    public class ListingAttributeIdDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int PropertyTypeId { get; set; } // PropertyType.Description
        public int StatusId { get; set; } // Status.Description
        public int CurrencyId { get; set; } // Currency.Description
        public decimal Price { get; set; }
        public int RealEstateUserId { get; set; } // RealEstateUser.Username

        public ListingAttributeIdDto(Listing listing)
        {
            StartDate = listing.StartDate;
            EndDate = listing.EndDate;
            Image = listing.Image;
            Thumbnail = listing.Thumbnail;
            Latitude = listing.Latitude;
            Longitude = listing.Longitude;
            PropertyTypeId = listing.PropertyTypeId;
            StatusId = listing.StatusId;
            CurrencyId = listing.CurrencyId;
            Price = listing.Price;
            RealEstateUserId = listing.RealEstateUser.Id;

        }
    }
}
