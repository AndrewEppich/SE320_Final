using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace receiptProject.receiptProjectBackend.Services
{
    /// <summary>
    /// Filters receipts based on their amount.
    /// </summary>
    public class AmountBasedReceiptFilter
    {
        private const string ConnectionString = "Server=localhost;Database=ReceiptProject;User=root;Password=540770;Port=3306;";

        /// <summary>
        /// Returns a list of receipts whose amount is between the specified minimum and maximum.
        /// </summary>
        /// <param name="minAmount">Minimum amount filter</param>
        /// <param name="maxAmount">Maximum amount filter</param>
        /// <returns>List of filtered receipts</returns>
        public List<Receipt> FilterByAmount(int minAmount, int maxAmount)
        {
            var filteredReceipts = new List<Receipt>();

            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            const string query = @"
                SELECT * 
                FROM receipts 
                INNER JOIN user u ON receipts.userID = u.userID 
                WHERE amount BETWEEN @minAmount AND @maxAmount 
                ORDER BY purchaseDate";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@minAmount", minAmount);
            command.Parameters.AddWithValue("@maxAmount", maxAmount);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                filteredReceipts.Add(new Receipt
                {
                    ReceiptID = reader.GetInt32("receiptID"),
                    UserID = reader.GetInt32("userID"),
                    Vendor = reader.GetString("vendor"),
                    Amount = reader.GetDecimal("amount"),
                    PurchaseDate = reader.GetDateTime("purchaseDate")
                });
            }

            return filteredReceipts;
        }
    }
}
