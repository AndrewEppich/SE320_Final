USE ReceiptProject;

SELECT *
FROM ReceiptProject.receipts
        JOIN ReceiptProject.user u on receipts.userID = u.userID
WHERE amount BETWEEN ? AND ?
ORDER BY purchaseDate;


SELECT *
FROM ReceiptProject.receipts
         JOIN ReceiptProject.user u on receipts.userID = u.userID
WHERE purchaseDate BETWEEN ? and ?
ORDER BY purchaseDate;

SELECT *
FROM ReceiptProject.summaries
         JOIN ReceiptProject.user u on summaries.userID = u.userID
         JOIN ReceiptProject.receipts r on u.userID = r.userID
WHERE summaryType = 'weekly' AND startDate = ? AND endDate = ?
ORDER BY purchaseDate;

SELECT *
FROM ReceiptProject.summaries
         JOIN ReceiptProject.user u on summaries.userID = u.userID
         JOIN ReceiptProject.receipts r on u.userID = r.userID
WHERE summaryType = 'monthly' AND startDate = ? AND endDate = ?
ORDER BY purchaseDate;

SELECT *
FROM ReceiptProject.summaries
         JOIN ReceiptProject.user u on summaries.userID = u.userID
         JOIN ReceiptProject.receipts r on u.userID = r.userID
WHERE summaryType = 'vendor'
ORDER BY purchaseDate;


SELECT *
FROM ReceiptProject.receipts
         JOIN ReceiptProject.user u on receipts.userID = u.userID
ORDER BY purchaseDate;
