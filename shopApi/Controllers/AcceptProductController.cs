using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
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
        public void Post(int Eid, string Ename, string brand, string model, string productName, string description, DateOnly date, int price, int amount, string image)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = $"INSERT INTO `acceptproduct`(`Eid`, `Ename`, `brand`, `model`, `description`, `date`, `price`, `image`) " +
                            $"VALUES ('{Eid}','{Ename}','{brand}','{model}','{productName}','{description}','{date}','{price}','{image}')";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.ExecuteNonQuery();


            mycon.Close();
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
