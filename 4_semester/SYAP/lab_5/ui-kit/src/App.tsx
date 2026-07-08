import { useState } from 'react';
import { Button } from './components/ui/Button';
import { Input } from './components/ui/Input';
import { Badge } from './components/ui/Badge';
import { LayoutCard } from './components/ui/LayoutCard';

const App = () => {
    const [name, setName] = useState('');

  return (
    <div style={{ padding: '40px', background: '#f0f2f5', minHeight: '100vh', display: 'flex',
            justifyContent: 'center', alignItems: 'center', }}>
      <LayoutCard
        title="пупупу..."
        footer={<Badge color="green" text="Статус: Активен" />}
      >
        <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>

          <Input
            label="Имя пользователя"
            placeholder="Введите имя..."
            value={name}
            onChange={(e) => setName(e.target.value)}
            error={name.length < 3 ? "Слишком короткое имя" : ""}
            isFullWidth
          />

          <div style={{ display: 'flex', gap: '10px' }}>
            <Button variant="primary" size="medium">Сохранить</Button>
            <Button variant="danger" size="small">Удалить</Button>
            <Button variant="secondary" isLoading={true}>Загрузка</Button>
          </div>

          <div>
            Роли: <Badge color="blue" text="Admin" /> <Badge color="orange" text="Editor" />
          </div>

        </div>
      </LayoutCard>
    </div>
  );
};

export default App;