﻿using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.BrandService;
using ShoesStoreApp.BLL.Services.Image;
using ShoesStoreApp.BLL.Services.ProductService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IProductService _productService;
        private readonly IBrandService _brandService;

        public ProductController(IImageService imageService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
                                 IProductService productService, IBrandService brandService)
        {
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;

            _productService = productService;
            _brandService = brandService;
        }

        // Upload Image Product
        [HttpPost("Upload-Image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded or file is empty.");
                }

                _imageService.ValidateFileUpload(file);

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Product", $"{fileName}{fileExtension}");

                using (var stream = new FileStream(localPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var uploadedImage = await _imageService.SaveImageToDatabaseAsync(fileName, fileExtension);

                return Ok(uploadedImage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpGet("Get-All-Product")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productService.GetAllAsync();
            var productVms = new List<ProductVm>();

            foreach (var product in products)
            {
                var brand = await _brandService.GetByIdAsync(product.BrandId);
                productVms.Add(new ProductVm
                {
                    ProductId = product.ProductId,
                    BrandId = product.BrandId,
                    BrandName = brand.BrandName,
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    Price = product.Price,
                    Description = product.Description,
                    Status = product.Status,
                });
            }

            return Ok(productVms);
        }


        [HttpGet("Get-All-Product-With-Status")]
        public async Task<IActionResult> GetAllProductWithStatus([FromQuery] string status,int pageIndex,int pageSize)
        {

            if (string.IsNullOrEmpty(status))
            {
                return BadRequest(new { message = "Status cannot be null or empty." });
            }

            if (pageIndex <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "PageIndex and PageSize must be greater than 0." });
            }

            var products = await _productService.GetProductsByStatusAsync(status, pageIndex, pageSize);

            var productVms = products.Items.Select(product => new ProductVm
            {
                ProductId = product.ProductId,
                BrandId = product.BrandId,
                BrandName = product.Brand.BrandName,
                ProductName = product.ProductName,
                ProductImage = product.ProductImage,
                Price = product.Price,
                Description = product.Description,
                Status = product.Status,
            }).ToList();

            var response = new PaginatedResult<ProductVm>(productVms, products.TotalPages, products.PageIndex, pageSize);

            return Ok(response);

        }


        [HttpPost("Get-Filtered-Products")]
        public async Task<IActionResult> GetFilteredProducts([FromBody] ProductFilterVm filter)
        {
            if (filter.PageIndex <= 0 || filter.PageSize <= 0)
            {
                return BadRequest(new { message = "PageIndex and PageSize must be greater than 0." });
            }

            var products = await _productService.GetFilteredProductsAsync(filter);

            var productVms = products.Items.Select(product => new ProductVm
            {
                ProductId = product.ProductId,
                BrandId = product.BrandId,
                BrandName = product.Brand.BrandName,
                ProductName = product.ProductName,
                ProductImage = product.ProductImage,
                Price = product.Price,
                Description = product.Description,
                Status = product.Status,
            }).ToList();

            var response = new PaginatedResult<ProductVm>(productVms, products.TotalPages, products.PageIndex, filter.PageSize);

            return Ok(response);
        }

        [HttpGet("Get-Product-By-Id/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product != null)
            {
                var brand = await _brandService.GetByIdAsync(product.BrandId);
                var producVm = new ProductVm
                {
                    ProductId = product.ProductId,
                    BrandId = product.BrandId,
                    BrandName = brand.BrandName,
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    Price = product.Price,
                    Description = product.Description,
                    Status = product.Status,
                };

                return Ok(producVm);
            }

            return NotFound($"Product with ID {id} doesn't already exist");
        }


        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductVm productVm)
        {
            var brand = await _brandService.GetByIdAsync(productVm.BrandId);

            if (brand == null)
            {
                return BadRequest("Brand doesn't already exist");
            }

            var product = new Product
            {
                BrandId = productVm.BrandId,
                ProductName = productVm.ProductName,
                ProductImage = productVm.ProductImage,
                Price = productVm.Price,
                Description = productVm.Description,
                Status = productVm.Status
            };

            await _productService.AddAsync(product);

            var productResponse = new ProductVm
            {
                ProductId = product.ProductId,
                BrandId = product.BrandId,
                ProductName = product.ProductName,
                ProductImage = product.ProductImage,
                Price = product.Price,
                Description = product.Description,
                Status = product.Status
            };



            return Ok(productResponse);

        }


        [HttpPut("Update-Product/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] AddProductVm productVm)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product != null)
            {
                var brand = await _brandService.GetByIdAsync(productVm.BrandId);

                if (brand == null)
                {
                    return BadRequest("Brand doesn't already exist");
                }

                product.BrandId= productVm.BrandId;
                product.ProductName= productVm.ProductName;
                product.ProductImage= productVm.ProductImage;
                product.Price = productVm.Price;
                product.Description = productVm.Description;    
                product.Status = productVm.Status;

                await _productService.UpdateAsync(product);
                return Ok(product);
            }

            return NotFound($"Product with ID {id} doesn't already exist");

        }


        [HttpPut("Delete-Product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, [FromBody] DeleteProductVm productVm)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product != null)
            {
                product.Status = productVm.Status;

                await _productService.UpdateAsync(product);
                return Ok("Delete Succes");
            }

            return BadRequest("Delete Fail");

        }
    }
}
