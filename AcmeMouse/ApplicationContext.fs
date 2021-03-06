﻿module ApplicationContext

open System.Windows.Forms
open System.Resources
open System.Reflection
open MouseChord


let menuItem text handler config =
    let item = new MenuItem(text, new System.EventHandler (handler))
    match config with
        | Some (config) -> 
            config item
            item
        | None ->
            item

type AcmeMouseContext() =
    inherit ApplicationContext()
    let resources = new ResourceManager("Resources",Assembly.GetExecutingAssembly ())
    let components = new System.ComponentModel.Container ()
    let trayIcon = new NotifyIcon (components)
    let hook = new ChordedMouseHook ()
    let toggleMouseWheel (sender: obj) e =
        hook.BlockWheel <- not hook.BlockWheel
        let menuItem = sender :?> MenuItem
        menuItem.Checked <- hook.BlockWheel

    let showHelp = fun sender e -> MessageBox.Show(([ "Press the mouse buttons in one motion"
                                                      "(i.e without releasing any buttons)"
                                                      ""
                                                      "L->M: Ctrl+X"
                                                      "L->M->R: Ctrl+C"
                                                      "L->R: Ctrl+V"
                                                      "L->R->M: cancel"
                                                    ] |> String.concat System.Environment.NewLine),
                                                    "AcmeMouse")
                                    |> ignore
        
    let onExit args =
        trayIcon.Visible <- false
        (hook :> System.IDisposable).Dispose ()
    do
        trayIcon.Icon <- resources.GetObject "AppIcon" :?> System.Drawing.Icon
        trayIcon.Text <- "AcmeMouse"
        trayIcon.ContextMenu <- new ContextMenu([|
                                                menuItem "&Help" showHelp None;
                                                menuItem "Block Mouse &Wheel" toggleMouseWheel (Some(fun item -> item.Checked <- hook.BlockWheel));
                                                menuItem "E&xit" (fun s e -> Application.Exit ()) None;
                                                |])
        trayIcon.Visible <- true
        System.Console.WriteLine "context created"
        Application.ApplicationExit.Add onExit
    member this.TrayIcon = trayIcon
    member this.Hook = hook