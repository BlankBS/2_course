export {};

interface IExams {
    maths: boolean;
    programming: boolean;
}

interface IStudies {
    university: string;
    speciality: string;
    year: number;
    exams: IExams;
}

interface IUser4 {
    name: string;
    age: number;
    studies: IStudies; 
}

let user4: IUser4 = {
    name: 'Masha',
    age: 19,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        exams: {
            maths: true,
            programming: false
        }
    }
};

let user4_: IUser4 = {
    ...user4,
    studies: {
        ...user4.studies,
        exams: {
            ...user4.studies.exams
        }
    }
};

console.log(user4);
console.log(user4_);