// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open MouseHandler
open MouseChord
open TrayIcon
//open PInvoke.Types

let error = System.Runtime.InteropServices.Marshal.GetLastWin32Error()

[<EntryPoint; System.STAThread>]
let main argv =                                        
    printfn "started"
(*
    let mouseEvent = new Event<string>()
    use eventedHook = new LowLevelMouseHook(fun nCode wParam lParam ->
        mouseEvent.Trigger(sprintf "%A %A (x:%A y:%A d:%A f:%A t:%A)" nCode wParam lParam.pt.x lParam.pt.y lParam.mouseData lParam.flags lParam.time) |> ignore
        true
    )
    mouseEvent.Publish.Subscribe(fun (str: string) -> System.Console.WriteLine str) |> ignore
*)
    let chorder = new ChordedMouseHook()
    printfn "hooked"

    use context = new TrayContext()

    System.Windows.Forms.Application.Run(context)

    0 // return an integer exit code
