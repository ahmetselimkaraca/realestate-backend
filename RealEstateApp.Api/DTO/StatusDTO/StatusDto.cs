using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.DTO.StatusDTO
{
    public class StatusDto : BaseDto
    {
        public string Description { get; set; }

        public Status ToStatus()
        {
            return new Status
            {
                Id = 0,
                Description = Description
            };
        }
    }
}
