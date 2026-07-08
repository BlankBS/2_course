-- 4b read commited

begin transaction
	insert into AUDITORIUM (AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY) values
		('500-5', '500-5', 'ЛК', 30);
	waitfor delay '00:00:05';
rollback;

-- 5b

begin transaction
	update AUDITORIUM set AUDITORIUM_CAPACITY = 207 where AUDITORIUM = '236-1';
commit;

-- 6b

begin transaction 
	update AUDITORIUM set AUDITORIUM_CAPACITY = 251 where AUDITORIUM = '206-1';

	insert into AUDITORIUM (AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY) values 
		('777-7', '777-7', 'ЛК', 205);
commit;
delete AUDITORIUM where AUDITORIUM = '777-7';

-- 7b

begin transaction
	insert into AUDITORIUM (AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_TYPE, AUDITORIUM_CAPACITY) values 
		('888-8', '888-8', 'ЛК', 205);
commit;
delete AUDITORIUM where AUDITORIUM = '888-8';

print 'Уровень вложенности: ' + cast(@@TRANCOUNT as varchar);