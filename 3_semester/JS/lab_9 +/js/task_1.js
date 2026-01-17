let yellowSquare = {
    color: "yellow",
    getColor() {
        return this.color;
    }
};

function Square(size){
    this.size = size;
}

Square.prototype = Object.create(yellowSquare);

let largeSquare = new Square(500);
let smallSquare = new Square(100);

smallSquare.name = "Маленький квадрат";
console.log(Object.getPrototypeOf(smallSquare));
console.log(smallSquare.name);

// -----------------------------------------------

let middleCircle = {
    size: 300
};

function Circle(newColor) {
    this.color = newColor;
    this.GetColor = () => this.color;
}

Circle.prototype = Object.create(middleCircle);

let whiteCircle = new Circle("white");
let greenCircle = new Circle("green");

console.log(whiteCircle.size);

// -----------------------------------------------

let middleTriangle = {
    size: 300
};

function Triangle(newLinesAmount) {
    this.linesAmount = newLinesAmount;

    this.GetLinesAmount = () => this.linesAmount;
}

Triangle.prototype = Object.create(middleTriangle);

let triangle1 = new Triangle(1);
let triangle3 = new Triangle(3);

console.log(triangle3.size);

// -----------------------------------------------

console.log("Свойства: ");
console.log("smallSquare.hasOwnProperty('name') = " + smallSquare.hasOwnProperty("name"));
console.log("greenCircle.GetColor() = " + greenCircle.GetColor());
console.log("triangle3.GetLinesAmount() = " + triangle3.GetLinesAmount());