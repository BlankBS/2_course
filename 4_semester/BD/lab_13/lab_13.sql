use UNIVER;

-- 1 процедуру PSUBJECT код дисциплина кафедра

go
create procedure PSUBJECT
as
begin
	select 
		SUBJECT.SUBJECT as [Код],
		SUBJECT.SUBJECT_NAME as [Дисциплина],
		SUBJECT.PULPIT as [Кафедра]
	from SUBJECT
	order by SUBJECT.SUBJECT

	return @@ROWCOUNT;
end;

declare @result int;
exec @result = PSUBJECT;
print 'Результат: ' + cast(@result as varchar(2));

-- 2 

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[PSUBJECT]
	@p varchar(20) = NULL,
	@c int OUTPUT
as
begin
	select 
		SUBJECT.SUBJECT as [Код],
		SUBJECT.SUBJECT_NAME as [Дисциплина],
		SUBJECT.PULPIT as [Кафедра]
	from SUBJECT
	where SUBJECT.PULPIT = isnull(@p, SUBJECT.PULPIT)
	order by SUBJECT.SUBJECT

	set @c = @@ROWCOUNT;

	return (select count(*) from SUBJECT);
end;
go

declare @filtred_rows int, 
		@total_rows int;
exec @total_rows = PSUBJECT @p = 'ИСиТ', @c = @filtred_rows OUTPUT;

print 'Дисциплин на выбранной кафедре: ' + cast(@filtred_rows as varchar(10));
print 'Всего дисциплин в университете: ' + cast(@total_rows as varchar(10));

GO

-- 3 

create table #SUBJECT 
(
	Code char(10),
	Name varchar(100),
	PulpitCode char(20)
);

go
alter procedure PSUBJECT @p varchar(20) = null
as begin
	select SUBJECT.SUBJECT, SUBJECT.SUBJECT_NAME, SUBJECT.PULPIT
	from SUBJECT
	where PULPIT = isnull(@p, PULPIT);
end;
go

insert into #SUBJECT exec PSUBJECT @p = 'ИСиТ';
insert into #SUBJECT exec PSUBJECT @p = 'ПОиСОИ';

select * from #SUBJECT;

-- 4

go
create procedure PAUDITORIUM_INSERT 
	@a char(20), @n varchar(50), @c int = 0, @t char(10)
as
begin
	begin try
		insert into AUDITORIUM (AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_CAPACITY, AUDITORIUM_TYPE) values
			(@a, @n, @c, @t);
		return 1;
	end try
	begin catch
		print 'Ошибка ' + cast(ERROR_NUMBER() as varchar(10)) + ': ' + ERROR_MESSAGE();
		print 'Уровень серьезности: ' + cast(ERROR_SEVERITY() as varchar(5));
		return -1;
	end catch
end;
go

declare @res int;
exec @res = PAUDITORIUM_INSERT @a = '555-5', @n = '555-5', @c = 55, @t = 'ЛК';
print 'Результат вставки: ' + cast(@res as varchar(2));

-- 5

go
create procedure SUBJECT_REPORT
	@p char(10)
as
begin
	declare @rc int = 0;
	begin try
		if not exists (select * from PULPIT where PULPIT = @p)
			raiserror('Ошибка в параметрах: кафедра %s не найдена', 11, 1, @p);

		declare @sub_name char(10), @report varchar(max) = '';
		declare sub_cur cursor for 
			select SUBJECT.SUBJECT from SUBJECT where PULPIT = @p;

		open sub_cur;
		fetch sub_cur into @sub_name;
		while @@FETCH_STATUS = 0
		begin
			set @report = @report + rtrim(@sub_name) + ', ';
			set @rc = @rc + 1;
			fetch sub_cur into @sub_name;
		end;

		close sub_cur;
		deallocate sub_cur;

		if len(@report) > 0
			set @report = left(@report, len(@report) - 1);
		print 'Дисциплины кафедры ' + @p + ': ' + @report;

		return @rc;
	end try
	begin catch
		print error_message();
		if error_procedure() is not null
			print 'Процедура: ' + error_procedure();
		return 0;
	end catch;
end;
go

declare @count_sub int;
exec @count_sub = SUBJECT_REPORT @p = 'ИСиТ';
print 'количество в отчете: ' + cast(@count_sub as varchar(2));

-- 6

go
create procedure PAUDITORIUM_INSERTX
	@a char(20), @n varchar(50), @c int = 0, @t char(10), @tn varchar(50)
as
begin
	declare @rc int;
	set transaction isolation level serializable
	begin try	
		begin transaction
			insert into AUDITORIUM_TYPE (AUDITORIUM_TYPE, AUDITORIUM_TYPENAME) values
				(@t, @tn);
			
			exec @rc = PAUDITORIUM_INSERT @a, @n, @c, @t;

			if @rc = -1
				throw 50001, 'Ошибка при вставке в AUDITORIUM', 1;
		commit transaction;
	end try
	begin catch
		if @@TRANCOUNT > 0 
			rollback transaction;

		print 'Номер ошибки: ' + cast(error_number() as varchar(10));
		print 'Сообщение: ' + error_message();
		print 'Строка: ' + CAST(ERROR_LINE() AS VARCHAR(10));
		return -1;
	end catch;
end;
go

declare @res_x int;
exec @res_x = PAUDITORIUM_INSERTX @a = '666-6', @n = '666-6', @c = 66, @t = 'ТП', @tn = 'Телепортаницонная';
print 'Итоговый статус: ' + cast(@res_x as varchar(2));















-- студент сдает экзамен по предмету, если оценка меньше 4 то на другую группу переводят

go
create procedure passExam 
	@sId int, @passNote int, @newGroupId int
as
begin
	if @passNote < 4
	begin
		update STUDENT set IDGROUP = @newGroupId where @sId = IDSTUDENT;
	end;
end;
go

declare @stId int = 1000;

select * from STUDENT where STUDENT.IDSTUDENT = @stId;

declare @oldGroup int;
select @oldGroup = STUDENT.IDGROUP from STUDENT where STUDENT.IDSTUDENT = @stId;

declare @result int;
exec @result = passExam @sId = 1000, @passNote = 3, @newGroupId = 3;

select * from STUDENT where STUDENT.IDSTUDENT = 1000;

update STUDENT set IDGROUP = 2 where STUDENT.IDSTUDENT = @stId;

select * from STUDENT where STUDENT.IDSTUDENT = 1000;

-- переводим преподавателя на другую кафедру
-- если не существует - создаем

go
create procedure swapPulpit
	@tCode nvarchar(8), @newPulpit nvarchar(20), @newPulpitName nvarchar(50), @newFaculty nvarchar(20)
as
begin
	if exists (select PULPIT.PULPIT from PULPIT where PULPIT.PULPIT = @newPulpit)
	begin
		update TEACHER set TEACHER.PULPIT = @newPulpit where TEACHER.TEACHER = @tCode;
	end;
	else
	begin
		insert into PULPIT (PULPIT, PULPIT_NAME, FACULTY) values
			(@newPulpit, @newPulpitName, @newFaculty)
	end;
end;
go

delete PULPIT where PULPIT.PULPIT = 'first';

select * from PULPIT where PULPIT.PULPIT = 'ИСиТ'
select * from TEACHER where TEACHER.TEACHER = 'МРЗ';

exec swapPulpit @tCode = 'МРЗ', @newPulpit = 'ИСиТ', @newPulpitName = 'левое весло', @newFaculty = 'ИТ';

select * from PULPIT where PULPIT.PULPIT = 'ИСиТ'
select * from TEACHER where TEACHER.TEACHER = 'МРЗ';

go
create proc SwapGroupOnPassExam
	@stId int, @mark int, @newGroupId int = 0
as
begin
	if @mark < 4
	begin
		if @newGroupId = 0
			update student set IDGROUP = IDGROUP + 1 where STUDENT.IDSTUDENT = @stId;
		else
			update student set IDGROUP = @newGroupId where STUDENT.IDSTUDENT = @stId;
	end;
end;
go

declare @tempId int = 1000;

select * from STUDENT where STUDENT.IDSTUDENT = @tempId;
exec SwapGroupOnPassExam @stId = @tempId, @mark = 3, @newGroupId = 1;
select * from STUDENT where STUDENT.IDSTUDENT = @tempId;


