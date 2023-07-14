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

        //[HttpPut]
        //public void Put(int id, int Eid, string Ename, string brand, string model, string productName, string description, DateOnly date, int price, string image)
        //{
        //    string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

        //    string query = $"UPDATE `acceptproduct` SET `Eid`='{Eid}',`Ename`='{Ename}',`productName`='{productName}',`description`='{description}'," +
        //        $"`date`='{date}',`price`='{price}',`image`='{image}' WHERE `id`='{id}'";

        //    MySqlConnection mycon = new MySqlConnection(sqlDataSource);
        //    mycon.Open();

        //    MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
        //    mySqlCommand.ExecuteNonQuery();


        //    mycon.Close();
        //}
    }
}
