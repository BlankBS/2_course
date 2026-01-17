function AddStudent(student)
{
    StudentsList.add(student);
}

function IsAvailableStudent(number)
{
    for (const student of StudentsList)
    {
        if(number == student.number) return true;
    }
    return false;
}

function RemoveStudent(number)
{
    if(!IsAvailableStudent(number))
    {
        return;
    }

    for(const student of StudentsList)
    {
        if(student.number == number)
        {
            StudentsList.delete(student);
            return;
        }
    }
}

function FilterByGroup(students, group)
{
    let filtredStudents = new Set();
    for (let student of students)
    {
        if(student.group == group)
        {
            filtredStudents.add(student);
        }
    }
    return filtredStudents;
}

function SortByNumber(students)
{
    let arr = Array.from(students);
    students.clear();
    arr.sort((el1, el2) => el1.number > el2.number ? 1 : -1);
    for(const elem of arr)
    {
        students.add(elem);
    }
}

let StudentsList = new Set();

AddStudent({number: 123, group: 1, fio: "BlankBS"});
AddStudent({number: 567, group: 1, fio: "Xynmpy"});
AddStudent({number: 345, group: 2, fio: "p0lmandarina"});
AddStudent({number: 234, group: 2, fio: "lbdpln"});

let filter = FilterByGroup(StudentsList, 1);
console.log(filter);

RemoveStudent(345);
console.log(StudentsList);

SortByNumber(StudentsList);
console.log(StudentsList);