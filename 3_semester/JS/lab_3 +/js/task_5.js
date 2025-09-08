function assignObjects(...args)
{
    let assignedObjects = Object.assign({}, ...args);
    return assignedObjects;
}

let obj1 = {a: 1, b: 1};
let obj2 = {b: 2};
let obj3 = {c: 3};
let obj4 = {d: 4, c: 2};

console.log(assignObjects(obj1, obj2, obj3, obj4));