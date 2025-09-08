#include "In.h"
#include "Error.h"
#include <fstream>

namespace In
{
    IN getin(wchar_t infile[])
    {
        IN in;
        in.size = 0;
        in.lines = 1;
        in.ignor = 0;
        in.text = new unsigned char[IN_MAX_LEN_TEXT];

        int table[] = IN_CODE_TABLE;
        for (int i = 0; i < 256; i++) {
            in.code[i] = table[i];
        }

        std::ifstream fin(infile);
        if (!fin.is_open())
            throw ERROR_THROW(110);

        unsigned char c;
        int col = 1;
        while (fin.read((char*)&c, sizeof(c)))
        {
            int code = in.code[(int)c];
            switch (code)
            {
            case IN::F:
                throw ERROR_THROW_IN(111, in.lines, col);
                break;
            case IN::I:
                in.ignor++;
                break;
            case IN::T:
                in.text[in.size++] = c;
                break;
            default:
                in.text[in.size++] = (unsigned char)code;
                break;
            }

            if (c == IN_CODE_ENDL)
            {
                in.lines++;
                col = 1;
            }
            else
            {
                col++;
            }
        }
        in.text[in.size] = '\0';
        fin.close();
        return in;
    }
}