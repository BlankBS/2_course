function AddItems(items, id, name, amount, price)
{
    return items.set(id, {name, amount, price});
}

function DeleteItemByID(items, id)
{
    if(items.has(id))
    {
        console.log(`Товар №${id} успешно удален`);
        return items.delete(id);
    }

    else
    {
        console.log(`Товар ${id} не найден`);
    }
}

function DeleteItemByName(items, name)
{
    for(let item of items)
    {
        if(item[1].name == name)
        {
            items.delete(item[0]);
            console.log(`Товар "${item[1].name}" удален`);
        }
    }
    return items;
}

function ChangeItemsAmount(items, ...amount) {
    let i = 0;
    for(let item of items.values()) {
        if(i < amount.length) {
            item.amount = amount[i++];
        }
    }
}

function ChangeItemPrice(items, id, newPrice)
{
    for(let ptr of items.keys())
    {
        if(ptr == id)
        {
            items.get(ptr).price = newPrice;
            console.log(`Цена товара №${id} изменена`);
        }
    }
}

function GetItemPrices(items)
{
    let sum = 0;
    for(let item of items.values())
    {
        sum += item.price * item.amount; 
    }
    return sum;
}

function GetItemsAmount(items)
{
    return items.size;
}

let items = new Map();

AddItems(items, 1, "Bed", 5, 1000);
AddItems(items, 2, "chair", 15, 2000);
AddItems(items, 3, "table", 10, 3000);
AddItems(items, 4, "laptop", 3, 9999);
AddItems(items, 5, "bulb", 99, 11);

console.log(items);
console.log(`Количество различных товаров: ${GetItemsAmount(items)}`);

DeleteItemByID(items, 3);
console.log(items);

DeleteItemByName(items, "bulb");
console.log(items);

ChangeItemsAmount(items, 1, 2);
console.log(items);

ChangeItemPrice(items, 4, 8888);
console.log(items);

console.log(`Количество различных товаров: ${GetItemsAmount(items)}`);
console.log(`Товара на сумму: ${GetItemPrices(items)}`);