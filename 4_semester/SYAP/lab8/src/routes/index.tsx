import { createFileRoute, Link } from '@tanstack/react-router'
import { useAuth } from '../context/AuthContext'

export const Route = createFileRoute('/')({
  component: IndexComponent,
})

function IndexComponent() {
  const { isAuthenticated, user } = useAuth()

  return (
    <div style={{ textAlign: 'center', padding: '80px 20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1 style={{ fontSize: '56px', fontWeight: 800, lineHeight: 1.1, marginBottom: '24px', color: 'var(--text-primary)' }}>
        Управляйте товарами с <span style={{ color: 'var(--accent-color)' }}>TanStack Экосистемой</span>
      </h1>
      
      <p style={{ fontSize: '20px', color: 'var(--text-secondary)', marginBottom: '40px' }}>
        {isAuthenticated 
          ? `Рады видеть вас снова, ${user?.email || 'пользователь'}!` 
          : 'Лучшее решение для высокопроизводительных приложений на React.'
        }
      </p>
      
      <div style={{ display: 'flex', gap: '16px', justifyContent: 'center' }}>
        <Link to="/catalog" style={primaryBtnStyle}>
          Открыть каталог
        </Link>

        {!isAuthenticated ? (
          <Link to="/login" style={secondaryBtnStyle}>
            Войти в аккаунт
          </Link>
        ) : (
          <div style={{ 
            padding: '14px 32px', 
            color: 'var(--accent-color)', 
            fontWeight: 600,
            border: '1px solid var(--accent-color)',
            borderRadius: '10px'
          }}>
            Вы авторизованы ✓
          </div>
        )}
      </div>
    </div>
  )
}

const primaryBtnStyle = {
  background: 'var(--accent-color)',
  color: '#fff',
  padding: '14px 32px',
  borderRadius: '10px',
  fontWeight: 600,
  textDecoration: 'none'
}

const secondaryBtnStyle = {
  background: '#fff',
  color: 'var(--text-primary)',
  padding: '14px 32px',
  borderRadius: '10px',
  fontWeight: 600,
  border: '1px solid var(--border-color)',
  textDecoration: 'none'
}