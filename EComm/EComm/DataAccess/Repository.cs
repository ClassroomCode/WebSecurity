using EComm.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.DataAccess
{
    public interface IRepository
    {
        List<Product> GetProductsByName(string name);
        Product GetProduct(int id);
    }

    public class Repository : IRepository
    {
        private readonly string _connStr;

        public Repository(string connStr)
        {
            _connStr = connStr;
        }

        public List<Product> GetProductsByName(string q)
        {
            var retVal = new List<Product>();
            var conn = new SqlConnection(_connStr);
            var sql = "SELECT * FROM Products WHERE ProductName LIKE '" + q + "%'";
            var cmd = new SqlCommand(sql, conn);
            conn.Open();
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                retVal.Add(new Product() {
                    Id = (int)rdr["Id"],
                    ProductName = (string)rdr["ProductName"],
                    Package = (string)rdr["Package"]
                });
            }
            conn.Close();
            return retVal;
        }

        public Product GetProduct(int id)
        {
            Product product = null;
            var conn = new SqlConnection(_connStr);
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", id);
            conn.Open();
            var rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                product = new Product() {
                    Id = (int)rdr["Id"],
                    ProductName = (string)rdr["ProductName"]
                };
            }
            conn.Close();
            return product;
        }
    }
}
