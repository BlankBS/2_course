use Univer;

-- 1 неявная транзакция

set nocount on;
if exists (select * from sys.objects where object_id = object_id(N'DBO.X'))
	drop table X;

declare @flag char(1) = 'c', @count int;
set implicit_transactions on;

create table X (ID int, NAME varchar(50));
	insert into X values
		(1, 'ИТ'),
		(2, 'ТОВ');
	
	select @count = count(*) from X;
	print 'Строк в таблице: ' + cast(@count as varchar(2));

	if @flag = 'c'
		commit;
	else
		rollback;

set implicit_transactions off;

if exists(select * from sys.objects where object_id = object_id(N'DBO.X'))
	print 'Таблица сохранена'
else
	print 'Таблицы нет';

-- 2 атомарность явной

begin try
	begin transaction
		insert into AUDITORIUM (AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY)
        VALUES ('100-1', '100-1', 'ЛК', 50);

		update AUDITORIUM set AUDITORIUM_CAPACITY = 500 where AUDITORIUM = '100-1';

	commit transaction;
	print 'Транзакция успешно зафиксирована';
end try
begin catch
	print 'Ошибка: ' + error_message();
	if @@TRANCOUNT > 0
		rollback transaction;
	print 'Транзакция отключена (свойство атомарности)';
end catch;

-- 3 save tran

declare @count1 int;
select @count1 = count(*) from FACULTY;
print 'Количество до: ' + cast(@count1 as varchar(2));
begin transaction
	insert into FACULTY (FACULTY, FACULTY_NAME) values
		('ИЭ', 'Экономический');

	print 'Экономический успешно вставлен';

	save transaction savePoint1;

	begin try
		insert into FACULTY (FACULTY, FACULTY_NAME) values
			('ИТ', 'пупупу');
		print 'Экономический успешно вставлен';
	end try
	begin catch
		print 'Вставка не удалась, откатываемся к контрольной точке';
		rollback transaction savePoint1;
	end catch;
commit transaction;
select @count1 = count(*) from FACULTY;
print 'Количество до: ' + cast(@count1 as varchar(2));
delete from FACULTY where FACULTY = 'ИЭ';

-- 4а read uncommitted

set transaction isolation level read uncommitted;
begin transaction
	select * from AUDITORIUM where AUDITORIUM = '500-5';
commit;

-- 5a read committed

set transaction isolation level read committed;
begin transaction
	select AUDITORIUM_CAPACITY from AUDITORIUM where AUDITORIUM = '236-1';
	select AUDITORIUM_CAPACITY from AUDITORIUM where AUDITORIUM = '236-1';
commit;

-- 6a repeatable read

set transaction isolation level repeatable read;
begin transaction
	select * from AUDITORIUM where AUDITORIUM_CAPACITY > 200;
	waitfor delay '00:00:07';
	select * from AUDITORIUM where AUDITORIUM_CAPACITY > 200;
commit;


-- 7а serializable

set transaction isolation level serializable
begin transaction
	select count(*) from AUDITORIUM;
	waitfor delay '00:00:07';
	select count(*) from AUDITORIUM;
commit;

-- 8 вложенные

print '1) Уровень вложенности: ' + cast(@@TRANCOUNT as varchar);

begin transaction
	print '2) Уровень вложенности: ' + cast(@@TRANCOUNT as varchar);
	
	begin transaction
		print '3) Уровень вложенности: ' + cast(@@TRANCOUNT as varchar);
	commit transaction;

	print '4) Уровень вложенности: ' + cast(@@TRANCOUNT as varchar);
rollback transaction;
print '5) Уровень вложенности: ' + cast(@@TRANCOUNT as varchar);