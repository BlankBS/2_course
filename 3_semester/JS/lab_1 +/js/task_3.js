let i = 2;
let a = ++i;
let b = i++;

console.log("a = " + a);
console.log("b = " + b);

if(a > b)
    console.log("a > b")
else if(a < b)
    console.log("a < b")
else
    console.log("a = b")