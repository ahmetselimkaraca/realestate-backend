using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.DTO.PropertyTypeDTO
{
    public class PropertyTypeDto : BaseDto
    {
        public string Description { get; set; }

        public PropertyType ToPropertyType()
        {
            return new PropertyType
            {
                Id = 0,
                Description = Description
            };
        }
    }
}
