using Dapper;
using DapperDemoProject.Context;
using DapperDemoProject.Dtos.CategoryDtos;

namespace DapperDemoProject.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly DapperContext _context;

        public CategoryService(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            string query = "insert into TblCategory (CategoryName) values (@categoryName)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", createCategoryDto.CategoryName);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var query = "delete From TblCategory where CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var query = "select * from TblCategory";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultCategoryDto>(query);
            return values.ToList();
        }
        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(int id)
        {
            string query = "select * from TblCategory Where CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryId", id);
            var connection = _context.CreateConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetByIdCategoryDto>(query, parameters);
            return values;
        }
        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {

            var query = "Update TblCategory Set CategoryName=@categoryName Where CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryId",updateCategoryDto.CategoryId);
            parameters.Add("@categoryName",updateCategoryDto.CategoryName);
            var connection = _context.CreateConnection(); 
            await connection.ExecuteAsync(query,parameters);

        }
    }
}
