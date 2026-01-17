#include "Serializer.h"
#include <iostream>
#include <vector>

int main()
{
	std::ifstream inputFile("D:\\BSTU\\2_course\\3_semester\\KPO\\asm4\\asm4\\stream.bin", std::ios::binary);
	char testArray[] = "VeraVibe";
	bool testBool = true;

	std::ofstream file("stream.bin", std::ios::binary);

	if (file.is_open())
	{
		Serializer::Serialize(testArray, file);
		Serializer::Serialize(testBool, file);

		file.close();

		std::cout << "Data serialized to stream.bin" << std::endl;
        
        std::cout << "Original char array: " << testArray << std::endl;
        std::cout << "Original bool: " << (testBool ? "true" : "false") << std::endl;
	}

	return 0;
}