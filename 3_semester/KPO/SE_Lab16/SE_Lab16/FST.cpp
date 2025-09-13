#include "FST.h"
#include <cstring>

namespace FST
{
    bool execute(FST& fst)
    {
        int len = (int)strlen(fst.string);
        short* prev = new short[fst.nstates];
        short* next = new short[fst.nstates];

        for (int i = 0; i < fst.nstates; i++) prev[i] = -1;
        prev[0] = 0;

        for (fst.position = 0; fst.position < len; fst.position++)
        {
            // Пропускаем все пробелы
            while (fst.position < len && fst.string[fst.position] == ' ')
                fst.position++;

            if (fst.position >= len) break;

            char sym = fst.string[fst.position];

            for (int i = 0; i < fst.nstates; i++) next[i] = -1;

            for (int i = 0; i < fst.nstates; i++)
            {
                if (prev[i] == fst.position)
                {
                    for (int j = 0; j < fst.nodes[i].n_relation; j++)
                    {
                        RELATION rel = fst.nodes[i].relations[j];
                        if (rel.symbol == sym)
                            next[rel.nnode] = fst.position + 1;
                    }
                }
            }

            for (int i = 0; i < fst.nstates; i++) prev[i] = next[i];
        }

        for (int i = 0; i < fst.nstates; i++)
            fst.rstates[i] = prev[i];

        bool result = (fst.rstates[fst.nstates - 1] >= 0);

        delete[] prev;
        delete[] next;
        return result;
    }
}
