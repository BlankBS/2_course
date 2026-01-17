.586
.MODEL FLAT, STDCALL
includelib kernel32.lib
ExitProcess PROTO : DWORD
.STACK 4096

.CONST

.DATA

CHAR_ARR0	DB	BlankBS", 0
BOOL_VAL1	DWORD	1


.CODE
main PROC
START :
push - 1
call ExitProcess
main ENDP
end main
