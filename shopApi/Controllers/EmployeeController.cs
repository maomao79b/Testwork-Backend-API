using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace shopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"SELECT * FROM `employee`";

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
            string query = $"DELETE FROM `employee` WHERE id = {id};";

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
        public void Post(string name, string age, string address, string phone, string username, string password, string position)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = $"INSERT INTO `employee`(`name`, `age`, `address`, `phone`, `username`, `password`,`position`) " +
                            $"VALUES ('{name}','{age}','{address}','{phone}','{username}','{password}','{position}')";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.ExecuteNonQuery();


            mycon.Close();
        }

        [HttpPut]
        public void Put(int id, string name, string age, string address, string phone, string username, string password, string position)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = $"UPDATE `employee` SET `name`='{name}',`age`='{age}'," +
                $"`address`='{address}',`phone`='{phone}',`username`='{username}',`password`='{password}',`position`='{position}' WHERE `id`='{id}'";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.ExecuteNonQuery();


            mycon.Close();
        }
        [HttpGet("login")]
        public JsonResult Login(string username, string password)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = $"SELECT * FROM `employee` WHERE BINARY `username` = '{username}' AND `password` = '{password}';";

            try
            {
                MySqlConnection mycon = new MySqlConnection(sqlDataSource);
                mycon.Open();

                MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
                myReader = mySqlCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                mycon.Close();
                return new JsonResult(table);

            }catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
