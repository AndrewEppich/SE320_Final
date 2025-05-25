using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using receiptProject.receiptProjectBackend.Services;

namespace receiptProject.receiptProjectBackend.Services{
    public class DateBasedReceiptFilter
    {
        public List<Receipt> GetReceiptsByDateRange(DateTime startDate, DateTime endDate)
        {
            var receiptsByDate = new List<Receipt>();
            
            string connectionString = "Server=localhost;Database=ReceiptProject;User=root;Password=540770;Port=3306;";
            
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var dateQuery = new MySqlCommand(
                    "SELECT * FROM receipts INNER JOIN user u ON receipts.userID = u.userID WHERE purchaseDate BETWEEN @startDate AND @endDate ORDER BY purchaseDate", 
                    conn);
                dateQuery.Parameters.AddWithValue("@startDate", startDate);
                dateQuery.Parameters.AddWithValue("@endDate", endDate);

                var dateReader = dateQuery.ExecuteReader();
                while (dateReader.Read())
                {
                    receiptsByDate.Add(new Receipt
                    {
                        ReceiptID = dateReader.GetInt32("receiptID"),
                        UserID = dateReader.GetInt32("userID"),
                        Vendor = dateReader.GetString("vendor"),
                        Amount = dateReader.GetDecimal("amount"),
                        PurchaseDate = dateReader.GetDateTime("purchaseDate")
                    });
                }
            }

            return receiptsByDate;
        }
    }
}
