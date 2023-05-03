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
    public class Paging : CustomQuery
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
            string query = @"SELECT * FROM dbo.users
                            WHERE id != 1
                            ORDER BY id " + sorting + @" OFFSET (1-1)* " + rows + " ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

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
            string query = @"SELECT * FROM dbo.users
                            WHERE id != 1
                            ORDER BY username " + sorting + " OFFSET (1-1)* "+ rows +" ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

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
            string query = @"SELECT * FROM dbo.users
                             WHERE id != 1
                             ORDER BY email "+ sorting +" OFFSET (1-1)* "+ rows +" ROWS FETCH NEXT  "+ rows +"  ROWS ONLY";

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

        // Order user by role
        public static List<User> PaginateByUserRole(string sorting, int rows)
        {
            List<User> users = new List<User>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string query = @"SELECT * FROM dbo.users
                            WHERE id != 1
                            ORDER BY role " + sorting + " OFFSET (1-1)* " + rows + " ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

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

        // Pagination for category
        // Show per page pagination
        public static List<Category> PaginatePerPageCategory(int totalDisplay)
        {
            List<Category> categories = new List<Category>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string query = "";
            if(totalDisplay > 0)
            {
                query = "SELECT TOP(" + totalDisplay + ") * FROM dbo.categories";
            }
            else
            {
                query = "SELECT TOP(2) * FROM dbo.categories";
            }
            
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
                    var cats = new Category
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        category = rdr["category"].ToString(),
                    };
                    categories.Add(cats);
                }
            }
            return categories;
        }

        // Show per step category
        public static List<Category> PaginatePerStepCategory(int page, int rows, string sorting, string name)
        {
            List<Category> categories = new List<Category>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            int pageNumber = page;
            int rowsOfPage = rows;
            string extendQuery = "";
            string query = @"SELECT * FROM dbo.categories
                            ORDER BY " + name + " " + sorting + @" OFFSET (" + page + "-1)* " + rows + " ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

            if (sorting != "none" && name != "none")
            {
                extendQuery = name + " " + sorting;
            }

            if (extendQuery.Length == 0)
            {
                query = @"SELECT * FROM dbo.categories
                          ORDER BY id OFFSET (" + pageNumber + "-1)*" + rowsOfPage + @" ROWS
                          FETCH NEXT " + rowsOfPage + " ROWS ONLY";
            }

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
                    var category = new Category
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        category = rdr["category"].ToString(),
                    };
                    categories.Add(category);
                }
            }
            return categories;
        }

        // Show category by ID
        public static List<Category> ShowCategoryByID(string sorting, int rows)
        {
            List<Category> categories = new List<Category>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string query = @"SELECT * FROM dbo.categories
                            ORDER BY id " + sorting + @" OFFSET (1-1)* " + rows + " ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

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
                    var Category = new Category
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        category = rdr["category"].ToString(),
                    };
                    categories.Add(Category);
                }
            }
            return categories;
        }

        // PaginateByCategory
        // Sort by category 
        public static List<Category> PaginateByCategory(string sorting, int rows)
        {
            List<Category> categories = new List<Category>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string query = @"SELECT * FROM dbo.categories
                            ORDER BY category " + sorting + @" OFFSET (1-1)* " + rows + " ROWS FETCH NEXT  " + rows + "  ROWS ONLY";

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
                    var Category = new Category
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        category = rdr["Category"].ToString(),
                    };
                    categories.Add(Category);
                }
            }
            return categories;
        }

    }
}