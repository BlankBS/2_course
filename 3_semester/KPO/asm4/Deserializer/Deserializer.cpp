#include "Deserializer.h"
#include <cstdint>
#include <cstring>

std::vector<std::pair<std::string, bool>> Deserializer::Deserialization(std::iostream& os)
{
	std::vector<std::pair<std::string, bool>> des;
	bool boolValue;
	uint8_t len = 0;
	char strValue[11];
	char firstByte;
	int counter = 0;

	std::ofstream writeCode("..\\asm\\lab.asm");
	if (!writeCode.is_open()) {
		std::cerr << "Cannot create assembly file!" << std::endl;
		return des;
	}

	writeCode << HEADER;

	os.read(&firstByte, 1);
	uint8_t type = static_cast<uint8_t>(firstByte);

	while (os)
	{
		switch (type)
		{
		case(0x06):
		{
			os.read(reinterpret_cast<char*>(&len), 1);

			if (len > 10)
			{
				len = 10;
			}

			writeCode << "CHAR_ARR" << counter << "\tDB\t\"";
			for (int i = 0; i < len; i++)
			{
				os.read(&strValue[i], sizeof(char));
				writeCode << strValue[i];
			}
			strValue[len] = '\0';
			writeCode << "\", 0\n";

			std::string str(strValue);
			des.push_back(std::make_pair(str, false));
			counter++;
			break;
		}
		case(0x07):
		{
			os.read(reinterpret_cast<char*>(&boolValue), sizeof(boolValue));

			writeCode << "BOOL_VAL" << counter << "\tDWORD\t" << (boolValue ? "1" : "0") << "\n";
			des.push_back(std::make_pair("", boolValue));
			counter++;
			break;
		}
		default:
			std::cerr << "Unknown type: 0x" << std::hex << static_cast<int>(type) << std::endl;
			break;
		}
		os.read(&firstByte, 1);
		type = static_cast<uint8_t>(firstByte);
	}

	writeCode << FOOTER;
	writeCode.close();

	return des;
}