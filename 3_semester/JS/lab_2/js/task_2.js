function SumOfCubes(n){
    let sum = 0;
    for (let i = 0; i < n; i++)
    {
        sum += i*i*i;
    }
    return sum;
}

console.log("SumOfCubes(4) = " + SumOfCubes(4));