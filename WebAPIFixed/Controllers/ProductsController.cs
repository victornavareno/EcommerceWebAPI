using CRUDWithWebAPI.DAL;
using CRUDWithWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClientMVC.Models;

namespace CRUDWithWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public ProductsController(MyAppDbContext context)
        {
            _context = context;
        }


        // Obtiene todos los productos
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();

                // los productos recibidos a la api estan vacios
                if (products.Count == 0)
                {
                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Ecuentra el producto con un id determinado
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                {
                    return NotFound($"Product details not found with id {id}");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Product model) { 
            try
            {
                _context.Products.Add(model);
                _context.SaveChanges();

                return Ok("Product created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Product model)
        {
            try
            {
                if (model == null || model.Id == 0)
                {
                    if (model == null)
                    {
                        return BadRequest("Model data is invalid.");
                    }

                    else if (model.Id == 0)
                    {
                        return BadRequest("Product id is empty.");
                    }
                }
                var product = _context.Products.Find(model.Id);
                if (product == null)
                {
                    return NotFound($"Product not found with id {model.Id}");

                }

                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _context.SaveChanges();
                return Ok("Product details updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product not found with id {id}");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("Product details deleted.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     
    }
}
