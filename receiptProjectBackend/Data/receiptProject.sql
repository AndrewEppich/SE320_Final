-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: receiptproject
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `receiptitems`
--

DROP TABLE IF EXISTS `receiptitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `receiptitems` (
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receiptitems`
--

LOCK TABLES `receiptitems` WRITE;
/*!40000 ALTER TABLE `receiptitems` DISABLE KEYS */;
INSERT INTO `receiptitems` VALUES (1,1,'Bluetooth Speaker',1,120.45,120.45,'1x Bluetooth Speaker $120.45'),(2,2,'Milk',2,2.50,5.00,'2x Milk $2.50'),(3,2,'Bread',1,2.30,2.30,'1x Bread $2.30'),(4,3,'Jeans',1,89.99,89.99,'1x Jeans $89.99');
/*!40000 ALTER TABLE `receiptitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receipts`
--

DROP TABLE IF EXISTS `receipts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receipts`
--

LOCK TABLES `receipts` WRITE;
/*!40000 ALTER TABLE `receipts` DISABLE KEYS */;
INSERT INTO `receipts` VALUES (1,1,'Amazon',120.45,'2025-01-05','/images/amazon1.jpg','{\"category\": \"electronics\"}'),(2,2,'Walmart',52.30,'2025-01-12','/images/walmart1.jpg','{\"category\": \"groceries\"}'),(3,1,'Target',89.99,'2025-01-20','/images/target1.jpg','{\"category\": \"clothing\"}');
/*!40000 ALTER TABLE `receipts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
                        `userID` int NOT NULL AUTO_INCREMENT,
                        `email` varchar(250) NOT NULL,
                        `password` varchar(250) NOT NULL,
                        PRIMARY KEY (`userID`),
                        UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'alice@example.com','password123'),(2,'bob@example.com','secure456'),(3,'carol@example.com','qwerty789');
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

-- Dump completed on 2025-05-21 18:30:00
