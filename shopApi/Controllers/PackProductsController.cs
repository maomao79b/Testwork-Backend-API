using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using shopApi.Models;
using System.Data;

namespace shopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PackProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"SELECT * FROM `packproduct`";

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
            string query = $"DELETE FROM `packproduct` WHERE id = {id};";

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
        public JsonResult Post(PackProducts packProducts)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"INSERT INTO `packproduct`(`Eid`, `Ename`, `brand`, `model`, `description`, `price`, `amount`, `status`, `image`) " +
                            @"VALUES (@Eid,@Ename,@brand,@model,@description,@price,@amount,@status,@image)";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@Eid", packProducts.Eid);
            mySqlCommand.Parameters.AddWithValue("@Ename", packProducts.Ename);
            mySqlCommand.Parameters.AddWithValue("@brand", packProducts.Brand);
            mySqlCommand.Parameters.AddWithValue("@model", packProducts.Model);
            mySqlCommand.Parameters.AddWithValue("@description", packProducts.Description);
            mySqlCommand.Parameters.AddWithValue("@price", packProducts.Price);
            mySqlCommand.Parameters.AddWithValue("@amount", packProducts.Amount);
            mySqlCommand.Parameters.AddWithValue("@status", packProducts.Status);
            mySqlCommand.Parameters.AddWithValue("@image", packProducts.Image);
            mySqlCommand.ExecuteNonQuery();

            mycon.Close();
            return new JsonResult("Insert Successfully");
        }
        [HttpPut]
        public JsonResult Put(PackProducts packProducts)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"UPDATE `packproduct` SET `Eid`=@Eid, `Ename`=@Ename, `brand`=@brand, `model`=@model, `description`=@description, 
                        `date`=@date, `price`=@price, `amount`=@amount, `status`=@status, `image`=@image WHERE `id`=@id";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@id", packProducts.Id);
            mySqlCommand.Parameters.AddWithValue("@Eid", packProducts.Eid);
            mySqlCommand.Parameters.AddWithValue("@Ename", packProducts.Ename);
            mySqlCommand.Parameters.AddWithValue("@brand", packProducts.Brand);
            mySqlCommand.Parameters.AddWithValue("@model", packProducts.Model);
            mySqlCommand.Parameters.AddWithValue("@description", packProducts.Description);
            //mySqlCommand.Parameters.AddWithValue("@date", packProducts.Date);
            mySqlCommand.Parameters.AddWithValue("@price", packProducts.Price);
            mySqlCommand.Parameters.AddWithValue("@amount", packProducts.Amount);
            mySqlCommand.Parameters.AddWithValue("@status", packProducts.Status);
            mySqlCommand.Parameters.AddWithValue("@image", packProducts.Image);
            mySqlCommand.ExecuteNonQuery();

            mycon.Close();
            return new JsonResult("Update Successfully");
        }

        [HttpGet("search")]
        public JsonResult SearchAll(string search)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = $"SELECT * FROM `acceptproduct` WHERE id LIKE '%{search}%' OR Ename LIKE '%{search}%' OR date LIKE '%{search}%' OR " +
                           $"status LIKE '%{search}%' OR price LIKE '%{search}%' OR brand LIKE '%{search}%'" +
                           $" OR model LIKE '%{search}%' OR description LIKE '%{search}%';";

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
