let numbers = [1, 2, 3];

Object.defineProperty(numbers, "sum", {
    get: function()
    {
        let sum = 0;

        for(let elem of numbers)
        {
            sum += elem;
        }
        return sum;
    },
    configurable: false
});

console.log(`Sum = ${numbers.sum}`);