using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductService : Product.ProductBase
    {
        private readonly ProductContext _context;
        private readonly ILogger<ProductService> _logger;
        public ProductService(ILogger<ProductService> logger, ProductContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<CreateProductReply> AddProduct(CreateProductRequest request, ServerCallContext context)
        {
            _context.Products.Add(new Models.Product()
            {
                Name = request.Data.Name,
                Category= request.Data.Category,
                Description= request.Data.Description,
                ImageUrl= request.Data.ImageUrl,
                Price= request.Data.Price,
            });
            _context.SaveChanges();
            return Task.FromResult(new CreateProductReply
            {
                Message = "Created"
            });
        }

        public override Task<GetProductPaginateReply> GetProductPaginate(GetProductPaginateRequest request, ServerCallContext context)
        {
            _context.Products.Load();
            var prods = (from prod in _context.Products
                         where prod.Id > request.AfterID
                         select new ProductData 
                         { 
                             Id = prod.Id, 
                             Name = prod.Name, 
                             Category = prod.Category, 
                             Description = prod.Description,
                             Price= prod.Price,
                             ImageUrl = prod.ImageUrl
                         }).Take(request.Limit);
            var result = new GetProductPaginateReply();
            foreach (var prod in prods)
            {
                result.ProductList.Add(prod);
            }
            return Task.FromResult(result);

        }

        public override Task<GetNumOfProductReply> GetNumOfProduct(GetNumOfProductRequest request, ServerCallContext context)
        {
            _context.Products.Load();
            var result = new GetNumOfProductReply();
            result.Total = _context.Products.Count();
            return Task.FromResult(result);
        }

        public override Task<GetProductByIdReply> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            var prod = (from p in _context.Products
                        where p.Id == request.Id
                        select new ProductData 
                        { 
                            Id = p.Id, 
                            Name = p.Name,
                            Category = p.Category,
                            Description = p.Description,
                            Price = p.Price,
                            ImageUrl = p.ImageUrl
                        }).SingleOrDefault();

            var result = new GetProductByIdReply { Data = prod };
            return Task.FromResult(result);
        }

        public override Task<UpdateProductReply> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var prod = (from p in _context.Products
                        where p.Id == request.Data.Id
                        select p).SingleOrDefault();

            if (prod == null)
            {
                return Task.FromResult(new UpdateProductReply { IsSuccess = false });
            }
            else
            {
                prod.Name = request.Data.Name;
                prod.Price = request.Data.Price;
                prod.ImageUrl = request.Data.ImageUrl;
                prod.Description = request.Data.Description;
                prod.Category = request.Data.Category;
                _context.SaveChanges();
            }
            return Task.FromResult(new UpdateProductReply { IsSuccess = true });
        }

        public override Task<DeleteProductReply> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            var prod = (from p in _context.Products
                        where p.Id == request.Id
                        select p).SingleOrDefault();
            if (prod == null)
            {
                return Task.FromResult(new DeleteProductReply { IsSuccess = false });
            }
            else
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
            }
            return Task.FromResult(new DeleteProductReply { IsSuccess = true });
        }

        public override Task<GetProductPriceReply> GetProductPrice(GetProductPriceRequest request, ServerCallContext context)
        {
            var price  = (from p in _context.Products
                          where p.Id == request.Id
                          select p.Price).SingleOrDefault();
            return Task.FromResult(new GetProductPriceReply { Price = price });
        }
    }
}