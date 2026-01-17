let user = {
    name: "BlankBS",
    age: 18
}

let admin = {
    admin: true,
    ...user
}

console.log(admin);