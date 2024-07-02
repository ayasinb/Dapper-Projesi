namespace DapperNight.Dtos.ProductDtos
{
    public class GetByIdProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
