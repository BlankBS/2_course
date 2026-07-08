import { createFileRoute, Link } from '@tanstack/react-router'
import { useRecoilState } from 'recoil'
import { cartState } from '../store/atoms'
import { useQuery, useQueryClient } from '@tanstack/react-query'
import { fetchProducts } from '../api/products'

export const Route = createFileRoute('/cart')({
  component: CartPage,
})

function CartPage() {
  const [cart, setCart] = useRecoilState(cartState);
  const queryClient = useQueryClient();
  const { data: products } = useQuery({ 
    queryKey: ['products'], 
    queryFn: () => fetchProducts(),
    staleTime: 1000 * 60 * 10
  });

  const cartItems = cart.map(item => {
    let fullProduct = products?.find(p => p.id.toString() === item.id.toString());
    if (!fullProduct) {
      const allCachedData = queryClient.getQueriesData({ queryKey: ['products'] });
      for (const [key, data] of allCachedData) {
        if (Array.isArray(data)) {
          const found = data.find((p: any) => p.id.toString() === item.id.toString());
          if (found) { fullProduct = found; break; }
        }
      }
    }
    return { ...item, ...fullProduct };
  }).filter(i => i.title);

  const totalPrice = cartItems.reduce((acc, item) => acc + (item.price || 0) * item.quantity, 0);

  return (
    <div style={{ maxWidth: '800px', margin: '40px auto', padding: '20px' }}>
      <h1 style={{ fontSize: '32px', fontWeight: 800, marginBottom: '30px', color: 'var(--text-primary)' }}>
        Ваша корзина
      </h1>
      
      {cartItems.length === 0 ? (
        <div style={{ 
          textAlign: 'center', padding: '60px', 
          background: 'var(--card-bg)', borderRadius: '24px', 
          border: '1px solid var(--border-color)', color: 'var(--text-secondary)' 
        }}>
          <p style={{ marginBottom: '20px', fontSize: '18px' }}>Корзина пока пуста...</p>
          <Link to="/catalog" style={{ color: 'var(--accent-color)', fontWeight: 600, textDecoration: 'none' }}>
            ← Вернуться в каталог
          </Link>
        </div>
      ) : (
        <div style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
          {cartItems.map(item => (
            <div key={item.id} style={{ 
              display: 'flex', alignItems: 'center', justifyContent: 'space-between', 
              background: 'var(--card-bg)', padding: '24px', borderRadius: '20px', 
              boxShadow: '0 4px 6px rgba(0,0,0,0.02)', border: '1px solid var(--border-color)',
              color: 'var(--text-primary)'
            }}>
              <div style={{ flex: 2 }}>
                <div style={{ fontWeight: 700, fontSize: '18px' }}>{item.title}</div>
                <div style={{ color: 'var(--accent-color)', fontWeight: 600 }}>${item.price}</div>
              </div>

              <div style={{ flex: 1, display: 'flex', alignItems: 'center', gap: '15px', justifyContent: 'center' }}>
                <button onClick={() => setCart(prev => prev.map(i => i.id === item.id ? {...i, quantity: Math.max(1, i.quantity - 1)} : i))} style={qtyBtnStyle}>-</button>
                <span style={{ fontWeight: 700, minWidth: '20px', textAlign: 'center' }}>{item.quantity}</span>
                <button onClick={() => setCart(prev => prev.map(i => i.id === item.id ? {...i, quantity: i.quantity + 1} : i))} style={qtyBtnStyle}>+</button>
              </div>

              <div style={{ flex: 1, textAlign: 'right', fontWeight: 800, fontSize: '18px' }}>
                ${((item.price || 0) * item.quantity).toFixed(2)}
              </div>

              <button 
                onClick={() => setCart(prev => prev.filter(i => i.id !== item.id))} 
                style={{ marginLeft: '20px', background: 'none', border: 'none', color: '#ef4444', cursor: 'pointer', fontWeight: 600 }}
              >
                Удалить
              </button>
            </div>
          ))}

          {/* Итоговая панель */}
          <div style={{ 
            marginTop: '20px', padding: '30px', 
            background: 'var(--card-bg)', color: 'var(--text-primary)', 
            borderRadius: '24px', display: 'flex', justifyContent: 'space-between', 
            alignItems: 'center', border: '1px solid var(--border-color)' 
          }}>
            <div>
              <div style={{ color: 'var(--text-secondary)', fontSize: '14px' }}>Итого к оплате:</div>
              <div style={{ fontSize: '32px', fontWeight: 800 }}>${totalPrice.toFixed(2)}</div>
            </div>
            <button onClick={() => setCart([])} style={{ 
              background: '#ef4444', color: '#fff', border: 'none', 
              padding: '14px 28px', borderRadius: '14px', cursor: 'pointer', fontWeight: 700 
            }}>
              Очистить всё
            </button>
          </div>
        </div>
      )}
    </div>
  )
}

const qtyBtnStyle = {
  width: '36px', height: '36px', borderRadius: '10px', border: '1px solid var(--border-color)',
  background: 'var(--bg-color)', color: 'var(--text-primary)', cursor: 'pointer', fontWeight: 700,
  display: 'flex', alignItems: 'center', justifyContent: 'center'
};