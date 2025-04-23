using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return new Product() { id = id };
        }
        [HttpPost]
        public ActionResult<Product> Addproduct()
        {
            return new Product();
        }
        [HttpPut]
        public ActionResult<Product> Updateproduct()
        {
            return new Product();
        }

        [HttpDelete]
        public ActionResult<Product> Deleteproduct()
        {
            return new Product();
        }

    }
}
