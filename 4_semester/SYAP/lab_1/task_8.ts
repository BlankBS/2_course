export {};

interface IProfessor {
    name: string;
    degree: string;
}

interface IExam {
    maths?: boolean;     
    programming?: boolean;
    mark: number;
    professor: IProfessor;
    name?: string;       
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

interface IUser6 {
    name: string;
    age: number;
    studies: IStudies;
}

const user6: IUser6 = {
    name: 'Masha',
    age: 21,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            { 
                maths: true,
                mark: 8,
                professor: {
                    name: 'Ivan Ivanov ',
                    degree: 'PhD'
                }
            },
            { 
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Petrov',
                    degree: 'PhD'
                }
            },
        ]
    }
};

let user6_: IUser6 = {
    ...user6,
    studies: {
        ...user6.studies,
        department: {
            ...user6.studies.department
        },
        exams: user6.studies.exams.map((elem: IExam): IExam => ({
            ...elem,
            professor: {
                ...elem.professor,
            }
        }))
    }
};

user6_.studies.exams[0].name = "BlankBS";
user6_.studies.exams[1].name = "blanketka";

console.log(user6);
console.log(user6_);