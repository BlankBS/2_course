interface ILocation {
    city: string;
    country: string;
}

interface IUserWithLocation{
    name: string;
    age: number;
    location: ILocation;
}

let user1: IUserWithLocation = {
    name: 'Masha',
    age: 23,
    location: {
        city: 'Minsk',
        country: 'Belarus'
    }
}

let user1_: IUserWithLocation = {
    ...user1,
    location: {
        ...user1.location
    }
};

console.log(user1);
console.log(user1_);