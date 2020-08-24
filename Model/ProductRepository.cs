using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace APIwithDapper.Model {
    public class ProductRepository
    {
        private string connectionString = "Data Source=DESKTOP-U5J6LI2\\SQLEXPRESS;Initial Catalog=DAPPERDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void Add(Product prod) {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO Products (Name,Quantity,Price) VALUES(@Name,@Quantity,@Price)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, prod);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT * FROM Products";
                dbConnection.Open();
                return dbConnection.Query<Product>(sQuery);
            }
        }

        public Product GetById(int id) {
            using (IDbConnection dbConnection = Connection) {
                string sQuery = @"SELECT * FROM Products WHERE ProductId=@Id";
                dbConnection.Open();
                return dbConnection.Query<Product>(sQuery, new {Id=id}).FirstOrDefault();
            }
        }

        public void Update(Product product) {
            using (IDbConnection dbConnection = Connection) {
                string sQuery = @"UPDATE Products SET Name=@Name,Quantity=@Quantity,Price=@Price WHERE ProductId=@ProductId";
                dbConnection.Open();
                dbConnection.Execute(sQuery, product);
            }
        }

        public void Delete(int id) {
            using (IDbConnection dbConnection = Connection) {
                string sQuery = @"DELETE FROM Products WHERE ProductId=@Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }
    }
}
