using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using w10d4_CICD.Models;
using w10d4_CICD.Services;

namespace w10d4_CICD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var appName = _configuration["AppSettings:AppName"];
            var currency = _configuration["AppSettings:DefaultCurrency"];
            Response.Headers.Add("X-App-Name", appName);
            Response.Headers.Add("X-Currency", currency);

            return Ok(_productService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            _productService.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
    }
}
