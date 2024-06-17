
--SQL QUERIES FOR FINAL SVILUPPO PROJECT

-- Víctor Navareño, Marco Vega y Jorge Sanchez


-- 1. START BY CREATING THE DATABASE
CREATE DATABASE ecommerece


--2. CREATE THE TABLE Products within the database 
CREATE TABLE Products (
    id INT NOT NULL PRIMARY KEY,
    ProductName VARCHAR(50) NOT NULL,
    Price FLOAT NOT NULL,
    Qty INT NOT NULL
);

-- TEST IT WORKS (The table Products should be empty the first time:)

USE ecommerece
SELECT * FROM Products;
--IMPORTANT: To connect to the database from the api, Remember to update the "connection string" in the project"