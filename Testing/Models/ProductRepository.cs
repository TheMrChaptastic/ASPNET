using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Testing.Models
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        private readonly IDbConnection _conn;
        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products");
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("Select * FROM products WHERE ProductID = @Id;", new { @Id = id });
        }
    }
}
