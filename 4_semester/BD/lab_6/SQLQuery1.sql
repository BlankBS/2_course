use UNIVER;

-- 1 для каждого типа аудиторий максимальную, минимальную, среднюю вместимость аудиторий, суммарную вместимость всех аудиторий и общее количество аудиторий данного типа
-- столбец с наименованием типа аудиторий с вычисленными столбцами
select AUDITORIUM_TYPE.AUDITORIUM_TYPENAME as [Тип аудитории],
	   max(AUDITORIUM.AUDITORIUM_CAPACITY) as [Максимальная вместимость],
	   min(AUDITORIUM.AUDITORIUM_CAPACITY) as [Минимальная вместимость],
	   AVG(cast(AUDITORIUM.AUDITORIUM_CAPACITY as float)) as [Средняя вместимость],
	   sum(AUDITORIUM.AUDITORIUM_CAPACITY) as [Суммарная вместимость],
	   count(*) as [Количество аудиторий]
from AUDITORIUM_TYPE inner join AUDITORIUM on AUDITORIUM_TYPE.AUDITORIUM_TYPE = AUDITORIUM.AUDITORIUM_TYPE
group by AUDITORIUM_TYPE.AUDITORIUM_TYPENAME

-- 3 значения экз оценок и их количество в заданном интервале, по возрастанию

select Интервал, Количество
from (select
		case 
			when PROGRESS.NOTE between 1 and 3 then N'1-3 (Некст раз повезет)'
			when PROGRESS.NOTE between 4 and 8 then N'5-8 (Хорошо)'
			when PROGRESS.NOTE between 9 and 10 then N'9-10 (Отлично)'
		end as Интервал,
		count(*) as Количество,
		avg(cast(PROGRESS.NOTE as float)) as [Сортированное значение]
	from PROGRESS
	group by 
			case 
				when PROGRESS.NOTE between 1 and 3 then N'1-3 (Некст раз повезет)'
				when PROGRESS.NOTE between 4 and 8 then N'5-8 (Хорошо)'
				when PROGRESS.NOTE between 9 and 10 then N'9-10 (Отлично)'
			end
	) as Подзапрос
	order by [Сортированное значение] desc;

-- 4 средняя экз оценка для каждого курса каждой специальности и факультета, по убыванию средней, два знака после запятой

select FACULTY.FACULTY_NAME as [Факультет],
	PROFESSION.PROFESSION_NAME as [Специальность],
	(2014 - GROUPS.YEAR_FIRST) as [Курс],
	round(avg(cast(PROGRESS.NOTE as float)), 2) as [Средний балл]
from FACULTY 
inner join GROUPS on FACULTY.FACULTY = GROUPS.FACULTY
inner join PROFESSION on GROUPS.PROFESSION = PROFESSION.PROFESSION
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
group by FACULTY.FACULTY_NAME, PROFESSION.PROFESSION_NAME, GROUPS.YEAR_FIRST
order by [Средний балл] desc;

-- 5 4 + оценки только по БД и ОАиП

select FACULTY.FACULTY_NAME as [Факультет],
	PROFESSION.PROFESSION_NAME as [Специальность],
	(2014 - GROUPS.YEAR_FIRST) as [Курс],
	round(avg(cast(PROGRESS.NOTE as float)), 2) as [Средний балл]
from FACULTY 
inner join GROUPS on FACULTY.FACULTY = GROUPS.FACULTY
inner join PROFESSION on GROUPS.PROFESSION = PROFESSION.PROFESSION
inner join STUDENT on GROUPS.IDGROUP = STUDENT.IDGROUP
inner join PROGRESS on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
where PROGRESS.SUBJECT = N'ОАиП' or PROGRESS.SUBJECT = N'БД'
group by FACULTY.FACULTY_NAME, PROFESSION.PROFESSION_NAME, GROUPS.YEAR_FIRST
order by [Средний балл] desc;

-- 6 специальность, дисциплины, средние оценки на фтТОВ

select GROUPS.PROFESSION [Специальность],
	   PROGRESS.SUBJECT as [Дисциплина],
	   avg(cast(PROGRESS.NOTE as float)) as [Средний балл]
from FACULTY
inner join GROUPS on FACULTY.FACULTY = GROUPS.FACULTY
inner join STUDENT on STUDENT.IDGROUP = GROUPS.IDGROUP
inner join PROGRESS on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
where FACULTY.FACULTY = N'ТОВ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT;

-- 7 для каждой дисциплины, количество студентов с отметкой 8 и 9

select PROGRESS.SUBJECT as [Дисциплина],
	   count(PROGRESS.IDSTUDENT) as [Количество студентов]
from PROGRESS
where PROGRESS.NOTE in (8, 9)
group by PROGRESS.SUBJECT
having count(PROGRESS.IDSTUDENT) > 0
order by [Количество студентов] desc;