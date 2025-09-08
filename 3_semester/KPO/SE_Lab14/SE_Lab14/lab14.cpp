#include <iostream>
#include <locale>
#include <cwchar>
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

        In::IN in = In::getin(parm.in);
        Log::WriteIn(log, in);

        std::ofstream out(parm.out);
        if (out.is_open()) {
            out.write((const char*)in.text, in.size);
            out.close();
        }

        Log::Close(log);
    }
    catch (Error::ERROR e)
    {
        Log::WriteError(log, e);
        Log::Close(log);
    }

    return 0;
}