using MassTransit;
using OrdersApi.Data;
using OrdersApi.Models;
using Shared;

namespace OrdersApi.Consumer
{
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        private readonly OrdersApiContext _context;
        public ProductCreatedConsumer(OrdersApiContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var newProduct = new Product
            {
                Name = context.Message.Name
            };
            _context.Add(newProduct);
            await _context.SaveChangesAsync();  
        }
    }
}
