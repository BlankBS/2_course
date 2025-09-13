#include "In.h"
#include "Error.h"
#include <fstream>
#include <string>

namespace In
{
    IN getin(Parm::PARM parm)
    {
        IN in_stats = { 0, 0, 0, 0, nullptr, {0} };
        int table[] = IN_CODE_TABLE;

        std::ifstream fin(parm.in);
        if (!fin.is_open())
            throw ERROR_THROW(110);

        std::ofstream out(parm.out);
        if (!out.is_open())
            throw Error::geterror(112);

        bool first_line = true;
        std::string line;

        char c;
        std::string current_line;
        int line_number = 1;
        int col_number = 1;
        bool error_in_current_line = false;

        while (fin.get(c))
        {
            unsigned char uc = static_cast<unsigned char>(c);
            int code = table[uc];

            switch (code)
            {
            case IN::F:
                if (!error_in_current_line) {
                    out << "ÎØÈÁÊÀ:ñòðîêà=" << line_number << ", ïîçèöèÿ=" << col_number << "|";
                    error_in_current_line = true;
                }
                in_stats.errors++;
                current_line += c;
                break;

            case IN::I:
                in_stats.ignor++;
                break;

            case IN::T:
                current_line += c;
                break;
            default:
                current_line += static_cast<unsigned char>(code);
                break;
            }

            if (c == '\n' || c == '\r')
            {
                if (!error_in_current_line) {
                    //if (!first_line) out << "|";
                    out << current_line;
                    in_stats.size += current_line.length();
                }

                current_line.clear();
                line_number++;
                col_number = 1;
                error_in_current_line = false;
                first_line = false;

                if (c == '\r' && fin.peek() == '\n') {
                    fin.get(c);
                }
            }
            else
            {
                col_number++;
            }
        }

        if (!current_line.empty())
        {
            if (!error_in_current_line) {
                if (!first_line) out << "|";
                out << current_line;
                in_stats.size += current_line.length();
            }
        }

        fin.close();
        out.close();

        return in_stats;
    }
}
