using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MVCCSharpProject.ViewModels;

namespace MVCCSharpProject.Models
{
    public class Repository
    {

        public string _connectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["mvcCsharp"].ConnectionString;


        public void Create(Customers customerForm)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO CustomersTable
                                                 (CustomerName,CustomerAddress,CustomerPhone)
                                         VALUES  (@CustomerName,@CustomerAddress,@CustomerPhone)";
                command.Parameters.AddWithValue("@CustomerName", customerForm.CustomerName);
                command.Parameters.AddWithValue("@CustomerAddress", customerForm.CustomerAddress);
                command.Parameters.AddWithValue("@CustomerPhone", customerForm.CustomerPhone);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public LoginForm Authenticate(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                 command.CommandText = @"SELECT *
                                        FROM users
                                        WHERE UserName =@UserName
                                        and Password = @Password;
                                       ";
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                var reader = command.ExecuteReader();
                LoginForm user = null;
                if (reader.Read())
                {
                    user = new LoginForm();
                    user.UserName = reader["UserName"] as string;
                    user.Password = reader["Password"] as string;
                }
                return user;
            }
        }


        // ADDED THIS CODE TO GET THE CUSTOMER SELECTED IN THE LIST AND WE WILL USE IT FOR THE DELETE OPERATION TOO.

        public Customers GetCustomerForEdit(string ID)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT *
                                      FROM CustomersTable WHERE ID = @ID";

                command.Parameters.AddWithValue("@ID", ID);
                connection.Open();
                var reader = command.ExecuteReader();
                Customers customers = null;
                while (reader.Read())
                {
                    customers = new Customers();
                    customers.Id = (int)reader["Id"];
                    customers.CustomerName = reader["CustomerName"] as string;
                    customers.CustomerAddress = reader["CustomerAddress"] as string;
                    customers.CustomerPhone = reader["CustomerPhone"] as string;
                }
                return customers;
            }
        }




        // added this method to update customer

        internal void UpdateCustomer(Customers customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"Update customersTable set CustomerName =@CustomerName,
                                             CustomerAddress=@CustomerAddress,
                                             CustomerPhone =@CustomerPhone
                                      WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", customer.Id);
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@CustomerAddress", customer.CustomerAddress);
                command.Parameters.AddWithValue("@CustomerPhone", customer.CustomerPhone);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }




        // Delete method/code

        internal void DeleteCustomer(Customers customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"DELETE
                                        FROM CUSTOMERSTABLE
                                        WHERE ID = @Id";
                command.Parameters.AddWithValue("@Id", customer.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal List<Customers> GetCustomerReport(string searchTerm)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT *
                                        FROM CustomersTable
                                        WHERE CustomerName LIKE @searchTerm";

                command.Parameters.AddWithValue("@searchTerm", searchTerm + "%%");
                connection.Open();
                var reader = command.ExecuteReader();
                var customers = new List<Customers>();
                while (reader.Read())
                {
                    Customers customersList = new Customers();
                    customersList.Id = (int)reader["Id"];
                    customersList.CustomerName = reader["CustomerName"] as string;
                    customersList.CustomerAddress = reader["CustomerAddress"] as string;
                    customersList.CustomerPhone = reader["CustomerPhone"] as string;
                    customers.Add(customersList);
                }
                return customers;
            }
        }

        internal List<Customers> GetCustomers()
        {

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM CustomersTable";

                connection.Open();

                var reader = command.ExecuteReader();
                var customers = new List<Customers>();

                while (reader.Read())
                {
                    var customerList = new Customers();
                    customerList.Id = (int)reader["Id"];
                    customerList.CustomerName = reader["CustomerName"] as string;
                    customerList.CustomerAddress = reader["CustomerAddress"] as string;
                    customerList.CustomerPhone = reader["CustomerPhone"] as string;
                    customers.Add(customerList);

                }
                return customers;
            }

        }
    }
}