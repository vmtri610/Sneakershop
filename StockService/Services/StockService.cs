using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using StockService.Models;

namespace StockService.Services
{
    public class StockService : Stock.StockBase
    {
        private readonly StockContext _context;
        private readonly ILogger<StockService> _logger;
        public StockService(ILogger<StockService> logger,StockContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<CreateStockReply> AddStock(CreateStockRequest request, ServerCallContext context)
        {
            var cvt_Quantity = BitConverter.GetBytes(request.Data.Quantity);
            _context.Stocks.Add(new Models.Stock()
            {
                ProdId = request.Data.ProdID,
                Quantity = cvt_Quantity

            });
            _context.SaveChanges();
            return Task.FromResult(new CreateStockReply
            {
                Message = "Created"
            });
        }

        public override Task<GetStockPaginateReply> GetStockPaginate(GetStockPaginateRequest request, ServerCallContext context)
        {
            _context.Stocks.Load();
           /* foreach(var stock in _context.Stocks)
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(stock.Quantity);
            }*/
            var stocks = (from stock in _context.Stocks
                         where stock.Id > request.AfterID
                         select new StockData 
                         { 
                             Id = stock.Id, 
                             ProdID = stock.ProdId, 
                             Quantity = BitConverter.ToInt32(stock.Quantity, 0) 
                         }).Take(request.Limit);
            var result = new GetStockPaginateReply();
            foreach (var stock in stocks)
            {
                result.StockList.Add(stock);
            }
            return Task.FromResult(result);

        }

        public override Task<GetNumOfStockReply> GetNumOfStock(GetNumOfStockRequest request, ServerCallContext context)
        {
            _context.Stocks.Load();
            var result = new GetNumOfStockReply();
            result.Total = _context.Stocks.Count();
            return Task.FromResult(result);
        }

        public override Task<GetStockByProdIdReply> GetStockById(GetStockByProdIdRequest request, ServerCallContext context)
        {
         /*   foreach (var s in _context.Stocks)
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(s.Quantity);
            }*/
            var stock = (from s in _context.Stocks
                        where s.ProdId == request.ProdID
                        select new StockData 
                        {
                            Id = s.Id,
                            ProdID = s.ProdId,
                            Quantity = BitConverter.ToInt32(s.Quantity, 0)
                        }).SingleOrDefault();

            var result = new GetStockByProdIdReply { Data = stock };
            return Task.FromResult(result);
        }

        public override Task<UpdateStockReply> UpdateStock(UpdateStockRequest request, ServerCallContext context)
        {
            var cvt_Quantity = BitConverter.GetBytes(request.Data.Quantity);
            var stock = (from s in _context.Stocks
                        where s.Id == request.Data.Id
                        select s).SingleOrDefault();

            if (stock == null)
            {
                return Task.FromResult(new UpdateStockReply { IsSuccess = false });
            }
            else
            {
                stock.Quantity = cvt_Quantity;
                _context.SaveChanges();
            }
            return Task.FromResult(new UpdateStockReply { IsSuccess = true });
        }

        public override Task<DeleteStockReply> DeleteStock(DeleteStockRequest request, ServerCallContext context)
        {
            var stock = (from s in _context.Stocks
                        where s.ProdId == request.Id
                        select s).SingleOrDefault();
            if (stock == null)
            {
                return Task.FromResult(new DeleteStockReply { IsSuccess = false });
            }
            else
            {
                _context.Stocks.Remove(stock);
                _context.SaveChanges();
            }
            return Task.FromResult(new DeleteStockReply { IsSuccess = true });
        }

    }
}