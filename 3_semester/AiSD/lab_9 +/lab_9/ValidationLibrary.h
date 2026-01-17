#pragma once

#include <string>

#ifdef VALIDATIONLIBRARY_EXPORTS
#define VALIDATIONLIBRARY_API __declspec(dllexport)
#else
#define VALIDATIONLIBRARY_API __declspec(dllimport)
#endif

namespace ValidationLibrary
{
	class VALIDATIONLIBRARY_API Validator
	{
	public:
		void ValidatedIntInput(const std::string& EnterMessage, int& EnterVariable, std::string ErrorMessage);
		bool isNumber(const std::string& str);
		bool isInteger(const std::string& str);

	private:
		bool CheckOnLetter();
	};
}