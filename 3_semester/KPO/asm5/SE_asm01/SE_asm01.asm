.586P
.MODEL FLAT, STDCALL
includelib kernel32.lib

ExitProcess PROTO : DWORD
MessageBoxA PROTO : DWORD, : DWORD, : DWORD, : DWORD

.STACK 4096

.CONST 

Arr sdword 6,4,6,8,2,5,6,8,9,10

.DATA

str1 DB "Задание 1", 0
str2 DB "мин =   макс =   ", 0

MB_OK EQU 0  
HW DWORD ?

min sdword ?
max sdword ?

.CODE

main PROC
START:

push lengthof Arr
push offset Arr

call getMin
mov min, eax

push lengthof Arr
push offset Arr
call getmax
mov max, eax

    mov eax, min;

    mov EBX, EAX                               
    mov ECX, 10                                
    lea EDI, [str2 + 6] 
  ConvertToString:
    xor EDX, EDX                               
    div ECX                                    
    add DL, '0'                                
    mov [EDI], DL                              
    dec EDI                                    
    test EAX, EAX                              
    jnz ConvertToString
  
  mov eax, max;
  
    mov EBX, EAX                               
    mov ECX, 10                                
    lea EDI, [str2 + 16] 
  ConvertToString1:
    xor EDX, EDX                               
    div ECX                                    
    add DL, '0'                                
    mov [EDI], DL                              
    dec EDI                                    
    test EAX, EAX                              
    jnz ConvertToString1

    INVOKE MessageBoxA, HW, offset str2,offset str1, 0

push 0
call ExitProcess
main ENDP

getMin PROC uses ecx esi ebx, parm01: dword, parm02: dword

      mov   ecx, parm02
      mov   esi, parm01
      mov   eax, [esi]

CYCLE: 

      mov   ebx, [esi]
      add   esi, 4
      cmp   eax, ebx
      jl    False
      xchg  eax, ebx

False: 

      loop  CYCLE

    ret

getMin ENDP

getMax PROC uses ecx esi ebx, parm01: dword, parm02: dword

      mov   ecx, parm02
      mov   esi, parm01
      mov   eax, [esi]

CYCLE: 

      mov   ebx, [esi]
      add   esi, 4
      cmp   eax, ebx
      jg    False
      xchg  eax, ebx

False: 

      loop  CYCLE

    ret

getMax ENDP

END main
