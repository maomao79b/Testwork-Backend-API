using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using shopApi.Models;
using System.Data;
using System.Xml.Linq;

namespace shopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get(string? id)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;

            string query = @"SELECT * FROM `product`";

            if (!string.IsNullOrEmpty(id))
            {
                query += $" WHERE `id` = '{id}'";
            }


            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            myReader = mySqlCommand.ExecuteReader();
            table.Load(myReader);

            myReader.Close();
            mycon.Close();
            return new JsonResult(table);
        }
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = $"DELETE FROM `product` WHERE id = {id};";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            myReader = mySqlCommand.ExecuteReader();
            table.Load(myReader);

            myReader.Close();
            mycon.Close();
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Products products)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"INSERT INTO `product`(`brand`, `model`, `description`, `price`, `amount`, `category`,`image`) " +
                            @"VALUES (@brand,@model,@description,@price,@amount,@category,@image)";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@brand", products.Brand);
            mySqlCommand.Parameters.AddWithValue("@model", products.Model);
            mySqlCommand.Parameters.AddWithValue("@description", products.Description);
            mySqlCommand.Parameters.AddWithValue("@price", products.Price);
            mySqlCommand.Parameters.AddWithValue("@amount", products.Amount);
            mySqlCommand.Parameters.AddWithValue("@category", products.Category);
            mySqlCommand.Parameters.AddWithValue("@image", products.Image);
            mySqlCommand.ExecuteNonQuery();

            mycon.Close();
            return new JsonResult("Insert Successfully");
        }
        [HttpPut]
        public JsonResult Put(Products products)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"UPDATE `product` SET `brand`=@brand,`model`=@model,`description`=@description," +
                @"`price`=@price,`amount`=@amount,`category`=@category,`image`=@image WHERE `id`=@id";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@id", products.Id);
            mySqlCommand.Parameters.AddWithValue("@brand", products.Brand);
            mySqlCommand.Parameters.AddWithValue("@model", products.Model);
            mySqlCommand.Parameters.AddWithValue("@description", products.Description);
            mySqlCommand.Parameters.AddWithValue("@price", products.Price);
            mySqlCommand.Parameters.AddWithValue("@amount", products.Amount);
            mySqlCommand.Parameters.AddWithValue("@category", products.Category);
            mySqlCommand.Parameters.AddWithValue("@image", products.Image);
            mySqlCommand.ExecuteNonQuery();


            mycon.Close();

            return new JsonResult("Updated Successfully");
        }

        [HttpGet("search")]
        public JsonResult SearchAll(string search)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = $"SELECT * FROM `product` WHERE id LIKE '%{search}%' OR price LIKE '%{search}%' OR brand LIKE '%{search}%' OR " +
                           $"model LIKE '%{search}%' OR description LIKE '%{search}%' OR category LIKE '%{search}%';";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            myReader = mySqlCommand.ExecuteReader();
            table.Load(myReader);

            myReader.Close();
            mycon.Close();
            return new JsonResult(table);
        }
    }
}
