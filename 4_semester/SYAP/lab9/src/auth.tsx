// src/auth.tsx
export interface AuthContext {
  isAuthenticated: boolean;
  user: any;
  login: (data: any) => void;
  logout: () => void;
}