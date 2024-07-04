using Dapper;
using DapperNight.Context;
using DapperNight.Dtos.CategoryDtos;
using DapperNight.Dtos.ProductDtos;

namespace DapperNight.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DapperContext _context;

        public ProductService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            string query = "insert into TblProduct (Name,Quantity,Price,CategoryId) values (@name, @stock, @price, @categoryId)";
            var parameters = new DynamicParameters();
            parameters.Add("name", createProductDto.Name);
            parameters.Add("stock", createProductDto.Quantity);
            parameters.Add("price", createProductDto.Price);
            parameters.Add("categoryId", createProductDto.CategoryId);
            var connection=_context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteProductAsync(int id)
        {
            string query = "Delete From TblProduct where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("productId", id);
            var connection=_context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * From TblProduct";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultProductDto>(query);    
            return values.ToList(); 
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            string query = "SELECT dbo.TblProduct.ProductId, dbo.TblProduct.Name, dbo.TblCategory.CategoryName, dbo.TblProduct.Quantity, dbo.TblProduct.Price FROM   dbo.TblProduct INNER JOIN dbo.TblCategory ON dbo.TblProduct.CategoryId = dbo.TblCategory.CategoryId";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
            return values.ToList();
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryProcAsync()
        {
            string query = "Execute ProductList";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
            return values.ToList();
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(int id)
        {
            string query = "Select * From TblProduct Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var connection = _context.CreateConnection();
            var value = await connection.QueryFirstOrDefaultAsync<GetByIdProductDto>(query, parameters);
            return value;
        }

        public async Task<int> GetProductCountAsync()
        {
            string query = "Select count(*) from TblProduct";
            var connection = _context.CreateConnection();
            int productCount=await connection.QueryFirstAsync<int>(query);
            return productCount;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            string query = "Update TblProduct Set Name=@p1,Quantity=@p2,Price=@p3,CategoryId=@p4 where ProductId=@p5";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", updateProductDto.Name);
            parameters.Add("@p2", updateProductDto.Quantity);
            parameters.Add("@p3", updateProductDto.Price);
            parameters.Add("@p4", updateProductDto.CategoryId);
            parameters.Add("@p5", updateProductDto.ProductId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
