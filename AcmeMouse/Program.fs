// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open MouseHandler
open MouseChord

open PInvoke.Types

let han a b c =
     printfn "%A %A %A" a b c 
     PInvoke.Types.LRESULT.Zero

let error = System.Runtime.InteropServices.Marshal.GetLastWin32Error()

[<EntryPoint>]
let main argv = 
(*
    let mh = new MouseHook(PInvoke.Methods.LowLevelMouseProc(fun a b c -> printfn "%A %A %A" a b c
                                                                          0n))
                     
                     
                                                                                                  
                                                            *)
                                        
    printfn "started"

    let mouseEvent = new Event<string>()
    use eventedHook = new LowLevelMouseHook(fun nCode wParam lParam ->
        mouseEvent.Trigger(sprintf "%A %A (x:%A y:%A d:%A f:%A t:%A)" nCode wParam lParam.pt.x lParam.pt.y lParam.mouseData lParam.flags lParam.time) |> ignore
        true
    )
    mouseEvent.Publish.Subscribe(fun (str: string) -> System.Console.WriteLine str) |> ignore

    let chorder = new ChordedMouseHook()


    (*
    use noRight = new LowLevelMouseHook(fun nCode wParam lParam ->
        if wParam = WM.RBUTTONDOWN || wParam = WM.RBUTTONUP then
            false
        else
            true
    )
    *)
    printfn "hooked"

    System.Windows.Forms.Application.Run()

    (*
    let message = PInvoke.Types.MSG()
    while PInvoke.Methods.GetMessage(message, 0n, 0u, 0u) do
        printfn "!%A" error
        *)

    System.Console.ReadLine() |> ignore
    printfn "%A" argv
    0 // return an integer exit code
