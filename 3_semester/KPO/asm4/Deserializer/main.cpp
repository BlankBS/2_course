#include "Deserializer.h"
#include <fstream>
#include <iostream>

int main()
{
	std::fstream file("..\\asm4\\stream.bin", std::ios::in | std::ios::binary);
	if (!file.is_open())
	{
		std::cerr << "Cannot open stream.bin!" << std::endl;
		return 1;
	}

	std::vector<std::pair<std::string, bool>> result = Deserializer::Deserialization(file);
	file.close();

	std::cout << "Deserialized data:" << std::endl;
	for (size_t i = 0; i < result.size(); i++)
	{
		if (!result[i].first.empty())
		{
			std::cout << "Char array " << i << ": " << result[i].first << std::endl;
		}
		else
		{
			std::cout << "Bool " << i << ": " << (result[i].second ? "true" : "false") << std::endl;
		}
	}

	return 0;
}