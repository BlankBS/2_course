#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <iostream>
#include <winsock2.h>
#include <string>
#include <vector>
#include <sstream>

#pragma comment(lib, "ws2_32.lib")

struct Teacher
{
	std::string fullname;
	std::string degree;
	int experience;
	std::string subject;
};

std::vector<Teacher> teachers =
{
	{"Иванов Иван Иванович", "Доктор наук", 10, "Math"},
	{"Петров Петр Петрович", "Кандидат наук", 8, "Physic"},
	{"Сидорова Анна Сергеевна", "Доктор наук", 15, "Math"},
	{"Кузнецова Ольга Владимировна", "Кандидат наук", 3, "Physic"},
	{"Смирнов Алексей Дмитриевич", "Доктор наук", 12, "Inf"},
	{"Васильева Екатерина Игоревна", "Кандидат наук", 6, "Math"},
	{"Николаев Дмитрий Александрович", "Доктор наук", 20, "Physic"},
	{"Павлова Мария Петровна", "Кандидат наук", 4, "Inf"}
};

std::string ProcessRequest(const std::string subject)
{
	std::stringstream result;
	bool found = false;

	if (subject.empty() || subject == "exit")
	{
		return "Неверный запрос\n";
	}

	result << "Преподаватель предмета '" << subject << "' со стажем >= 5 лет:\n";
	
	for (const Teacher& teacher : teachers)
	{
		if (teacher.subject == subject && teacher.experience >= 5)
		{
			result << '\n';
			result << "ФИО: " << teacher.fullname << '\n';
			result << "Степень: " << teacher.degree << '\n';
			result << "Стаж: " << teacher.experience << '\n';
			found = true;
		}
	}

	if (!found)
	{
		result << "Преподаватели по данному предмету со стажем >= 5 лет не найдены\n";
	}

	return result.str();
}

int numcl = 0;

void print()
{
	if (numcl)
	{
		std::cout << numcl << " client(s) connected\n";
	}
	else
	{
		std::cout << "No clients connected\n";
	}
}

DWORD WINAPI ThreadFunc(LPVOID client_socket)
{
	SOCKET s2 = ((SOCKET*)client_socket)[0];
	char buf[256] = { 0 };
	int bytesReceived;
	while (bytesReceived = recv(s2, buf, sizeof(buf), 0) > 0)
	{
		std::string subject(buf);
	
		if (subject.empty() || subject == "exit")
		{
			std::cout << "Клиент отключился\n";
			break;
		}

		std::cout << "Получен запрос по предмету " << subject << '\n';

		std::string response = ProcessRequest(subject);
		std::cout << "Отправленный ответ:\n" << response << '\n';

		send(s2, response.c_str(), response.length() + 1, 0);
	}

	numcl--;
	print();

	closesocket(s2);
	return 0;
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	WORD wVersionRequested;
	WSADATA wsaData;
	wVersionRequested = MAKEWORD(2, 2);
	if (WSAStartup(wVersionRequested, &wsaData) != 0)
	{
		std::cout << "WSAStartup failed: " << WSAGetLastError() << '\n';
		return 1;
	}

	SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
	sockaddr_in local_addr;
	local_addr.sin_family = AF_INET;
	local_addr.sin_port = htons(1280);
	local_addr.sin_addr.s_addr = INADDR_ANY;

	if (bind(s, (sockaddr*)&local_addr, sizeof(local_addr)) == SOCKET_ERROR)
	{
		std::cout << "Bind failed: " << WSAGetLastError() << '\n';
		WSACleanup();
		return 1;
	}

	int c = listen(s, 5);
	std::cout << "Server started and listening on port 1280\n";
	std::cout << "Доступные предметы: Math, Physic, Inf\n\n";

	SOCKET client_socket;
	sockaddr_in client_addr;
	int client_addr_size = sizeof(client_addr);

	while ((client_socket = accept(s, (sockaddr*)&client_addr, &client_addr_size)))
	{
		if (client_socket == INVALID_SOCKET)
		{
			std::cout << "Accept failed: " << WSAGetLastError() << '\n';
			continue;
		}

		numcl++;
		print();

		DWORD thID;
		HANDLE hThread = CreateThread(NULL, NULL, ThreadFunc, &client_socket, NULL, &thID);

		if (hThread == NULL)
		{
			std::cout << "Thread creation failed\n";
			closesocket(client_socket);
			numcl--;
			print();
		}
		else
		{
			CloseHandle(hThread);
		}
	}

	closesocket(s);
	WSACleanup();
}