using Microsoft.AspNetCore.Mvc;

namespace SneakerShop_Core.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class StockController : ControllerBase
    {
        private StockService.Stock.StockClient _stockClient;

        public StockController(StockService.Stock.StockClient stockClient)
        {
            this._stockClient = stockClient;
        }

        [HttpPost]
        public async Task<StockService.CreateStockReply> Create(StockService.CreateStockRequest createStockRequest)
        {
            var result = await _stockClient.AddStockAsync(createStockRequest);
            return result;
        }

        [HttpGet("paginate")]
        public async Task<StockService.GetStockPaginateReply> GetStockPaginate([FromQuery] long afterID, [FromQuery] int limit)
        {
            return await _stockClient.GetStockPaginateAsync(new StockService.GetStockPaginateRequest { AfterID = afterID, Limit = limit });
        }

        [HttpGet("total")]
        public async Task<StockService.GetNumOfStockReply> GetNumOfStock()
        {
            return await _stockClient.GetNumOfStockAsync(new StockService.GetNumOfStockRequest { Message = "" });
        }

        [HttpGet("search")]
        public async Task<StockService.GetStockByProdIdReply> GetStockById([FromQuery] long prodID)
        {
            return await _stockClient.GetStockByIdAsync(new StockService.GetStockByProdIdRequest { ProdID = prodID });
        }

        [HttpPut("update")]
        public async Task<StockService.UpdateStockReply> UpdateStock(StockService.UpdateStockRequest updateStockRequest)
        {
            var result = await _stockClient.UpdateStockAsync(updateStockRequest);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<StockService.DeleteStockReply> DeleteStock([FromQuery] long id)
        {
            var result = await _stockClient.DeleteStockAsync(new StockService.DeleteStockRequest { Id = id });
            return result;
        }
    }
}
