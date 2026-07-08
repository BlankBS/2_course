-- переводить всех студентов определенной группы в другую

use UNIVER;

go
create proc ChangeGroup
	@fromGroupId int, @toGroupId int
as
begin
	update STUDENT set IDGROUP = @toGroupId where IDGROUP = @fromGroupId;
end;
go

select * from STUDENT where STUDENT.IDGROUP = 1 or STUDENT.IDGROUP = 2;
exec ChangeGroup @fromGroupId = 1, @toGroupId = 2;
select * from STUDENT where STUDENT.IDGROUP = 1 or STUDENT.IDGROUP = 2;
