let rectangle = {
    width: 10,
    height: 5,

    get Square()
    {
        return "Площадь прямоугольника: " + (this.height * this.width);
    },

    get widthRectangle()
    {
        return "Ширина прямоугольника: " + this.width;
    },

    set widthRectangle(newWidth)
    {
        this.width = newWidth;
    },

    get heightRectangle()
    {
        return "Длина прямоугольника: " + this.height;
    },
    
    set heightRectangle(newHeight)
    {
        this.height = newHeight;
    }
};

console.log(rectangle.widthRectangle);
console.log(rectangle.heightRectangle);
console.log(rectangle.Square);

rectangle.widthRectangle = 20;
rectangle.heightRectangle = 30;

console.log(rectangle.widthRectangle);
console.log(rectangle.heightRectangle);
console.log(rectangle.Square);