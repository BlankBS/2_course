interface IArticle {
    title: string;
    pagesNumber: number;
}

interface IProfessor {
    name: string;
    degree: string;
    articles: IArticle[];
}

interface IExam {
    maths?: boolean;
    programming?: boolean;
    mark: number;
    professor: IProfessor;
}

interface IDepartment {
    faculty: string;
    group: number;
}

interface IStudies {
    universty: string;
    speciality: string;
    year: number;
    department: IDepartment;
    exams: IExam[];
}

interface IUser7 {
    name: string;
    age: number;
    studies: IStudies;
}


const user7: IUser7 = {
    name: 'Masha',
    age: 20,
    studies: {
        universty: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10
        },
        exams: [
            {
                maths: true,
                mark: 8,
                professor: {
                    name: 'Ivan Petrov',
                    degree: 'PhD',
                    articles: [
                        {title: "About HTML", pagesNumber: 3},
                        {title: "About CSS", pagesNumber: 5},
                        {title: "About JavaScript", pagesNumber: 1},
                    ]
                }
            },
            {
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Ivanov',
                    degree: 'PhD',
                    articles: [
                        {title: "About HTML", pagesNumber: 3},
                        {title: "About CSS", pagesNumber: 5},
                        {title: "About JavaScript", pagesNumber: 1},
                    ]
                }
            }
        ]
    }
};

let user7_: IUser7 = {
    ...user7,
    studies: {
        ...user7.studies,
        department:{
            ...user7.studies.department
        },
        exams: user7.studies.exams.map((elem: IExam): IExam => (
            {
                ...elem,
                professor: {
                    ...elem.professor,
                    articles: elem.professor.articles.map((item: IArticle): IArticle => (
                        {
                            ...item
                        }
                    ))
                }
            }
        ))
    }
};

user7_.studies.exams[1].professor.articles[1].pagesNumber = 3;

console.log(user7);
console.log(user7_);