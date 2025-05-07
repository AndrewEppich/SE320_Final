using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SE320_Final
{
    public class ReceiptDateFilter
    {
        private string _connectionString = "YOUR_CONNECTION_STRING_HERE";

        public List<Receipt> GetReceiptsByDateRange(DateTime startDate, DateTime endDate)
        {
            var receiptsByDate = new List<Receipt>();
            
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var dateQuery = new SqlCommand("SELECT * FROM ReceiptProject.receipts JOIN ReceiptProject.user u on receipts.userID = u.userID WHERE purchaseDate BETWEEN @startdate and @enddate ORDER BY purchaseDate", conn);
                dateQuery.Parameters.AddWithValue("@startDate", startDate);
                dateQuery.Parameters.AddWithValue("@endDate", endDate);

                var Datereader = dateQuery.ExecuteReader();
                while (Datereader.Read())
                {
                    receiptsByDate.Add(new Receipt
                    {
                        ReceiptID = Datereader.GetInt32("receiptID"),
                        UserID = Datereader.GetInt32("userID"),
                        Vendor = Datereader.GetString("vendor"),
                        Amount = Datereader.GetDecimal("amount"),
                        PurchaseDate = Datereader.GetDateTime("purchaseDate"),
                        ImagePath = Datereader.IsDBNull(reader.GetOrdinal("imagePath")) ? null : Datereader.GetString("imagePath"),
                        MetadataJson = Datereader.IsDBNull(reader.GetOrdinal("metadataJson")) ? null : Datereader.GetString("metadataJson")
                    });
                }
            }

            return receiptsByDate;
        }
    }
}
