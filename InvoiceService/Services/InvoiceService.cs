using Grpc.Core;
using InvoiceService;
using InvoiceService.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Services
{
    public class InvoiceService : Invoice.InvoiceBase
    {
        private readonly InvoiceContext _context;
        private readonly ILogger<InvoiceService> _logger;
        public InvoiceService(ILogger<InvoiceService> logger, InvoiceContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<CreateInvoiceReply> AddInvoice(CreateInvoiceRequest request, ServerCallContext context)
        {
            _context.Invoices.Add(new Models.Invoice()
            {
                OrderId = request.Data.OrderID,
                CreatedAt = DateTime.Now.ToString()
            });
            _context.SaveChanges();
            return Task.FromResult(new CreateInvoiceReply
            {
                Message = "Created"
            });
        }

        public override Task<GetInvoicePaginateReply> GetInvoicePaginate(GetInvoicePaginateRequest request, ServerCallContext context)
        {
            _context.Invoices.Load();
            var invoices = (from invoice in _context.Invoices
                             where invoice.Id > request.AfterID
                             select new InvoiceData { Id = invoice.Id, OrderID = invoice.OrderId, CreateAt = invoice.CreatedAt }).Take(request.Limit);
            var result = new GetInvoicePaginateReply();
            foreach (var invoice in invoices)
            {
                result.InvoiceList.Add(invoice);
            }
            return Task.FromResult(result);

        }

        public override Task<GetNumOfInvoiceReply> GetNumOfInvoice(GetNumOfInvoiceRequest request, ServerCallContext context)
        {
            _context.Invoices.Load();
            var result = new GetNumOfInvoiceReply();
            result.Total = _context.Invoices.Count();
            return Task.FromResult(result);
        }

        public override Task<GetInvoiceByIdReply> GetInvoiceById(GetInvoiceByIdRequest request, ServerCallContext context)
        {
            var invoice = (from i in _context.Invoices
                            where i.Id == request.Id
                            select new InvoiceData { Id = i.Id, OrderID = i.OrderId, CreateAt = i.CreatedAt }).SingleOrDefault();

            var result = new GetInvoiceByIdReply { Data = invoice };
            return Task.FromResult(result);
        }

 
        public override Task<DeleteInvoiceReply> DeleteInvoice(DeleteInvoiceRequest request, ServerCallContext context)
        {
            var invoice = (from s in _context.Invoices
                            where s.OrderId == request.Id
                            select s).SingleOrDefault();
            if (invoice == null)
            {
                return Task.FromResult(new DeleteInvoiceReply { IsSuccess = false });
            }
            else
            {
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();
            }
            return Task.FromResult(new DeleteInvoiceReply { IsSuccess = true });
        }



    }
}