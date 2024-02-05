using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.DTO.CurrencyDTO
{
    public class CurrencyDto : BaseDto
    {
        public string Name { get; set; }

        public Currency ToCurrency()
        {
            return new Currency
            {
                Id = 0,
                Name = Name
            };
        }
    }
}
