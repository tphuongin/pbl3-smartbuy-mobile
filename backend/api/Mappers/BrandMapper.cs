using api.DTOs.Brand;
using api.Models;

namespace api.Mappers
{
    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand brand)
        {
            return new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Logo = brand.Logo,
                Categories = brand.Categories?.Select(c => c.ToDTO()).ToList()
            };
        }
    }
}