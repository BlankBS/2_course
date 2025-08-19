class Human {
    constructor(name, lastName, year, age, adress) {
        this.name = name;
        this.lastName = lastName;
        this.birthYear = year;
        this.age = age;
        this.adress = adress;
    }

    get currentAge() {
        const currentYear = new Date().getFullYear();
        return currentYear - this.birthYear;
    }

    set changeAge(newAge) {
        if (newAge < 0) {
            console.log("Возраст должен быть натуральным числом");
            return;
        }
        
        this.age = newAge;
        const currentYear = new Date().getFullYear();
        this.birthYear = currentYear - this.age;
    }
};

class Student extends Human {
    constructor(name, lastName, year, age, adress, faculty, course, group, number){
        super(name, lastName, year, age, adress);
        this.faculty = faculty;
        this.course = course;
        this.group = group;
        this.number = number;        
    };

    ChangeCourse(newCourse) {
        if (newCourse <= 0)
        {
            console.log("Курс должен быть натуральным числом");
        }

        this.course = newCourse;
    }

    ChangeGroup(newGroup){
        if(newGroup <= 0) {
            console.log("Группа должна быть натуральным числом");
        }

        this.group = newGroup;
    }

    GetFullName() {
        return this.name + ' ' + this.lastName;
    }

    DecodeGradeBook() {
        const strNumber = this.number.toString();

        return {
            nFaculty: strNumber[0],
            nSpeciality: strNumber[1],
            YearAttended: 2000 + parseInt(strNumber.slice(2, 4)),
            isFree: strNumber[4],
            uniqueNumber: strNumber.slice(5),
        };
    }
};

class Faculty {
    constructor(facultyName, groupsAmount, studentsAmount) {
        this.facultyName = facultyName;
        this.groupsAmount = groupsAmount;
        this.studentsAmount = studentsAmount;
    };

    ChangeGroupsAmount(newGroupsAmount) {
        if (newGroupsAmount < 0)
        {
            console.log("Количество групп должно быть положительным числом");
            return;
        }

        this.groupsAmount = newGroupsAmount;        
    }

    ChangeStudentsAmount(newStudentsAmount) {
        if (newStudentsAmount < 0) 
        {
            console.log("Количество студентов должно быть положительным числом");
            return;
        }

        this.studentsAmount = newStudentsAmount;
    }

    getDev() {
        const filtredStudents = this.studentsAmount.filter(student => student.DecodeGradeBook().nSpeciality == 3);
        console.log(`Количество студентов специальности ДЭиВИ: ${filtredStudents.length}`);
    }

    getGroup(group) {
        const filteredStudents = this.studentsAmount.filter((student) => student.group === group);
        console.log(`Студенты группы ${group}: `);
        console.log(filteredStudents.forEach(student => { console.log(student.GetFullName()) }));
    }
};

let student1 = new Student("Blank", "BS", "24", 18, "Minsk", "IT", 2, 8, 71242000);
let student2 = new Student("Xynmpy", "BS", "23", 19, "Minsk", "IT", 1, 8, 73202137);
let student3 = new Student("p0lmandarina", "BS", "21", 17, "Minsk", "IT", 1, 7, 71202456);
let student4 = new Student("lbdpln", "BS", "25", 15, "Minsk", "IT", 2, 6, 73201789);
let student5 = new Student("tctdgjc", "BS", "24", 18, "Minsk", "IT", 2, 9, 73201119);

const ITFaculty = new Faculty("IT", 10, [student1, student2, student3, student4, student5]);
ITFaculty.getDev();
ITFaculty.getGroup(8);