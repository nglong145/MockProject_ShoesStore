using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.Services.BrandService;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brand;

        public BrandController(IBrandService brand)
        {
            _brand = brand;
        }

        [HttpGet("get-all-brand")]
        public async Task<IActionResult> GetAllBrand()
        {
            var brands = await _brand.GetAllAsync();
            var brandVm = new List<BrandVm>();
            foreach (var brand in brands)
            {
                brandVm.Add(new BrandVm
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    BrandImage = brand.BrandImage,
                    Description = brand.Description
                });
            }
            return Ok(brandVm);
        }

        [HttpGet("get-brand-by-id/{id}")]
        public async Task<IActionResult> GetBrandById(Guid id)
        {
            var brand = await _brand.GetByIdAsync(id);
            if(brand != null)
            {
                var brandVm = new BrandVm()
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    BrandImage = brand.BrandImage,
                    Description = brand.Description
                };
                return Ok(brandVm);
            }
            return NotFound("The brand does not exist!");
        }

        [HttpPost("add-new-brand")]
        public async Task<IActionResult> AddNewBrand([FromBody] AddBrandVm addBrandVm)
        {
            var brand = new Brand()
            {
                BrandId = Guid.NewGuid(),
                BrandName = addBrandVm.BrandName,
                BrandImage = addBrandVm.BrandImage,
                Description = addBrandVm.Description
            };
            await _brand.AddAsync(brand);
            return Ok(brand);
        }

        [HttpPut("update-brand/{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, [FromBody] AddBrandVm addBrandVm)
        {
            var brand = await _brand.GetByIdAsync(id);
            if (brand != null)
            {
                brand.BrandName = addBrandVm.BrandName;
                brand.BrandImage = addBrandVm.BrandImage;
                brand.Description = addBrandVm.Description;

                await _brand.UpdateAsync(brand);
                return Ok(brand);
            }
            return BadRequest("The brand does not exist!");
        }

        [HttpDelete("delete-brand/{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var brand = await _brand.GetByIdAsync(id);
            if (brand != null)
            {
                await _brand.DeleteAsync(brand);
                return Ok(brand);
            }
            return BadRequest("Delete Faild!");
        }
    }
}
