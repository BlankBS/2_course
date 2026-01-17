#include "Parm.h"
#include "Error.h"
#include <wchar.h>
#include <string>

namespace Parm
{
    PARM getparm(int argc, wchar_t* argv[])
    {
        PARM parm;

        parm.in[0] = L'\0';
        parm.out[0] = L'\0';
        parm.log[0] = L'\0';

        for (int i = 1; i < argc; i++)
        {
            if (wcsstr(argv[i], PARM_IN))
            {
                if (wcslen(argv[i]) > PARM_MAX_SIZE) throw ERROR_THROW(104);
                wcscpy_s(parm.in, argv[i] + wcslen(PARM_IN));
            }
            else if (wcsstr(argv[i], PARM_OUT))
            {
                if (wcslen(argv[i]) > PARM_MAX_SIZE) throw ERROR_THROW(104);
                wcscpy_s(parm.out, argv[i] + wcslen(PARM_OUT));
            }
            else if (wcsstr(argv[i], PARM_LOG))
            {
                if (wcslen(argv[i]) > PARM_MAX_SIZE) throw ERROR_THROW(104);
                wcscpy_s(parm.log, argv[i] + wcslen(PARM_LOG));
            }
        }

        if (parm.in[0] == L'\0') throw ERROR_THROW(100);

        if (parm.out[0] == L'\0')
        {
            wcscpy_s(parm.out, parm.in);
            wcsncat_s(parm.out, PARM_OUT_DEFAULT_EXT, PARM_MAX_SIZE - wcslen(parm.out) - 1);
        }

        if (parm.log[0] == L'\0')
        {
            wcscpy_s(parm.log, parm.in);
            wcsncat_s(parm.log, PARM_LOG_DEFAULT_EXT, PARM_MAX_SIZE - wcslen(parm.log) - 1);
        }

        return parm;
    }
}