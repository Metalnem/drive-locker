namespace Win32
    
open System
open System.Runtime.InteropServices

#nowarn "9"

[<StructLayout(LayoutKind.Sequential)>]
type PREVENT_MEDIA_REMOVAL =
    new() = {}
    [<DefaultValue>] val mutable PreventMediaRemoval : byte
