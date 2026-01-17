#include <iostream>
#include <locale>

#include "Error.h"
#include "Parm.h"
#include "Log.h"
#include "In.h"

int wmain(int argc, wchar_t* argv[])
{
    setlocale(LC_ALL, "rus");

    Log::LOG log = Log::INITLOG;

    try
    {
        Parm::PARM parm = Parm::getparm(argc, argv);
        log = Log::getlog(parm.log);
        Log::WriteLog(log);
        Log::WriteParm(log, parm);
        Log::WriteLine(log, "----------------------------------------\n");

        In::IN stats = In::getin(parm);

        Log::WriteLine(log, "Анализ файла завершен.\n");
        Log::WriteLine(log, "Всего строк обработано: %d\n", stats.lines);
        Log::WriteLine(log, "Всего записано символов: %d\n", stats.size);
        Log::WriteLine(log, "Всего символов проигнорировано: %d\n", stats.ignor);
        Log::WriteLine(log, "Обнаружено строк с ошибками: %d\n", stats.errors);

        Log::Close(log);
    }
    catch (Error::ERROR e)
    {
        Log::WriteError(log, e);
        if (log.stream) {
            Log::Close(log);
        }
    }

    return 0;
}