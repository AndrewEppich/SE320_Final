using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace receiptProject.receiptProjectBackend.Services
{
    /// <summary>
    /// Filters receipts based on a date range
    /// </summary>
    public class DateBasedReceiptFilter
    {
        private const string ConnectionString = "Server=localhost;Database=ReceiptProject;User=root;Password=540770;Port=3306;";

        /// <summary>
        /// Retrieves receipts between the said start and end dates
        /// </summary>
        public List<Receipt> FilterByDateRange(DateTime startDate, DateTime endDate)
        {
            var receipts = new List<Receipt>();

            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            const string query = @"
                SELECT * 
                FROM receipts 
                WHERE purchaseDate BETWEEN @startDate AND @endDate 
                ORDER BY purchaseDate";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@startDate", startDate);
            command.Parameters.AddWithValue("@endDate", endDate);

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
    }
}
