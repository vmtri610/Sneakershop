using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ShippingService;
using ShippingService.Models;

namespace ShippingService.Services
{
    public class ShippingService : Shipping.ShippingBase
    {
        private readonly ShippingContext _context;
        private readonly ILogger<ShippingService> _logger;
        public ShippingService(ILogger<ShippingService> logger, ShippingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<CreateShippingReply> AddShipping(CreateShippingRequest request, ServerCallContext context)
        {
            _context.Shippings.Add(new Models.Shipping()
            {
                Status = request.Data.Status,
                OrderId = request.Data.OrderID,
            });
            _context.SaveChanges();
            return Task.FromResult(new CreateShippingReply
            {
                Message = "Created"
            });
        }

        public override Task<GetShippingPaginateReply> GetShippingPaginate(GetShippingPaginateRequest request, ServerCallContext context)
        {
            _context.Shippings.Load();
            var shippings = (from shipping in _context.Shippings
                         where shipping.Id > request.AfterID
                         select new ShippingData { Id = shipping.Id, Status = shipping.Status, OrderID = shipping.OrderId }).Take(request.Limit);
            var result = new GetShippingPaginateReply();
            foreach (var shipping in shippings)
            {
                result.ShippingList.Add(shipping);
            }
            return Task.FromResult(result);

        }

        public override Task<GetNumOfShippingReply> GetNumOfShipping(GetNumOfShippingRequest request, ServerCallContext context)
        {
            _context.Shippings.Load();
            var result = new GetNumOfShippingReply();
            result.Total = _context.Shippings.Count();
            return Task.FromResult(result);
        }

        public override Task<GetShippingByIdReply> GetShippingById(GetShippingByIdRequest request, ServerCallContext context)
        {
            var shipping = (from s in _context.Shippings
                        where s.Id == request.Id
                        select new ShippingData { Id = s.Id, Status = s.Status, OrderID = s.OrderId }).SingleOrDefault();

            var result = new GetShippingByIdReply { Data = shipping };
            return Task.FromResult(result);
        }

        public override Task<UpdateShippingReply> UpdateShipping(UpdateShippingRequest request, ServerCallContext context)
        {
            var shipping = (from s in _context.Shippings
                        where s.Id == request.Data.Id
                        select s).SingleOrDefault();
            if (shipping == null)
            {
                return Task.FromResult(new UpdateShippingReply { IsSuccess = false });
            }
            else
            {
                shipping.Status = request.Data.Status;
                _context.SaveChanges();
            }
            return Task.FromResult(new UpdateShippingReply { IsSuccess = true });
        }

        public override Task<DeleteShippingReply> DeleteShipping(DeleteShippingRequest request, ServerCallContext context)
        {
            var shipping = (from s in _context.Shippings
                            where s.OrderId == request.Id
                            select s).SingleOrDefault();
            if (shipping == null)
            {
                return Task.FromResult(new DeleteShippingReply { IsSuccess = false });
            }
            else
            {
                _context.Shippings.Remove(shipping);
                _context.SaveChanges();
            }
            return Task.FromResult(new DeleteShippingReply { IsSuccess = true });
        }


    }
}