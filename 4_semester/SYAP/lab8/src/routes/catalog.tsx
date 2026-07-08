import { createFileRoute, redirect, Link } from '@tanstack/react-router'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { z } from 'zod'
import { useState } from 'react'
import { fetchProducts, ProductSchema } from '../api/products'
import type { IProduct } from '../api/products'
import { Badge } from '../components/Badge'

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

  // Состояния для формы добавления
  const [isAdding, setIsAdding] = useState(false)
  const [newTitle, setNewTitle] = useState('')
  const [newPrice, setNewPrice] = useState('')

  // 1. Загрузка товаров
  const { data: products, isLoading, isError } = useQuery({
    queryKey: ['products', category],
    queryFn: () => fetchProducts(category),
    staleTime: 1000 * 60 * 10, 
  })

  // 2. Мутация ДОБАВЛЕНИЯ (CREATE)
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
    // ХАК ДЛЯ ЛАБЫ: Игнорируем ID 195 от сервера и ставим уникальный штамп времени
    const uniqueProduct = { ...createdProduct, id: Date.now() };

    queryClient.setQueryData(['products', category], (old: any) => {
      return [uniqueProduct, ...(old || [])]
    })
    
    setNewTitle('')
    setNewPrice('')
    setIsAdding(false)
  }
})

  // 3. Мутация УДАЛЕНИЯ (из прошлых шагов)
  const deleteMutation = useMutation({
    mutationFn: (id: number) => fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' }),
    onMutate: async (id) => {
      await queryClient.cancelQueries({ queryKey: ['products', category] })
      const previous = queryClient.getQueryData(['products', category])
      queryClient.setQueryData(['products', category], (old: any) => old?.filter((p: any) => p.id !== id))
      return { previous }
    },
    onError: (_err, _id, context) => {
      queryClient.setQueryData(['products', category], context?.previous)
    }
  })

  const handleAddSubmit = () => {
    // Валидация через Zod
    const result = ProductSchema.safeParse({
      id: Date.now(), // Генерируем временный ID
      title: newTitle,
      price: Number(newPrice)
    })

    if (!result.success) {
      alert(result.error.flatten().fieldErrors.title?.[0] || "Ошибка валидации цены")
      return
    }

    addMutation.mutate(result.data)
  }

  if (isLoading) return <div style={{ padding: '40px' }}>Загрузка товаров...</div>
  if (isError) return <div style={{ padding: '40px' }}>Ошибка API</div>

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '32px' }}>
        <h2 style={{ fontSize: '28px', margin: 0 }}>Каталог товаров</h2>
        <div style={{ display: 'flex', gap: '15px', alignItems: 'center' }}>
          <Badge color="blue" text={`${products?.length} позиций`} />
          <button 
            onClick={() => setIsAdding(!isAdding)}
            style={{ 
              padding: '10px 20px', 
              background: isAdding ? '#f1f5f9' : '#3b82f6', 
              color: isAdding ? '#1e293b' : '#fff',
              border: 'none', borderRadius: '10px', fontWeight: 600, cursor: 'pointer' 
            }}
          >
            {isAdding ? 'Отмена' : '+ Добавить товар'}
          </button>
        </div>
      </div>

      {/* ФОРМА ДОБАВЛЕНИЯ */}
      {isAdding && (
  <div style={{ 
    background: '#fff', 
    padding: '24px', 
    borderRadius: '16px', 
    marginBottom: '32px', 
    border: '2px dashed #e2e8f0',
    display: 'grid', // Используем GRID вместо FLEX для стабильности
    gridTemplateColumns: '3fr 1fr auto', // Название пошире, цена поменьше, кнопка по контенту
    gap: '20px', 
    alignItems: 'end'
  }}>
    <div style={{ display: 'flex', flexDirection: 'column' }}>
      <label style={{ fontSize: '11px', fontWeight: 800, color: '#64748b', marginBottom: '8px', textTransform: 'uppercase' }}>Название</label>
      <input 
        style={{ 
          padding: '12px', 
          borderRadius: '10px', 
          border: '1px solid #e2e8f0',
          fontSize: '14px',
          width: '100%',
          boxSizing: 'border-box' // Важно: чтобы padding не раздувал ширину
        }}
        value={newTitle} 
        onChange={e => setNewTitle(e.target.value)}
        placeholder="Напр: iPhone 15 Pro"
      />
    </div>

    <div style={{ display: 'flex', flexDirection: 'column' }}>
      <label style={{ fontSize: '11px', fontWeight: 800, color: '#64748b', marginBottom: '8px', textTransform: 'uppercase' }}>Цена ($)</label>
      <input 
        type="number"
        style={{ 
          padding: '12px', 
          borderRadius: '10px', 
          border: '1px solid #e2e8f0',
          fontSize: '14px',
          width: '100%',
          boxSizing: 'border-box'
        }}
        value={newPrice} 
        onChange={e => setNewPrice(e.target.value)}
        placeholder="999"
      />
    </div>

    <button 
      onClick={handleAddSubmit}
      disabled={addMutation.isPending}
      style={{ 
        padding: '12px 24px', 
        background: '#10b981', 
        color: '#fff', 
        border: 'none', 
        borderRadius: '10px', 
        fontWeight: 600, 
        cursor: 'pointer',
        height: '45px', // Фиксируем высоту кнопки под инпуты
        minWidth: '120px'
      }}
    >
      {addMutation.isPending ? '...' : 'Создать'}
    </button>
  </div>
)}

      {/* СЕТКА ТОВАРОВ */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: '24px' }}>
        {products?.map((p: IProduct) => (
          <div key={p.id} style={{ 
            background: '#fff', borderRadius: '16px', padding: '24px',
            boxShadow: '0 4px 6px -1px rgba(0,0,0,0.05)', border: '1px solid #f1f5f9',
            display: 'flex', flexDirection: 'column'
          }}>
            <div style={{ color: '#94a3b8', fontSize: '11px', fontWeight: 600, marginBottom: '10px' }}>ID: {p.id}</div>
            <h4 style={{ margin: '0 0 12px 0', fontSize: '18px', fontWeight: 700, minHeight: '52px' }}>{p.title}</h4>
            <div style={{ fontSize: '26px', fontWeight: 800, marginBottom: '24px' }}>${p.price}</div>
            <div style={{ display: 'flex', gap: '12px', marginTop: 'auto' }}>
              <Link to="/product/$id" params={{ id: p.id.toString() }} style={{ flex: 1, textAlign: 'center', padding: '12px', background: '#f1f5f9', color: '#475569', borderRadius: '10px', textDecoration: 'none', fontSize: '14px', fontWeight: 600 }}>Детали</Link>
              <button onClick={() => deleteMutation.mutate(p.id)} style={{ flex: 1, padding: '12px', background: '#fee2e2', color: '#ef4444', border: 'none', borderRadius: '10px', fontSize: '14px', fontWeight: 600, cursor: 'pointer' }}>Удалить</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}