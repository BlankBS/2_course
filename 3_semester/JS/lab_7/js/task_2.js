let car = {
    model: "Mercedes",
    year: 2024,

    getInfo(){
        return "Марка: " + this.model + "\nгод выпуска: " + this.year;
    }
};

console.log(car.getInfo());