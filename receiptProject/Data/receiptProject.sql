-- MySQL dump 10.13  Distrib 5.7.24, for osx11.1 (x86_64)
--
-- Host: localhost    Database: ReceiptProject
-- ------------------------------------------------------
-- Server version	9.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `receiptItems`
--

DROP TABLE IF EXISTS `receiptItems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `receiptItems` (
  `itemID` int NOT NULL AUTO_INCREMENT,
  `receiptID` int NOT NULL,
  `itemName` varchar(250) DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `unitPrice` decimal(10,2) DEFAULT NULL,
  `totalPrice` decimal(10,2) DEFAULT NULL,
  `rawText` text,
  PRIMARY KEY (`itemID`),
  KEY `receiptID` (`receiptID`),
  CONSTRAINT `receiptitems_ibfk_1` FOREIGN KEY (`receiptID`) REFERENCES `receipts` (`receiptID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receiptItems`
--

LOCK TABLES `receiptItems` WRITE;
/*!40000 ALTER TABLE `receiptItems` DISABLE KEYS */;
/*!40000 ALTER TABLE `receiptItems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receipts`
--

DROP TABLE IF EXISTS `receipts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `receipts` (
  `receiptID` int NOT NULL AUTO_INCREMENT,
  `userID` int NOT NULL,
  `vendor` varchar(250) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `purchaseDate` date DEFAULT NULL,
  `imagePath` text,
  `metadataJson` json DEFAULT NULL,
  PRIMARY KEY (`receiptID`),
  KEY `userID` (`userID`),
  CONSTRAINT `receipts_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receipts`
--

LOCK TABLES `receipts` WRITE;
/*!40000 ALTER TABLE `receipts` DISABLE KEYS */;
/*!40000 ALTER TABLE `receipts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `summaries`
--

DROP TABLE IF EXISTS `summaries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `summaries` (
  `summaryID` int NOT NULL AUTO_INCREMENT,
  `userID` int NOT NULL,
  `summaryType` enum('weekly','monthly','vendor') NOT NULL,
  `startDate` date DEFAULT NULL,
  `endDate` date DEFAULT NULL,
  `totalSpent` decimal(10,2) DEFAULT NULL,
  `dataJson` json DEFAULT NULL,
  PRIMARY KEY (`summaryID`),
  KEY `userID` (`userID`),
  CONSTRAINT `summaries_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `summaries`
--

LOCK TABLES `summaries` WRITE;
/*!40000 ALTER TABLE `summaries` DISABLE KEYS */;
/*!40000 ALTER TABLE `summaries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `userID` int NOT NULL AUTO_INCREMENT,
  `email` varchar(250) NOT NULL,
  `password` varchar(250) NOT NULL,
  PRIMARY KEY (`userID`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-01 18:33:33


-- Insert dummy users
INSERT INTO user (email, password) VALUES
('alice@example.com', 'password123'),
('bob@example.com', 'secure456'),
('carol@example.com', 'qwerty789');

-- Insert dummy receipts
INSERT INTO receipts (userID, vendor, amount, purchaseDate, imagePath, metadataJson) VALUES
(1, 'Amazon', 120.45, '2025-01-05', '/images/amazon1.jpg', '{"category":"electronics"}'),
(2, 'Walmart', 52.30, '2025-01-12', '/images/walmart1.jpg', '{"category":"groceries"}'),
(1, 'Target', 89.99, '2025-01-20', '/images/target1.jpg', '{"category":"clothing"}');

-- Insert dummy receiptItems
INSERT INTO receiptItems (receiptID, itemName, quantity, unitPrice, totalPrice, rawText) VALUES
(1, 'Bluetooth Speaker', 1, 120.45, 120.45, '1x Bluetooth Speaker $120.45'),
(2, 'Milk', 2, 2.50, 5.00, '2x Milk $2.50'),
(2, 'Bread', 1, 2.30, 2.30, '1x Bread $2.30'),
(3, 'Jeans', 1, 89.99, 89.99, '1x Jeans $89.99');

-- Insert dummy summaries
INSERT INTO summaries (userID, summaryType, startDate, endDate, totalSpent, dataJson) VALUES
(1, 'monthly', '2025-01-01', '2025-01-31', 210.44, '{"Amazon": 120.45, "Target": 89.99}'),
(2, 'monthly', '2025-01-01', '2025-01-31', 7.30, '{"Walmart": 7.30}');
