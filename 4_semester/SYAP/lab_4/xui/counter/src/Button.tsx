interface ButtonProps {
  title: string
  onClick: () => void
  disabled?: boolean
}

function Button({ title, onClick, disabled }: ButtonProps) {
  const isIncrease = title === 'Increase'
  
  const buttonStyle = {
    padding: '15px 30px',
    fontSize: '18px',
    fontWeight: 'bold' as const,
    border: 'none',
    borderRadius: '50px',
    cursor: disabled ? 'not-allowed' as const : 'pointer' as const,
    minWidth: '140px',
    background: disabled 
      ? '#cccccc' 
      : isIncrease 
        ? 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)'
        : 'linear-gradient(135deg, #f093fb 0%, #f5576c 100%)',
    color: 'white',
    boxShadow: disabled 
      ? 'none' 
      : '0 4px 15px rgba(0,0,0,0.2)',
    transition: 'all 0.3s ease',
    transform: 'scale(1)',
    opacity: disabled ? 0.6 : 1,
    ':hover': !disabled ? {
      transform: 'scale(1.05)',
      boxShadow: '0 6px 20px rgba(0,0,0,0.3)'
    } : {}
  }

  return (
    <button 
      style={buttonStyle}
      onClick={onClick} 
      disabled={disabled}
      onMouseEnter={(e) => {
        if (!disabled) {
          e.currentTarget.style.transform = 'scale(1.05)'
          e.currentTarget.style.boxShadow = '0 6px 20px rgba(0,0,0,0.3)'
        }
      }}
      onMouseLeave={(e) => {
        if (!disabled) {
          e.currentTarget.style.transform = 'scale(1)'
          e.currentTarget.style.boxShadow = '0 4px 15px rgba(0,0,0,0.2)'
        }
      }}
    >
      {title === 'Increase' ? '+ ' : '↺ '}{title}
    </button>
  )
}

export default Button