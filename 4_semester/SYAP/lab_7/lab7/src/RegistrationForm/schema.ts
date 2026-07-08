import { z } from 'zod';

// Описываем схему всех полей формы
export const RegistrationSchema = z.object({
    // Шаг 1
    email: z.string().email("Некорректный формат email"),
    password: z.string().min(8, "Пароль должен быть минимум 8 символов"),
    
    // Шаг 2
    username: z.string().min(1, "Имя не может быть пустым"),
    city: z.string().min(1, "Укажите город"),
    
    // Шаг 3
    occupation: z.string().min(1, "Выберите профессию"),
    agree: z.boolean().refine(val => val === true, "Нужно ваше согласие")
});

// Автоматически создаем тип данных на основе схемы Zod
export type IFormData = z.infer<typeof RegistrationSchema>;