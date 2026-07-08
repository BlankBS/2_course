create database PUBGSkinMarket;

create table Categories
(
	Id int primary key identity,
	CategoryName nvarchar(50) not null
);

create table Skins
(
	Id int primary key identity,
	ShortName nvarchar(100) not null,
	Price decimal(18, 2) not null,
	Quantity int default 0,
	Rarity nvarchar(50),
	CategoryId int foreign key references Categories(Id),
	ImageBytes varbinary(max)
);

create table SalesLog
(
	Id int primary key identity,
	SkinId int foreign key references Skins(Id),
	SaleDate datetime default getdate(),
	Amount int
);

insert into Categories (CategoryName) values ('Оружие'), ('Одежда'), ('Транспорт');

