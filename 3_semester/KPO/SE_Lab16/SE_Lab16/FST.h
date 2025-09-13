#pragma once
#include <iostream>

namespace FST
{
    struct RELATION
    {
        char symbol;
        short nnode;
        RELATION(char c = 0, short ns = 0) : symbol(c), nnode(ns) {}
    };

    struct NODE
    {
        short n_relation;
        RELATION* relations;

        NODE() : n_relation(0), relations(nullptr) {}

        NODE(short nm, RELATION rels[])
        {
            n_relation = nm;
            relations = new RELATION[nm];
            for (int i = 0; i < nm; i++)
                relations[i] = rels[i];
        }
    };

    struct FST
    {
        char* string;
        short position;
        short nstates;
        NODE* nodes;
        short* rstates;

        FST(char* s, short ns, NODE ns_arr[])
            : string(s), position(0), nstates(ns)
        {
            nodes = new NODE[ns];
            for (int i = 0; i < ns; i++)
            {
                nodes[i].n_relation = ns_arr[i].n_relation;
                if (ns_arr[i].n_relation > 0)
                {
                    nodes[i].relations = new RELATION[ns_arr[i].n_relation];
                    for (int j = 0; j < ns_arr[i].n_relation; j++)
                        nodes[i].relations[j] = ns_arr[i].relations[j];
                }
                else
                    nodes[i].relations = nullptr;
            }

            rstates = new short[ns];
            for (int i = 0; i < ns; i++)
                rstates[i] = -1;
        }

        ~FST()
        {
            for (int i = 0; i < nstates; i++)
                delete[] nodes[i].relations;
            delete[] nodes;
            delete[] rstates;
        }
    };

    bool execute(FST& fst);
}
