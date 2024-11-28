CREATE DATABASE PriceTracking
GO

USE PriceTracking
GO

CREATE TABLE Objetos (
id int IDENTITY,
Nombre varchar(20),
Tienda varchar(20),
Link varchar(500),
Precio varchar(15)
)
GO
