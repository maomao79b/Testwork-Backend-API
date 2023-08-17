using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using shopApi.Models;
using System.Data;

namespace shopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcceptProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AcceptProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"SELECT * FROM `acceptproduct`";
           
            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();
            
            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            myReader = mySqlCommand.ExecuteReader();
            table.Load(myReader);

            myReader.Close();
            mycon.Close();

            return new JsonResult(table);
        }

        [HttpGet("id")]
        public JsonResult GetByid(string id)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = $"SELECT * FROM `acceptproduct` WHERE id = {id}";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            myReader = mySqlCommand.ExecuteReader();
            table.Load(myReader);

            myReader.Close();
            mycon.Close();

            return new JsonResult(table);
        }

        [HttpGet("confirmPage")]
        public JsonResult GetConfirm()
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"SELECT * FROM `acceptproduct` WHERE status = 'รออนุมัติ';";

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
            string query = $"DELETE FROM `acceptproduct` WHERE id = {id};";

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
        public JsonResult Post(AcceptProducts acceptProducts)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"INSERT INTO `acceptproduct`(`Eid`, `Ename`, `brand`, `model`, `description`, `price`, `amount`, `status`, `product`, `image`) " +
                            @"VALUES (@Eid,@Ename,@brand,@model,@description,@price,@amount,@status,@product,@image);";
            //string getmax = @"SELECT MAX(`id`) as `id` FROM `acceptproduct`;";
            //DataTable table = new DataTable();
            //MySqlDataReader myReader;
            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@Eid", acceptProducts.Eid);
            mySqlCommand.Parameters.AddWithValue("@Ename", acceptProducts.Ename);
            mySqlCommand.Parameters.AddWithValue("@brand", acceptProducts.Brand);
            mySqlCommand.Parameters.AddWithValue("@model", acceptProducts.Model);
            mySqlCommand.Parameters.AddWithValue("@description", acceptProducts.Description);
            mySqlCommand.Parameters.AddWithValue("@price", acceptProducts.Price);
            mySqlCommand.Parameters.AddWithValue("@amount", acceptProducts.Amount);
            mySqlCommand.Parameters.AddWithValue("@status", acceptProducts.Status);
            mySqlCommand.Parameters.AddWithValue("@product", acceptProducts.Product);
            mySqlCommand.Parameters.AddWithValue("@image", acceptProducts.Image);
            mySqlCommand.ExecuteNonQuery();

            //MySqlCommand mySqlCommand2 = new MySqlCommand(getmax, mycon);
            //myReader = mySqlCommand2.ExecuteReader();
            //table.Load(myReader);

            mycon.Close();
            return new JsonResult("Inserted Successfully");
        }

        [HttpPut("status")]
        public JsonResult UpdateStatus(UpdateStatusAccept updateStatus)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"UPDATE `acceptproduct` SET `status` = @status WHERE `acceptproduct`.`id` = @id;";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@id", updateStatus.Id);
            mySqlCommand.Parameters.AddWithValue("@status", updateStatus.Status);
            mySqlCommand.ExecuteNonQuery();


            mycon.Close();
            return new JsonResult("Updated Succesfully");
        }


        [HttpPut]
        public JsonResult Update(AcceptProducts update)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"UPDATE `acceptproduct` SET `Eid`=@Eid,`Ename`=@Ename,`brand`=@brand,`model`=@model,
                            `description`=@description,`price`=@price,`amount`=@amount,`status`=@status,`product`=@product,
                            `image`=@image WHERE `id`=@id";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("@id", update.Id);
            mySqlCommand.Parameters.AddWithValue("@Eid", update.Eid);
            mySqlCommand.Parameters.AddWithValue("@Ename", update.Ename);
            mySqlCommand.Parameters.AddWithValue("@brand", update.Brand);
            mySqlCommand.Parameters.AddWithValue("@model", update.Model);
            mySqlCommand.Parameters.AddWithValue("@description", update.Description);
            mySqlCommand.Parameters.AddWithValue("@price", update.Price);
            mySqlCommand.Parameters.AddWithValue("@amount", update.Amount);
            mySqlCommand.Parameters.AddWithValue("@status", update.Status);
            mySqlCommand.Parameters.AddWithValue("@product", update.Product);
            mySqlCommand.Parameters.AddWithValue("@image", update.Image);
            mySqlCommand.ExecuteNonQuery();


            mycon.Close();
            return new JsonResult("Updated Succesfully");
        }
        [HttpGet("search")]
        public JsonResult SearchAll(string search)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = $"SELECT * FROM `acceptproduct` WHERE status = 'รออนุมัติ' AND (id LIKE '%{search}%' OR Ename LIKE '%{search}%' OR date LIKE '%{search}%' OR " +
                           $"price LIKE '%{search}%' OR brand LIKE '%{search}%'" +
                           $" OR model LIKE '%{search}%' OR description LIKE '%{search}%');";

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
