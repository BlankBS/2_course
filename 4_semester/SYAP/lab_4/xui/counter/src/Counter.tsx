import { useState, useEffect } from 'react'
import Button from './Button'

interface Particle {
  id: number
  left: number
  top: number
  size: number
  color: string
  delay: number
  xMove: number
  yMove: number
  rotation: number
}

function Counter() {
  const [count, setCount] = useState<number>(0)
  const [explosion, setExplosion] = useState<boolean>(false)
  const [particles, setParticles] = useState<Particle[]>([])

  const handleIncrease = () => {
    const newCount = count + 1
    setCount(newCount)
    
    if (newCount === 5) {
      setExplosion(true)
      
      const newParticles: Particle[] = []
      for (let i = 0; i < 30; i++) {
        newParticles.push({
          id: i,
          left: 50 + (Math.random() - 0.5) * 20, 
          top: 50 + (Math.random() - 0.5) * 20,
          size: Math.random() * 15 + 5,
          color: `hsl(${Math.random() * 360}, 100%, 60%)`,
          delay: Math.random() * 0.3,
          xMove: (Math.random() - 0.5) * 400,
          yMove: (Math.random() - 0.5) * 400,
          rotation: Math.random() * 720
        })
      }
      setParticles(newParticles)
    }
  }

  const handleReset = () => {
    setCount(0)
    setExplosion(false)
    setParticles([])
  }

  const isIncreaseDisabled = count >= 5
  const isResetDisabled = count === 0

  useEffect(() => {
    if (explosion) {
      const timer = setTimeout(() => {
        setExplosion(false)
        setParticles([])
      }, 1000)
      return () => clearTimeout(timer)
    }
  }, [explosion])

  const getCountColor = (): string => {
    if (count === 0) return '#333'
    if (count >= 5) return '#ff4444'
    return '#4CAF50'
  }

  return (
    <div style={{ 
      minHeight: '100vh',
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'center',
      alignItems: 'center',
      background: explosion 
        ? 'linear-gradient(135deg, #ff6b6b 0%, #4ecdc4 100%)'
        : 'linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%)',
      fontFamily: 'Arial, sans-serif',
      position: 'relative',
      overflow: 'hidden',
      transition: 'background 0.5s ease'
    }}>
      {explosion && particles.map((p) => (
        <div
          key={p.id}
          style={{
            position: 'absolute',
            left: `${p.left}%`,
            top: `${p.top}%`,
            width: `${p.size}px`,
            height: `${p.size}px`,
            background: p.color,
            borderRadius: '50%',
            boxShadow: `0 0 ${p.size}px ${p.color}`,
            animation: `explode 1s ease-out forwards`,
            animationDelay: `${p.delay}s`,
            pointerEvents: 'none',
            zIndex: 5
          }}
        />
      ))}

      {explosion && ['💥', '✨', '🎆', '🔥', '⭐'].map((emoji, index) => (
        <span
          key={`emoji-${index}`}
          style={{
            position: 'absolute',
            fontSize: `${Math.random() * 30 + 20}px`,
            left: `${Math.random() * 100}%`,
            top: `${Math.random() * 100}%`,
            animation: `flyEmoji 1s ease-out forwards`,
            animationDelay: `${Math.random() * 0.3}s`,
            pointerEvents: 'none',
            zIndex: 6
          }}
        >
          {emoji}
        </span>
      ))}

      <div style={{
        backgroundColor: 'rgba(255, 255, 255, 0.95)',
        padding: '50px',
        borderRadius: '30px',
        boxShadow: explosion 
          ? '0 0 100px rgba(255, 215, 0, 0.8), 0 20px 60px rgba(0,0,0,0.3)' 
          : '0 20px 60px rgba(0,0,0,0.2)',
        textAlign: 'center',
        transform: explosion ? 'scale(1.1)' : 'scale(1)',
        transition: 'all 0.3s cubic-bezier(0.68, -0.55, 0.265, 1.55)',
        position: 'relative',
        zIndex: 10,
        border: explosion ? '3px solid gold' : 'none'
      }}>
        <h1 style={{ 
          fontSize: explosion ? '90px' : '60px',
          marginBottom: '30px',
          color: getCountColor(),
          transition: 'all 0.3s ease',
          animation: explosion ? 'shake 0.5s ease-in-out, rainbow 1s linear infinite' : 'none',
          textShadow: explosion ? '0 0 20px gold' : 'none'
        }}>
          {count}
          {explosion && ' 💥'}
        </h1>
        
        <div style={{
          display: 'flex',
          gap: '20px',
          justifyContent: 'center'
        }}>
          <Button
            title="Increase"
            onClick={handleIncrease}
            disabled={isIncreaseDisabled}
          />
          
          <Button
            title="Reset"
            onClick={handleReset}
            disabled={isResetDisabled}
          />
        </div>

        {count === 5 && (
          <div style={{
            marginTop: '25px',
            animation: 'bounce 0.5s ease'
          }}>
            <p style={{ 
              color: '#ff4444', 
              fontSize: '24px',
              fontWeight: 'bold',
              margin: '5px 0'
            }}>
              🎉 БУМ! МАКСИМУМ! 🎉
            </p>
            <p style={{ 
              color: '#666',
              fontSize: '16px'
            }}>
              Нажми Reset чтобы начать заново
            </p>
          </div>
        )}
      </div>

      <style>{`
        @keyframes shake {
          0%, 100% { transform: translateX(0); }
          10%, 30%, 50%, 70%, 90% { transform: translateX(-10px); }
          20%, 40%, 60%, 80% { transform: translateX(10px); }
        }
        
        @keyframes explode {
          0% {
            transform: scale(1) rotate(0deg);
            opacity: 1;
          }
          100% {
            transform: scale(3) translate(${() => (Math.random() * 400 - 200)}px, ${() => (Math.random() * 400 - 200)}px) rotate(${() => Math.random() * 720}deg);
            opacity: 0;
          }
        }
        
        @keyframes flyEmoji {
          0% {
            transform: translate(0, 0) rotate(0deg) scale(1);
            opacity: 1;
          }
          100% {
            transform: translate(${() => (Math.random() * 400 - 200)}px, ${() => (Math.random() * 400 - 200)}px) rotate(${() => Math.random() * 720}deg) scale(2);
            opacity: 0;
          }
        }
        
        @keyframes bounce {
          0%, 100% { transform: translateY(0); }
          50% { transform: translateY(-20px); }
        }
        
        @keyframes rainbow {
          0% { color: #ff0000; }
          17% { color: #ff8800; }
          33% { color: #ffff00; }
          50% { color: #00ff00; }
          67% { color: #0088ff; }
          83% { color: #8800ff; }
          100% { color: #ff0000; }
        }
      `}</style>
    </div>
  )
}

export default Counter