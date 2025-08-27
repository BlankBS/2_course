#include <iostream>

int MinDistance(int arr_min[9], bool visited[])
{
	int min = INT_MAX, min_index;

	for (int i = 0; i < 9; i++)
	{
		if (!visited[i] && arr_min[i] <= min)
		{
			min = arr_min[i];
			min_index = i;
		}
	}
	return min_index;
}

void PrintSolution(int arr_min[])
{
	char startCh = 'A';
	for (int i = 0; i < 9; i++)
	{
		std::cout << "До вершины " << startCh << " - " << arr_min[i] << '\n';
		startCh++;
	}
}

void SolveGraph(int graph[9][9], int start)
{
	int arr_min[9];
	bool visited[9];

	for (int i = 0; i < 9; i++)
	{
		arr_min[i] = INT_MAX;
		visited[i] = false;
	}

	arr_min[start] = 0;

	for (int i = 0; i < 8; i++)
	{
		int u = MinDistance(arr_min, visited);
		visited[u] = true;

		for (int j = 0; j < 9; j++)
		{
			if (!visited[j] && graph[u][j] && arr_min[u] != INT_MAX && arr_min[u] + graph[u][j] < arr_min[j])
			{
				arr_min[j] = arr_min[u] + graph[u][j];
			}
		}
	}

	PrintSolution(arr_min);
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	int start;
	int graph[9][9] =
	{
		{0, 7, 10, 0, 0, 0, 0, 0, 0},
		{7, 0, 0, 0, 0, 9, 27, 0, 0},
		{10, 0, 0, 0, 31, 8, 0, 0, 0},
		{0, 0, 0, 0, 32, 0, 0, 17, 21},
		{0, 0, 31, 32, 0, 0, 0, 0, 0,},
		{0, 9, 8, 0, 0, 0, 0, 11, 0},
		{0, 27, 0, 0, 0, 0, 0, 0, 15},
		{0, 0, 0, 17, 0, 11, 0, 0, 15},
		{0, 0, 0, 21, 0, 0, 15, 15, 0}
	};

	bool isFirstTime = true;

	do 
	{
		std::cout << (isFirstTime ? "Индекс стартовой вершины: " : "Неправильные данные. Повторите(1 - 8): ");
		std::cin >> start;
		isFirstTime = false;
	} while (start < 1 || start > 8);

	SolveGraph(graph, start);

	return 0;
}