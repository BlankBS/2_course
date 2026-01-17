#include <iostream>
#include <vector>

const int size = 8;

int graph[size][size] =
{
	{0, 2, 0, 8, 2, 0, 0, 0},
	{2, 0, 3, 10, 5, 0, 0, 0},
	{0, 3, 0, 0, 12, 0, 0, 7},
	{8, 10, 0, 0, 14, 3, 1, 0},
	{2, 5, 12, 14, 0, 11, 4, 8},
	{0, 0, 0, 3, 11, 0, 6, 0},
	{0, 0, 0, 1, 4, 6, 0, 9},
	{0, 0, 7, 0, 8, 0, 9, 0}
};

void Print(int (&graph)[size][size])
{

}

void SolvePrima()
{
	int edgeCount = 0;
	bool visited[size];

	for (int i = 0; i < size; i++)
	{
		visited[i] = false;
	}

	int num;
	while(true)
	{
		std::cout << "\nНачальная вершина(1 - " << size << "): ";
		std::cin >> num;

		if (!std::cin)
		{
			system("cls");
			std::cout << "Ввода больше нет :(\n";
			return;
		}
		std::cout << '\n';

		if (std::cin.fail() || (num < 1 && num > 8))
		{
			system("cls");
			std::cin.clear();
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
			std::cout << "Неправильные данные. Повторите ввод.\n";
		}
		else
		{
			break;
		}
	}

	visited[num - 1] = true;

	long long totalWeight = 0;

	while (edgeCount < size - 1)
	{
		int min = INT_MAX;
		int a = -1, b = -1;

		for (int i = 0; i < size; i++)
		{
			if (visited[i])
			{
				for (int j = 0; j < size; j++)
				{
					if (!visited[j] && graph[i][j] > 0)
					{
						if (min > graph[i][j])
						{
							min = graph[i][j];
							a = i;
							b = j;
						}
					}
				}
			}
		}	

		if (a != -1 && b != -1)
		{
			std::cout << a + 1 << " -> " << b + 1 << " = " << graph[a][b] << '\n';
			visited[b] = true;
			edgeCount++;
			totalWeight += graph[a][b];
		}
	}
	std::cout << "Общий вес минимального остовного дерева: " << totalWeight << '\n';
}

void SolveKruskal()
{
	int edgeCount = 0;
	int visited[size];
	int totalWeight = 0;

	for (int i = 0; i < size; i++) {
		visited[i] = i;
	}

	while (edgeCount < size - 1) {
		int min = INT_MAX;
		int a = -1, b = -1;

		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {

				int rootI = i;
				while (visited[rootI] != rootI) {
					rootI = visited[rootI];
				}

				int rootJ = j;
				while (visited[rootJ] != rootJ) {
					rootJ = visited[rootJ];
				}

				if (rootI != rootJ && graph[i][j] < min && graph[i][j] != 0) {
					min = graph[i][j];
					a = i;
					b = j;
				}
			}
		}

		if (a != -1 && b != -1) {

			int rootA = a;
			while (visited[rootA] != rootA) {
				rootA = visited[rootA];
			}

			int rootB = b;
			while (visited[rootB] != rootB) {
				rootB = visited[rootB];
			}

			std::cout << a + 1 << " -> " << b + 1 << " = " << min << std::endl;
			visited[rootA] = rootB;
			edgeCount++;
			totalWeight += min;
		}
	}
	std::cout << "\nОбщий вес минимального остовного дерева: " << totalWeight << std::endl;
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	int choose;
	while (true)
	{
		std::cout << "Выберите\n1 - Прима\n2 - Крускала\nВыбор: ";
		std::cin >> choose;
		if (!std::cin)
		{
			system("cls");
			std::cout << "Ввода больше нет :(\n";
			return 0;
		}
		std::cout << '\n';

		if (std::cin.fail() || (choose != 1 && choose != 2))
		{
			system("cls");
			std::cin.clear();
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
			std::cout << "Неправильные данные. Повторите ввод.\n";
		}
		else
		{
			choose == 1 ? SolvePrima() : SolveKruskal();
			break;
		}
	}

	return 0;
}