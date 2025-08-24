using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Premosh10.Controllers
{
    public class SecurityDemoController : Controller
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=my_db;Trusted_Connection=True;";
        // ---------------- SQL Injection ----------------
        public IActionResult SQLVulnerable(string username)
        {
            string result = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Vulnerable query: user input concatenated
                string query = $"SELECT Name FROM Users WHERE Name = '{username}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result += reader["Name"].ToString() + " ";
                }
            }
            ViewBag.Result = result;
            return View();
        }

        public IActionResult SQLSafe(string username)
        {
            string result = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Parameterized query prevents SQL Injection
                string query = "SELECT Name FROM Users WHERE Name = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result += reader["Name"].ToString() + " ";
                }
            }
            ViewBag.Result = result;
            return View("SQLSafe");
        }
        // ---------------- CSRF ----------------
        [HttpGet]
        public IActionResult CSRFTest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CSRFTest(string comment)
        {
            ViewBag.Comment = comment;
            return View();
        }

    }
}
