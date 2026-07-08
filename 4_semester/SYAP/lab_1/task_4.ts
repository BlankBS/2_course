interface IUserWithSkills {
    name: string;
    age: number;
    skills: string[]; 
}

let user2: IUserWithSkills = {
    name: 'Masha',
    age: 28,
    skills: ["HTML", "CSS", "JavaScript", "React"]
};

let user2_: IUserWithSkills = {
    ...user2,
    skills: [...user2.skills]
};

console.log(user2);
console.log(user2_);