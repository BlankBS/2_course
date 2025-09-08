#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS

#include <winsock2.h>
#include <iostream>
#include <cstring>

#pragma comment(lib, "ws2_32.lib")

int main()
{
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    sockaddr_in peer;
    peer.sin_family = AF_INET;
    peer.sin_port = htons(1280);
    peer.sin_addr.s_addr = inet_addr("127.0.0.1");

    SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
    if (connect(s, (sockaddr*)&peer, sizeof(peer)) == SOCKET_ERROR) {
        std::cerr << "Connect error: " << WSAGetLastError() << std::endl;
        return 1;
    }

    int x1, y1, x2, y2, x, y;
    std::cout << "Rectangle(x1 y1 x2 y2): ";
    std::cin >> x1 >> y1 >> x2 >> y2;
    std::cout << "Point(x y): ";
    std::cin >> x >> y;

    char buff[255], b[255];
    sprintf(buff, "%d %d %d %d %d %d", x1, y1, x2, y2, x, y);

    send(s, buff, strlen(buff), 0);

    int bytes_received = recv(s, b, sizeof(b) - 1, 0);
    if (bytes_received > 0)
    {
        b[bytes_received] = '\0';
        std::cout << "Server response: " << b << std::endl;
    }
    else if (bytes_received == 0) {
        std::cout << "Server closed connection" << std::endl;
    }
    else {
        std::cerr << "Recv error: " << WSAGetLastError() << std::endl;
    }

    closesocket(s);
    WSACleanup();
    return 0;
}
