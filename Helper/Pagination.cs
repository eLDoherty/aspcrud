using learnnet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace learnnet.Helper
{
    public class Pagination : CustomQuery
    {

        // User pagination
        public static int TotalUserRecord()
        {
            return GetAllUser().Count();
        }

        // Show per page pagination
        public static List<User> PaginatePerPageUser(int totalDisplay)
        {
            List<User> users = new List<User>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string query = "SELECT TOP("+totalDisplay+") * FROM dbo.users WHERE id != 1";

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        username = rdr["username"].ToString(),
                        email = rdr["email"].ToString(),
                        role = rdr["role"].ToString()
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        // Paginate page step
        public static List<User> PaginatePerStep(int page, int rows, string sorting, string name)
        {
            List<User> users = new List<User>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            int pageNumber = page;
            int rowsOfPage = rows;
            string extendQuery = "";
            string query = @"SELECT * FROM dbo.users
                            WHERE id != 1
                            ORDER BY "+name+" "+sorting+ @" OFFSET ("+page+"-1)* "+rows+" ROWS FETCH NEXT  "+rows+"  ROWS ONLY";

            if (sorting != "none" && name != "none")
            {
                extendQuery = name + " " + sorting; 
            }

            if (extendQuery.Length == 0)
            {
                query = @"SELECT * FROM dbo.users
                          WHERE id != 1
                          ORDER BY id OFFSET (" + pageNumber + "-1)*" + rowsOfPage + @" ROWS
                          FETCH NEXT " + rowsOfPage + " ROWS ONLY";
            }

            var test1 = query;

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        username = rdr["username"].ToString(),
                        email = rdr["email"].ToString(),
                        role = rdr["role"].ToString()
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        // Order user by ID Query
        public static List<User> PaginateByUserId(string sorting, int rows)
        {
            List<User> users = new List<User>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string extendQuery = "";
            string query = @"SELECT * FROM dbo.users
                            WHERE id != 1
                            ORDER BY id " + sorting + @" OFFSET (1-1)* " + rows + " ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

            int test = extendQuery.Length;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        username = rdr["username"].ToString(),
                        email = rdr["email"].ToString(),
                        role = rdr["role"].ToString()
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        // Order user by username
        public static List<User> PaginateByUsername(string sorting, int rows)
        {
            List<User> users = new List<User>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string extendQuery = "";
            string query = @"SELECT * FROM dbo.users
                            WHERE id != 1
                            ORDER BY username " + sorting + @" OFFSET (1-1)* "+ rows +" ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

            int test = extendQuery.Length;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        username = rdr["username"].ToString(),
                        email = rdr["email"].ToString(),
                        role = rdr["role"].ToString()
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        // Order user by email
        public static List<User> PaginateByUserEmail(string sorting, int rows)
        {
            List<User> users = new List<User>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string extendQuery = "";
            string query = @"SELECT * FROM dbo.users
                            WHERE id != 1
                            ORDER BY email "+ sorting +@" OFFSET (1-1)* "+ rows +" ROWS FETCH NEXT  "+ rows +"  ROWS ONLY";

            int test = extendQuery.Length;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        username = rdr["username"].ToString(),
                        email = rdr["email"].ToString(),
                        role = rdr["role"].ToString()
                    };
                    users.Add(user);
                }
            }
            return users;
        }
    }
}