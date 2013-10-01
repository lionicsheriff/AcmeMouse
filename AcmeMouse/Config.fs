module Config

type Chord = class end
type Action = interface
    abstract member Run: bool
    end

type KeyEvent = class
    val Code: int
    val Event: int
    new (_Code,_Event) = {Code = _Code; Event = _Event}
    end
     
type KeyDown(Code: int) = class inherit KeyEvent(Code,1) end
type KeyUp(Code: int) = class inherit KeyEvent(Code,1) end

type SendKeys =
    interface Action with
        member this.Run = true
    val Keys: KeyEvent[]


type Mapping =
    val Chord: Chord
    val Action: Action

