using Microsoft.AspNetCore.Mvc;
using MidtermAPI_AryanGajjar.Models;
using MidtermAPI_AryanGajjar.Services;
using System.Diagnostics.Metrics;

namespace MidtermAPI_AryanGajjar.Controllers
{
    public class AGProductController : Controller
    {
        private readonly IAGProductService _service;
        private readonly AGUsageCounter _usagecounter;

        public AGProductController(IAGProductService service, AGUsageCounter usagecounter)
        {
            _service = service;
            _usagecounter = usagecounter;
        }


        [HttpGet]
        [Route("Products")]
        public IActionResult GetProducts()
        {
            var products = _service.GetAll();
            return Ok(products);
        }


        [HttpPost]
        [Route("Product")]
        public IActionResult CreateProduct([FromBody] AGProduct productName)
        {
            var error = ValidateProductGivenByUser(productName);
            if (error is not null)
            {
                return BadRequest(new
                {
                    error = "InvalidProduct",
                    message = error
                });
            }

            var created = _service.Add(productName);

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Product created",
                Product = created
            });
        }
        

        [HttpGet]
        [Route("ProductsJS")]
        public IActionResult ProductsJs([FromQuery] bool fail = false)
        {
            if (fail)
                throw new Exception("Intentional failure for middleware demo.");

            return Ok(new { ok = true, message = "Use ?fail=true to trigger exception middleware." });
        }

        
        [HttpGet]
        [Route("usage")]
        public IActionResult Usage()
        {
            Request.Headers.TryGetValue("X-Api-Key", out var apiKey);
            var key = apiKey.ToString();

            var count = _usagecounter.Increment(key);

            return Ok(new
            {
                apiKey = key,
                count = count
            });
        }

        private static string? ValidateProductGivenByUser(AGProduct productName)
        {
            var name = (productName.Name ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
                return "Name cannot be empty";

            if (name.Length < 1 || name.Length > 25)
                return "Name must be between 1 and 25 characters";

            if (productName.Quantity < 0)
                return "Quantity cannot be negative";


            productName.Name = name;

            return null;
        }
    }
}