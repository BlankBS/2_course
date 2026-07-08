import { createFileRoute, useNavigate, redirect } from '@tanstack/react-router'
import { useAuth } from '../context/AuthContext'
import { useState } from 'react'
import { z } from 'zod'

const LoginSchema = z.object({
  email: z.string().email("Введите корректный email адрес (например, user@mail.com)")
})

export const Route = createFileRoute('/login')({
  beforeLoad: ({ context }) => {
    if (context.auth.isAuthenticated) {
      throw redirect({ to: '/catalog' })
    }
  },
  component: LoginPage,
})

function LoginPage() {
  const auth = useAuth()
  const navigate = useNavigate()
  const [email, setEmail] = useState('')
  const [error, setError] = useState<string | null>(null)

  const handleLogin = () => {
    const result = LoginSchema.safeParse({ email })

    if (!result.success) {
      setError(result.error.flatten().fieldErrors.email?.[0] || 'Ошибка')
      return
    }

    setError(null)
    auth.login({ email: result.data.email, id: 1 })
    navigate({ to: '/catalog' })
  }

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '70vh' }}>
      <div style={{ 
        width: '100%', 
        maxWidth: '400px', 
        background: '#fff', 
        padding: '40px', 
        borderRadius: '24px', 
        boxShadow: '0 20px 25px -5px rgba(0, 0, 0, 0.1)',
        border: '1px solid #f1f5f9'
      }}>
        <h2 style={{ textAlign: 'center', fontSize: '28px', fontWeight: 800, marginBottom: '8px', color: '#1e293b' }}>
          Авторизация
        </h2>
        <p style={{ textAlign: 'center', color: '#64748b', fontSize: '14px', marginBottom: '32px' }}>
          Введите данные для доступа к каталогу
        </p>

        <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
          <div>
            <label style={{ display: 'block', fontSize: '13px', fontWeight: 600, color: '#475569', marginBottom: '6px' }}>
              Email адрес
            </label>
            <input 
              placeholder="name@example.com" 
              value={email} 
              onChange={(e) => {
                setEmail(e.target.value)
                if (error) setError(null) 
              }} 
              style={{ 
                width: '100%', 
                padding: '12px 16px', 
                borderRadius: '12px', 
                border: `1px solid ${error ? '#ef4444' : '#e2e8f0'}`,
                fontSize: '15px',
                outline: 'none',
                boxSizing: 'border-box'
              }}
            />
            {error && (
              <p style={{ color: '#ef4444', fontSize: '12px', marginTop: '6px', fontWeight: 500 }}>
                {error}
              </p>
            )}
          </div>

          <button 
            onClick={handleLogin} 
            style={{ 
              width: '100%', 
              padding: '14px', 
              background: '#3b82f6', 
              color: '#fff', 
              border: 'none', 
              borderRadius: '12px', 
              fontSize: '16px', 
              fontWeight: 600, 
              cursor: 'pointer'
            }}
          >
            Войти в систему
          </button>
        </div>
      </div>
    </div>
  )
}