let car = {
    mark: "Mercedes",
    model: "W464",
    year: 2024
};

Object.defineProperties(car, {
    mark:{
        writable: true,
        configurable: true
    },
    model:{
        writable: true,
        configurable: true
    },
    year:{
        writable: true,
        configurable: true
    }
});

console.log(car);

delete car.year;
console.log(car);

car.year = 2020;
console.log(car);

Object.freeze(car);
console.log("Все свойства теперь неизменяемы");

car.year = 2023;
console.log(car);

delete car.year;
console.log(car);