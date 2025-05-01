using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceApstraction;
using Shared.DataTransfereObjects.BasketModuleDto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        { 
            var basket =await serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var Baskets = await serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Baskets);
        }
        [HttpGet("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var result = await serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(result);
        }
        
    }

}
