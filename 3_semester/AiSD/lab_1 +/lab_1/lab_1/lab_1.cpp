#include <iostream>

int moves = 1;

void SolveTower(int N, int k, int i)
{
	if (N == 1)
	{
		std::cout << moves << ". Переместить диск 1 с " << i << " на " << k << " стержень\n";
		moves++;
		return;
	}

	int tmp = 6 - i - k;
	SolveTower(N - 1, tmp, i);
	std::cout << moves << ". Переместить диск " << N << " с " << i << " на " << k << " стержень\n";
	moves++;
	SolveTower(N - 1, k, tmp);
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	int N, k;

	std::cout << "Колиество дисков: ";
	std::cin >> N;

	std::cout << "Конечный стержень: ";
	std::cin >> k;

	if (N < 1 || k < 2 || k > 3)
	{
		std::cout << "Введены некорректные данные";
	}
	else
	{
		SolveTower(N, k, 1);
	}

	return 0;
}