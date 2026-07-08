use UNIVER;

-- 1 скалярная
go
create function COUNT_STUDENTS (@faculty varchar(20))
returns int
as
begin
	declare @res int;

	select @res = count(STUDENT.IDSTUDENT)
		from FACULTY
		inner join GROUPS on GROUPS.FACULTY = FACULTY.FACULTY
		inner join STUDENT on STUDENT.IDGROUP = GROUPS.IDGROUP
		where FACULTY.FACULTY = @faculty;

	return isnull(@res, 0);
end;
go

select dbo.COUNT_STUDENTS('ИТ') as [кол-во студентов];

go
alter function COUNT_STUDENTS
(
	@faculty varchar(20) = null,
	@prof varchar(20) = null
)
returns int
as
begin
	declare @res int;

	SELECT @res = COUNT(S.IDSTUDENT)
    FROM FACULTY F
    INNER JOIN GROUPS G ON F.FACULTY = G.FACULTY
    INNER JOIN STUDENT S ON G.IDGROUP = S.IDGROUP
    WHERE (F.FACULTY = @faculty OR @faculty IS NULL)
      AND (G.PROFESSION = @prof OR @prof IS NULL);

    RETURN ISNULL(@res, 0);
end;
go

SELECT dbo.COUNT_STUDENTS('ИТ', '1-40 01 02') AS [кол-во студентов];

-- 2 

go
create function FSUBJECTS (@p varchar(20))
returns varchar(300)
as
begin
	declare @res varchar(300) = 'Дисциплины: ';
	declare @count int = 0;
	declare @sub char(10);

	declare sub_cursor cursor local static for
		select SUBJECT
		from SUBJECT
		where SUBJECT.PULPIT = @p;

	open sub_cursor;
	fetch sub_cursor into @sub;

	while @@FETCH_STATUS = 0
	begin
		if @count > 0 
            set @res = @res + ', ';
        
        set @res = @res + RTRIM(@sub);
        set @count = @count + 1;

		fetch sub_cursor into @sub;
	end;

	close sub_cursor;
	deallocate sub_cursor;

	if @count = 0
		set @res = '-';
	else
		set @res = @res + '.'

	return @res;
end;
go

select PULPIT, dbo.FSUBJECTS(PULPIT)
from PULPIT;

-- 3 

go
create function FFACPUL (@faculty char(10), @pulpit char(20))
returns table
as
return
(
	select FACULTY.FACULTY, 
		   PULPIT.PULPIT
		from FACULTY
		left outer join PULPIT on FACULTY.FACULTY = PULPIT.FACULTY
		where (FACULTY.FACULTY = @faculty or @faculty is null) and 
			  (PULPIT.PULPIT = @pulpit or @pulpit is null)
);
go

select * from dbo.FFACPUL(null, null);
select * from dbo.FFACPUL('ИДиП', null);
select * from dbo.FFACPUL(null, 'ЛМиЛЗ');
select * from dbo.FFACPUL('ТТЛП', 'ЛМиЛЗ');

-- 4

go
create function FCTEACHER (@pulpit char(20) = null)
returns int
as
begin
	declare @res int;

	if @pulpit is null
		select @res = count(*) from TEACHER;
	else
		select @res = count(*) from TEACHER where TEACHER.PULPIT = @pulpit;

	return @res;
end;
go

select PULPIT, dbo.FCTEACHER(PULPIT) as [кол-во преподавателей]
from PULPIT;

select dbo.FCTEACHER(null) as [кол-во преподавателей нулл];


-- 5 

create function COUNT_PULPITS(@faculty varchar(50))
returns int
as
begin
    declare @count int;
    select @count = count(PULPIT) from PULPIT where FACULTY = @faculty;
    return @count;
end;
go

create function COUNT_GROUPS(@faculty varchar(50))
returns int
as
begin
    declare @count int;
    select @count = count(IDGROUP) from GROUPS where FACULTY = @faculty;
    return @count;
end;
go

create function COUNT_PROFESSIONS(@faculty varchar(50))
returns int
as
begin
    declare @count int;
    select @count = count(PROFESSION) from PROFESSION where FACULTY = @faculty;
    return @count;
end;
go