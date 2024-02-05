namespace RealEstateApp.Api.Entity
{
    public class RealEstateUser : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Listing> Listings { get; set; }
    }
}
