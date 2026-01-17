#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <stdio.h>
#include <string>
#include <winsock2.h>
#include <windows.h>
#include <iostream>

#pragma comment(lib, "ws2_32.lib")

int main()
{
    char buff[200] = {0};

    WORD wVerisonRequested;
    WSADATA wsaData;
    wVerisonRequested = MAKEWORD(2, 2);
    if (WSAStartup(wVerisonRequested, &wsaData) != 0)
    {
        std::cerr << "WSAStartup failed\n";
        return 0;
    }

    SOCKET clientSocket = socket(AF_INET, SOCK_DGRAM, 0);
    if (clientSocket == INVALID_SOCKET)
    {
        std::cerr << "Socket creation failed\n";
        WSACleanup();
        return 0;
    }

    sockaddr_in add;
    add.sin_family = AF_INET;
    add.sin_port = htons(1024);
    add.sin_addr.s_addr = inet_addr("127.0.0.1");
    int addLength = sizeof(add);

    while (true)
    {
        std::cout << "Enter the string ('exit' to quit)\n";
        std::string input;
        std::getline(std::cin, input);

        if (input == "exit")
        {
            break;
        }

        sendto(clientSocket, input.c_str(), input.length() + 1, 0, (struct sockaddr*)&add, addLength);

        //memset(buff, 0, sizeof(buff));
        int bytesReceived = recvfrom(clientSocket, buff, sizeof(buff) - 1, 0, (struct sockaddr*)&add, &addLength);

        if (bytesReceived == SOCKET_ERROR)
        {
            std::cerr << "recvfrom failed: " << WSAGetLastError() << '\n';
            continue;
        }

        buff[bytesReceived] = '\0';
        std::cout << "Server response: " << buff << "\n\n";
    }

    closesocket(clientSocket);
    WSACleanup();
    return 0;
}