using Dapper;
using DapperDemoProject.Context;
using DapperDemoProject.Dtos.ProductDtos;

namespace DapperDemoProject.Services.ProductServices
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
            string query = "insert into TblProduct (Name,Stock,Price,CategoryId) values(@name,@stock,@price,@categoryId)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", createProductDto.Name);
            parameters.Add("@stock", createProductDto.Stock);
            parameters.Add("@price", createProductDto.Price);
            parameters.Add("@categoryId", createProductDto.CategoryId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteProductAsync(int id)
        {
            string query = "delete From TblProduct Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "select ProductId,TblProduct.Name,CategoryName,Stock,Price from TblProduct inner join TblCategory on TblProduct.CategoryId=TblCategory.CategoryId";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values.ToList();
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(int id)
        {
            string query = "select ProductId,TblProduct.Name,CategoryName,Stock,Price from TblProduct inner join TblCategory on TblProduct.CategoryId=TblCategory.CategoryId where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var connection = _context.CreateConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetByIdProductDto>(query, parameters);
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            string query = "update TblProduct Set Name=@name,Stock=@stock,Price=@price,CategoryId=@categoryId Where ProductId=@productId";
            var parameters= new DynamicParameters();
            parameters.Add("@name",updateProductDto.Name);
            parameters.Add("@stock", updateProductDto.Stock);
            parameters.Add("@price", updateProductDto.Price);
            parameters.Add("@categoryId", updateProductDto.CategoryId);
            parameters.Add("@productId", updateProductDto.ProductId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
