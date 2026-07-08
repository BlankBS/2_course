use UNIVER;

-- 1 специальность, дисциплины и средние оценки при сдаче экзамена на факультете ТОВ
-- группировка по полям FACULTY, PROFESSION, SUBJECT
-- + roolup

select FACULTY.FACULTY, GROUPS.PROFESSION, PROGRESS.SUBJECT, 
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from FACULTY
inner join GROUPS on FACULTY.FACULTY = GROUPS.FACULTY
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
where FACULTY.FACULTY = N'ТОВ'
group by rollup (FACULTY.FACULTY, GROUPS.PROFESSION, PROGRESS.SUBJECT);

-- 2 + cube

select FACULTY.FACULTY, GROUPS.PROFESSION, PROGRESS.SUBJECT, 
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from FACULTY
inner join GROUPS on FACULTY.FACULTY = GROUPS.FACULTY
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
where FACULTY.FACULTY = N'ТОВ'
group by cube (FACULTY.FACULTY, GROUPS.PROFESSION, PROGRESS.SUBJECT);

-- 3 результаты сдачи экзаменов, специальности, дисциплины, средние оценки студентов на факультете ТОВ
-- результаты сдачи экзаменов на факультете ХТиТ
-- объединить результат двух запросов с использованием UNION + UNION ALL

select GROUPS.PROFESSION, PROGRESS.SUBJECT, 
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from GROUPS
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
where GROUPS.FACULTY = N'ТОВ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT

union all

select GROUPS.PROFESSION, PROGRESS.SUBJECT,
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from GROUPS
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
where GROUPS.FACULTY = N'ХТиТ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT;

-- 4 intersect

select GROUPS.PROFESSION, PROGRESS.SUBJECT, 
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from GROUPS
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
where GROUPS.FACULTY = N'ТОВ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT

intersect

select GROUPS.PROFESSION, PROGRESS.SUBJECT,
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from GROUPS
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
where GROUPS.FACULTY = N'ХТиТ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT;

-- 5

select GROUPS.PROFESSION, PROGRESS.SUBJECT, 
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from GROUPS
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
where GROUPS.FACULTY = N'ТОВ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT

except

select GROUPS.PROFESSION, PROGRESS.SUBJECT,
	avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from GROUPS
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
where GROUPS.FACULTY = N'ХТиТ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT;