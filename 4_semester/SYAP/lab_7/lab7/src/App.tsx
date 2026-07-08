import { AuthProvider, useAuth } from './context/AuthContext';
import { ProductProvider } from './context/ProductContext';
import { CatalogPage } from './pages/CatalogPages';
import { RegistrationForm } from './RegistrationForm/RegistrationForm'; // Из лабы №6

const Root = () => {
  const { isAuthenticated, login } = useAuth();

  // Если не авторизован - показываем форму из лабы 6
  if (!isAuthenticated) {
    return <RegistrationForm onComplete={(data: any) => login(data)} />;
  }

  return <CatalogPage />;
};

function App() {
  return (
    <AuthProvider>
      <ProductProvider>
        <Root />
      </ProductProvider>
    </AuthProvider>
  );
}

export default App;