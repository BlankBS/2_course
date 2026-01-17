let user5 = {
    name: 'Masha',
    age: 22,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10
        },
        exams: [
            {maths: true, mark: 8},
            {programming: true, mark: 4}
        ]
    }
};

let user5_ = {
    ...user5,
    studies:{
        ...user5.studies,
        department: {
            ...user5.studies.department,
            ...user5.studies.exams
        },
        exams: user5.studies.exams.map(elem => (
            {
                ...elem
            }
        ))
    }
};

user5_.studies.department.group = 12;
user5_.studies.exams[1].mark = 10;

console.log(user5);
console.log(user5_);