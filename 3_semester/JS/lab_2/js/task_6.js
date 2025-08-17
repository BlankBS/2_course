function compare(arr1, arr2)
{
    const arr3 = arr1.filter(n => !arr2.includes(n));
    return arr3;
}

let myArr1 = ["abc", "123", "qwe", "---"];
let myArr2 = ["asd", "abc", "123", "456", "789"];

console.log(compare(myArr1, myArr2));