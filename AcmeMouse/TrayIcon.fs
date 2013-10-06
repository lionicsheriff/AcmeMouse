module TrayIcon

open System.Windows.Forms

open System.Resources
open System.Reflection


type TrayContext() =
    inherit ApplicationContext()
    let resources = new ResourceManager("Resources",Assembly.GetExecutingAssembly())
    let components = new System.ComponentModel.Container()
    let trayIcon = new NotifyIcon(components)
    do  
        trayIcon.Icon <- resources.GetObject "AppIcon" :?> System.Drawing.Icon
        trayIcon.Text <- "AcmeMouse"
        trayIcon.ContextMenu <- new ContextMenu([|new MenuItem("E&xit", new System.EventHandler(fun sender e -> Application.Exit()))|])
        trayIcon.Visible <- true            
        System.Console.WriteLine "context created"
        Application.ApplicationExit.Add (fun args -> trayIcon.Visible <- false)
    member this.TrayIcon = trayIcon
    member this.Dispose =
        trayIcon.Visible <- false
 