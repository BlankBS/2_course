import { RouterProvider, createRouter } from '@tanstack/react-router'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { routeTree } from './routeTree.gen'
import { AuthProvider, useAuth } from './context/AuthContext'
import { RecoilRoot } from 'recoil';

const queryClient = new QueryClient()

const router = createRouter({
  routeTree,
  context: {
    auth: undefined!,
  },
})

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router
  }
}

function InnerApp() {
  const auth = useAuth()
  return <RouterProvider router={router} context={{ auth }} />
}

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <RecoilRoot>
      <AuthProvider>
        <InnerApp />
      </AuthProvider>
    </RecoilRoot>
    </QueryClientProvider>
  )
}