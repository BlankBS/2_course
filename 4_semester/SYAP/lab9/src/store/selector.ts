import { selector } from 'recoil';
import { cartState } from './atoms';

// Сумма всех товаров в корзине
export const cartCountState = selector({
  key: 'cartCountState',
  get: ({ get }) => {
    const cart = get(cartState);
    return cart.reduce((acc, item) => acc + item.quantity, 0);
  }
});