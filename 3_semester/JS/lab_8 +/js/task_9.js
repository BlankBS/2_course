let user7 = {
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

let user7_ = {
    ...user7,
    studies: {
        ...user7.studies,
        department:{
            ...user7.studies.department
        },
        exams: user7.studies.exams.map(elem => (
            {
                ...elem,
                professor: {
                    ...elem.professor,
                    articles: elem.professor.articles.map(item => (
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