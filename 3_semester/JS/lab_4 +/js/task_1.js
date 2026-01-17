function addItem(set, item)
{
    console.log(`${item} успешно добавлен`);
    set.add(item);
}

function removeItem(set, item)
{
    if(set.has(item))
    {
        console.log(`${item} успешно удален`);
        set.delete(item);
    }
    else
    {
        console.log(`${item} не найден`);
    }
}

function IsAvailableItem(set, item)
{
    if(set.has(item))
    {
        console.log(`${item} есть в наличии`);
    }
    else
    {
        console.log(`${item} закончился`);
    }
}

let items = ["mouse", "keyboard", "microphone"];
let set = new Set(items);

console.log(set);
addItem(set, "headphones");
removeItem(set, "mouse");
IsAvailableItem(set, "mouse");
IsAvailableItem(set, "keyboard");
console.log(`Количество товара ${set.size}`);
console.log(set);