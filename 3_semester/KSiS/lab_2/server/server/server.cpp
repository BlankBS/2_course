#include <winsock2.h>
#include <iostream>
#include <stdlib.h>
#include <process.h>

#pragma comment(lib, "ws2_32.lib")

const int port = 1024;

int main()
{
	WORD wVerisonRequsted;
	WSADATA wsaData;
	int err;
	wVerisonRequsted = MAKEWORD(2, 2);
	err = WSAStartup(wVerisonRequsted, &wsaData);

	SOCKET s;
	s = socket(AF_INET, SOCK_DGRAM, 0);

	sockaddr_in ad;
	ad.sin_port = htons(port);
	ad.sin_family = AF_INET;
	ad.sin_addr.s_addr = INADDR_ANY;
	if (bind(s, (struct sockaddr*)&ad, sizeof(ad)) == SOCKET_ERROR)
	{
		std::cerr << "Bind failed: " << WSAGetLastError() << '\n';
		closesocket(s);
		WSACleanup();
		return 0;
	}

	std::cout << "Sever started at port " << port << "...\n";

	char buff[200], tmp = '\0';
	sockaddr_in clientAddr;
	int clientAddrSize;

	while (true)
	{
		clientAddrSize = sizeof(clientAddr);
		int bytesReceived = recvfrom(s, buff, sizeof(buff), 0, (struct sockaddr*)&clientAddr, &clientAddrSize);

		if (bytesReceived == SOCKET_ERROR)
		{
			std::cerr << "recvfrom failed: " << WSAGetLastError() << '\n';
			continue;
		}

		buff[bytesReceived] = '\0';
		std::string receivedStr(buff);

		std::cout << "Received from client: " << receivedStr << '\n';
		std::cout << "Length: " << receivedStr.length() << '\n';

		std::string response;
		if (receivedStr.length() > 7)
		{
			response = '{' + receivedStr + '}';
			std::cout << "Sending response: " << response << '\n';
		}
		else
		{
			response = receivedStr;
			std::cout << "Received string length less then 7, sending back unchanged\n";
		}

		sendto(s, response.c_str(), response.length() + 1, 0, (struct sockaddr*)&clientAddr, clientAddrSize);
	}

	closesocket(s);

	WSACleanup();

	return 0;
}