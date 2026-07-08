// ConsoleApplication1.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

int main()
{
	char str[] = "123";

	int n = 4;
	for (int i = 0; i < n; i++)
	{
		char x = str[n - 1];
		for (int k = n - 1; k > 0; k--)
		{
			str[k] = str[k - 1];
		}
		str[0] = x;
	}

	std::cout << str;
}