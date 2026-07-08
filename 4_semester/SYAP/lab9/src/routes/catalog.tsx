import { createFileRoute, redirect, Link } from '@tanstack/react-router'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { z } from 'zod'
import { useState } from 'react'
import { fetchProducts, ProductSchema } from '../api/products'
import type { IProduct } from '../api/products'
import { Badge } from '../components/Badge'

// Импорты Recoil
import { useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';
import { uiSettingsState, favoritesState, cartState } from '../store/atoms';

export const Route = createFileRoute('/catalog')({
  validateSearch: (search) => z.object({ category: z.string().optional() }).parse(search),
  beforeLoad: ({ context }) => {
    if (!context.auth.isAuthenticated) throw redirect({ to: '/login' })
  },
  component: CatalogPage,
})

function CatalogPage() {
  const { category } = Route.useSearch()
  const queryClient = useQueryClient()

  // --- 1. СОСТОЯНИЯ RECOIL (UI) ---
  const [uiSettings, setUiSettings] = useRecoilState(uiSettingsState);
  const [favorites, setFavorites] = useRecoilState(favoritesState);
  const setCart = useSetRecoilState(cartState);

  // --- 2. ЛОКАЛЬНЫЕ СОСТОЯНИЯ ФОРМЫ ---
  const [isAdding, setIsAdding] = useState(false)
  const [newTitle, setNewTitle] = useState('')
  const [newPrice, setNewPrice] = useState('')

  // --- 3. ЗАГРУЗКА ДАННЫХ (QUERY) ---
  const { data: products, isLoading, isError } = useQuery({
    queryKey: ['products', category],
    queryFn: () => fetchProducts(category),
    staleTime: 1000 * 60 * 10, 
  })

  // --- 4. МУТАЦИИ (СЕРВЕРНАЯ ЛОГИКА) ---
  const addMutation = useMutation({
    mutationFn: async (newProd: any) => {
      const res = await fetch('https://dummyjson.com/products/add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newProd),
      })
      return res.json()
    },
    onSuccess: (createdProduct) => {
      const uniqueProduct = { ...createdProduct, id: Date.now() };
      queryClient.setQueryData(['products', category], (old: any) => [uniqueProduct, ...(old || [])])
      setNewTitle(''); setNewPrice(''); setIsAdding(false);
    }
  })

  // Внутри CatalogPage найди deleteMutation и обнови её:

const deleteMutation = useMutation({
  mutationFn: (id: number) => fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' }),
  onMutate: async (id) => {
    // 1. Стандартная логика TanStack Query (отмена и бэкап)
    await queryClient.cancelQueries({ queryKey: ['products', category] })
    const previous = queryClient.getQueryData(['products', category])
    queryClient.setQueryData(['products', category], (old: any) => old?.filter((p: any) => p.id !== id))

    // --- ВОТ ЗДЕСЬ ДОБАВЛЯЕМ СИНХРОНИЗАЦИЮ С RECOIL ---
    
    // 2. Удаляем товар из корзины Recoil (чтобы счетчик уменьшился)
    setCart((prev) => prev.filter(item => item.id !== id));

    // 3. Удаляем товар из избранного Recoil
    setFavorites((prev) => prev.filter(favId => favId !== id));

    return { previous }
  },
  onError: (_err, _id, context) => {
    queryClient.setQueryData(['products', category], context?.previous)
  }
})

  // --- 5. ФУНКЦИИ RECOIL (КЛИЕНТСКАЯ БИЗНЕС-ЛОГИКА) ---
  const toggleFavorite = (id: number) => {
    setFavorites(prev => prev.includes(id) ? prev.filter(f => f !== id) : [...prev, id]);
  };

  const addToCart = (id: number) => {
    setCart(prev => {
      const existing = prev.find(item => item.id === id);
      if (existing) return prev.map(item => item.id === id ? { ...item, quantity: item.quantity + 1 } : item);
      return [...prev, { id, quantity: 1 }];
    });
  };

  const handleAddSubmit = () => {
    const result = ProductSchema.safeParse({ id: Date.now(), title: newTitle, price: Number(newPrice) })
    if (!result.success) return alert("Ошибка валидации");
    addMutation.mutate(result.data)
  }

  if (isLoading) return <div style={{ padding: '40px' }}>Загрузка...</div>

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '32px' }}>
        <h2 style={{ color: 'var(--text-primary)' }}>Каталог</h2>
        <div style={{ display: 'flex', gap: '10px' }}>
          <div style={{ background: 'var(--border-color)', padding: '4px', borderRadius: '10px', display: 'flex', gap: '4px' }}>
            <button onClick={() => setUiSettings(s => ({...s, viewMode: 'grid'}))} style={{ border: 'none', background: uiSettings.viewMode === 'grid' ? 'var(--card-bg)' : 'transparent', color: 'var(--text-primary)', padding: '8px 12px', borderRadius: '6px', cursor: 'pointer' }}>Сетка</button>
            <button onClick={() => setUiSettings(s => ({...s, viewMode: 'list'}))} style={{ border: 'none', background: uiSettings.viewMode === 'list' ? 'var(--card-bg)' : 'transparent', color: 'var(--text-primary)', padding: '8px 12px', borderRadius: '6px', cursor: 'pointer' }}>Список</button>
          </div>
          <button onClick={() => setIsAdding(!isAdding)} style={{ padding: '10px 20px', background: 'var(--accent-color)', color: 'white', border: 'none', borderRadius: '10px', cursor: 'pointer' }}>{isAdding ? 'Отмена' : '+ Добавить'}</button>
        </div>
      </div>

      {/* ФОРМА ДОБАВЛЕНИЯ - ТЕПЕРЬ С ТЕМОЙ */}
      {isAdding && (
        <div style={{ background: 'var(--card-bg)', color: 'var(--text-primary)', border: '2px dashed var(--border-color)', padding: '24px', borderRadius: '16px', marginBottom: '32px', display: 'grid', gridTemplateColumns: '3fr 1fr auto', gap: '20px', alignItems: 'end' }}>
          <input value={newTitle} onChange={e => setNewTitle(e.target.value)} placeholder="Название" style={{ background: 'var(--input-bg)', color: 'var(--text-primary)', border: '1px solid var(--border-color)', padding: '12px', borderRadius: '8px' }} />
          <input type="number" value={newPrice} onChange={e => setNewPrice(e.target.value)} placeholder="Цена" style={{ background: 'var(--input-bg)', color: 'var(--text-primary)', border: '1px solid var(--border-color)', padding: '12px', borderRadius: '8px' }} />
          <button onClick={handleAddSubmit} style={{ padding: '12px 24px', background: '#10b981', color: 'white', border: 'none', borderRadius: '8px', cursor: 'pointer' }}>Создать</button>
        </div>
      )}

      {/* СЕТКА / СПИСОК ТОВАРОВ */}
<div style={{ 
  display: 'grid', 
  gridTemplateColumns: uiSettings.viewMode === 'grid' ? 'repeat(auto-fill, minmax(280px, 1fr))' : '1fr',
  gap: '24px' 
}}>
  {products?.map((p: IProduct) => (
    <div 
      key={p.id} 
      style={{ 
        // ИСПОЛЬЗУЕМ ПЕРЕМЕННЫЕ ТЕМЫ ЗДЕСЬ:
        background: 'var(--card-bg)', 
        color: 'var(--text-primary)', 
        border: '1px solid var(--border-color)', 
        
        borderRadius: '16px', 
        padding: '24px', 
        boxShadow: '0 4px 6px rgba(0,0,0,0.05)', 
        display: 'flex', 
        flexDirection: uiSettings.viewMode === 'grid' ? 'column' : 'row', 
        alignItems: uiSettings.viewMode === 'grid' ? 'stretch' : 'center', 
        justifyContent: 'space-between',
        transition: 'all 0.3s ease' // Плавная смена цвета при переключении
      }}
    >
      <div style={{ flex: 1 }}>
        {/* Вторичный текст тоже через переменную */}
        <div style={{ color: 'var(--text-secondary)', fontSize: '11px', fontWeight: 600, marginBottom: '5px' }}>
          ID: {p.id}
        </div>
        
        <h4 style={{ margin: '0 0 10px 0', fontSize: '18px', fontWeight: 700 }}>
          {p.title}
        </h4>
        
        {/* Цена: убираем жесткий цвет #0f172a, ставим переменную */}
        <div style={{ fontSize: '22px', fontWeight: 800, color: 'var(--text-primary)' }}>
          ${p.price}
        </div>
      </div>
      
      <div style={{ display: 'flex', gap: '8px', alignItems: 'center', marginTop: uiSettings.viewMode === 'grid' ? '20px' : '0' }}>
        {/* Кнопки тоже можно слегка подкрасить под тему, если нужно, 
            но обычно в UI Kit кнопки сохраняют свои акцентные цвета (синий/красный) */}
        <Link 
          to="/product/$id" 
          params={{ id: p.id.toString() }} 
          style={{ padding: '8px 12px', background: 'var(--bg-color)', color: 'var(--text-secondary)', border: '1px solid var(--border-color)', borderRadius: '10px', textDecoration: 'none', fontSize: '13px', fontWeight: 600 }}
        >
          Детали
        </Link>

        <button onClick={() => toggleFavorite(p.id)} style={{ border: 'none', background: 'none', fontSize: '20px', cursor: 'pointer' }}>
          {favorites.includes(p.id) ? '❤️' : '🤍'}
        </button>

        <button onClick={() => addToCart(p.id)} style={{ padding: '8px 12px', background: 'var(--accent-color)', color: '#fff', border: 'none', borderRadius: '10px', fontSize: '13px', fontWeight: 600, cursor: 'pointer' }}>
          В корзину
        </button>

        <button onClick={() => deleteMutation.mutate(p.id)} style={{ padding: '8px', background: '#fee2e2', color: '#ef4444', border: 'none', borderRadius: '10px', cursor: 'pointer' }}>
          🗑️
        </button>
      </div>
    </div>
  ))}
</div>
    </div>
  )
}