import { createFileRoute } from '@tanstack/react-router'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { useState, useEffect } from 'react'
import { ProductSchema } from '../api/products'

export const Route = createFileRoute('/product/$id')({
  component: ProductDetails,
})

function ProductDetails() {
  const { id } = Route.useParams()
  const queryClient = useQueryClient()
  const [isEditing, setIsEditing] = useState(false)
  
  const [title, setTitle] = useState('')
  const [price, setPrice] = useState(0)

  // 1. Загрузка данных товара
  const { data: product, isLoading, isError } = useQuery({
  queryKey: ['product', id],
  queryFn: async () => {
    try {
      const res = await fetch(`https://dummyjson.com/products/${id}`);
      
      if (!res.ok) {
        // Если сервер ответил 404, ищем товар в кэше КАТАЛОГА
        const catalogData = queryClient.getQueryData(['products', undefined]);
        
        // Пытаемся найти товар в массиве по ID
        const localProduct = (catalogData as any[])?.find(
          (p: any) => p.id.toString() === id.toString()
        );

        if (localProduct) {
          console.log("Товар найден в локальном кэше");
          return localProduct;
        }
        
        throw new Error("Товар не найден ни на сервере, ни в кэше");
      }

      const data = await res.json();
      return ProductSchema.parse(data);
    } catch (err) {
      // На случай полной ошибки сервера, пробуем еще раз поискать локально
      const catalogData = queryClient.getQueryData(['products', undefined]);
      const localProduct = (catalogData as any[])?.find(
        (p: any) => p.id.toString() === id.toString()
      );
      if (localProduct) return localProduct;
      
      throw err;
    }
  },
  staleTime: 1000 * 60 * 10,
});

  // Синхронизация полей формы с данными из кэша
  useEffect(() => {
    if (product) {
      setTitle(product.title)
      setPrice(product.price)
    }
  }, [product])

  // 2. Мутация изменения (UPDATE)
  const updateMutation = useMutation({
    mutationFn: async (updatedData: { title: string; price: number }) => {
      const res = await fetch(`https://dummyjson.com/products/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updatedData),
      });
      return res.json();
    },
    onSuccess: (dataFromServer) => {
      // Создаем объект с обновленными данными
      // Используем данные, которые мы сами отправили, так как сервер DummyJSON не сохраняет их
      const finalData = { ...product, title, price };

      // Обновляем кэш конкретного товара (этой страницы)
      queryClient.setQueryData(['product', id], finalData);

      // Обновляем кэш всех списков (каталога)
      queryClient.setQueriesData({ queryKey: ['products'] }, (oldData: any) => {
        if (!oldData) return oldData;
        // Если это массив товаров
        if (Array.isArray(oldData)) {
          return oldData.map((p: any) => 
            p.id.toString() === id.toString() ? finalData : p
          );
        }
        return oldData;
      });

      setIsEditing(false);
    },
  });

  const handleSave = () => {
    const result = ProductSchema.safeParse({
      id: Number(id),
      title: title,
      price: price
    });

    if (!result.success) {
      const error = result.error.flatten().fieldErrors.title?.[0];
      alert(error || "Ошибка валидации");
      return; 
    }

    // Вызываем мутацию
    updateMutation.mutate(result.data);
  };

  if (isLoading) return <div style={{ padding: '40px' }}>Загрузка...</div>
  if (isError || !product) return <div style={{ padding: '40px' }}>Ошибка загрузки или товар не найден</div>

  return (
    <div style={{ padding: '40px', maxWidth: '800px' }}>
      <button 
        onClick={() => window.history.back()} 
        style={{ 
            display: 'inline-flex',
            alignItems: 'center',
            gap: '8px',
            marginBottom: '32px', 
            padding: '10px 20px', 
            background: '#fff', 
            color: '#64748b', 
            border: '1px solid #e2e8f0', 
            borderRadius: '12px', 
            fontSize: '14px',
            fontWeight: 600,
            cursor: 'pointer'
        }}
        >
        <span>←</span> Назад в каталог
      </button>

      {isEditing ? (
        <div style={{ background: '#fff', padding: '30px', borderRadius: '16px', boxShadow: '0 4px 12px rgba(0,0,0,0.1)' }}>
          <h2 style={{ marginBottom: '20px' }}>Редактирование</h2>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
            <input 
              style={{ padding: '10px' }}
              value={title} 
              onChange={(e) => setTitle(e.target.value)} 
            />
            <input 
              type="number" 
              style={{ padding: '10px' }}
              value={price} 
              onChange={(e) => setPrice(Number(e.target.value))} 
            />
            <div style={{ display: 'flex', gap: '10px' }}>
              <button 
                onClick={handleSave} 
                disabled={updateMutation.isPending}
                style={{ padding: '10px 20px', background: '#3b82f6', color: '#fff', border: 'none', borderRadius: '8px' }}
              >
                {updateMutation.isPending ? 'Сохранение...' : 'Сохранить'}
              </button>
              <button onClick={() => setIsEditing(false)}>Отмена</button>
            </div>
          </div>
        </div>
      ) : (
        <div>
          <h1 style={{ fontSize: '42px', marginBottom: '16px', color: '#1e293b' }}>
            {product.title}
          </h1>
          <div style={{ fontSize: '32px', fontWeight: 800, color: '#0f172a', marginBottom: '30px' }}>
            Цена: {product.price} $
          </div>
          <button 
            onClick={() => setIsEditing(true)}
            style={{ padding: '12px 30px', background: '#3b82f6', color: '#fff', border: 'none', borderRadius: '10px', fontWeight: 600 }}
          >
            Изменить данные
          </button>
        </div>
      )}
    </div>
  )
}