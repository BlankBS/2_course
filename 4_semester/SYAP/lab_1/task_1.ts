interface IUser {
    name: string;
    age: number;
}

let user: IUser = {
    name: 'Masha',
    age: 21
}

let user_: IUser = { ...user };

console.log(user);
console.log(user_);