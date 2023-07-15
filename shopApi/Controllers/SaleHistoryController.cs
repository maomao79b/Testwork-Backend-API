using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using shopApi.Models;
using System.Data;

namespace shopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleHistoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SaleHistoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"SELECT * FROM `sale_history`";

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
            string query = $"DELETE FROM `sale_history` WHERE id = {id};";

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
        public JsonResult Post(SaleHistory saleHistory)
        {
            string sqlDataSource = _configuration.GetConnectionString("ShopAppcon");

            string query = @"INSERT INTO `sale_history` (`id`, `cid`, `total`, `product`) " +
                            @"VALUES (@id,@cid,@total,@product)";

            MySqlConnection mycon = new MySqlConnection(sqlDataSource);
            mycon.Open();

            MySqlCommand mySqlCommand = new MySqlCommand(query, mycon);
            mySqlCommand.Parameters.AddWithValue("id", saleHistory.Id);
            mySqlCommand.Parameters.AddWithValue("@cid", saleHistory.Cid);
            mySqlCommand.Parameters.AddWithValue("@total", saleHistory.Total);
            mySqlCommand.Parameters.AddWithValue("@product", saleHistory.Product);
            mySqlCommand.ExecuteNonQuery();

            mycon.Close();
            return new JsonResult("Insert Successfully");
        }
    }
}
