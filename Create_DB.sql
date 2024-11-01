CREATE DATABASE VendasDB;
go

USE VendasDB;
go

CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL
);
go

CREATE TABLE Filiais (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL
);
go

CREATE TABLE Produtos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Preco DECIMAL(18, 2) NOT NULL
);
go

CREATE TABLE Vendas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DataVenda DATETIME NOT NULL,
    ClienteId INT FOREIGN KEY REFERENCES Clientes(Id),
    FilialId INT FOREIGN KEY REFERENCES Filiais(Id),
    ValorTotal DECIMAL(18, 2) NOT NULL,
    Cancelado BIT NOT NULL
);
go

CREATE TABLE ItensVenda (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VendaId INT FOREIGN KEY REFERENCES Vendas(Id),
    ProdutoId INT FOREIGN KEY REFERENCES Produtos(Id),
    Quantidade INT NOT NULL,
    ValorUnitario DECIMAL(18, 2) NOT NULL,
    Desconto DECIMAL(18, 2) NOT NULL,
    ValorTotal DECIMAL(18, 2) NOT NULL
);
go
