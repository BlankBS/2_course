#include <iostream>
#include <vector>

int main()
{
	setlocale(LC_CTYPE, "rus");

	std::cout << "Длина последовательности: ";
	int N;
	std::cin >> N;

	if (!std::cin)
	{
		system("cls");
		std::cout << "Ввода больше нет";
		return 0;
	}

	if (N < 1)
	{
		std::cout << "Длина должна быть натуральным числом";
		return 0;
	}

	std::vector<int> elements(N);
	std::vector<int> buff(N);
	std::vector<int> path(N);

	std::cout << "Элементы последовательности: ";
	for (int i = 0; i < N; i++)
	{
		std::cin >> elements[i];
		if (!std::cin)
		{
			system("cls");
			std::cout << "Ввода больше нет";
			return 0;
		}
		buff[i] = 1;
		path[i] = -1;
	}

	std::cout << '\n';

	for (int i = 1; i < N; i++)
	{
		for (int j = 0; j < i; j++)
		{
			if (elements[i] > elements[j] &&
				elements[i] - elements[j] == 1)
			{
				if (buff[i] <= buff[j])
				{
					buff[i] = buff[j] + 1;
					path[i] = j;
				}
			}
		}
	}

	int max = 0;
	int check = 0;
	for (int i = 0; i < N; i++)
	{
		if (buff[i] > max)
		{
			check = i;
			max = buff[i];
		}
	}

	std::cout << "Длина максимальной подпоследовательности: " << max << '\n';
	std::cout << "Элементы максимальной подпоследовательности: ";
	std::vector<int> result(max + 1); int x;
	for (int i = max; i > 0; i--)
	{
		//std::cout << "\n check = " << check;
		result[i] = elements[check];
		x = check;
		check = path[x];
	}
	//std::cout << result.size();

	for (int i = 1; i < max + 1; i++)
	{
		std::cout << result[i] << ' ';
	}

	std::cout << "\n\n\n\n";

	return 0;
}