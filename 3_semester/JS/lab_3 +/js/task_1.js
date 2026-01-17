let arr = [1, [1, 2, [3, 4]], [2, 4]];

const flattenArray = (array) => {
    return array.reduce((acc, current) => {
        return acc.concat(Array.isArray(current) ? flattenArray(current) : current);
    }, []);
};

console.log("flattenArray([1, [1, 2, [3, 4]], [2, 4]]) = " + flattenArray([1, [1, 2, [3, 4]], [2, 4]]));