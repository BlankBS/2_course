#include <iostream>
#include <iomanip>

const int size = 6;

void Print(int (&arr)[size][size])
{
	int cell_width = 3;

	std::cout << '+';
	for (int j = 0; j < size; j++) {
		std::cout << std::string(cell_width, '-') << '+';
	}
	std::cout << '\n';

	for (int i = 0; i < size; i++)
	{
		std::cout << '|';

		for (int j = 0; j < size; j++)
		{
			if (arr[i][j] == INT_MAX)
			{
				std::cout << std::setw(cell_width) << std::left << " INF" << '|';
			}
			else
			{
				std::cout << std::setw(cell_width) << std::left << arr[i][j] << '|';
			}
		}
		std::cout << '\n';

		std::cout << '+';
		for (int j = 0; j < size; j++) {
			std::cout << std::string(cell_width, '-') << '+';
		}
		std::cout << '\n';
	}
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	int D[size][size] =
	{
		{0, 28, 21, 59, 12, 27},
		{7, 0, 24, INT_MAX, 21, 9},
		{9, 32, 0, 13, 11, INT_MAX},
		{8, INT_MAX, 5, 0, 16, INT_MAX},
		{14, 13, 15, 10, 0, 22},
		{15, 18, INT_MAX, INT_MAX, 6, 0}
	};

	int S[size][size] =
	{
		{0, 2, 3, 4, 5, 6},
		{1, 0, 3, 4, 5, 6},
		{1, 2, 0, 4, 5, 6},
		{1, 2, 3, 0, 5, 6},
		{1, 2, 3, 4, 0, 6},
		{1, 2, 3, 4, 5, 0}
	};

	for (int m = 0; m < size; m++)
	{
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				if (i != j && j != m && i != m)
				{
					if (D[i][j] > D[i][m] + D[m][j])
					{
						D[i][j] = D[m][j] + D[i][m];
						S[i][j] = m + 1;
					}
				}
			}
		}
	}

	std::cout << "\tМатрица D\n";
	Print(D);

	std::cout << "\n\tМатрица S\n";
	Print(S);
	
	return 0;
}