using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.IO;

namespace UnsecureApp.Controllers
{
    public class MyController
    {

        public string ReadFile(string userInput)
        {
            if (IsPathTraversal(userInput))
            {
                throw new ArgumentException("Invalid file path.");
            }

            string basePath = "/safe/directory"; // Change this to your safe directory
            string fullPath = Path.GetFullPath(Path.Combine(basePath, userInput));

            if (!fullPath.StartsWith(basePath))
            {
                throw new ArgumentException("Invalid file path.");
            }

            using (FileStream fs = File.Open(fullPath, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    return temp.GetString(b);
                }
            }

            return null;
        }

        public int GetProduct(string productName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandText = "SELECT ProductId FROM Products WHERE ProductName = @productName",
                    CommandType = CommandType.Text,
                };
                sqlCommand.Parameters.AddWithValue("@productName", productName);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                return reader.GetInt32(0); 
            }
        }

        public int GetProductt(string productName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandText = "SELECT ProductId FROM Products WHERE ProductName = @productName",
                    CommandType = CommandType.Text,
                };
                sqlCommand.Parameters.AddWithValue("@productName", productName);

                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
                return -1; // or handle the case where no product is found
            }
        }

        public void GetObject()
        {
            try
            {
                object o = null;
                o.ToString();
            }
            catch (Exception e)
            {
                this.Response.Write(e.ToString());
            }
        
        }

        private string connectionString = "";

        private bool IsPathTraversal(string path)
        {
            return path.Contains("..") || path.Contains("/") || path.Contains("\\");
        }
    }
}
