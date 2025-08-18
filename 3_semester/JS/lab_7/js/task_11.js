let user = {
    firstName: "Blank",
    lastName: "BS",

    get fullName()
    {
        return "Полное имя: " + this.firstName + ' ' + this.lastName;
    },
    set fullName(newName)
    {
        return [this.firstName, this.lastName] = newName.split(" ");
    }
};

console.log(user.fullName);
user.fullName = "blanketka bska";
console.log(user.fullName);