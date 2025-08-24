#include <iostream>
#include <queue>
#include <stack>

const int n = 10;
int graph[n][n] =
{
	{0, 1, 0, 0, 1, 0, 0, 0, 0, 0},
	{1, 0, 0, 0, 0, 0, 1, 1, 0, 0},
	{0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
	{0, 0, 0, 0, 0, 1, 0, 0, 1, 0},
	{1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
	{0, 0, 0, 1, 1, 0, 0, 0, 1, 0},
	{0, 1, 0, 0, 0, 0, 0, 1, 0, 0},
	{0, 1, 1, 0, 0, 0, 1, 0, 0, 0},
	{0, 0, 0, 1, 0, 1, 0, 0, 0, 1},
	{0, 0, 0, 0, 0, 0, 0, 0, 1, 0}
};

bool* visited = new bool[n];

void BFS(int start)
{
	bool visited[n] = { false };
	std::queue<int> place;
	place.push(start);
	visited[start] = true;

	while (!place.empty())
	{
		int view_cell = place.front();
		std::cout << view_cell + 1 << ' ';
		place.pop();

		for (int i = 0; i < n; i++)
		{
			if (graph[view_cell] != 0 && !visited[i])
			{
				place.push(i);
				visited[i] = true;
			}
		}
	}
}

void DFS(int start)
{
	std::stack<int> place;
	place.push(start);

	while (!place.empty())
	{
		int current = place.top();
		place.pop();

		if (!visited[current])
		{
			std::cout << current + 1 << ' ';
			visited[current] = true;
		}
		for (int i = n - 1; i >= 0; i--)
		{
			if (graph[current][i] != 0 && !visited[i])
			{
				place.push(i);
			}
		}
	}
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	std::cout << "Список ребер: \n";
	int arr_1[11] = {1, 1, 2, 2, 3, 4, 4, 5, 6, 7, 9};
	int arr_2[11] = {2, 5, 7, 8, 8, 6, 9, 6, 9, 8, 10};

	for (int i = 0; i < 11; i++)
	{
		std::cout << '{' << arr_1[i] << ", " << arr_2[i] << '}' << '\n';
		std::cout << '{' << arr_2[i] << ", " << arr_1[i] << '}' << '\n';
	}

	std::cout << "\nМатрица смежности:\n";
	for (int i = 0; i < n; i++)
	{
		visited[i] = false;
		for (int j = 0; j < n; j++)
		{
			std::cout << graph[i][j] << ' ';
		}
		std::cout << '\n';
	}

	std::cout << "\nСписок смежности:\n";
	int arrRibs[n][3] =
	{
		{2, 5},
		{1, 7, 8},
		{8},
		{6, 9},
		{1, 6},
		{4, 5, 9},
		{2, 8},
		{2, 3, 7},
		{4, 10},
		{9}
	};

	for (int i = 0; i < n; i++)
	{
		std::cout << i + 1 << " -> {";
		for (int j = 0; j < 3; j++)
		{
			if (arrRibs[i][j] == 0)
			{
				break;
			}
			std::cout << arrRibs[i][j];
			if (j < 2 && arrRibs[i][j + 1] != 0) std::cout << ' ';
		}
		std::cout << "}\n";
	}

	std::cout << "\nПоиск в ширину\n";
	std::cout << "Начальная вершина: ";
	int start_1;
	std::cin >> start_1;
	std::cout << "Посещение вершины: ";
	BFS(start_1 - 1);

	std::cout << "\nПоиск в глубину\n";
	std::cout << "Начальную вершину: ";
	int start_2;
	std::cin >> start_2;
	DFS(start_2 - 1);
	
	delete[] visited;
	return 0;
}