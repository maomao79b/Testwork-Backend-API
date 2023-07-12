using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using shopApi.Models;
using System.Data;
using System.Xml.Linq;

namespace shopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get(string? id, string? name)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"SELECT * FROM `customer`";

            if (!string.IsNullOrEmpty(id))
            {
                query += $" WHERE `id` = '{id}'";
            }
            else if (!string.IsNullOrEmpty(name))
            {
                query += $" WHERE `name` = '{name}'";
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
            string query = $"DELETE FROM `customer` WHERE id = {id};";

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
        public JsonResult Post(Customers customers)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            
            string query =  $"INSERT INTO `customer`(`name`, `age`, `address`, `phone`, `username`, `password`) " +
                            $"VALUES (@name,@age,@address,@phone,@username,@password)";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            
            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@name", customers.Name);
            mySqlCommand.Parameters.AddWithValue("@age", customers.Age);
            mySqlCommand.Parameters.AddWithValue("@address", customers.Address);
            mySqlCommand.Parameters.AddWithValue("@phone", customers.Phone);
            mySqlCommand.Parameters.AddWithValue("@username", customers.Username);
            mySqlCommand.Parameters.AddWithValue("@password", customers.Password);
            mySqlCommand.ExecuteReader();

            mycon.Close();
            return new JsonResult("Inserted Successfully");
        }

        [HttpPut]
        public JsonResult Put(Customers customers)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"UPDATE `customer` SET `name`=@name,`age`=@age," +
                @"`address`=@address,`phone`=@phone,`username`=@username,`password`=@password WHERE `id`=@id";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@id", customers.Id);
            mySqlCommand.Parameters.AddWithValue("@name", customers.Name);
            mySqlCommand.Parameters.AddWithValue("@age", customers.Age);
            mySqlCommand.Parameters.AddWithValue("@address", customers.Address);
            mySqlCommand.Parameters.AddWithValue("@phone", customers.Phone);
            mySqlCommand.Parameters.AddWithValue("@username", customers.Username);
            mySqlCommand.Parameters.AddWithValue("@password", customers.Password);
            mySqlCommand.ExecuteNonQuery();

            mycon.Close();
            return new JsonResult("Updated Successfully");
        }


    }
}
