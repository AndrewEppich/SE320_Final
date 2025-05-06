using System;
using System.Collections.Generic;
// using System.Data.SqlClient; // Uncomment for SQL Server
// using MySql.Data.MySqlClient; // Uncomment for MySQL

namespace SE320_Final.Features
{
    public class Receipt
    {
        public int ReceiptID { get; set; }
        public int UserID { get; set; }
        public string Vendor { get; set; }
        public decimal Amount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ImagePath { get; set; }
        public string MetadataJson { get; set; }
    }

    public class ReceiptDateFilter
    {
        private string _connectionString = "YOUR_CONNECTION_STRING_HERE";

        public List<Receipt> GetReceiptsByDateRange(DateTime startDate, DateTime endDate)
        {
            var receipts = new List<Receipt>();

            // Placeholder for actual database connection and query logic
            /*
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM receipts WHERE purchaseDate BETWEEN @startDate AND @endDate", conn);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    receipts.Add(new Receipt
                    {
                        ReceiptID = reader.GetInt32("receiptID"),
                        UserID = reader.GetInt32("userID"),
                        Vendor = reader.GetString("vendor"),
                        Amount = reader.GetDecimal("amount"),
                        PurchaseDate = reader.GetDateTime("purchaseDate"),
                        ImagePath = reader.IsDBNull(reader.GetOrdinal("imagePath")) ? null : reader.GetString("imagePath"),
                        MetadataJson = reader.IsDBNull(reader.GetOrdinal("metadataJson")) ? null : reader.GetString("metadataJson")
                    });
                }
            }
            */

            return receipts;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var filter = new ReceiptDateFilter();
            DateTime start = new DateTime(2025, 1, 1);
            DateTime end = new DateTime(2025, 1, 31);

            var receipts = filter.GetReceiptsByDateRange(start, end);

            foreach (var r in receipts)
            {
                Console.WriteLine($"{r.PurchaseDate.ToShortDateString()} - {r.Vendor} - ${r.Amount}");
            }
        }
    }
}
