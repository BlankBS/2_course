let user6 = {
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

let user6_ = {
    ...user6,
    studies: {
        ...user6.studies,
        department: {
            ...user6.studies.department
        },
        exams: user6.studies.exams.map(elem => (
            {
                ...elem,
                professor: {
                    ...elem.professor,
                }
            }
        ))
    }
};

user6_.studies.exams[0].name = "BlankBS";
user6_.studies.exams[1].name = "blanketka";

console.log(user6);
console.log(user6_);