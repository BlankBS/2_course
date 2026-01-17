let user1 = {
    name: 'Masha',
    age: 23,
    location: {
        city: 'Minsk',
        country: 'Belarus'
    }
};

let user1_ = {
    ...user1,
    location: {
        ...user1.location
    }
};

console.log(user1);
console.log(user1_);