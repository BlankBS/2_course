let person = {
    name: "BlankBS",
    age: 18,

    greet(){
        return "Привет " + this.name;
    },

    ageAfterYears(years){
        return "Возраст: " + (this.age + years);
    }
};

console.log(person.greet());
console.log(person.ageAfterYears(5));