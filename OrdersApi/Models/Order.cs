using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; }
        public string? CustomerName { get; set; }
        public int ProductId { get; set; }
    }
}
