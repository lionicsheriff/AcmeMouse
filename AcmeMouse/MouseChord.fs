module MouseChord

open MouseHandler
open PInvoke.Types

let ChordMap = Map.ofList [([WM.LBUTTONDOWN;WM.MBUTTONDOWN],[VK.CONTROL;VK.X]);
                           ([WM.LBUTTONDOWN;WM.MBUTTONDOWN;WM.RBUTTONDOWN],[VK.CONTROL;VK.C]);
                           ([WM.LBUTTONDOWN;WM.RBUTTONDOWN],[VK.CONTROL;VK.V]) ]
type ChordedMouseHook()= 
    let mutable currentChord = List.Empty
    let hook = new LowLevelMouseHook(fun nCode wParam lParam ->
        match wParam with
            | WM.LBUTTONDOWN | WM.MBUTTONDOWN | WM.RBUTTONDOWN ->
                currentChord <-  (currentChord @ [wParam])
                System.Console.WriteLine currentChord
                if wParam <> currentChord.Head then
                    // we are in a chord, so block the mouse event from reaching the application
                    // this was primarily to stop the scrolling behavior for the middle button,
                    // but probably is relevant for any button
                    false
                else
                    true

            | WM.LBUTTONUP | WM.MBUTTONUP | WM.RBUTTONUP ->
                if wParam = WM.RBUTTONUP && currentChord.IsEmpty then
                    // Swallow the right mouse button if we just performed a chord so the context menu
                    // doesn't show afterwards. This condition works because a normal click is a chord of one,
                    // so the only time we have a button up and an empty chord is after a successful match (it
                    // clears the chord)
                    // We don't want to swallow left mouse button up though as the app probably won't release
                    // highlight mode. Who knows about the middle mouse button...
                    false
                else if ChordMap.ContainsKey currentChord then
                    System.Console.WriteLine ChordMap.[currentChord]
                    currentChord <- List.empty
                    false
                else
                    // reset the chord
                    currentChord <- List.empty
                    true

            | _ -> true
    )
    member this.Hook = hook