using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace receiptProject.receiptProjectBackend.Services
{
    /// <summary>
    /// Handles db for receipts
    /// </summary>
    public class ReceiptRepository : IReceiptRepository
    {
        private const string ConnectionString = "Server=localhost;Database=ReceiptProject;User=root;Password=540770;Port=3306;";

        public List<Receipt> GetAllReceipts()
        {
            var receipts = new List<Receipt>();

            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            using var command = new MySqlCommand("SELECT * FROM receipts", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                receipts.Add(new Receipt
                {
                    ReceiptID = reader.GetInt32("receiptID"),
                    UserID = reader.GetInt32("userID"),
                    Vendor = reader.GetString("vendor"),
                    Amount = reader.GetDecimal("amount"),
                    PurchaseDate = reader.GetDateTime("purchaseDate")
                });
            }

            return receipts;
        }

        public Receipt GetReceiptById(int id)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            const string query = "SELECT * FROM receipts WHERE receiptID = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Receipt
                {
                    ReceiptID = reader.GetInt32("receiptID"),
                    UserID = reader.GetInt32("userID"),
                    Vendor = reader.GetString("vendor"),
                    Amount = reader.GetDecimal("amount"),
                    PurchaseDate = reader.GetDateTime("purchaseDate")
                };
            }

            return null;
        }

        public void AddReceipt(Receipt receipt)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            const string query = @"
                INSERT INTO receipts (userID, vendor, amount, purchaseDate) 
                VALUES (@userID, @vendor, @amount, @purchaseDate)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", receipt.UserID);
            command.Parameters.AddWithValue("@vendor", receipt.Vendor);
            command.Parameters.AddWithValue("@amount", receipt.Amount);
            command.Parameters.AddWithValue("@purchaseDate", receipt.PurchaseDate);

            command.ExecuteNonQuery();
        }

        public void UpdateReceipt(Receipt receipt)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            const string query = @"
                UPDATE receipts 
                SET userID = @userID, vendor = @vendor, amount = @amount, purchaseDate = @purchaseDate 
                WHERE receiptID = @receiptID";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", receipt.UserID);
            command.Parameters.AddWithValue("@vendor", receipt.Vendor);
            command.Parameters.AddWithValue("@amount", receipt.Amount);
            command.Parameters.AddWithValue("@purchaseDate", receipt.PurchaseDate);
            command.Parameters.AddWithValue("@receiptID", receipt.ReceiptID);

            command.ExecuteNonQuery();
        }

        public void DeleteReceipt(int id)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            const string query = "DELETE FROM receipts WHERE receiptID = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}
