using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using learnnet.Models;

namespace learnnet.Helper
{
    public class CustomQuery : Controller
    {


        // Retrieve all data in db
        public static List<Product> GetData()
        {
            List<Product> ProductList = new List<Product>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.products", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        var prd = new Product
                        {
                            id = Convert.ToInt32(rdr["id"]),
                            name = rdr["name"].ToString(),
                            price = Convert.ToInt32(rdr["price"]),
                            slug = rdr["slug"].ToString(),
                            thumbnail = rdr["thumbnail"].ToString(),
                            description = rdr["description"].ToString(),
                            status = rdr["status"].ToString(),
                            trending = rdr["trending"].ToString(),
                            country = rdr["country"].ToString()
                        };

                        ProductList.Add(prd);
                    }
                }
            }
            return ProductList;
        }

        // Insert data to DB -- returning latest product id, otherwise is returning 0
        public static int InsertData(Product prd)
        {
            var test = prd.thumbnail;
            int trending = prd.trending == "on" ? 1 : Convert.ToInt32(prd.trending);
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string desc = prd.description.Replace("'", "");
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
               
                var query = "INSERT INTO dbo.products (name ,price ,slug ,thumbnail, description, status, trending, country) VALUES ('" + prd.name + "','" + prd.price + "','" + Slugify(prd.name) + "','" + prd.thumbnail + "','" + desc + "','" + prd.status + "','" + trending + "','" + prd.country + "') SELECT CAST(scope_identity() AS int)";
                using (
                    SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = (Int32)command.ExecuteScalar();
                        return result;
                    }
                    catch (Exception err)
                    {
                        var testErr = err;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return 0;
                }
            }
        }

        // Find data by ID
        public static Product ChooseData(int id)
        {
            return GetData().Where(data => data.id == id).FirstOrDefault();
        }

        //  Edit data in DB 
        public static bool EditData(Product prd)
        { 
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string desc = prd.description.Replace("'", "");
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                int trending = prd.trending == "on" ? 1 : Convert.ToInt32(prd.trending);
                string slug = Slugify(prd.name);

                string query = @"UPDATE dbo.products SET name = '" + prd.name + "'," +
                            "price = '" + prd.price + "' ," +
                            "slug = '" + slug + "' ," +
                            "thumbnail = '" + prd.thumbnail + "' ," +
                            "description = '" + desc + "' ," +
                            "status = '" + prd.status + "' ," +
                            "country = '" + prd.country + "' ," +
                            "trending = '" + trending + "'WHERE id=" + prd.id;

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
                    catch (Exception err)
                    {
                        var test = err;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        // Delete data by ID
        public static bool DeletData(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = "DELETE FROM dbo.products WHERE id=" + id;
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

        // Get data with pagination
        public static List<Product> GetDataPagination(int currentPage)
        {
            List<Product> ProductList = new List<Product>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            int pageNumber = currentPage;
            int rowsOfPage = 3;
            string query = @"SELECT * FROM dbo.products
                             WHERE status = 'publish'
                             ORDER BY id 
                             OFFSET (" + pageNumber + "-1)*" + rowsOfPage + @" ROWS
                             FETCH NEXT " + rowsOfPage + " ROWS ONLY";

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
                    var prd = new Product
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        name = rdr["name"].ToString(),
                        price = Convert.ToInt32(rdr["price"]),
                        slug = rdr["slug"].ToString(),
                        thumbnail = rdr["thumbnail"].ToString(),
                        description = rdr["description"].ToString(),
                        status = rdr["status"].ToString(),
                        trending = rdr["trending"].ToString(),
                    };
                    ProductList.Add(prd);
                }
            }
            return ProductList;
        }

        /*
         *  Insert category
         */
        public static bool InsertCategory(string category)
        {

            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))

            {
                con.Open();
                var query = @"INSERT INTO dbo.categories (category) VALUES ('" + category + "')";
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

        /***
         * 
         * Insert Category
         * 
         */
        public static bool InsertCategory(Category cat)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))

            {
                con.Open();
                string query = String.Concat("INSERT INTO dbo.categories (category) VALUES ('", cat.category, "')");
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

        // Get All Category
        public static List<Category> GetCategories()
        {
            List<Category> Categories = new List<Category>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.categories", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        var cat = new Category
                        {
                            id = Convert.ToInt32(rdr["id"]),
                            category = rdr["category"].ToString(),
                        };

                        Categories.Add(cat);
                    }
                }
            }
            return Categories;
        }

        // Save all selected category form id
        public static bool ProductCategory(string[] cat_ids, int product_id)
        {
            if (product_id > 0)
            {
                if (cat_ids != null)
                {
                    for (var i = 0; i < cat_ids.Count(); i++)
                    {
                        ProductCategoryTable(product_id, Convert.ToInt32(cat_ids[i]));
                    }
                }
                return true;
            }
            return false;
        }

        // Insert Into ProductCategory Table
        public static void ProductCategoryTable(int product_id, int cat_id)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                string query = String.Concat("INSERT INTO dbo.product_category (product_id, category_id) VALUES ('", product_id, "','", cat_id, "')");
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        var result = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        // Get category by product id
        public static List<Category> GetCategoriesById(int product_id)
        {
            List<Category> Categories = new List<Category>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            var ids = GetCategoryId(product_id) + "0";
            if (ids.Length > 0)
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.categories WHERE id IN (" + ids + ")", con)
                    {
                        CommandType = CommandType.Text
                    };

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var cat = new Category
                            {
                                id = Convert.ToInt32(rdr["id"]),
                                category = rdr["category"].ToString(),
                            };
                            Categories.Add(cat);
                        }
                    }
                }
                return Categories;
            }
            return null;
        }

        public static string GetCategoryId(int product_id)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT category_id FROM dbo.product_category WHERE product_id =" + product_id, con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        var cat_id = new ProductCategory
                        {
                            category_id = Convert.ToInt32(rdr["category_id"]),
                        };
                        productCategories.Add(cat_id);
                    }
                }
            }
            string stringCatID = string.Empty;
            foreach (var cat_id in productCategories)
            {
                stringCatID += cat_id.category_id + ",";
            }

            return stringCatID;
        }

        // Delete All Categories From Products
        public static bool DeletePrevCategories(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = "DELETE FROM dbo.product_category WHERE product_id=" + id;
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

        // Get all country
        public static List<Country> getCountries()
        {
            List<Country> Countries = new List<Country>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.countries", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        var ctry = new Country
                        {
                            CountryID = Convert.ToInt32(rdr["countryID"]),
                            countryDesc = rdr["countryDesc"].ToString(),
                            countryCode = rdr["countryCode"].ToString(),
                        };

                        Countries.Add(ctry);
                    }
                }
            }
            return Countries;
        }

        // Get all selected country
        // Retrieve all data in db
        public static List<string> GetSelectedCountries()
        {
            List<string> Countries = new List<string>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT country FROM dbo.products", con)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Countries.Add(rdr.GetString(0));
                    }
                }
            }
            return Countries;
        }

        // Query filtering product data by country
        public static List<Product> FilterProductData(string country)
        {
            List<Product> ProductList = new List<Product>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            string query = "SELECT * FROM dbo.products WHERE country ='" + country + "'";

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
                    var prd = new Product
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        name = rdr["name"].ToString(),
                        price = Convert.ToInt32(rdr["price"]),
                        slug = rdr["slug"].ToString(),
                        thumbnail = rdr["thumbnail"].ToString(),
                        description = rdr["description"].ToString(),
                        status = rdr["status"].ToString(),
                        trending = rdr["trending"].ToString()
                    };
                    ProductList.Add(prd);
                }
            }
            return country == "all" ? GetDataPagination(1) : ProductList;
        }

        // Media handler
        public static bool AddMediaImage(string uri, string name)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))

            {
                con.Open();
                string query = "INSERT INTO dbo.images (uri, name) VALUES ('" + uri + "','"+ name+ "')";
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
                    catch (Exception err)
                    {
                        var errs = err;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        // Get all image in media
        public static List<Image> GetImageMedia()
        {
            List<Image> Images = new List<Image>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.images";
                SqlCommand command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var img = new Image
                    {
                        id = Convert.ToInt32(reader["id"]),
                        name = reader["name"].ToString(),
                        uri = reader["uri"].ToString()
                    };
                    Images.Add(img);
                }
            }
            return Images;
        }

        /* 
         * Utility -- should create on another class?
         * 
         * Slugify string
         */
        public static string Slugify(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            var stringBuilder = new StringBuilder();
            foreach (char c in input.ToArray())
            {
                if (char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(c);
                }
                else if (c == ' ')
                {
                    stringBuilder.Append("-");
                }
            }
            return stringBuilder.ToString().ToLower();
        }


        // User Static Query
        public static List<User> GetAllUser()
        {
            List<User> Users = new List<User>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.users";
                SqlCommand command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var usr = new User
                        {
                            id = Convert.ToInt32(reader["id"]),
                            username = reader["username"].ToString(),
                            email = reader["email"].ToString(),
                            role = reader["role"].ToString(),
                            password = reader["password"].ToString(),
                        };
                        Users.Add(usr);
                    }
                }
            
            }
            return Users; 
        }

        // Find User By Email
        public static User ChooseUser(string email)
        {
            return GetAllUser()?.Where(data => data.email == email).FirstOrDefault();
        }

        // Check if current user identity has role admin
        public static bool IsAdmin(string email)
        {
            var user = ChooseUser(email);
            if (user != null && user.role == "admin")
            {
                return true;
            }
            return false;
        }

        public static bool IsEditor(string email)
        {
            var user = ChooseUser(email);
            if(user != null && user.role == "editor")
            {
                return true;
            }
            return false;
        }

        // Delete user by Email
        public static bool DeleteUserById(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = "DELETE FROM dbo.users WHERE id=" + id;
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
                    catch (Exception err)
                    {
                        var data = err;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        // Get previlege by current user
        public static List<Previlege> AllUserPrevilege()
        {
            List<Previlege> previleges = new List<Previlege>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.previlege";
                SqlCommand command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var prv = new Previlege
                        {
                            id = Convert.ToInt32(reader["id"]),
                            canCreate = Convert.ToInt32(reader["canCreate"]),
                            canEdit = Convert.ToInt32(reader["canEdit"]),
                            canDelete = Convert.ToInt32(reader["canDelete"]),
                            user_id = Convert.ToInt32(reader["user_id"]),
                        };
                        previleges.Add(prv);
                    }
                }

            }
            return previleges;
        }

        // Get user previlege by current user id
        public static Previlege UserPrevilege(int id)
        {
            var previlege = AllUserPrevilege()?.Where(data => data.user_id == id).FirstOrDefault();
            return previlege != null ? previlege : null;
        }

        // get current user id
        public static int GetCurrentUserId(string email)
        {
            var currentUser = GetAllUser()?.Where(data => data.email == email).FirstOrDefault();
            return currentUser != null ? currentUser.id : 0;
        }

        // Find data by ID
        public static User SelectUser(int id) 
        {
            return GetAllUser()?.Where(data => data.id == id).FirstOrDefault();
        }

        // Delete previlege by user id
        public static bool DeletePrevilege(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = "DELETE FROM dbo.previlege WHERE user_id=" + id;
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

        // Get All Sections
        public static List<Section> AllSections()
        {
            List<Section> sections = new List<Section>();
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.sections";
                SqlCommand command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var sct = new Section
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                        };
                        sections.Add(sct);
                    }
                }

            }
            return sections;
        }

        // Delete previous accessbility
        public static bool DeleteAccessbility(int userId)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                var query = "DELETE FROM dbo.accessbility WHERE userId=" + userId;
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

        // Insert new accessbility
        public static bool AddAccessbility(int sectionId, int addition, int edition, int deletion, int userId, string role)
        {
            string CS = ConfigurationManager.ConnectionStrings["learnnet"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))

            {
                con.Open();
                string query = "INSERT INTO dbo.accessbility (sectionId, addition, edition, deletion, userId, role) VALUES ('" + sectionId + "','" + addition + "','" + edition + "','" + deletion + "','" + userId + "','" + role + "')";
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
                    catch (Exception err)
                    {
                        var errs = err;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        // Logged in user
        public static bool LoggedInUser(string email)
        {
            var user = ChooseUser(email);
            if(user != null)
            {
                return true;
            }
            return false;
        }

        // Previlege validation
        public static bool HasDeletePermission(string email)
        {
            var data = UserPrevilege(GetCurrentUserId(email));
            if (data != null && data.canDelete == 1)
            {
                return true;
            }
            return false;
        }

        public static bool HasCreatePermission(string email)
        {
            var data = UserPrevilege(GetCurrentUserId(email));
            if (data != null && data.canCreate == 1 )
            {
                return true;
            }
            return false;
        }

        public static bool HasEditPermission(string email)
        {
            var data = UserPrevilege(GetCurrentUserId(email));
            if (data != null && data.canEdit == 1)
            {
                return true;
            }
            return false;
        }
        
        public static bool UserIsEditor(string email)
        {
            return false;
        }

        public static bool IsSuperAdmin(string email)
        {
            if(HasCreatePermission(email) && HasDeletePermission(email) && HasEditPermission(email) )
            {
                return true;
            }
            return false;
        }
    }
}
