using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.DTO.ListingDTO
{
    public class ListingDetailDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string PropertyType { get; set; } // PropertyType.Description
        public string Status { get; set; } // Status.Description
        public string Currency { get; set; } // Currency.Description
        public decimal Price { get; set; }
        public int RealEstateUser { get; set; } // RealEstateUser.Username

        public ListingDetailDto(Listing listing)
        {
            Id = listing.Id;
            StartDate = listing.StartDate;
            Image = listing.Image;
            Thumbnail = listing.Thumbnail;
            Latitude = listing.Latitude;
            Longitude = listing.Longitude;
            EndDate = listing.EndDate;
            PropertyType = listing.PropertyType.Description;
            Status = listing.Status.Description;
            Currency = listing.Currency.Name;
            Price = listing.Price;
            RealEstateUser = listing.RealEstateUser.Id;

        }
    }
}
