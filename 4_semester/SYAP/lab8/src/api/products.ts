// src/api/products.ts
import { z } from 'zod';

export const ProductSchema = z.object({
  id: z.number(),
  title: z.string()
    .trim() 
    .min(3, "Название должно содержать минимум 3 символа (не считая пробелы)"),
  price: z.number().positive(),
  category: z.string().optional(),
});

export type IProduct = z.infer<typeof ProductSchema>;

export const fetchProducts = async (category?: string): Promise<IProduct[]> => {
  const url = category 
    ? `https://dummyjson.com/products/category/${category}`
    : 'https://dummyjson.com/products?limit=10';
    
  const res = await fetch(url);
  const data = await res.json();
  
  // Внедряем валидацию ответов (Пункт 5 задания)
  try {
    return z.array(ProductSchema).parse(data.products);
  } catch (e) {
    console.error("Ошибка структуры данных", e);
    throw new Error("Ошибка структуры данных");
  }
};