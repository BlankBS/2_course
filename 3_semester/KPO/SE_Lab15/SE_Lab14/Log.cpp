#include "Log.h"
#include <iostream>
#include <ctime>
#include <string>
#include <cstdarg>

namespace Log
{
    LOG getlog(wchar_t logfile[])
    {
        LOG log;
        wcscpy_s(log.logfile, logfile);
        log.stream = new std::ofstream(logfile);
        if (!log.stream->is_open()) {
            delete log.stream;
            throw ERROR_THROW(112);
        }
        return log;
    }

    void WriteLine(LOG log, const char* c, ...)
    {
        char buffer[1024];
        va_list args;
        va_start(args, c);
        vsprintf_s(buffer, c, args);
        va_end(args);
        *log.stream << buffer;
    }

    void WriteLine(LOG log, const wchar_t* c, ...)
    {
        wchar_t wbuffer[1024];
        va_list args;
        va_start(args, c);
        vswprintf_s(wbuffer, c, args);
        va_end(args);

        char buffer[2048];
        size_t convertedChars = 0;
        wcstombs_s(&convertedChars, buffer, sizeof(buffer), wbuffer, _TRUNCATE);
        *log.stream << buffer;
    }

    void WriteLog(LOG log)
    {
        time_t rawtime;
        struct tm timeinfo;
        char buffer[80];

        time(&rawtime);
        localtime_s(&timeinfo, &rawtime);

        strftime(buffer, sizeof(buffer), "%d.%m.%Y %H:%M:%S", &timeinfo);
        *log.stream << "--- Протокол --- " << buffer << " ---\n";
    }

    void WriteParm(LOG log, Parm::PARM parm)
    {
        *log.stream << "Параметры:\n";
        WriteLine(log, L"-in: %s\n", parm.in);
        WriteLine(log, L"-out: %s\n", parm.out);
        WriteLine(log, L"-log: %s\n", parm.log);
    }

    void WriteIn(LOG log, In::IN in)
    {
        *log.stream << "Исходные данные:\n";
        *log.stream << "Количество символов: " << in.size << "\n";
        *log.stream << "Количество строк: " << in.lines << "\n";
        *log.stream << "Проигнорировано: " << in.ignor << "\n";
    }

    void WriteError(LOG log, Error::ERROR error)
    {
        if (log.stream && log.stream->is_open())
        {
            *log.stream << "Ошибка " << error.id << ": " << error.message;
            if (error.inext.line != -1) {
                *log.stream << ", строка " << error.inext.line << ", позиция " << error.inext.col;
            }
            *log.stream << "\n";
        }
        else
        {
            std::cout << "Ошибка " << error.id << ": " << error.message;
            if (error.inext.line != -1) {
                std::cout << ", строка " << error.inext.line << ", позиция " << error.inext.col;
            }
            std::cout << "\n";
        }
    }

    void Close(LOG log)
    {
        if (log.stream)
        {
            log.stream->close();
            delete log.stream;
        }
    }
}