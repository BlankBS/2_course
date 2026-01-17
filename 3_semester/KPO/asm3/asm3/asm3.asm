.586
.model flat, stdcall

includelib kernel32.lib
includelib user32.lib

ExitProcess PROTO : DWORD      
MessageBoxA PROTO :DWORD, :DWORD, :DWORD, :DWORD
wsprintfA PROTO C :DWORD, :DWORD, :VARARG

.stack 4096

.const
.data
str1 BYTE "Сикорский Артём Александрович 2-8",0
resultMessage BYTE 256 DUP(0)

templateZero BYTE "Сумма массива: %d, есть нуль", 0
templateNoZero BYTE "Сумма массива: %d, нет нуля", 0

myBytes BYTE 10h, 20h, 30h, 40h
myWords WORD 8Ah, 3Bh, 5Fh, 99h
myDoubles DWORD 1, 2, 3, 4, 5, 6
myPointer DWORD myDoubles

mass DWORD 1, 2, 3, 4, 5, 6, 7

.code

main PROC

mov EDI, 4 ; Extended Destination Index (регистр индекса)
mov AX, [myWords + EDI] ; младшие 16 бит регистра EAX (Accumulator)
mov BX, myWords[0] ;  младшие 16 бит регистра EBX (Base register)

mov EAX, 0 ; totalSum      основной регистр для арифметических операций     
mov ECX, 7 ; countElements счетчик (Counter register)
mov ESI, 0 ; index         Extended Source Index (регистр индекса)

CYCLE:
    add eax, mass[ESI * 4] 
    inc ESI                
    loop CYCLE

    mov EBX, EAX

    mov ECX, 7
    mov ESI, 0
    mov EAX, 0

CHECKZERO:
    cmp mass[ESI * 4], 0 
    je FOUND_ZERO          
    inc ESI              
    loop CHECKZERO
    jmp NO_ZERO_FOUND

FOUND_ZERO:
    mov EAX, 1

NO_ZERO_FOUND:
    push EAX
    mov EAX, EBX

    pop EBX
    cmp EBX, 1
    je USE_ZERO_TEMPLATE

    invoke wsprintfA, OFFSET resultMessage, OFFSET templateNoZero, EAX
    jmp SHOW_MESSAGE

USE_ZERO_TEMPLATE:
    invoke wsprintfA, OFFSET resultMessage, OFFSET templateZero, EAX

SHOW_MESSAGE:
    invoke MessageBoxA, 0, OFFSET resultMessage, OFFSET str1, 0

ENDPROGRAM:
    push 0
    call ExitProcess

main ENDP
end main