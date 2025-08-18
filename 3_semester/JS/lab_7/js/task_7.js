let circle = {
    radius: 10,

    get Square()
    {
        return "Площадь круга: " + (Math.PI * this.radius * this.radius);
    },

    get changeRadius()
    {
        return "Радиус круга: " + this.radius;
    },

    set changeRadius(newRadius)
    {
        this.radius = newRadius;
    }
};

console.log(circle.changeRadius);
console.log(circle.Square);

circle.changeRadius = 20;
console.log(circle.changeRadius);
console.log(circle.Square);