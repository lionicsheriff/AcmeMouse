namespace PInvoke.Types
open System.Runtime.InteropServices

[<Struct; StructLayout(LayoutKind.Sequential)>]
type MOUSEINPUT =
    val dx: LONG
    val dy: LONG
    val mouseData: DWORD
    val dwflags: DWORD
    val time: DWORD
    val dwExtraInfo: ULONG_PTR

[<Struct; StructLayout(LayoutKind.Sequential)>]
type KEYBDINPUT =
    val wVk: VK
    val wScan: WORD
    val dwflags: KEYEVENTF
    val time: DWORD
    val dwExtraInfo: ULONG_PTR
    new(_wVk, _wScan, _dwflags, _time, _dwExtraInfo) =
        { wVk = _wVk
          wScan = _wScan
          dwflags = _dwflags
          time = _time
          dwExtraInfo = _dwExtraInfo }


[<Struct; StructLayout(LayoutKind.Sequential)>]
type HARDWAREINPUT =
    val uMSG: DWORD
    val wParamL: WORD
    val wParamH: WORD

[<Struct; StructLayout(LayoutKind.Explicit)>]
type INPUTUNION =
    [<FieldOffset(0)>]
    val mutable mi: MOUSEINPUT
    [<FieldOffset(0)>]
    val mutable ki: KEYBDINPUT
    [<FieldOffset(0)>]
    val mutable hi: HARDWAREINPUT

[<Struct; StructLayout(LayoutKind.Sequential)>]
type INPUT = 
    val ``type``: int
    val input: INPUTUNION
    new(_type, _input) =
    {
        ``type`` = _type
        input = _input
    }
