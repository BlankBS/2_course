use UNIVER;

-- 1 определить все индексы, которые имеются в БД UNIVER
-- создать временную локальную таблицу > 1000 строк
-- Разработать запрос, в котором получить план запроса и определить его стоимость
-- создать кластеризованный индекс, уменьшающий стоимость запроса

-- sys

select _tables.name as [Table], _indexes.name as [Index], _indexes.type_desc as [Type]
from sys.indexes _indexes
inner join sys.tables _tables on _indexes.object_id = _tables.object_id;

-- SP_HELPINDEX

exec sp_helpindex 'AUDITORIUM' 
exec sp_helpindex 'AUDITORIUM_TYPE'
exec sp_helpindex 'FACULTY'
exec sp_helpindex 'GROUPS'
exec sp_helpindex 'PROFESSION'
exec sp_helpindex 'PROGRESS'
exec sp_helpindex 'PULPIT'
exec sp_helpindex 'STUDENT'
exec sp_helpindex 'SUBJECT'
exec sp_helpindex 'TEACHER'

create table #temp1 (ID int, Name varchar(50));

set nocount on;
declare @i int = 1;
while @i <= 1000
begin
	insert into #temp1 values(@i, cast(@i as varchar) + ' row');
	set @i += 1;
end;

checkpoint;
dbcc dropcleanbuffers;
select * from #temp1 where #temp1.ID = 500;

create clustered index ix_temp1_id on #temp1(ID);

select * from #temp1 where #temp1.ID = 500;

drop index ix_temp1_id on #temp1;
drop table #temp1;

-- 2 создать временную локальную таблицу > 10000 строк
-- разработать select-запрос, получить план и определить его стоимость
-- создать некластеризованный неуникальный составной индекс
-- оценить процедуры поиска информации

create table #temp2 
(
	ID int,
	CC int identity(1,1),
	Name varchar(100)
);

set nocount on;
declare @j int = 0;
while @j < 20000
begin
	insert #temp2(ID, Name) values(floor(30000*rand()), 'smth');
	set @j = @j + 1;
end;

select * from #temp2 where ID = 556 and CC > 3;

create index #temp2_nonclu on #temp2(ID, CC);

select * from #temp2 where ID = 556 and CC > 3;

drop index #temp2_nonclu on #temp2;
drop table #temp2;

-- 3 временная таблица > 10000 строк
-- план + стоимость
-- некластеризованный индекс покрытия, уменьшающий стоимость select-запроса

select CC from #temp2 where ID > 1500;

create index #temp2_ID_X on #temp2(ID) include(CC);

select CC from #temp2 where ID > 1500;

drop index #temp2_ID_X on #temp2;

-- 4 некластеризованный фильтруемый

select ID from #temp2 where ID between 15000 and 19999;

create index #temp2_where on #temp2(ID) where (ID >= 15000 and ID < 20000);

select ID from #temp2 where ID between 15000 and 19999;

drop index #temp2_where on #temp2;

-- 5 фрагментация

create index #temp2_id on #temp2(ID);

select ii.name as [name], avg_fragmentation_in_percent as [Фрагментация (%)]
from sys.dm_db_index_physical_stats(DB_ID(N'tempdb'), OBJECT_ID(N'tempdb..#temp2'), NULL, NULL, NULL) ss
inner join tempdb.sys.indexes ii on 
	ss.object_id = ii.object_id and 
	ss.index_id = ii.index_id
where ii.name = '#temp2_id';

insert top(50000) #temp2(ID, Name) select ID, Name from #temp2;

alter index #temp2_id on #temp2 reorganize;

alter index #temp2_id on #temp2 rebuild with (online = off);

drop index #temp2_id on #temp2;

-- 6 fillfactor

drop index #temp2_id on #temp2;
create index #temp2_id on #temp2(ID) with (fillfactor = 65);

insert top(50) percent into #temp2(ID, Name) select ID, Name from #temp2;

select ii.name as [name], avg_fragmentation_in_percent as [Фрагментация (%)]
from sys.dm_db_index_physical_stats(DB_ID(N'tempdb'), OBJECT_ID(N'tempdb..#temp2'), NULL, NULL, NULL) ss
inner join tempdb.sys.indexes ii on 
	ss.object_id = ii.object_id and 
	ss.index_id = ii.index_id
where ii.name = '#temp2_id';