#include "Serializer.h"

size_t Serializer::Serialize(char arr[], std::ostream& os)
{
	auto pos = os.tellp();
	std::uint8_t byteVal = 0x06;
	size_t maxLength = 10;

	size_t actualLength = strlen(arr);
	if (actualLength > maxLength)
	{
		actualLength = maxLength;
	}

	os.write(reinterpret_cast<const char*>(&byteVal), 1);

	std::uint8_t len = static_cast<std::uint8_t>(actualLength);
	os.write(reinterpret_cast<const char*>(&len), sizeof(len));

	os.write(arr, actualLength);

	return static_cast<size_t>(os.tellp() - pos);
}

size_t Serializer::Serialize(bool boolValue, std::ostream& os)
{
	auto pos = os.tellp();
	std::uint8_t byteVal = 0x07;

	os.write(reinterpret_cast<const char*>(&byteVal), 1);

	os.write(reinterpret_cast<const char*>(&boolValue), sizeof(boolValue));

	return static_cast<size_t>(os.tellp() - pos);
}