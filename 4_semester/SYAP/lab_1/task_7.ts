export {};

interface IExam {
    maths?: boolean;
    programming?: boolean;
    mark: number;
}

interface IDepartment {
    faculty: string;
    group: number;
}

interface IStudies {
    university: string;
    speciality: string;
    year: number;
    department: IDepartment;
    exams: IExam[];
}

interface IUser5 {
    name: string;
    age: number;
    studies: IStudies;
}

const user5: IUser5 = {
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
            { maths: true, mark: 8 },
            { programming: true, mark: 4 }
        ]
    }
};

let user5_: IUser5 = {
    ...user5,
    studies: {
        ...user5.studies,
        department: {
            ...user5.studies.department,
        },
        exams: user5.studies.exams.map((elem: IExam): IExam => ({
            ...elem
        }))
    }
};

user5_.studies.department.group = 12;
user5_.studies.exams[1].mark = 10;

console.log(user5);
console.log(user5_);