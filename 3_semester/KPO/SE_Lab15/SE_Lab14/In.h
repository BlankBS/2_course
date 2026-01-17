#pragma once
#define IN_MAX_LEN_TEXT 1024*1024
#define IN_CODE_ENDL '\n'

#include "Parm.h"

#define IN_CODE_TABLE { \
/* 0-15   */ IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::T, '|', IN::F, IN::F, IN::I, IN::F, IN::F, \
/* 16-31  */ IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 32-47  */ IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 48-63  */ IN::T, IN::F, IN::T, IN::F, IN::F, IN::F, IN::F, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 64-79  */ IN::F, IN::T, '#', IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::T, IN::F, IN::F, \
/* 80-95  */ IN::F, IN::F, IN::F, IN::T, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 96-111 */ IN::F, IN::T, IN::F, IN::T, IN::T, IN::T, IN::F, IN::F, IN::T, IN::T, IN::F, IN::T, IN::T, IN::T, IN::T, IN::T, \
/* 112-127*/ IN::F, IN::F, IN::T, IN::T, IN::T, IN::F, IN::T, IN::F, IN::T, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 128-143*/ IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 144-159*/ IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 160-175*/ IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 176-191*/ IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
/* 192-207*/ IN::T, IN::T, IN::F, IN::T, IN::T, IN::F, IN::F, IN::F, IN::F, IN::T, '!', IN::T, IN::T, IN::T, IN::T, IN::F, \
/* 208-223*/ IN::F, IN::T, IN::T, IN::T, IN::F, IN::F, IN::T, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::T, IN::F, IN::F, \
/* 224-239*/ IN::T, IN::F, IN::T, IN::T, IN::T, IN::T, IN::F, IN::F, IN::T, IN::T, IN::T, IN::T, IN::T, IN::T, IN::T, IN::F, \
/* 240-255*/ IN::T, IN::T, IN::T, IN::F, IN::F, IN::F, IN::F, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, \
}
//
namespace In
{
    struct IN
    {
        enum { T = 1024, F = 2048, I = 4096 };
        int size;
        int lines;
        int ignor;

        int errors;

        unsigned char* text;
        int code[256];
    };

    IN getin(Parm::PARM parm);
}