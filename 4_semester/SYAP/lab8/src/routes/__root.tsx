import { createRootRouteWithContext, Link, Outlet, useNavigate } from '@tanstack/react-router'
import { TanStackRouterDevtools } from '@tanstack/router-devtools'
import { useAuth } from '../context/AuthContext'

export const Route = createRootRouteWithContext<any>()({
  component: () => {
    const { isAuthenticated, logout } = useAuth()
    const navigate = useNavigate()

    const handleLogout = () => {
      logout()
      navigate({ to: '/' })
    }

    return (
      <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
        <header style={{ 
          background: '#fff', 
          borderBottom: '1px solid var(--border-color)',
          padding: '0 40px',
          height: '70px',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          position: 'sticky',
          top: 0,
          zIndex: 100
        }}>
          <div style={{ fontWeight: 800, fontSize: '22px', letterSpacing: '-0.5px', color: 'var(--accent-color)' }}>
            STORE<span style={{ color: 'var(--text-primary)' }}>OS</span>
          </div>
          
          <nav style={{ display: 'flex', gap: '30px', alignItems: 'center' }}>
            <Link to="/" activeProps={{ style: { color: 'var(--accent-color)' } }} style={navLinkStyle}>Главная</Link>
            <Link to="/catalog" activeProps={{ style: { color: 'var(--accent-color)' } }} style={navLinkStyle}>Каталог</Link>
            
            {isAuthenticated ? (
              <button onClick={handleLogout} style={logoutButtonStyle}>
                Выйти
              </button>
            ) : (
              <Link to="/login" activeProps={{ style: { color: 'var(--accent-color)' } }} style={navLinkStyle}>
                Войти
              </Link>
            )}
          </nav>
        </header>

        <main style={{ flex: 1, padding: '40px' }}>
          <div style={{ maxWidth: '1200px', margin: '0 auto' }}>
            <Outlet />
          </div>
        </main>

        <footer style={{ textAlign: 'center', padding: '40px', color: 'var(--text-secondary)', fontSize: '14px' }}>
          © 2026 dev by BlankBS
        </footer>
        <TanStackRouterDevtools />
      </div>
    )
  },
})

const navLinkStyle = {
  fontSize: '15px',
  fontWeight: 500,
  color: 'var(--text-secondary)',
  textDecoration: 'none',
}

const logoutButtonStyle = {
  padding: '8px 16px',
  background: '#fee2e2',
  color: '#ef4444',     
  border: 'none',
  borderRadius: '10px',
  fontSize: '14px',
  fontWeight: 600,
  cursor: 'pointer',
  transition: 'background 0.2s',
}