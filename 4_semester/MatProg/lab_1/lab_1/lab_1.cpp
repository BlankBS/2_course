#include "pch.h"

#define CYCLE 1000000

bool CheckOnLetter()
{
	if (!std::cin)
	{
		std::cin.clear();
		std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
		return true;
	}
	return false;
}

void ValidatedIntInput(const std::string& EnterMessage, int& EnterVariable, std::string ErrorMessage, int minValue = 0, int maxValue = 0)
{
	bool isMistake = false;

	do
	{
		isMistake = false;
		std::cout << EnterMessage;
		std::cin >> EnterVariable;
		if (maxValue == 0)
		{
			maxValue = INT_MAX;
		}

		if (std::cin.fail())
		{
			isMistake = true;
			std::cin.clear();
		}
		if (EnterVariable < minValue || EnterVariable > maxValue)
		{
			isMistake = true;
		}
		if (isMistake)
		{
			std::cout << ErrorMessage << '\n';
		}
		if (CheckOnLetter())
		{
			isMistake = true;
		}
		std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
		std::cin.clear();
	} while (isMistake);
}

void first_task()
{
	Auxil::start();

	std::cout << Auxil::iget(5, 100) << '\n';
	std::cout << Auxil::iget(5, 100) << '\n';
	std::cout << Auxil::iget(5, 100) << '\n';
	std::cout << Auxil::iget(5, 100) << '\n';
	std::cout << Auxil::iget(5, 100) << '\n';
	std::cout << Auxil::iget(5, 100) << '\n';
}

void second_task()
{
	double  av1 = 0, av2 = 0;
	clock_t  t1 = 0, t2 = 0;

	Auxil::start();                          // старт генерации 
	t1 = clock();                            // фиксация времени 
	for (int i = 0; i < CYCLE; i++)
	{
		av1 += (double)Auxil::iget(-100, 100); // сумма случайных чисел 
		av2 += Auxil::dget(-100, 100);         // сумма случайных чисел 
	}
	t2 = clock();                            // фиксация времени 


	std::cout << std::endl << "количество циклов:         " << CYCLE;
	std::cout << std::endl << "среднее значение (int):    " << av1 / CYCLE;
	std::cout << std::endl << "среднее значение (double): " << av2 / CYCLE;
	std::cout << std::endl << "продолжительность (у.е):   " << (t2 - t1);
	std::cout << std::endl << "                  (сек):   "
		<< ((double)(t2 - t1)) / ((double)CLOCKS_PER_SEC);
	std::cout << std::endl;
	system("pause");

}

long long fib(int n)
{
	if (n <= 1)
	{
		return n;
	}

	return fib(n - 1) + fib(n - 2);
}

void third_task(int& _n)
{
	auto start = std::chrono::high_resolution_clock::now();
	std::cout << "\nФибоначчи[" << _n - 1 << "] = " << fib(_n);
	auto end = std::chrono::high_resolution_clock::now();
	auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(end - start);
	std::cout << "\nВремя: " << duration.count();
}


int main()
{
	setlocale(LC_ALL, "rus");

	int choose = 1;
	std::string EnterStr = "Выберите какое задание запустить:\n1. первое\n2. второе\n3. третье\nВыбор: ";
	ValidatedIntInput(EnterStr, choose, "Выберите верное значение", 1, 4);
	switch (choose)
	{
	case(1):
	{
		first_task(); break;
	}
	case(2):
	{
		second_task();
		break;
	}
	case(3):
	{
		int n;
		std::string str1 = "Введите номер числа Фибоначчи для вычисления: ";
		std::string str2 = "Номер числа должен быть натуральным числом";
		ValidatedIntInput(str1, n, str2, 1);

		third_task(n);
		break;
	}
	default:
	{
		std::cout << "error.";
		return 0;
	}
	}
}