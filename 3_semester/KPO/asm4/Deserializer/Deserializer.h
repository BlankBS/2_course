#pragma once

#include <fstream>
#include <iostream>
#include <vector>
#include <string>

#define HEADER ".586\n.MODEL FLAT, STDCALL\nincludelib kernel32.lib\nExitProcess PROTO : DWORD\n.STACK 4096\n\n.CONST\n\n.DATA\n\n"
#define FOOTER "\n\n.CODE\nmain PROC\nSTART :\npush - 1\ncall ExitProcess\nmain ENDP\nend main\n"

static class Deserializer
{
public:
	static std::vector<std::pair<std::string, bool>> Deserialization(std::iostream& os);
};