#pragma once

#include <fstream>
#include <cstdint>
#include <cstring>

static class Serializer
{
public: 
	static size_t Serialize(char arr[], std::ostream& os);
	static size_t Serialize(bool boolValue, std::ostream& os);
};