#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS
#define NOMINMAX
#include <iostream>
#include <winsock2.h>
#include <cstring>
#include <algorithm>

#pragma comment(lib, "ws2_32.lib")

int main()
{
	WSADATA wsaData;
	WSAStartup(MAKEWORD(2, 2), &wsaData);

	SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
	if (s == INVALID_SOCKET) {
		std::cerr << "Socket error: " << WSAGetLastError() << std::endl;
		return 1;
	}

	sockaddr_in local;
	local.sin_family = AF_INET;
	local.sin_port = htons(1280);
	local.sin_addr.s_addr = htonl(INADDR_ANY);

	if (bind(s, (sockaddr*)&local, sizeof(local)) == SOCKET_ERROR) {
		std::cerr << "Bind error: " << WSAGetLastError() << std::endl;
		closesocket(s);
		WSACleanup();
		return 1;
	}

	if (listen(s, 5) == SOCKET_ERROR) {
		std::cerr << "Listen error: " << WSAGetLastError() << std::endl;
		closesocket(s);
		WSACleanup();
		return 1;
	}

	std::cout << "Server started on port 1280. Waiting for connections..." << std::endl;

	while (true) {
		sockaddr_in remote_addr;
		int addr_len = sizeof(remote_addr);
		SOCKET s2 = accept(s, (sockaddr*)&remote_addr, &addr_len);

		if (s2 == INVALID_SOCKET) {
			std::cerr << "Accept error: " << WSAGetLastError() << std::endl;
			continue;
		}

		std::cout << "Client connected!" << std::endl;

		char b[255];
		int bytes_received;

		while ((bytes_received = recv(s2, b, sizeof(b) - 1, 0)) > 0) {
			b[bytes_received] = '\0';

			std::cout << "Received: " << b << std::endl;

			int x1, y1, x2, y2, x, y;
			if (sscanf(b, "%d %d %d %d %d %d", &x1, &y1, &x2, &y2, &x, &y) == 6)
			{
				int minX = std::min(x1, x2), maxX = std::max(x1, x2);
				int minY = std::min(y1, y2), maxY = std::max(y1, y2);
				
				const char* result = (x >= minX && x <= maxX && y >= minY && y <= maxY) ? "YES" : "NO";

				send(s2, result, strlen(result), 0);
				std::cout << "Sent: " << result << '\n';
			}
			else
			{
				const char* err = "Invalid input";
				send(s2, err, strlen(err), 0);
			}
		}

		if (bytes_received == 0) {
			std::cout << "Client disconnected" << std::endl;
		}
		else {
			std::cerr << "Recv error: " << WSAGetLastError() << std::endl;
		}

		closesocket(s2);
	}

	closesocket(s);
	WSACleanup();
	return 0;
}