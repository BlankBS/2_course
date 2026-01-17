let cache = new WeakMap();

function cashData(item)
{
    if(!cache.has(item))
    {
        let result = MultipleValue(item);
        cache.set(item, result);
        console.log("Данные в кэш успешно добавлены");

        return result;
    }

    console.log("Беру данные из кэша");
    return cache.get(item);
}

function MultipleValue(item)
{
    return item.value * 2;
}

let itemValue = {value: 29};

let result1 = cashData(itemValue);
console.log(result1);

let result2 = cashData(itemValue);
console.log(result2);

itemValue = null;