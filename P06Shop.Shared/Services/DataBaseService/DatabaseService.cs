using MySql.Data.MySqlClient;
using P06Shop.Shared.Shop;

namespace P06Shop.Shared.Services.DataBaseService
{
    public class DatabaseService
    {
        public void AddProduct(ref Product p)
        {
            string selectQuery = "SELECT * FROM Products WHERE Title = @Title";
            string insertQuery = "INSERT INTO Products (Title, Description) VALUES (@Title, @Description)";

            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@Title", p.Title);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                p.Id = reader.GetInt32("Id");
                return;
            }
            reader.Close();

            cmd = new MySqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@Title", p.Title);
            cmd.Parameters.AddWithValue("@Description", p.Description);
            var inserted = cmd.ExecuteNonQuery();
            if (inserted != 1) { throw new Exception("Mysql error: Could not insert new row."); }

            cmd = new MySqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@Title", p.Title);
            reader = cmd.ExecuteReader();
            reader.Read();
            p.Id = reader.GetInt32("Id");
            reader.Close();
            conn.Close();
        }
        public void UpdateProduct(Product p)
        {
            string updateQuery = "UPDATE Products SET Title = @NewTitle, Description = @NewDescription WHERE Id = @Id";
            var conn = _CreateConnection();
            MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
            cmd = new MySqlCommand(updateQuery, conn);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@NewTitle", p.Title);
            cmd.Parameters.AddWithValue("@NewDescription", p.Description);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Product? GetProduct(int id)
        {
            string selectQuery = "SELECT * FROM Products WHERE Id = @Id";
            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Product p = new Product();
                p.Id = id;
                p.Title = reader.GetString("Title");
                p.Description = reader.GetString("Description");
                return p;
            }
            reader.Close();
            conn.Close();

            return null;
        }
        public Product[] GetAllProducts()
        {
            string selectQuery = "SELECT * FROM Products";
            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
            var reader = cmd.ExecuteReader();
            List<Product> products = new List<Product>();
            Product p = new Product();
            while (reader.Read())
            {
                p.Id = reader.GetInt32("Id");
                p.Title = reader.GetString("Title");
                p.Description = reader.GetString("Description");
                products.Add(p);
            }
            reader.Close();
            conn.Close();
            return products.ToArray();
        }
        public void DeleteProduct(int id)
        {
            string deleteQuery = "DELETE FROM Products WHERE Id = @Id";
            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
            cmd.Parameters.AddWithValue("Id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void AddBook(ref Book p)
        {
            string selectQuery = "SELECT * FROM Books WHERE Title = @Title";
            string insertQuery = "INSERT INTO Books (Title, Description, Author) VALUES (@Title, @Description, @Author)";

            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@Title", p.Title);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                p.Id = reader.GetInt32("Id");
                return;
            }
            reader.Close();

            cmd = new MySqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@Title", p.Title);
            cmd.Parameters.AddWithValue("@Description", p.Description);
            cmd.Parameters.AddWithValue("@Author", p.Author);
            var inserted = cmd.ExecuteNonQuery();
            if (inserted != 1) { throw new Exception("Mysql error: Could not insert new row."); }

            cmd = new MySqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@Title", p.Title);
            reader = cmd.ExecuteReader();
            reader.Read();
            p.Id = reader.GetInt32("Id");
            reader.Close();
            conn.Close();
        }
        public void UpdateBook(Book p)
        {
            string updateQuery = "UPDATE Books SET Title = @NewTitle, Description = @NewDescription, Author = @NewAuthor WHERE Id = @Id";
            var conn = _CreateConnection();
            MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
            cmd = new MySqlCommand(updateQuery, conn);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@NewTitle", p.Title);
            cmd.Parameters.AddWithValue("@NewDescription", p.Description);
            cmd.Parameters.AddWithValue("@NewAuthor", p.Author);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Book? GetBook(int id)
        {
            string selectQuery = "SELECT * FROM Books WHERE Id = @Id";
            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Book p = new Book();
                p.Id = id;
                p.Title = reader.GetString("Title");
                p.Description = reader.GetString("Description");
                p.Author = reader.GetString("Author");
                return p;
            }
            reader.Close();
            conn.Close();

            return null;
        }
        public Book[] GetAllBooks()
        {
            string selectQuery = "SELECT * FROM Books";
            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
            var reader = cmd.ExecuteReader();
            List<Book> books = new List<Book>();
            Book p = new Book();
            while (reader.Read())
            {
                p.Id = reader.GetInt32("Id");
                p.Title = reader.GetString("Title");
                p.Description = reader.GetString("Description");
                p.Author = reader.GetString("Author");
                books.Add(p);
            }
            reader.Close();
            conn.Close();
            return books.ToArray();
        }
        public void DeleteBook(int id)
        {
            string deleteQuery = "DELETE FROM Books WHERE Id = @Id";
            var conn = _CreateConnection();

            MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
            cmd.Parameters.AddWithValue("Id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        private MySqlConnection _CreateConnection()
        {
            string connectionString = "server=localhost;user=root;password=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            // Check if the database "l4" exists.
            string sql = "CREATE DATABASE IF NOT EXISTS l4;";
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }

            // Switch the connection string to use the "l4" database.
            connection.ChangeDatabase("l4");

            // Create the Product table if it does not exist.
            sql = @"CREATE TABLE IF NOT EXISTS Books (
                                Id INT AUTO_INCREMENT PRIMARY KEY, 
                                Title VARCHAR(255), 
                                Description TEXT, 
                                Author VARCHAR(255)
                               );";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }

            sql = @"CREATE TABLE IF NOT EXISTS Products (
                                Id INT AUTO_INCREMENT PRIMARY KEY, 
                                Title VARCHAR(255), 
                                Description TEXT
                               );";
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
            return connection;
        }
    }
}
