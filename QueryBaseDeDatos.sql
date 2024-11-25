CREATE DATABASE PriceTracking
GO

USE PriceTracking
GO

CREATE TABLE Objetos (
id int PRIMARY KEY IDENTITY,
Nombre varchar(20),
Tienda varchar(20),
link varchar(128)
)
GO
