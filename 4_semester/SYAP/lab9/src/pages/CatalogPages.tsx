import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchProducts } from '../api/products';
import type { IProduct } from '../api/products';
import { useSearch } from '@tanstack/react-router';

export const CatalogPage = () => {
  const queryClient = useQueryClient();
  const { category } = useSearch({ from: '/catalog' });

  const { data: products, isLoading, isError } = useQuery({
    queryKey: ['products', category],
    queryFn: () => fetchProducts(category),
    staleTime: 60000,
    gcTime: 300000, 
  });

  const deleteMutation = useMutation({
    mutationFn: (id: number) => fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' }),
    onMutate: async (id) => {
      await queryClient.cancelQueries({ queryKey: ['products'] });
      const previousProducts = queryClient.getQueryData<IProduct[]>(['products']);
      queryClient.setQueryData(['products'], (old: IProduct[]) => old?.filter(p => p.id !== id));
      return { previousProducts };
    },
    onError: (err, id, context) => {
      queryClient.setQueryData(['products'], context?.previousProducts);
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ['products'] });
    },
  });

  if (isLoading) return <div>Загрузка товаров...</div>;
  if (isError) return <div>Ошибка загрузки данных!</div>;

  return (
    <div className="grid">
      {products?.map(p => (
        <div key={p.id}>
          <h4>{p.title}</h4>
          <button onClick={() => deleteMutation.mutate(p.id)}>Удалить</button>
        </div>
      ))}
    </div>
  );
};