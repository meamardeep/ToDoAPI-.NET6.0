using POC.DataAccess;
using POC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string conectionString = "Server=SPARTAN\\SQLSERVER2017;Database=POC_DB;Integrated Security=true";

        public UserDataAccess GetUser(string username, string password)
        {
            string sqlQuery = "select * from Users where UserName ='"+username+ "' and Password ='"+password+ "'";
            UserDataAccess user = new UserDataAccess();
            using (var connect = new SqlConnection(conectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, connect);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         return new UserDataAccess()
                         {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FullName = reader["FullName"].ToString(),
                            UserName = reader["UserName"].ToString(),
                        };
                    }
                    connect.Close();
                };
            }
            return user;
        }

        public List<UserDataAccess> GetUsers(int otherthanUser)
        {
            List<UserDataAccess> users = new List<UserDataAccess>();
            string sqlQuery = "select * from Users u where u.UserId <>"+ otherthanUser;
            using (var connect = new SqlConnection(conectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, connect);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserDataAccess()
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FullName = reader["FullName"].ToString(),
                            UserName = reader["UserName"].ToString(),
                        });
                    }
                };
            }

            return users;
        }
    }
}
