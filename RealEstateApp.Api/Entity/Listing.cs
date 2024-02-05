using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Api.Entity
{
    public class Listing : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int PropertyTypeId { get; set; }
        public int StatusId { get; set; }
        public int CurrencyId { get; set; }
        public int RealEstateUserId { get; set; }

        public PropertyType PropertyType { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public RealEstateUser RealEstateUser { get; set; }
    }
}
