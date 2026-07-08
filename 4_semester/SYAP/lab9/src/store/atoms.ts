import { atom, DefaultValue } from 'recoil';

// Эффект для синхронизации с LocalStorage
const localStorageEffect = (key: string) => ({ setSelf, onSet }: any) => {
  const savedValue = localStorage.getItem(key);
  if (savedValue != null) {
    setSelf(JSON.parse(savedValue));
  }
  onSet((newValue: any) => {
    if (newValue instanceof DefaultValue) {
      localStorage.removeItem(key);
    } else {
      localStorage.setItem(key, JSON.stringify(newValue));
    }
  });
};

// 1. Настройки UI
export const uiSettingsState = atom({
  key: 'uiSettingsState',
 default: { 
    viewMode: 'grid' as 'grid' | 'list', 
    theme: 'light' as 'light' | 'dark' 
  },
  effects: [localStorageEffect('ui_settings')]
});

// 2. Избранное (массив ID)
export const favoritesState = atom<number[]>({
  key: 'favoritesState',
  default: [],
  effects: [localStorageEffect('favorites')]
});

// 3. Корзина (массив объектов)
export interface ICartItem { id: number; quantity: number; }
export const cartState = atom<ICartItem[]>({
  key: 'cartState',
  default: [],
  effects: [localStorageEffect('cart')]
});