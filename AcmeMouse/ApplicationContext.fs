module ApplicationContext

open System.Windows.Forms
open System.Resources
open System.Reflection
open MouseChord

type AcmeMouseContext() =
    inherit ApplicationContext()
    let resources = new ResourceManager("Resources",Assembly.GetExecutingAssembly())
    let components = new System.ComponentModel.Container()
    let trayIcon = new NotifyIcon(components)
    let hook = new ChordedMouseHook()
    let OnExit args =
        trayIcon.Visible <- false
        (hook :> System.IDisposable).Dispose ()
    do  
        trayIcon.Icon <- resources.GetObject "AppIcon" :?> System.Drawing.Icon
        trayIcon.Text <- "AcmeMouse"
        trayIcon.ContextMenu <- new ContextMenu([|new MenuItem("E&xit", new System.EventHandler(fun sender e -> Application.Exit()))|])
        trayIcon.Visible <- true
        System.Console.WriteLine "context created"
        Application.ApplicationExit.Add (OnExit)
    member this.TrayIcon = trayIcon
    member this.Hook = hook