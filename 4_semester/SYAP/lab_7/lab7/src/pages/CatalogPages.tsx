import React, { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { useProducts } from '../context/ProductContext';
import { ProductSchema, type IProduct } from '../context/ProductSchema';

export const CatalogPage = () => {
  const { logout, user } = useAuth();
  const { state, dispatch } = useProducts();

  // Состояния для управления формой
  const [isFormOpen, setIsFormOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<IProduct | null>(null);
  
  // Поля формы
  const [title, setTitle] = useState('');
  const [price, setPrice] = useState(0);

  // 1. READ: Загрузка данных
  useEffect(() => {
    fetch('https://dummyjson.com/products?limit=10')
      .then(res => res.json())
      .then(data => dispatch({ type: 'SET_PRODUCTS', payload: data.products }));
  }, [dispatch]);

  // Открытие формы для создания
  const openCreateForm = () => {
    setEditingProduct(null);
    setTitle('');
    setPrice(0);
    setIsFormOpen(true);
  };

  // Открытие формы для редактирования (UPDATE)
  const openEditForm = (product: IProduct) => {
    setEditingProduct(product);
    setTitle(product.title);
    setPrice(product.price);
    setIsFormOpen(true);
  };

  // СОХРАНЕНИЕ (CREATE или UPDATE)
  const handleSave = async () => {
  const rawData = { 
      id: editingProduct ? editingProduct.id : Date.now(), 
      title, 
      price: Number(price) 
  };
  
  const result = ProductSchema.safeParse(rawData);
  if (!result.success) {
    alert("Ошибка валидации!");
    return;
  }

  const validatedData = result.data;

  if (editingProduct) {
    // --- UPDATE ---
    try {
      const res = await fetch(`https://dummyjson.com/products/${editingProduct.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(validatedData)
      });

      // Если res.ok ИЛИ 404 (для созданных нами товаров), обновляем стейт
      if (res.ok || res.status === 404) {
        dispatch({ type: 'UPDATE_PRODUCT', payload: validatedData });
      }
    } catch (e) {
      dispatch({ type: 'UPDATE_PRODUCT', payload: validatedData });
    }
  } else {
    // --- CREATE ---
    try {
      const res = await fetch('https://dummyjson.com/products/add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(validatedData)
      });
      // При создании DummyJSON всегда возвращает успех, но мы добавим наш ID
      if (res.ok) {
        dispatch({ type: 'ADD_PRODUCT', payload: validatedData });
      }
    } catch (e) {
      dispatch({ type: 'ADD_PRODUCT', payload: validatedData });
    }
  }
  setIsFormOpen(false);
  setEditingProduct(null);
};

  // DELETE
  const handleDelete = async (id: number) => {
  try {
    const res = await fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' });
    
    // ЛОГИКА: Если сервер ответил OK или 404 (товар не найден на сервере, но есть у нас), 
    // всё равно удаляем из локального стейта.
    if (res.ok || res.status === 404) {
      dispatch({ type: 'DELETE_PRODUCT', payload: id });
    }
  } catch (e) {
    // На случай если интернет пропал, тоже удалим локально для наглядности
    dispatch({ type: 'DELETE_PRODUCT', payload: id });
  }
};

  return (
    <div style={{ padding: '20px', fontFamily: 'sans-serif' }}>
      <header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h2>Каталог для {user?.email}</h2>
        <button onClick={logout} style={{ background: '#ff4d4f', color: 'white', border: 'none', padding: '8px 15px', borderRadius: '4px' }}>Выйти</button>
      </header>

      <button onClick={openCreateForm} style={{ background: '#52c41a', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '4px', marginBottom: '20px', cursor: 'pointer' }}>
        + Добавить новый товар
      </button>

      {/* ФОРМА (Модальное окно или просто блок) */}
      {isFormOpen && (
        <div style={{ background: '#f0f2f5', padding: '20px', borderRadius: '8px', marginBottom: '30px', border: '1px solid #d9d9d9' }}>
          <h3>{editingProduct ? 'Редактировать товар' : 'Новый товар'}</h3>
          <div style={{ display: 'flex', gap: '10px', marginBottom: '10px' }}>
            <input placeholder="Название" value={title} onChange={e => setTitle(e.target.value)} style={{ padding: '8px', flex: 1 }} />
            <input type="number" placeholder="Цена" value={price} onChange={e => setPrice(Number(e.target.value))} style={{ padding: '8px', width: '100px' }} />
          </div>
          <button onClick={handleSave} style={{ marginRight: '10px' }}>Сохранить</button>
          <button onClick={() => setIsFormOpen(false)}>Отмена</button>
        </div>
      )}

      {/* СЕТКА ТОВАРОВ (GRID) */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', gap: '20px' }}>
        {state.products.map(product => (
          <div key={product.id} style={{ border: '1px solid #ddd', padding: '15px', borderRadius: '8px', background: 'white' }}>
            <h4>{product.title}</h4>
            <p style={{ color: '#888' }}>Цена: <b>${product.price}</b></p>
            <div style={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
              <button onClick={() => openEditForm(product)} style={{ flex: 1 }}>Изменить</button>
              <button onClick={() => handleDelete(product.id)} style={{ flex: 1, color: 'red' }}>Удалить</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};