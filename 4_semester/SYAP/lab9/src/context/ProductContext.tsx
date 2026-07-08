import React, { createContext, useContext, useReducer} from 'react';
import type { ReactNode } from 'react';
import type { IProduct } from '../context/ProductSchema';

type State = { products: IProduct[] };
type Action = 
  | { type: 'SET_PRODUCTS'; payload: IProduct[] }
  | { type: 'ADD_PRODUCT'; payload: IProduct }
  | { type: 'UPDATE_PRODUCT'; payload: IProduct }
  | { type: 'DELETE_PRODUCT'; payload: number };

const productReducer = (state: State, action: Action): State => {
  switch (action.type) {
    case 'SET_PRODUCTS': return { products: action.payload };
    case 'ADD_PRODUCT': return { products: [action.payload, ...state.products] };
    case 'UPDATE_PRODUCT':  return {
        ...state,
        products: state.products.map((item: any) => 
          item.id.toString() === action.payload.id.toString() ? action.payload : item
        )
      };
    case 'DELETE_PRODUCT': return {
      products: state.products.filter(p => p.id !== action.payload)
    };
    default: return state;
  }
};

const ProductContext = createContext<{ state: State; dispatch: React.Dispatch<Action> } | undefined>(undefined);

export const ProductProvider = ({ children }: { children: ReactNode }) => {
  const [state, dispatch] = useReducer(productReducer, { products: [] });
  return <ProductContext.Provider value={{ state, dispatch }}>{children}</ProductContext.Provider>;
};

export const useProducts = () => {
  const context = useContext(ProductContext);
  if (!context) throw new Error("useProducts must be used within ProductProvider");
  return context;
};