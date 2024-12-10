using Microsoft.AspNetCore.Mvc;
using static ShippingService.Shipping;

namespace SneakerShop_Core.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ShippingController : Controller
    {
        private ShippingService.Shipping.ShippingClient _shippingClient;
        public ShippingController(ShippingService.Shipping.ShippingClient shippingClient)
        {
            this._shippingClient = shippingClient;
        }

        [HttpPost]
        public async Task<ShippingService.CreateShippingReply> Create(ShippingService.CreateShippingRequest createShippingRequest)
        {
            var result = await _shippingClient.AddShippingAsync(createShippingRequest);
            return result;
        }

        [HttpGet("paginate")]
        public async Task<ShippingService.GetShippingPaginateReply> GetShippingPaginate([FromQuery] long afterID, [FromQuery] int limit)
        {
            return await _shippingClient.GetShippingPaginateAsync(new ShippingService.GetShippingPaginateRequest { AfterID = afterID, Limit = limit });
        }

        [HttpGet("total")]
        public async Task<ShippingService.GetNumOfShippingReply> GetNumOfShipping()
        {
            return await _shippingClient.GetNumOfShippingAsync(new ShippingService.GetNumOfShippingRequest { Message = "" });
        }

        [HttpGet("search")]
        public async Task<ShippingService.GetShippingByIdReply> GetShippingById([FromQuery] long id)
        {
            return await _shippingClient.GetShippingByIdAsync(new ShippingService.GetShippingByIdRequest { Id = id });
        }

        [HttpPut("update")]
        public async Task<ShippingService.UpdateShippingReply> UpdateShipping(ShippingService.UpdateShippingRequest updateShippingRequest)
        {
            var result = await _shippingClient.UpdateShippingAsync(updateShippingRequest);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<ShippingService.DeleteShippingReply> DeleteShipping([FromQuery] long id)
        {
            var result = await _shippingClient.DeleteShippingAsync(new ShippingService.DeleteShippingRequest { Id = id });
            return result;
        }
    }
}
