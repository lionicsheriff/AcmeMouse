module PInvoke.Methods
open PInvoke.Types

open System.Runtime.InteropServices


type LowLevelMouseProc =
    delegate of int * WM * byref<MSLLHOOKSTRUCT> -> LRESULT

type HOOKPROC = LowLevelMouseProc

[<DllImport("kernel32.dll")>]
extern uint32 GetCurrentThreadId()

[<DllImport("kernel32.dll", SetLastError = true)>]
extern nativeint GetModuleHandle(string lpModuleName)

[<DllImport("user32.dll", SetLastError = true)>]
extern bool UnhookWindowsHookEx(nativeint hhk)

[<DllImport("user32.dll", SetLastError = true)>]
extern nativeint SetWindowsHookEx(int idhook, HOOKPROC proc,  HINSTANCE hMod, DWORD threadId)

[<DllImport("user32.dll", SetLastError = true)>]
extern bool GetMessage(MSG lpMsg, nativeint hWnd, uint32 wMsgFilterMin, uint32 wMsgFilterMax);

[<DllImport("user32.dll", SetLastError = true)>]
extern UINT SendInput(UINT nInputs, [<MarshalAs(UnmanagedType.LPArray); In>] INPUT[] pInputs, int cbSize);

[<DllImport("user32.dll", SetLastError = true)>]
extern bool GetGUIThreadInfo(DWORD idThread, [<In; Out>]GUITHREADINFO& lpgui)

[<DllImport("user32.dll", SetLastError = true)>]
extern ULONG_PTR GetMessageExtraInfo()

[<DllImport("kernel32.dll", SetLastError = true)>]
extern DWORD GetLastError()

[<DllImport("user32.dll", SetLastError = true)>]
extern WORD MapVirtualKey(VK uCode, uint32 uMapType)


[<DllImport("user32.dll")>]
extern LRESULT CallNextHookEx(HHOOK hhk, int nCode, WPARAM wParam, [<In>]LPARAM& lParam)