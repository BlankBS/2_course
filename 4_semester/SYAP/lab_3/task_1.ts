const checkStock = (item: string) => {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            const isAvailable = true;
            isAvailable 
                ? resolve({ item, price: 600 }) 
                : reject("Товара нет в наличии");
        }, 1000);
    });
};

const processPayment = (order: any) => {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            order.price >= 500 
                ? resolve("Оплата прошла успешно") 
                : reject("Недостаточно средств");
        }, 2000);
    });
};

const deliverOrder = () => {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve("Заказ доставлен!");
        }, 1500);
    });
};

checkStock("Пицца")
    .then((order) => {
        console.log("Проверка склада...");
        return processPayment(order);
    })
    .then((paymentStatus) => {
        console.log(paymentStatus);
        return deliverOrder();
    })
    .then((deliveryStatus) => {
        console.log(deliveryStatus);
    })
    .catch((error) => {
        console.error("Ошибка:", error);
    })
    .finally(() => {
        console.log("Спасибо за заказ, приходите еще!");
    });

const fetchFast = () => new Promise(res => setTimeout(() => res("Fast Result"), 500));
const fetchSlow = () => new Promise(res => setTimeout(() => res("Slow Result"), 2000));

Promise.race([fetchFast(), fetchSlow()])
    .then(result => console.log("Победил:", result));


const p1 = Promise.resolve("Success 1");
const p2 = Promise.reject("Error 1");
const p3 = Promise.resolve("Success 2");
const p4 = Promise.reject("Error 2");
const p5 = Promise.resolve("Success 3");

Promise.allSettled([p1, p2, p3, p4, p5])
    .then((results) => {
        const successful = results
            .filter(res => res.status === 'fulfilled')
            .map(res => res.value);
        
        console.log("Успешные операции:", successful);
    });

async function getData() {
    try {
        const response = await fetch('https://api.example.com/data');
        
        if (!response.ok) {
            throw new Error(`Ошибка сети: ${response.status}`);
        }
        
        const data = await response.json();
        console.log(data);
    } catch (err) {
        console.error("Ошибка:", err);
    }
}

async function limitRequests(urls: string[], limit: number) {
    const results = [];
    for (let i = 0; i < urls.length; i += limit) {
        const chunk = urls.slice(i, i + limit);
        console.log(`Загрузка пачки: ${chunk}`);
        const responses = await Promise.all(chunk.map(url => fetch(url).catch(e => e))); 
        results.push(...responses);
    }
    return results;
}