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
    public class Permission
    {
        public static CommandType CommandType { get; private set; }

        /**
         * Product section base permission
         */
        public static string GetProductRole(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = "+ userId +" AND sectionId =" +1+";", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role;
        }

        public static bool CanAddProduct(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '1' AND addition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanEditProduct(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '1' AND edition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanDeleteProduct(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '1' AND deletion = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        /**
         * User section base permission
         */
        public static string GetUserSettingRole(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId =" + 2 + ";", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role;
        }

        public static bool CanAddUser(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '2' AND addition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanEditUser(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '2' AND edition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanDeleteUser(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '2' AND deletion = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        /**
         * Category section base permission
         */
        public static string GetCategoryRole(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId =" + 3 + ";", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role;
        }

        public static bool CanAddCategory(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '3' AND addition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanEditCategory(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '3' AND edition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanDeleteCategory(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '3' AND deletion = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        /**
         * Media section base permission
         */
        public static string GetMediaRole(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId =" + 4 + ";", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role;
        }

        public static bool CanAddMedia(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '4' AND addition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanEditMedia(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '4' AND edition = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        public static bool CanDeleteMedia(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId = '4' AND deletion = '1';", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role != null || role.Length == 0 ? false : true;
        }

        /**
         * Custom permission 
         */
         public static string GetSectionRole(int sectionId, int userId)
         {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string role = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT role FROM dbo.accessbility WHERE userId = " + userId + " AND sectionId =" + sectionId + ";", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    role = rdr["role"].ToString();
                }
            }
            return role;
         }

        public static List<Accessbility> GetAllAccessbility()
        {
            List<Accessbility> accessbilities = new List<Accessbility>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string query = "SELECT * FROM dbo.accessbility";

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
                    var prd = new Accessbility
                    {
                        sectionId = Convert.ToInt32(rdr["sectionId"]),
                        addition = Convert.ToInt32(rdr["addition"]),
                        edition = Convert.ToInt32(rdr["edition"]),
                        deletion = Convert.ToInt32(rdr["deletion"]),
                        userId = Convert.ToInt32(rdr["userId"]),
                        role = rdr["role"].ToString(),
                    };
                    accessbilities.Add(prd);
                }
            }
            return accessbilities;
        }

        public static bool GetAddPermission(int userId, int sectionId)
        {
            var accessbility = GetAllAccessbility()?.Where(data => data.userId == userId && data.sectionId == sectionId).FirstOrDefault();
            if(accessbility != null && accessbility.addition > 0)
            {
                return true;
            }
            return false;
        }

        public static bool GetEditPermission(int userId, int sectionId)
        {
            var accessbility = GetAllAccessbility()?.Where(data => data.userId == userId && data.sectionId == sectionId).FirstOrDefault();
            if (accessbility != null && accessbility.edition > 0)
            {
                return true;
            }
            return false;
        }

        public static bool GetDeletePermission(int userId, int sectionId)
        {
            var accessbility = GetAllAccessbility()?.Where(data => data.userId == userId && data.sectionId == sectionId).FirstOrDefault();
            if (accessbility != null && accessbility.deletion > 0)
            {
                return true;
            }
            return false;
        }

        // Full grant access to prpdict
        public static bool FullAccessProduct(int userId)
        {
            if(CanAddProduct(userId) && CanEditProduct(userId) && CanDeleteProduct(userId))
            {
                return true;
            }
            return false;
        }

        // Full grant access to user
        public static bool FullAccessUser(int userId)
        {
            if(CanAddUser(userId) && CanEditUser(userId) && CanDeleteUser(userId))
            {
                return true;
            }
            return false;
        }

        public static bool FullAccessCategory(int userId)
        {
            if (CanAddCategory(userId) && CanEditCategory(userId) && CanDeleteCategory(userId))
            {
                return true;
            }
            return false;
        }

        public static bool FullAccessMedia(int userId)
        {
            if(CanAddMedia(userId) && CanEditMedia(userId) && CanDeleteMedia(userId))
            {
                return true;
            }
            return false;
        }
    }
}