function basicOperation(operation, value1, value2){
    let result;
    switch(operation){
        case("+"): result = value1 + value2; break;
        case("-"): result = value1 - value2; break;
        case("/"): result = value1 / value2; break;
        case("*"): result = value1 * value2; break;
        default: alert("Введиет верную операцию!")
    }
    return result;
}

console.log("basicOperation('+', 1, 2) = " + basicOperation("+", 1, 2));
console.log("basicOperation('-', 3, 2) = " + basicOperation("-", 3, 2));
console.log("basicOperation('/', 6, 2) = " + basicOperation("/", 6, 2));
console.log("basicOperation('*', 5, 2) = " + basicOperation("*", 5, 2));