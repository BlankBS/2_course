import { z } from 'zod';

export const RegistrationSchema = z.object({
    email: z.string().email("Некорректный формат email"),
    password: z.string().min(8, "Пароль должен быть минимум 8 символов"),
    
    username: z.string().min(1, "Имя не может быть пустым"),
    city: z.string().min(1, "Укажите город"),
    
    occupation: z.string().min(1, "Выберите профессию"),
    agree: z.boolean().refine(val => val === true, "Нужно ваше согласие")
});

export type IFormData = z.infer<typeof RegistrationSchema>;