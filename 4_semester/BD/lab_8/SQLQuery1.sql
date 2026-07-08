use UNIVER

-- 1 представление с именем преподаватель. построено на основе запроса к таблице TEACHER и содержать:
-- код, имя преподавателя, пол, код кафедры

create view Преподаватель as
select 
	TEACHER.TEACHER as [Код],
	TEACHER.TEACHER_NAME as [Имя преподавателя],
	TEACHER.GENDER as [Пол],
	TEACHER.PULPIT as [Код кафедры]
from TEACHER;

select * from Преподаватель;

-- 2 представление с именем Количество кафедр
-- факультет, количество кафдер (количество строк таблицы PULPIT)

create view [Количество кафедр] as 
select 
	FACULTY.FACULTY as [Факультет], 
	count(PULPIT.PULPIT) as [Количество кафедр]
from FACULTY
left join PULPIT on FACULTY.FACULTY = PULPIT.FACULTY
group by FACULTY.FACULTY;

select * from [Количество кафедр];

-- 3 представление Аудитории.
-- код, наименование аудитории
-- только лекционные и допускать выполнение insert, update, delete

create view Аудитории as
select 
	AUDITORIUM.AUDITORIUM as [Код аудитории],
	AUDITORIUM.AUDITORIUM_TYPE as [Тип аудитории]
from AUDITORIUM
where AUDITORIUM.AUDITORIUM_TYPE like N'ЛК%';

select * from Аудитории

insert into Аудитории ([Код аудитории], AUDITORIUM_TYPE)
	values('500-1', 'ЛК');

delete from Аудитории where [Код аудитории] = '500-1';

update Аудитории set [Код аудитории] = '501-1' where [Код аудитории] = '500-1';

-- 4 представление Лекционные_аудитории
-- код, наименование

create view Лекционные_аудитории as
select 
	AUDITORIUM.AUDITORIUM as [Код аудитории],
	AUDITORIUM.AUDITORIUM_TYPE as [Тип аудитории]
from AUDITORIUM
where AUDITORIUM_TYPE like N'ЛК%' with check option;

select * from Лекционные_аудитории

insert into Лекционные_аудитории([Код аудитории], [Тип аудитории])
	values('601-1', 'ЛБ-к');

-- 5 представление Дисциплины
-- все дисциплины в алфавитном порядке
-- код, наименование дисциплины, код кафедры

create view Дисциплины as
select top 100 percent
	SUBJECT.SUBJECT as [Код дисциплины],
	SUBJECT.SUBJECT_NAME as [Наименование],
	SUBJECT.PULPIT as [Код кафедры]
from SUBJECT
order by SUBJECT_NAME;

select * from Дисциплины

-- 6 именить 2, чтобы было привязано к базовым таблицам

alter view [Количество кафедр]
with SCHEMABINDING as
select 
	FACULTY.FACULTY [Факультет],
	count(*) as [Количество кафедр]
from dbo.FACULTY
inner join dbo.PULPIT on FACULTY.FACULTY = PULPIT.FACULTY
group by FACULTY.FACULTY

drop table FACULTY