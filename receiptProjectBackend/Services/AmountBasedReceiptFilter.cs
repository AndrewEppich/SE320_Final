using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using receiptProject.receiptProjectBackend.Services;

namespace receiptProject.receiptProjectBackend.Services{
    public class AmountBasedReceiptFilter
    {
        public List<Receipt> FilterByAmount(int minAmount, int maxAmount)
        {
            var receiptsByAmount = new List<Receipt>();

            string connectionString = "Server=localhost;Database=ReceiptProject;User=root;Password=540770;Port=3306;";
            
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var amountQuery =
                    new MySqlCommand(
                        "SELECT * FROM receipts INNER JOIN user u ON receipts.userID = u.userID WHERE amount BETWEEN @minAmount AND @maxAmount ORDER BY purchaseDate",
                        conn);
                amountQuery.Parameters.AddWithValue("@minAmount", minAmount);
                amountQuery.Parameters.AddWithValue("@maxAmount", maxAmount);

                var amountReader = amountQuery.ExecuteReader();
                while (amountReader.Read())
                {
                    receiptsByAmount.Add(new Receipt
                    {
                        ReceiptID = amountReader.GetInt32("receiptID"),
                        UserID = amountReader.GetInt32("userID"),
                        Vendor = amountReader.GetString("vendor"),
                        Amount = amountReader.GetDecimal("amount"),
                        PurchaseDate = amountReader.GetDateTime("purchaseDate"),
                        ImagePath = amountReader.IsDBNull(amountReader.GetOrdinal("imagePath"))
                            ? null
                            : amountReader.GetString("imagePath"),
                        MetadataJson = amountReader.IsDBNull(amountReader.GetOrdinal("metadataJson"))
                            ? null
                            : amountReader.GetString("metadataJson")
                    });
                }
            }
            return receiptsByAmount;
        }
    }
}
