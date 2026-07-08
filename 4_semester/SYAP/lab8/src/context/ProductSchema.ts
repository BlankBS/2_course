import { z } from 'zod';

export const ProductSchema = z.object({
  id: z.number(),
  title: z.string().min(3, "Название: минимум 3 символа"),
  price: z.number().positive("Цена должна быть больше 0"),
});

export type IProduct = z.infer<typeof ProductSchema>;