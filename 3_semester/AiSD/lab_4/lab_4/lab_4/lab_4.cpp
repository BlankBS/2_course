#include <iostream>

int main()
{
	setlocale(LC_CTYPE, "rus");

	const int size = 6;

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

	std::cout << "Матрица D:\n";
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < size; j++)
		{
			if (D[i][j] == INT_MAX)
			{
				std::cout << "INF\t";
			}
			else
			{
				std::cout << D[i][j] << '\t';
			}
		}
		std::cout << '\n';
	}

	std::cout << "Матрица S:\n";
	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < size; j++)
		{
			std::cout << S[i][j] << '\t';
		}
		std::cout << '\n';
	}
	
	return 0;
}