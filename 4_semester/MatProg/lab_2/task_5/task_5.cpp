#include <iostream>
#include <iomanip> 
#include "Salesman.h"
#include "Auxil.h"

#define SPACE(n) std::setw(n)<<" "

void first_task()
{
	char  AA[][2] = { "A", "B", "C", "D" };
	std::cout << std::endl << " --- Генератор перестановок ---";
	std::cout << std::endl << "Исходное множество: ";
	std::cout << "{ ";
	for (int i = 0; i < sizeof(AA) / 2; i++)

		std::cout << AA[i] << ((i < sizeof(AA) / 2 - 1) ? ", " : " ");
	std::cout << "}";
	std::cout << std::endl << "Генерация перестановок ";
	combi::permutation p(sizeof(AA) / 2);
	__int64  n = p.getfirst();
	while (n >= 0)
	{
		std::cout << std::endl << std::setw(4) << p.np << ": { ";

		for (int i = 0; i < p.n; i++)

			std::cout << AA[p.ntx(i)] << ((i < p.n - 1) ? ", " : " ");

		std::cout << "}";

		n = p.getnext();
	};
	std::cout << std::endl << "всего: " << p.count() << std::endl;
}

void second_task()
{ 
	const int N = 5;
	int d[N][N] = {
				  {INF, 13, 32, 16, 9},
				  {14, INF, 25, 27, 55},
				  {20, 26, INF, 72, 50},
				  {31, 42, 39, INF, 26},
				  {72, 45, 45, 25, INF}
	};

	int r[N];                     // результат 
	int s = salesman(
		N,          // [in]  количество городов 
		(int*)d,          // [in]  массив [n*n] расстояний 
		r           // [out] массив [n] маршрут 0 x x x x  

	);
	std::cout << std::endl << "-- Задача коммивояжера -- ";
	std::cout << std::endl << "-- количество  городов: " << N;
	std::cout << std::endl << "-- матрица расстояний : ";
	for (int i = 0; i < N; i++)
	{
		std::cout << std::endl;
		for (int j = 0; j < N; j++)

			if (d[i][j] != INF) std::cout << std::setw(3) << d[i][j] << " ";

			else std::cout << std::setw(3) << "INF" << " ";
	}
	std::cout << std::endl << "-- оптимальный маршрут: ";
	for (int i = 0; i < N; i++) std::cout << r[i] + 1 << "-->"; std::cout << 1;
	std::cout << std::endl << "-- длина маршрута     : " << s;
	std::cout << std::endl;
}

void third_task()
{
	const int N = 12;
	int d[N * N + 1], r[N];
	auxil::start();
	for (int i = 0; i <= N * N; i++) d[i] = auxil::iget(10, 100);
	std::cout << std::endl << "-- Задача коммивояжера -- ";
	std::cout << std::endl << "-- количество ------ продолжительность -- ";
	std::cout << std::endl << "      городов           вычисления  ";
	clock_t t1, t2;
	for (int i = 7; i <= N; i++)
	{
		t1 = clock();
		salesman(i, (int*)d, r);
		t2 = clock();
		std::cout << std::endl << SPACE(7) << std::setw(2) << i
			<< SPACE(15) << std::setw(5) << (t2 - t1);
	}
	std::cout << std::endl;
}

int main()
{
	setlocale(LC_CTYPE, "rus");
	std::cout << "Первое задание:\n";
	first_task();
	std::cout << "\nПервое задание:\n";
	second_task();
	std::cout << "\nТретье задание:\n";
	third_task();
	system("pause");
	return 0;
}