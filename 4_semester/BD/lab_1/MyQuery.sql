SELECT *
FROM заказы1
WHERE Дата_поставки > '2026-01-01';

SELECT *
FROM ТОВАРЫ
WHERE Цена BETWEEN 50 AND 200;

SELECT DISTINCT Заказчик
FROM заказы1
WHERE Наименование_товара = 'Стол';

SELECT *
FROM заказы1
WHERE Заказчик = 'ООО Ромашка'
ORDER BY Дата_поставки;