use UNIVER;

-- 1 коды аудиторий и соотвутствующих им наименований типов аудиторий

select 
	AUDITORIUM.AUDITORIUM_TYPE as [Код аудтории], 
	AUDITORIUM_TYPE.AUDITORIUM_TYPENAME as [Наименование]
from AUDITORIUM inner join AUDITORIUM_TYPE
on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE

-- 2 коды аудиторий и соответствующих им наименований типов аудиторий с подстрокой компьютер

select AUDITORIUM.AUDITORIUM_TYPE as [Код аудитории], AUDITORIUM_TYPE.AUDITORIUM_TYPENAME as [Наименование]
	from AUDITORIUM inner join AUDITORIUM_TYPE
	on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE and
	   AUDITORIUM_TYPE.AUDITORIUM_TYPENAME like '%компьютер%'

-- 3 студенты, получившие экзаменационные оценки от 6 до 8. Столбцы: факультет, кафедра, специальность, дисциплина, имя, студент, оценка
-- в столбце оценка должны быть записаны экзаменационные оценки прописью: шесть, семь, восемь
-- отсортировать в порядке убывания по столбцу PROGRESS.NOTE

use UNIVER;

select 
	FACULTY.FACULTY_NAME as [Факультет],
	PULPIT.PULPIT_NAME as [Кафедра],
	GROUPS.PROFESSION as [Специальность],
	SUBJECT.SUBJECT_NAME as [Дисциплина],
	STUDENT.NAME as [Имя студента],
	case
		when PROGRESS.NOTE = 6 then 'шесть'
		when PROGRESS.NOTE = 7 then 'семь'
		when PROGRESS.NOTE = 8 then 'восемь'
		else 'другая оценка'
	end as [Оценка]
from PROGRESS
inner join STUDENT on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
inner join GROUPS on STUDENT.IDGROUP = GROUPS.IDGROUP
inner join FACULTY on GROUPS.FACULTY = FACULTY.FACULTY
inner join SUBJECT on PROGRESS.SUBJECT = SUBJECT.SUBJECT
inner join PULPIT on SUBJECT.PULPIT = PULPIT.PULPIT
where PROGRESS.NOTE between 6 and 8
order by PROGRESS.NOTE desc;

-- 4 кафедры и преподаватели на этих кафедрах
-- кафедра, преподаватель, если преподавателя нет - ***

select PULPIT.PULPIT_NAME as [Кафедра], isnull (TEACHER.TEACHER_NAME, '***') as [Преподаватель]
	from PULPIT
left outer join TEACHER on PULPIT.PULPIT = TEACHER.PULPIT;

-- 5 

select 
	FOJ1.id as [ID первый],
	FOJ1.txt as [TXT первый],
	FOJ2.id as [ID второй],
	FOJ2.txt as [TXT второй]
from FOJ1 
full outer join FOJ2 on FOJ1.id = FOJ2.id
where FOJ2.id is null;

select 
	FOJ1.id as [ID первый],
	FOJ1.txt as [TXT первый],
	FOJ2.id as [ID второй],
	FOJ2.txt as [TXT второй]
from FOJ1
full outer join FOJ2 on FOJ1.id = FOJ2.id
where FOJ1.id is null;
	
select 
	FOJ1.id as [ID первый],
	FOJ1.txt as [TXT первый],
	FOJ2.id as [ID второй],
	FOJ2.txt as [TXT второй]
from FOJ1
full outer join FOJ2 on FOJ1.id = FOJ2.id
where FOJ1.id is not null and FOJ2.id is not null;

-- 6 коды аудиторий и соотвутствующих им наименований типов аудиторий

select AUDITORIUM.AUDITORIUM_TYPE, AUDITORIUM_TYPE.AUDITORIUM_TYPENAME
from AUDITORIUM
cross join AUDITORIUM_TYPE
where AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE