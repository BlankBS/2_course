use UNIVER;

-- 1 дисциплины исит

declare @sub_list varchar(max) = '',
		@sub_name char(10);

declare subject_cursor cursor local for
			select SUBJECT.SUBJECT from SUBJECT where SUBJECT.PULPIT = N'ИСиТ';

open subject_cursor;
fetch next from subject_cursor into @sub_name;

while @@FETCH_STATUS = 0
begin
	if @sub_list = ''
		set @sub_list = RTRIM(@sub_name);
	else
		set @sub_list = @sub_list + ', ' + RTRIM(@sub_name);

	fetch next from subject_cursor into @sub_name;
end;

close subject_cursor;
deallocate subject_cursor;

print @sub_list;

-- 2 локал вс глобал

declare local_cursor cursor local for
	select AUDITORIUM.AUDITORIUM from AUDITORIUM;

open local_cursor;

declare @aud char(20);
fetch local_cursor into @aud;
print 'локал: ' + @aud;
go

declare @aud2 char(20);
fetch local_cursor into @aud2;
print 'локал2: ' + @aud2;


declare global_cursor cursor global for
	select AUDITORIUM.AUDITORIUM from AUDITORIUM;

open global_cursor;

declare @aud char(20);
fetch next from global_cursor into @aud;
print 'глобал: ' + @aud;
go

declare @aud2 char(20);
fetch next from global_cursor into @aud2;
print 'глобал2: ' + @aud2;

-- 3 статик вс динамик

declare @aud char(20), @cap int;
declare static_cursor cursor local static for 
	select AUDITORIUM.AUDITORIUM, AUDITORIUM.AUDITORIUM_CAPACITY
	from AUDITORIUM
	where AUDITORIUM.AUDITORIUM_TYPE = N'ЛК';

open static_cursor;

print 'кол-во строк: ' + cast(@@CURSOR_ROWS as varchar(5));

update AUDITORIUM set AUDITORIUM.AUDITORIUM_CAPACITY = 299 where AUDITORIUM.AUDITORIUM = '236-1';
delete from AUDITORIUM where AUDITORIUM = '408-2';
insert into AUDITORIUM(AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY) values
	('999-1', '999-1', 'ЛК', 100);

fetch next from static_cursor into @aud, @cap;
while @@FETCH_STATUS = 0
begin
	print 'ауд: ' + @aud + ' вместимость: ' + cast(@cap as varchar(5));
	fetch next from static_cursor into @aud, @cap;
end;

close static_cursor;
deallocate static_cursor;

update AUDITORIUM set AUDITORIUM_CAPACITY = 60 where AUDITORIUM = '236-1';
insert into AUDITORIUM (AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY) values
	('408-2', '408-2','ЛК', 90);
delete from AUDITORIUM where AUDITORIUM = '999-1';


declare @aud char(20), @cap int;
declare dynamic_cursor cursor local dynamic for 
	select AUDITORIUM.AUDITORIUM, AUDITORIUM.AUDITORIUM_CAPACITY
	from AUDITORIUM
	where AUDITORIUM.AUDITORIUM_TYPE = N'ЛК';

open dynamic_cursor;

print 'кол-во строк: ' + cast(@@CURSOR_ROWS as varchar(5));

update AUDITORIUM set AUDITORIUM_CAPACITY = 299 where AUDITORIUM = '236-1';
delete from AUDITORIUM where AUDITORIUM = '408-2';
insert into AUDITORIUM(AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY) values
	('999-1', '999-1', 'ЛК', 100);

fetch next from dynamic_cursor into @aud, @cap;
while @@FETCH_STATUS = 0
begin 
	print 'ауд: ' + @aud + ' вместимость: ' + cast(@cap as varchar(5));
	fetch next from dynamic_cursor into @aud, @cap;
end;

close dynamic_cursor;
deallocate dynamic_cursor;

-- 4 атрибут scroll

declare @row_num int, @stud_name nvarchar(100);

declare scroll_cursor cursor local dynamic scroll for
	select ROW_NUMBER() over (order by NAME), STUDENT.NAME
	from STUDENT;

open scroll_cursor;

fetch next from scroll_cursor into @row_num, @stud_name;
print 'next: ' + cast(@row_num as varchar(3)) + ' ' + @stud_name;

fetch last from scroll_cursor into @row_num, @stud_name;
print 'last: ' + cast(@row_num as varchar(3)) + ' ' + @stud_name;

fetch prior from scroll_cursor into @row_num, @stud_name;
print 'prior: ' + cast(@row_num as varchar(3)) + ' ' + @stud_name;

fetch absolute 3 from scroll_cursor into @row_num, @stud_name;
print 'absolute 3: ' + cast(@row_num as varchar(3)) + ' ' + @stud_name;

fetch absolute -2 from scroll_cursor into @row_num, @stud_name;
print 'absolute -2: ' + cast(@row_num as varchar(3)) + ' ' + @stud_name;

fetch relative -5 from scroll_cursor into @row_num, @stud_name;
print 'relative -5: ' + cast(@row_num as varchar(3)) + ' ' + @stud_name;

fetch first from scroll_cursor into @row_num, @stud_name;
print 'first: ' + cast(@row_num as varchar(3)) + ' ' + @stud_name;

close scroll_cursor;
deallocate scroll_cursor;

-- 5

declare @sub char(10), @stud_id int, @note int;

declare update_cursor cursor local for
	select PROGRESS.SUBJECT, PROGRESS.IDSTUDENT, PROGRESS.NOTE
	from PROGRESS
	where PROGRESS.SUBJECT = 'ОАиП'
	for update;

open update_cursor;

fetch next from update_cursor into @sub, @stud_id, @note;
if @@FETCH_STATUS = 0
begin
	delete from PROGRESS where current of update_cursor;
	print 'строка для студента ' + cast(@stud_id as varchar) + ' удалена';
end;

fetch next from update_cursor into @sub, @stud_id, @note;
if @@FETCH_STATUS = 0
begin
	update PROGRESS set NOTE = NOTE + 1 where current of update_cursor;
	print 'оценка для студента ' + cast(@stud_id as varchar) + ' увеличена на 1'; 
end;

close update_cursor;
deallocate update_cursor;

-- 6

select * into #t6 from progress
declare cur6 cursor local for select
p.note from #t6 p inner join 
student s on p.idstudent = s.idstudent inner join
groups g on s.idgroup=g.idgroup
where p.note < 4
for update

open cur6
fetch next from cur6	

while @@fetch_status=0
begin
delete from #t6
where current of cur6

fetch next from cur6
end
close cur6

select * from #t6
drop table #t6

-- если встретит оценку меньше 6, то увеличит на один балл

go
declare @note int, @idS int;
declare my_cursor cursor local for
	select PROGRESS.NOTE, PROGRESS.IDSTUDENT
	from PROGRESS
	for update of PROGRESS.NOTE;

open my_cursor;

fetch next from my_cursor into @note, @idS;

while @@FETCH_STATUS = 0
begin
	if @note < 7
	begin
		update PROGRESS set NOTE = NOTE + 1
			where current of my_cursor;
		print 'Оценка для студента ' + cast(@idS as varchar) + ' увеличена на 1';
	end;

	fetch next from my_cursor into @note, @idS;
end;

close my_cursor;
deallocate my_cursor;
