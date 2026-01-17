#define NOMINMAX
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <stdio.h>
#include <iostream>
#include <winsock2.h>
#include <string>
#include <sstream>

#pragma comment(lib, "ws2_32.lib")

bool SendString(SOCKET s, const std::string& str)
{
	int total = 0;
	int len = (int)str.length() + 1;
	const char* data = str.c_str();
	while (total < len)
	{
		int sent = send(s, data + total, len - total, 0);
		if (sent == SOCKET_ERROR) return false;
		total += sent;
	}
	return true;
}

std::string RecvString(SOCKET s)
{
	char buf[4096] = { 0 };
	int bytes = recv(s, buf, sizeof(buf) - 1, 0);
	if (bytes <= 0) return std::string();
	buf[bytes] = '\0';
	return std::string(buf);
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	WORD wVersionRequested = MAKEWORD(2, 2);
	WSADATA wsaData;
	if (WSAStartup(wVersionRequested, &wsaData) != 0)
	{
		std::cout << "WSAStartup failed: " << WSAGetLastError() << '\n';
		return 1;
	}

	while (true)
	{
		SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
		if (s == INVALID_SOCKET)
		{
			std::cout << "Socket creation failed: " << WSAGetLastError() << '\n';
			WSACleanup();
			return 1;
		}

		sockaddr_in dest_addr;
		dest_addr.sin_family = AF_INET;
		dest_addr.sin_port = htons(1280);
		dest_addr.sin_addr.s_addr = inet_addr("127.0.0.1");

		if (connect(s, (sockaddr*)&dest_addr, sizeof(dest_addr)) == SOCKET_ERROR)
		{
			std::cout << "Connection failed: " << WSAGetLastError() << '\n';
			closesocket(s);
			WSACleanup();
			return 1;
		}

		int choose = -1;

		do
		{
			std::cout << "1. Добавить преподавателя\n"
				<< "2. Посмотреть всех преподавателей\n"
				<< "3. Удалить преподавателя\n"
				<< "4. Обновить информацию о преподавателе\n"
				<< "5. Поиск преподавателей по предмету (стаж >= 5)\n"
				<< "0. Выйти\n"
				<< "Выбор: ";
			std::cin >> choose;

			if (choose >= 0 && choose <= 5) break;
		} while (true);

		int sendNumber = htonl(choose);
		if (send(s, (char*)&sendNumber, sizeof(sendNumber), 0) == SOCKET_ERROR)
		{
			std::cout << "Ошибка при отправке выбора: " << WSAGetLastError() << '\n';
			closesocket(s);
			continue;
		}

		if (choose == 0)
		{
			closesocket(s);
			break;
		}

		if (choose == 1)
		{
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
			std::string fullname, degree, experience, subject;
			std::cout << "Введите ФИО: "; std::getline(std::cin, fullname);
			std::cout << "Введите степень: "; std::getline(std::cin, degree);
			std::cout << "Введите стаж (число): "; std::getline(std::cin, experience);
			std::cout << "Введите предмет: "; std::getline(std::cin, subject);

			std::string srv = RecvString(s);
			std::string payload = fullname + "|" + degree + "|" + experience + "|" + subject + "|";
			SendString(s, payload);

			std::string response = RecvString(s);
			std::cout << response << '\n';
		}
		else if (choose == 2)
		{
			std::string response = RecvString(s);
			std::cout << response << '\n';
		}
		else if (choose == 3)
		{
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
			std::string fullname;
			std::cout << "Введите ФИО удаляемого преподавателя: "; std::getline(std::cin, fullname);
			std::string srv = RecvString(s);
			SendString(s, fullname);
			std::string response = RecvString(s);
			std::cout << response << '\n';
		}
		else if (choose == 4)
		{
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
			std::string fullname, degree, experience, subject;
			std::cout << "Введите ФИО преподавателя, которого нужно обновить: "; std::getline(std::cin, fullname);
			std::cout << "Введите новую степень: "; std::getline(std::cin, degree);
			std::cout << "Введите новый стаж (число): "; std::getline(std::cin, experience);
			std::cout << "Введите новый предмет: "; std::getline(std::cin, subject);

			std::string srv = RecvString(s); // ожидание "SEND_UPDATE"
			std::string payload = fullname + "|" + degree + "|" + experience + "|" + subject + "|";
			SendString(s, payload);
			std::string response = RecvString(s);
			std::cout << response << '\n';
		}
		else if (choose == 5)
		{
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
			std::string subject;
			std::cout << "Введите название предмета (Math, Physic, Inf): "; std::getline(std::cin, subject);

			std::string srv = RecvString(s); 
			SendString(s, subject);
			std::string response = RecvString(s);
			std::cout << response << '\n';
		}

		closesocket(s);
	}

	WSACleanup();
	return 0;
}
