using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using learnnet.Models;
using learnnet.Helper;
 
namespace learnnet.Helper
{
    public class UserQuery : CustomQuery
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString);

        public static bool IsUserExist(string email)
        {
            bool IsUserExist = false;
            string query = "SELECT * FROM dbo.users WHERE email=@email";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@email", email); 
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    IsUserExist = true;
                }
            }
            return IsUserExist;

        } 

        public static bool IsValidUser(string email, string password)
        {
            var encryptpassowrd = Base64Encode(password);
            bool IsValid = false;
            string query = "SELECT * FROM dbo.users WHERE email=@email AND password=@password";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", encryptpassowrd);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        // Edit user
        public static bool EditUser(User user)
        {
            User currentUser = SelectUser(user.id);
            var data = user;
            var test = data;
            string password = user.password != null ? Base64Encode(user.password) : currentUser.password;
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                string query = @"UPDATE dbo.users SET username = '" + user.username + "'," +
                            "email = '" + user.email + "' ," +
                            "role = '" + user.role + "' ," +
                            "password = '" + password + "'WHERE id=" + user.id;

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception E)
                    {
                        var error = E;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        // Create New User
        // Insert data to DB
        public static bool CreateUser(User user)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string password = Base64Encode(user.password);
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = @"INSERT INTO dbo.users (username ,email ,role , password) 
                            VALUES ('" + user.username + "','" + user.email + "','" + user.role + "','" + password + "')";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        /**
         * Previlege 
         */

        public static bool SetPrevilegeUser(int id, bool canCreate, bool canEdit, bool canDelete)
        {
            var data = canEdit;
            var tes = data;
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            int hasCreate = canCreate ? 1 : 0;
            int hasEdit = canEdit ? 1 : 0;
            int hasDelete = canDelete ? 1 : 0;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = @"INSERT INTO dbo.previlege (canCreate ,canEdit , canDelete , user_id) 
                            VALUES ('" + hasCreate + "','" + hasEdit + "','" + hasDelete + "','" + id + "')";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
}

