

USE master;
ALTER DATABASE ПРОДАЖИ SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE ПРОДАЖИ;
CREATE database ПРОДАЖИ;

use ПРОДАЖИ
CREATE table ТОВАРЫ
( 
  Наименование nvarchar(50) primary key,
  ЦЕНА real unique not null,
  Количество int
);

CREATE table Заказчики
(
	Наименование_фирмы nvarchar(20) primary key,
	Адрес nvarchar(50),
	Расчетный_счет nvarchar(20)
);

CREATE TABLE Заказы
(
    Номер_заказа int PRIMARY KEY, 
    Наименование_товара nvarchar(50) foreign key REFERENCES Товары (Наименование), 
    Цена_продажи decimal(18,2), 
    Количество int,
    Дата_поставки date,
    Заказчик nvarchar(20) REFERENCES Заказчики(Наименование_фирмы) 
);

alter table ТОВАРЫ add Дата_поступления date;

alter table ТОВАРЫ drop column Дата_поступления;

insert into ТОВАРЫ (Наименование, Цена, Количество)
    Values('Стол', 25.5, 4),
          ('Стул', 15, 3);

SELECT Наименование, ЦЕНА From ТОВАРЫ;

SELECT count(*) from ТОВАРЫ;

SELECT Наименование [Дешевые товары] from ТОВАРЫ 
    Where ЦЕНА < 20

UPDATE ТОВАРЫ set ЦЕНА = ЦЕНА + 1 Where Наименование = 'Стол';

Delete from ТОВАРЫ Where Наименование = 'Стул';

use master
ALTER DATABASE UNIVER SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE UNIVER;
go
create database UNIVER
on primary
(name = N'UNIVER_mdf', filename = N'D:\BD\UNIVER_mdf.mdf', size = 10240Kb, maxsize = UNLIMITED, filegrowth=1024Kb)
log on
(name = N'UNIVER_log', filename = N'D:\BD\UNIVER_log.ldf', size = 10240Kb, maxsize = 2048Gb, filegrowth = 10%)
go

use master
ALTER DATABASE UNIVER SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE UNIVER;
create database UNIVER on primary
( name = N'UNIVER_mdf', filename = N'D:\BD\UNIVER_mdf.mdf', 
   size = 10240Kb, maxsize=UNLIMITED, filegrowth=1024Kb),
( name = N'UNIVER_ndf', filename = N'D:\BD\UNIVER_ndf.ndf', 
   size = 10240KB, maxsize=1Gb, filegrowth=25%),
filegroup FG1
( name = N'UNIVER_fg1_1', filename = N'D:\BD\UNIVER_fgq-1.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),
( name = N'UNIVER_fg1_2', filename = N'D:\BD\UNIVER_fgq-2.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%)
log on
( name = N'UNIVER_log', filename=N'D:\BD\UNIVER_log.ldf',       
   size=10240Kb,  maxsize=2048Gb, filegrowth=10%)

use UNIVER
drop table if exists AUDITORIUM;
create table AUDITORIUM
(
AUDITORIUM char(20) primary key,
AUDITORIUM_CAPACITY int default 1 check (AUDITORIUM_CAPACITY between 1 and 300),
AUDITORIUM_NAME varchar(50)
) on FG1;