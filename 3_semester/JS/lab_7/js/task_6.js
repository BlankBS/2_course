let item = {
    price: 100
}
console.log(`Первая цена: ${item.price}`);

item.price = 200;
console.log(`Вторая цена: ${item.price}`);

Object.defineProperty(item, "price", {
    writable: false,
    configurable: false
});

item.price = 300;
console.log(`Третья цена: ${item.price}`);

delete item.price;
console.log(`Цена при попытке удалить: ${item.price}`);