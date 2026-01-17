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
	{"Ivanov Ivan Ivanovich", "Доктор наук", 10, "Math"},
	{"Petrov Petr Petrovich", "Кандидат наук", 8, "Physic"},
	{"Sidorov Sidr Sidorovich", "Доктор наук", 15, "Math"},
	{"Vladimirov Vladimir Vladimirovich", "Кандидат наук", 3, "Physic"},
	{"Dmitriev Dmitriy Dmitrievich", "Доктор наук", 12, "Inf"},
	{"Igorev Igor Igorevich", "Кандидат наук", 6, "Math"},
	{"Alexandrov Alexander Alexandrovich", "Доктор наук", 20, "Physic"},
	{"Pavlov Pavel Pavlovich", "Кандидат наук", 4, "Inf"}
};

int numcl = 0;

void print_clients()
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

std::string RecvString(SOCKET s)
{
	char buf[2048] = { 0 };
	int bytes = recv(s, buf, sizeof(buf) - 1, 0);
	if (bytes <= 0) return std::string();
	buf[bytes] = '\0';
	return std::string(buf);
}

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

std::string BuildAllTeachersList()
{
	std::stringstream ss;
	ss << "Список преподавателей:\n";
	for (const Teacher& t : teachers)
	{
		ss << "-------------------------\n";
		ss << "ФИО: " << t.fullname << '\n';
		ss << "Степень: " << t.degree << '\n';
		ss << "Стаж: " << t.experience << '\n';
		ss << "Предмет: " << t.subject << '\n';
	}
	return ss.str();
}

void ProcessRequest(int choose, SOCKET s)
{
	switch (choose)
	{
	case 1:
	{
		SendString(s, "SEND_ADD");
		std::string data = RecvString(s);
		if (data.empty())
		{
			SendString(s, "Ошибка: не получили данные для добавления.");
			break;
		}
		std::stringstream ss(data);
		std::string fullname, degree, experience_s, subject;
		std::getline(ss, fullname, '|');
		std::getline(ss, degree, '|');
		std::getline(ss, experience_s, '|');
		std::getline(ss, subject, '|');

		int exp = 0;
		try { exp = std::stoi(experience_s); }
		catch (...) { exp = 0; }

		Teacher t{ fullname, degree, exp, subject };
		teachers.push_back(t);
		SendString(s, "Преподаватель добавлен.");
		break;
	}
	case 2:
	{
		std::string out = BuildAllTeachersList();
		SendString(s, out);
		break;
	}
	case 3:
	{
		SendString(s, "SEND_DELETE");
		std::string fullname = RecvString(s);
		if (fullname.empty())
		{
			SendString(s, "Ошибка: не получили ФИО для удаления.");
			break;
		}
		bool found = false;
		for (auto it = teachers.begin(); it != teachers.end(); ++it)
		{
			if (it->fullname == fullname)
			{
				teachers.erase(it);
				found = true;
				break;
			}
		}
		if (found) SendString(s, "Преподаватель удалён.");
		else SendString(s, "Преподаватель с таким ФИО не найден.");
		break;
	}
	case 4:
	{
		SendString(s, "SEND_UPDATE");
		std::string data = RecvString(s);
		if (data.empty())
		{
			SendString(s, "Ошибка: не получили данные для обновления.");
			break;
		}
		std::stringstream ss(data);
		std::string fullname, degree, experience_s, subject;
		std::getline(ss, fullname, '|');
		std::getline(ss, degree, '|');
		std::getline(ss, experience_s, '|');
		std::getline(ss, subject, '|');

		int exp = 0;
		try { exp = std::stoi(experience_s); }
		catch (...) { exp = 0; }

		bool found = false;
		for (Teacher& t : teachers)
		{
			if (t.fullname == fullname)
			{
				t.degree = degree;
				t.experience = exp;
				t.subject = subject;
				found = true;
				break;
			}
		}
		if (found) SendString(s, "Информация обновлена.");
		else SendString(s, "Преподаватель с таким ФИО не найден.");
		break;
	}
	case 5:
	{
		SendString(s, "SEND_SUBJECT");
		std::string subject = RecvString(s);
		if (subject.empty())
		{
			SendString(s, "Ошибка: не получили предмет.");
			break;
		}
		std::stringstream out;
		out << "Результат поиска по предмету: " << subject << '\n';
		bool any = false;
		for (const Teacher& t : teachers)
		{
			if (_stricmp(t.subject.c_str(), subject.c_str()) == 0 && t.experience >= 5)
			{
				out << t.fullname << " (стаж " << t.experience << ")\n";
				any = true;
			}
		}
		if (!any) out << "Ничего не найдено.\n";
		SendString(s, out.str());
		break;
	}
	default:
	{
		SendString(s, "Неверный выбор.");
		break;
	}
	}
}

DWORD WINAPI ThreadFunc(LPVOID client_socket)
{
	SOCKET s2 = *((SOCKET*)client_socket);
	delete (SOCKET*)client_socket;
	print_clients();

	while (true)
	{
		int ReceivedNumber = 0;
		int bytesReceived = recv(s2, (char*)&ReceivedNumber, sizeof(ReceivedNumber), 0);
		if (bytesReceived <= 0)
		{
			break;
		}

		int choose = ntohl(ReceivedNumber);
		if (choose == 0)
		{
			std::cout << "Клиент запросил завершение соединения\n";
			break;
		}

		std::cout << "Получен выбор клиента: " << choose << '\n';

		ProcessRequest(choose, s2);
	}

	numcl--;
	//print_clients();

	closesocket(s2);
	return 0;
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

	SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
	if (s == INVALID_SOCKET)
	{
		std::cout << "Socket creation failed: " << WSAGetLastError() << '\n';
		WSACleanup();
		return 1;
	}

	sockaddr_in local_addr;
	local_addr.sin_family = AF_INET;
	local_addr.sin_port = htons(1280);
	local_addr.sin_addr.s_addr = INADDR_ANY;

	if (bind(s, (sockaddr*)&local_addr, sizeof(local_addr)) == SOCKET_ERROR)
	{
		std::cout << "Bind failed: " << WSAGetLastError() << '\n';
		closesocket(s);
		WSACleanup();
		return 1;
	}

	if (listen(s, 5) == SOCKET_ERROR)
	{
		std::cout << "Listen failed: " << WSAGetLastError() << '\n';
		closesocket(s);
		WSACleanup();
		return 1;
	}

	std::cout << "Server started and listening on port 1280\n";

	while (true)
	{
		sockaddr_in client_addr;
		int client_addr_size = sizeof(client_addr);
		SOCKET client_socket = accept(s, (sockaddr*)&client_addr, &client_addr_size);
		if (client_socket == INVALID_SOCKET)
		{
			std::cout << "Accept failed: " << WSAGetLastError() << '\n';
			continue;
		}

		numcl++;
		print_clients();

		SOCKET* ClientSock = new SOCKET;
		*ClientSock = client_socket;

		DWORD thID;
		HANDLE hThread = CreateThread(NULL, 0, ThreadFunc, ClientSock, 0, &thID);
		if (hThread == NULL)
		{
			std::cout << "Thread creation failed\n";
			closesocket(client_socket);
			delete ClientSock;
			numcl--;
			print_clients();
		}
		else 
		{
			CloseHandle(hThread);
		}
	}

	closesocket(s);
	WSACleanup();
	return 0;
}
