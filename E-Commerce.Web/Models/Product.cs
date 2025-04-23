using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product : ControllerBase
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}
