using Microsoft.AspNetCore.Mvc;
using static InvoiceService.Invoice;

namespace SneakerShop_Core.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class InvoiceController : Controller
    {
        private InvoiceService.Invoice.InvoiceClient _invoiceClient;
        private OrderService.Order.OrderClient _orderClient;

        public InvoiceController(InvoiceService.Invoice.InvoiceClient invoiceClient, OrderService.Order.OrderClient orderClient)
        {
            this._invoiceClient = invoiceClient;
            this._orderClient = orderClient;
        }

        [HttpPost]
        public async Task<InvoiceService.CreateInvoiceReply> Create(InvoiceService.CreateInvoiceRequest createInvoiceRequest)
        {
            var result = await _invoiceClient.AddInvoiceAsync(createInvoiceRequest);
            return result;
        }

        [HttpGet("paginate")]
        public async Task<InvoiceService.GetInvoicePaginateReply> GetInvoicePaginate([FromQuery] long afterID, [FromQuery] int limit)
        {
            return await _invoiceClient.GetInvoicePaginateAsync(new InvoiceService.GetInvoicePaginateRequest { AfterID = afterID, Limit = limit });
        }

        [HttpGet("total")]
        public async Task<InvoiceService.GetNumOfInvoiceReply> GetNumOfInvoice()
        {
            return await _invoiceClient.GetNumOfInvoiceAsync(new InvoiceService.GetNumOfInvoiceRequest { Message = "" });
        }

        [HttpGet("search")]
        public async Task<InvoiceService.GetInvoiceByIdReply> GetInvoiceById([FromQuery] long id)
        {
            return await _invoiceClient.GetInvoiceByIdAsync(new InvoiceService.GetInvoiceByIdRequest { Id = id });
        }

        [HttpDelete("delete")]
        public async Task<InvoiceService.DeleteInvoiceReply> DeleteInvoice([FromQuery] long id)
        {
            var result = await _invoiceClient.DeleteInvoiceAsync(new InvoiceService.DeleteInvoiceRequest { Id = id });
            return result;
        }

        [HttpGet("detail")]
        public async Task<Models.InvoiceResponse> GetInvoiceDetails([FromQuery] long id)
        {
            var invoice = await _invoiceClient.GetInvoiceByIdAsync(new InvoiceService.GetInvoiceByIdRequest { Id = id });
            var order = await _orderClient.GetOrderByIdAsync(new OrderService.GetOrderByIdRequest { Id = invoice.Data.Id });
            return new Models.InvoiceResponse { CreateAt = invoice.Data.CreateAt, Id = invoice.Data.Id, OrderData = order.OrderData, OrderDetailData = order.OrderDetailData.ToList() };
        }
    }
}
