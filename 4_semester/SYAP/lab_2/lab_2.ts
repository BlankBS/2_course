enum ProductCategory {
    Electronics = "Электроника",
    Clothing = "Одежда",
    Books = "Книги",
    Food = "Еда",
    Other = "Другое"
}

class Product {
    constructor(
        public id: number,
        public name: string,
        public price: number,
        public description: string | null,
        public category: ProductCategory
    ) {}

    getInfo(): string {
        return `Товар: ${this.name}\n
                Цена: ${this.price}₽\n
                Категория: ${this.category}\n
                Описание: ${this.description || 'Нет описания'} \n`;
    }
}

type PropertyKeys = Exclude<keyof Product, 'getInfo'>;
type ProductProps = Pick<Product, PropertyKeys>;

type ProductWithoutId = Omit<ProductProps, 'id'>;

type ProductUpdate = Partial<ProductProps>;

class Catalog {
    private products: Product[] = [];

    constructor() {}

    addProduct(product: Product): void {
        this.products.push(product);
    }

    removeProduct(id: number): boolean {
        const index = this.products.findIndex(p => p.id === id);
        if (index !== -1) {
            this.products.splice(index, 1);
            return true;
        }
        return false;
    }

    getProductById(id: number): Product | undefined {
        return this.products.find(p => p.id === id);
    }

    getAllProducts(): Product[] {
        return [...this.products];
    }

    getProductsByCategory(category: ProductCategory): Product[] {
        return this.products.filter(p => p.category === category);
    }
}

class Order<T extends Product> {
    public totalPrice: number = 0;

    constructor(
        public id: number,
        public products: T[]
    ) {
        this.calculateTotalPrice();
    }

    calculateTotalPrice(): void {
        this.totalPrice = this.products.reduce((sum, product) => sum + product.price, 0);
    }

    getOrderInfo(): string {
        const productList = this.products.map(p => `${p.name} - ${p.price}₽`).join('\n');
        return `Заказ #${this.id}\nТовары:\n${productList}\nОбщая стоимость: ${this.totalPrice}₽`;
    }
}

type OrderSummary = Pick<Order<Product>, 'id' | 'totalPrice'>;

class Customer {
    constructor(
        public id: number,
        public name: string,
        public email: string
    ) {}

    getCustomerInfo(): string {
        return `Покупатель: ${this.name}\nEmail: ${this.email}`;
    }
}

class OrderManager {
    private orders: Order<Product>[] = [];

    constructor() {}

    createOrder(customer: Customer, products: Product[]): Order<Product> {
        const orderId = this.orders.length + 1;
        const newOrder = new Order(orderId, products);
        this.orders.push(newOrder);
        return newOrder;
    }

    getOrderById(id: number): Order<Product> | undefined {
        return this.orders.find(order => order.id === id);
    }

    getAllOrders(): Order<Product>[] {
        return [...this.orders];
    }

    getOrdersByCustomer(customerId: number): Order<Product>[] {
        return this.getAllOrders();
    }
}

function demo() {
    const catalog = new Catalog();

    const product1 = new Product(
        1, 
        "Ноутбук", 
        75000, 
        "Мощный ноутбук для работы и игр", 
        ProductCategory.Electronics
    );
    
    const product2 = new Product(
        2, 
        "Футболка", 
        1500, 
        "Хлопковая футболка", 
        ProductCategory.Clothing
    );
    
    const product3 = new Product(
        3, 
        "TypeScript Guide", 
        2000, 
        "Книга по TypeScript", 
        ProductCategory.Books
    );

    catalog.addProduct(product1);
    catalog.addProduct(product2);
    catalog.addProduct(product3);

    console.log("=== Все товары ===");
    catalog.getAllProducts().forEach(p => console.log(p.getInfo()));

    console.log("\n=== Товары в категории Электроника ===");
    catalog.getProductsByCategory(ProductCategory.Electronics).forEach(p => console.log(p.name));

    const customer = new Customer(1, "Иван Петров", "ivan@email.com");

    const orderManager = new OrderManager();

    const order = orderManager.createOrder(customer, [product1, product3]);
    
    console.log("\n=== Информация о заказе ===");
    console.log(order.getOrderInfo());

    console.log("\n=== Демонстрация утилитных типов ===");
    
    const productData: ProductWithoutId = {
        name: "Товар без id",
        price: 500,
        description: "бубубу",
        category: ProductCategory.Other
    };
    console.log("ProductWithoutId (Omit):", productData);

    const productUpdate: ProductUpdate = {
        name: "Обновленное название",
        price: 600
    };
    console.log("ProductUpdate (Partial):", productUpdate);

    const orderSummary: OrderSummary = {
        id: order.id,
        totalPrice: order.totalPrice
    };
    console.log("OrderSummary (Pick):", orderSummary);

    const newProduct = new Product(
        4,
        productData.name,
        productData.price,
        productData.description,
        productData.category
    );
    
    console.log("\n=== Созданный товар из ProductWithoutId ===");
    console.log(newProduct.getInfo());
}

demo();