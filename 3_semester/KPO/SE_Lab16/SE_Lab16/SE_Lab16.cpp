#include "FST.h"
#include <iostream>
using namespace std;

int main()
{
    setlocale(LC_CTYPE, "rus");

    const char* chains[7] = {
        "main( )+((send; )+(wait; )+(show; ))* +return;",
        "main(   )  +  ((send; ) + (wait; ) + (show; )) *  +return;",
        "main(    ) + ( (send;  ) + (wait;  ) + (show;  )) *  +return;",
        "main( )+((send; )+(wait; )+(show; ))* +return;",
        "main( )+((send; )+(wait; )+(show; )))   *   +   return;",
        "main( )+((send; )+(wait; )+(show; ))* +return;",
        "main( ) + ((send; ) + (wait; ) + (show; )) *  +return;"
    };

    const int N = 36;
    FST::NODE nodes[N];

    // main( )+
    FST::RELATION r0[] = { {'m',1} }; nodes[0] = FST::NODE(1, r0);
    FST::RELATION r1[] = { {'a',2} }; nodes[1] = FST::NODE(1, r1);
    FST::RELATION r2[] = { {'i',3} }; nodes[2] = FST::NODE(1, r2);
    FST::RELATION r3[] = { {'n',4} }; nodes[3] = FST::NODE(1, r3);
    FST::RELATION r4[] = { {'(',5} }; nodes[4] = FST::NODE(1, r4);
    FST::RELATION r5[] = { {' ',6} }; nodes[5] = FST::NODE(1, r5);
    FST::RELATION r6[] = { {')',7} }; nodes[6] = FST::NODE(1, r6);
    FST::RELATION r7[] = { {'+',8} }; nodes[7] = FST::NODE(1, r7);

    // send/wait/show
    FST::RELATION r8[] = { {'s',9},{'w',16},{'h',23} }; nodes[8] = FST::NODE(3, r8);

    // send
    FST::RELATION r9[] = { {'e',10} }; nodes[9] = FST::NODE(1, r9);
    FST::RELATION r10[] = { {'n',11} }; nodes[10] = FST::NODE(1, r10);
    FST::RELATION r11[] = { {'d',12} }; nodes[11] = FST::NODE(1, r11);
    FST::RELATION r12[] = { {';',13} }; nodes[12] = FST::NODE(1, r12);
    FST::RELATION r13[] = { {' ',8} }; nodes[13] = FST::NODE(1, r13);

    // wait
    FST::RELATION r16[] = { {'a',17} }; nodes[16] = FST::NODE(1, r16);
    FST::RELATION r17[] = { {'i',18} }; nodes[17] = FST::NODE(1, r17);
    FST::RELATION r18[] = { {'t',19} }; nodes[18] = FST::NODE(1, r18);
    FST::RELATION r19[] = { {';',20} }; nodes[19] = FST::NODE(1, r19);
    FST::RELATION r20[] = { {' ',8} }; nodes[20] = FST::NODE(1, r20);

    // show
    FST::RELATION r23[] = { {'o',24} }; nodes[23] = FST::NODE(1, r23);
    FST::RELATION r24[] = { {'w',25} }; nodes[24] = FST::NODE(1, r24);
    FST::RELATION r25[] = { {';',26} }; nodes[25] = FST::NODE(1, r25);
    FST::RELATION r26[] = { {' ',8} }; nodes[26] = FST::NODE(1, r26);

    // return
    FST::RELATION r27[] = { {' ',28} }; nodes[27] = FST::NODE(1, r27);
    FST::RELATION r28[] = { {'r',29} }; nodes[28] = FST::NODE(1, r28);
    FST::RELATION r29[] = { {'e',30} }; nodes[29] = FST::NODE(1, r29);
    FST::RELATION r30[] = { {'t',31} }; nodes[30] = FST::NODE(1, r30);
    FST::RELATION r31[] = { {'u',32} }; nodes[31] = FST::NODE(1, r31);
    FST::RELATION r32[] = { {'r',33} }; nodes[32] = FST::NODE(1, r32);
    FST::RELATION r33[] = { {'n',34} }; nodes[33] = FST::NODE(1, r33);
    FST::RELATION r34[] = { {';',35} }; nodes[34] = FST::NODE(1, r34);

    nodes[35] = FST::NODE(); // финальное состояние

    for (int k = 0; k < 7; k++)
    {
        cout << "Цепочка " << k + 1 << ": " << chains[k] << "\n";
        FST::FST fst((char*)chains[k], 36, nodes);
        if (FST::execute(fst))
            cout << "Распознана\n\n";
        else
            cout << "Не распознана\n\n";
    }

    system("pause");
    return 0;
}
