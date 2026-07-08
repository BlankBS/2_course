--  1 список наименований кафедр, которые находятся на факультете, обеспечивающем подготовку по специальности, в наименовании которого содержится слово технология или технологии

use UNIVER

select PULPIT.PULPIT_NAME from PULPIT
where FACULTY in
	(select FACULTY from PROFESSION where PROFESSION_NAME like N'%технологи%')

-- 2 первый подзапрос в конструкии inner join секции from внешнего запроса

select PULPIT.PULPIT_NAME 
from PULPIT 
inner join (select distinct FACULTY from PROFESSION where PROFESSION_NAME like N'%технологи%') as TechFaculties
on PULPIT.FACULTY = TechFaculties.FACULTY;

-- 3 первый запрос без подзапроса, через inner join трех

select distinct PULPIT.PULPIT_NAME from PULPIT
inner join FACULTY on PULPIT.FACULTY = FACULTY.FACULTY
inner join PROFESSION on FACULTY.FACULTY = PROFESSION.FACULTY
where PROFESSION.PROFESSION_NAME like N'%технологи%';

-- 4 список аудиторий самых больших вместимостей для каждого типа аудиторий

select a1.AUDITORIUM_TYPE [Тип], a1.AUDITORIUM_NAME as [Номер], a1.AUDITORIUM_CAPACITY as [Вместимость]
from AUDITORIUM a1
where a1.AUDITORIUM_CAPACITY =
	(select top 1 a2.AUDITORIUM_CAPACITY from AUDITORIUM a2
		where a2.AUDITORIUM_TYPE = a1.AUDITORIUM_TYPE
		order by a2.AUDITORIUM_CAPACITY desc)
order by a1.AUDITORIUM_CAPACITY desc;

-- 5 список наименований факультетов на котором нет ни одной кафедры

select FACULTY.FACULTY_NAME from FACULTY
where not exists (select * from PULPIT
					where PULPIT.FACULTY = FACULTY.FACULTY);

-- 6 строку, содержащую  средние значения оценок по дисциплинам: ОАиП, БД, СУБД.

select
	(select avg(PROGRESS.NOTE) from PROGRESS where SUBJECT = N'ОАиП') as [ОАиП],
	(select avg(PROGRESS.NOTE) from PROGRESS where SUBJECT = N'БД') as [БД],
	(select avg(PROGRESS.NOTE) from PROGRESS where SUBJECT = N'СУБД') as [СУБД];

-- 7 all

select PROGRESS.IDSTUDENT from PROGRESS
where PROGRESS.NOTE > all
	(select PROGRESS.NOTE from PROGRESS where PROGRESS.SUBJECT = N'ОАиП');

-- 8 any

select SUBJECT from PROGRESS
where NOTE = any 
	(select NOTE from PROGRESS where IDSTUDENT = 1001);

