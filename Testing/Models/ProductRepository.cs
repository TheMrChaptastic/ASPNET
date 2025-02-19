﻿using Dapper;
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
            return _conn.QuerySingle<Product>("Select * FROM products WHERE ProductID = @Id;", new { Id = id });
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products SET Name = @name, Price = @price WHERE ProductID = @Id;", new { name = product.Name, price = product.Price, Id = product.ProductID });
        }

        public void InsertProduct(Product productToInsert)
        {
            _conn.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
                new { name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID });
        }

        public IEnumerable<Category> GetCategories()
        {
            return _conn.Query<Category>("SELECT * FROM categories;");
        }

        public Product AssignCategory()
        {
            var categoryList = GetCategories();
            var product = new Product();
            product.Categories = categoryList;

            return product;
        }

        public void DeleteProduct(Product product)
        {
            _conn.Execute("DELETE FROM REVIEWS WHERE ProductID = @Id;",
                new { Id = product.ProductID });
            _conn.Execute("DELETE FROM Sales WHERE ProductID = @Id;",
                new { Id = product.ProductID });
            _conn.Execute("DELETE FROM products WHERE ProductID = @Id;",
                new { Id = product.ProductID });
        }
    }
}
