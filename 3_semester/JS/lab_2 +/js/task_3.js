function arithmArr(arr){
    let sum = 0;
    for (let i = 0; i < arr.length; i++)
    {
        sum += arr[i];
    }
    let arithm = sum / arr.length;
    return arithm;
}

let numbers = [1, 2, 3, 4, 5, 6, 7];
console.log("arithmArr([1, 2, 3, 4, 5, 6, 7]) = " + arithmArr(numbers)); 