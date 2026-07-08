use UNIVER

-- 1 переменные, инициализировать при объявлении, присвоить set select

declare @c char(10) = 'Чар',
		@vc varchar(20) = 'варчар';
declare @dt datetime,
		@myTime time,
		@i int,
		@si smallint,
		@ti tinyint,
		@num numeric(12, 5);

set @dt = getdate();
set @myTime = '10:13:47';
select @i = 100, @si = 32, @ti = 2;
set @num = 123.45678;

select @c, @vc, @dt, @num;
print 'int: ' + cast(@i as varchar);
print 'time: ' + cast(@myTime as nvarchar(8));
print 'smallint: ' + cast(@si as varchar);

-- 2 определить общую вместимость аудиторий
-- если > 200, то количество аудиторий, среднюю вместимость, количество аудиторий меньше средней и процент
-- если < 200, то вывести сообщение о размере общей вместимости

declare @total_cap int, @avg_cap float, @count_below int, @total_aud int;

select @total_cap = sum(AUDITORIUM.AUDITORIUM_CAPACITY),
		@total_aud = count(*),
		@avg_cap = avg(cast(AUDITORIUM.AUDITORIUM_CAPACITY as float))
from AUDITORIUM;

if @total_cap > 200
begin
	select @count_below = count(*) from AUDITORIUM where AUDITORIUM_CAPACITY < @avg_cap;
	select 
		@total_aud as [Количество аудиторий],
		@avg_cap as [Средняя вместимость],
		@count_below as [Меньше средней],
		cast(@count_below * 100 / @total_aud as numeric(5,2)) as [Процент];
end
else
	print 'Общая вместимость: ' + cast(@total_cap as varchar);

-- 3 глобальные переменные

print 'число строк @@ROWCOUNT: ' + cast(@@ROWCOUNT as varchar);
print 'версия @@VERSION: ' + cast(@@VERSION as varchar);
print 'ид процесса @@SPID: ' + cast(@@SPID as varchar);
print 'код последней ошибки @@ERROR: ' + cast(@@ERROR as varchar);
print 'имя сервера @@SERVERNAME: ' + cast(@@SERVERNAME as varchar);
print 'уровень вложенности транзакции @@TRANCOUNT: ' + cast(@@TRANCOUNT as varchar);
print 'проверка результата считывания строк рез набора @@FETCH_STATUS: ' + cast(@@FETCH_STATUS as varchar);
print 'уровень вложенности процедуры @@NESTLEVEL: ' + cast(@@NESTLEVEL as varchar);

-- 4 вычисление

declare @z float, @x float = 0.5, @t float = 0.5;

if @t > @x
	set @z = power(sin(@t), 2);
else if @t < @x
	set @z = 4 * (@t + @x)
else
	set @z = 1 - exp(@x - 2);

select
	@z as [z],
	@t as [t],
	@x as [x];

--

declare @fio nvarchar(100) = N'Сикорский Артём Александрович';
select
	substring(@fio, 1, charindex(' ', @fio)) +
	substring(@fio, charindex(' ', @fio) + 1, 1) + '. ' +
	substring(@fio, charindex(' ', @fio, charindex(' ', @fio) + 1) + 1, 1) + '.' as [Фамилия и инициалы];

-- студенты день рождения в след месяце
select STUDENT.NAME, STUDENT.BDAY, datediff(year, STUDENT.BDAY, getdate()) as Age
from STUDENT
where month(STUDENT.BDAY) = month(getdate());

-- 

select datename(weekday, PROGRESS.PDATE) as [День недели]
from PROGRESS
where PROGRESS.SUBJECT = 'ОАиП';

-- 5 if else
if(select avg(PROGRESS.NOTE) from PROGRESS) > 7
	print 'учеба намана'
else
	print 'оценка - не главное';

-- 6 case
select STUDENT.NAME, PROGRESS.SUBJECT, PROGRESS.NOTE,
	case
		when PROGRESS.NOTE between 9 and 10 then 'Отлично'
		when PROGRESS.NOTE between 7 and 8 then 'Хорошо'
		when PROGRESS.NOTE between 4 and 6 then 'Сойдет'
		else 'оценка - не главное'
	end as [Результат]
from PROGRESS
inner join STUDENT on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
inner join GROUPS on STUDENT.IDGROUP = GROUPS.IDGROUP
where GROUPS.FACULTY = 'ИТ';

-- 7 таблица 3 строки 10 строк + while

create table #tempTable (ID int, Val int, Smth varchar(10));

declare @counter int = 1;
while @counter <= 10
begin
	insert into #tempTable values (@counter, @counter * 10, 'Smth ' + cast(@counter as varchar));
	set @counter = @counter + 1;
end

select * from #tempTable;
drop table #tempTable;

-- 8 return

declare @check int = 10;
print 'начало';

while @check > 0
begin
	if @check < 5
		return;
	print '@check = ' + cast(@check as varchar)
	set @check = @check - 1;
end;

-- 9 try catch

begin try
	declare @ex int = 10 / 0;
end try
begin catch
	select
		ERROR_NUMBER() as [Код ошибки],
		ERROR_MESSAGE() as [Сообщение],
		ERROR_LINE() as [Строка],
		ERROR_PROCEDURE() as [Процедура],
		ERROR_SEVERITY() as [Серьезность],
		ERROR_STATE() as [Состояние];
end catch;