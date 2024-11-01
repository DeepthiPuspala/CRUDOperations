using CRUDOperations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;

namespace CRUDOperations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly string connectionString = "Data Source=.;Integrated Security=True;Encrypt=False";

        [HttpPost]
        public IActionResult Create(Productdto productdto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO products (name, Brand, category, Price, description, createdAt) " +
                                 "OUTPUT inserted.* VALUES (@name, @Brand, @category, @price, @description, @CreatedAt)";

                    var product = new Product()
                    {
                        Name = productdto.Name,
                        Brand = productdto.Brand,
                        Category = productdto.Category,
                        Price = productdto.Price,
                        Description = productdto.Description,
                        CreatedAt = DateTime.Now
                    };

                    var newProduct = connection.QuerySingleOrDefault<Product>(sql, product);
                    if (newProduct != null)
                    {
                        return Ok(newProduct);
                    }
                    else
                    {
                        return BadRequest("Product creation failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return BadRequest("An error occurred while creating the product");
            }
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM products";
                    var products = connection.Query<Product>(sql).AsList(); // Query and convert to list
                    return Ok(products);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return BadRequest("An error occurred while retrieving products.");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * From products where Id = @Id";
                    var product = connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
                    if (product != null)
                    {
                        return Ok(product);
                    }
                    else
                    {
                        return NotFound("product not found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("we have xception: \n" + ex.Message);
                return BadRequest("An error occurred while retrieving the product.");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Productdto productdto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {


                    connection.Open();
                    string sql = "Update products SET name =@name,Brand =@Brand, category=@category, " + "Price=@Price,Description=@Description where Id=@Id";

                    var product = new Product()
                    {
                        Id = id,
                        Name = productdto.Name,
                        Brand = productdto.Brand,
                        Category = productdto.Category,
                        Price = productdto.Price,
                        Description = productdto.Description,
                    };

                    int count = connection.Execute(sql, product);
                    if (count < 1)
                    {
                        return NotFound("product not found");
                    }
                    else
                    {
                        return Ok(GetProduct(id));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an exception:\n" + ex.Message);
                return BadRequest("An error occured while updating the product");
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "Delete from products where id = @id";
                        int count = connection.Execute(sql, new { Id = id });
                        if (count < 1)
                        {
                            return NotFound();
                        }
                        else
                        {
                            return Ok("deleted prodcut");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("We have an exception: \n" + ex.Message);
                    return BadRequest();
                }

            }
        }
    }
}


        

        