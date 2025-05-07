using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SE320_Final
{
    public class AmountBasedReceiptFilter
    {
        public List<Receipt> AmountBasedReceiptFilter(int minAmount, int maxAmount)
        {

            private var receiptsByAmount = new List<Receipts>();

            using (var conn = new SqlConnection()){
                conn.Open()
                var amountQuery =
                    new SqlCommand(
                        "SELECT * FROM ReceiptProject.receipts JOIN ReceiptProject.user u on receipts.userID = u.userID WHERE purchaseDate BETWEEN @minAmount and @maxAmount ORDER BY purchaseDate",
                        conn);
                amountQuery.Parameters.AddValue("@minAmount", minAmount);
                amountQuery.Parameters.AddValue("@maxAmount", maxAmount);

                var amountReader = amountQuery.ExecuteReader()
                while (amountReader.Read())
                {

                    receiptsByAmount.Add(new Receipt
                    {
                        ReceiptID = amountReader.GetInt32("receiptID"),
                        UserID = amountReader.GetInt32("userID"),
                        Vendor = amountReader.GetString("vendor"),
                        Amount = amountReader.GetDecimal("amount"),
                        PurchaseDate = amountReader.GetDateTime("purchaseDate"),
                        ImagePath = amountReader.IsDBNull(reader.GetOrdinal("imagePath"))
                            ? null
                            : amountReader.GetString("imagePath"),
                        MetadataJson = amountReader.IsDBNull(reader.GetOrdinal("metadataJson"))
                            ? null
                            : amountReader.GetString("metadataJson")
                    });
                }
            }
            return receiptsByAmount;
        }
    }
}
