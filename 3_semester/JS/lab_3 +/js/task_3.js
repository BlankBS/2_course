function sortStudentsByGroup(students)
{
    let resultObj = {};

    students.forEach(student => {
        let{name, age, groupId} = student;
        if (age > 17) {
        if (resultObj[groupId]) {
          resultObj[groupId].push(student);
        } else {
          resultObj[groupId] = [student];
        }
      }
    });
    return resultObj;
}

let students = [
    {name: "Tema", age: 18, groupId: 1},
    {name: "Sasha", age: 19, groupId: 2},
    {name: "Dima", age: 16, groupId: 2},
    {name: "Polina", age: 15, groupId: 1},
    {name: "Roma", age: 18, groupId: 1}
];

let sortedStudents = sortStudentsByGroup(students);
console.log(sortedStudents);