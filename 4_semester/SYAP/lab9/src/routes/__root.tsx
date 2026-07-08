import { createRootRouteWithContext, Link, Outlet, useNavigate } from '@tanstack/react-router'
import { TanStackRouterDevtools } from '@tanstack/router-devtools'
import { useAuth } from '../context/AuthContext'
import { useRecoilState, useRecoilValue, useRecoilCallback } from 'recoil'
import { useEffect } from 'react'
import { uiSettingsState, cartState, favoritesState } from '../store/atoms'
import { cartCountState } from '../store/selector'

export const Route = createRootRouteWithContext<any>()({
  component: () => {
    const { isAuthenticated, logout } = useAuth()
    const navigate = useNavigate()
    const [uiSettings, setUiSettings] = useRecoilState(uiSettingsState)
    const cartCount = useRecoilValue(cartCountState)

    useEffect(() => {
      document.documentElement.setAttribute('data-theme', uiSettings.theme)
    }, [uiSettings.theme])

    const handleLogout = () => {
      logout()
      navigate({ to: '/' })
    }

    const resetAll = useRecoilCallback(({ reset }) => () => {
      reset(uiSettingsState); reset(cartState); reset(favoritesState);
      alert('Все настройки сброшены!');
    })

    return (
      <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column', background: 'var(--bg-color)' }}>
        <header style={{ 
          background: 'var(--card-bg)', borderBottom: '1px solid var(--border-color)',
          padding: '0 40px', height: '70px', display: 'flex',
          alignItems: 'center', justifyContent: 'space-between',
          position: 'sticky', top: 0, zIndex: 100, transition: 'all 0.3s ease'
        }}>
          <div style={{ fontWeight: 800, fontSize: '22px', color: 'var(--accent-color)' }}>STOREOS</div>
          
          <nav style={{ display: 'flex', gap: '20px', alignItems: 'center' }}>
            {/* Переключатель темы */}
            <button onClick={() => setUiSettings(p => ({...p, theme: p.theme === 'light' ? 'dark' : 'light'}))}
              style={{ background: 'var(--bg-color)', border: '1px solid var(--border-color)', borderRadius: '10px', cursor: 'pointer', color: 'var(--text-primary)', padding: '8px 12px' }}>
              {uiSettings.theme === 'light' ? '🌙' : '☀️'}
            </button>

            <Link to="/" style={{color: 'var(--text-secondary)', textDecoration: 'none', fontSize: '14px', fontWeight: 500}}>Главная</Link>
            <Link to="/catalog" style={{color: 'var(--text-secondary)', textDecoration: 'none', fontSize: '14px', fontWeight: 500}}>Каталог</Link>
            
            <Link to="/cart" style={{ position: 'relative', color: 'var(--text-primary)', textDecoration: 'none', fontWeight: 700, fontSize: '14px' }}>
              Корзина 
              {cartCount > 0 && (
                <span style={{ 
                  position: 'absolute', top: '-10px', right: '-15px',
                  background: '#ef4444', color: 'white', borderRadius: '10px', 
                  padding: '2px 6px', fontSize: '10px' 
                }}>{cartCount}</span>
              )}
            </Link>

            {isAuthenticated && (
              <div style={{ display: 'flex', gap: '12px', alignItems: 'center', marginLeft: '10px', paddingLeft: '20px', borderLeft: '1px solid var(--border-color)' }}>
                {/* СТИЛИЗОВАННАЯ КНОПКА СБРОСА */}
                <button onClick={resetAll} style={{ 
                  background: 'none', border: 'none', color: 'var(--text-secondary)', 
                  cursor: 'pointer', fontSize: '13px', fontWeight: 500 
                }}>
                  Сброс
                </button>
                
                <button onClick={handleLogout} style={{ 
                  padding: '8px 16px', background: '#fee2e2', color: '#ef4444', 
                  border: 'none', borderRadius: '10px', cursor: 'pointer', fontWeight: 600, fontSize: '13px' 
                }}>
                  Выйти
                </button>
              </div>
            )}
          </nav>
        </header>

        <main style={{ flex: 1, padding: '40px' }}>
          <Outlet />
        </main>
      </div>
    )
  }
})