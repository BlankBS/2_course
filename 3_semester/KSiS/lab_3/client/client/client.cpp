#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <stdio.h>
#include <iostream>
#include <winsock2.h>
#include <string>

#pragma comment(lib, "ws2_32.lib")

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

	while (true)
	{
		SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
		sockaddr_in dest_addr;
		dest_addr.sin_family = AF_INET;
		dest_addr.sin_port = htons(1280);
		dest_addr.sin_addr.s_addr = inet_addr("127.0.0.1");

		if (connect(s, (sockaddr*)&dest_addr, sizeof(dest_addr)) == SOCKET_ERROR)
		{
			std::cout << "Connection failed: " << WSAGetLastError() << '\n';
			WSACleanup();
			return 1;
		}

		std::cout << "Доступные предметы: Math, Physic, Inf\n";
		std::cout << "Введите название предмета (или 'exit' для выхода):\n";

		std::string subject;
		std::getline(std::cin, subject);

		if (subject == "exit")
		{
			break;
		}

		send(s, subject.c_str(), subject.length() + 1, 0);

		char buf[1024] = { 0 };
		int bytesReceived = recv(s, buf, sizeof(buf), 0);

		if (bytesReceived > 0)
		{
			std::cout << "\nРезультат поиска:\n";
			std::cout << buf << '\n';
		}
		else
		{
			"Ошибка при получении данных от сервера\n";
		}

		closesocket(s);
	}
	WSACleanup();
	return 0;
}