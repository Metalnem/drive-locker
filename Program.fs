open System
open System.Diagnostics
open System.IO
open System.Text.RegularExpressions
open System.Threading
open Microsoft.Win32
open Microsoft.Win32.SafeHandles
open Win32

let (|Command|_|) (s: string) =
    if s.Equals("-l", StringComparison.OrdinalIgnoreCase) then Some(true)
    elif s.Equals("-u", StringComparison.OrdinalIgnoreCase) then Some(false)
    elif s.Equals("--lock", StringComparison.OrdinalIgnoreCase) then Some(true)
    elif s.Equals("--unlock", StringComparison.OrdinalIgnoreCase) then Some(false)
    else None

let (|DriveLetter|_|) (s: string) =
    if Regex.IsMatch(s, "[A-Za-z]:?") then
        Some(s.Substring(0, 1).ToUpper())
    else
        None

let lockDrive (lock : bool) driveLetter =
    let driveInfo = DriveInfo (sprintf "%s:\\\\" driveLetter)

    if driveInfo.DriveType <> DriveType.CDRom then
        failwith (sprintf "%s: is not an optical drive" driveLetter)

    let input = PREVENT_MEDIA_REMOVAL()
    
    if lock then
        input.PreventMediaRemoval <- 1uy

    let fileName = sprintf @"\\.\%s:" driveLetter
    let mutable bytesReturned = 0u
    let mutable overlapped = NativeOverlapped()

    use file = NativeMethods.CreateFile(fileName, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.Open, enum<FileAttributes> 0, IntPtr.Zero)
    let success = NativeMethods.DeviceIoControl(file, ControlCode.IOCTL_STORAGE_MEDIA_REMOVAL, input :> obj, 1u, null, 0u, &bytesReturned, &overlapped)

    if not success then
        failwith (sprintf "Failed to %s drive %s:" (if lock then "lock" else "unlock") driveLetter)

let usage = """
Usage:
  DriveLocker.exe [-l|--lock] <drive_letter>
  DriveLocker.exe [-u|--unlock] <drive_letter>

Options:
  -l --lock    Lock the drive.
  -u --unlock  Unlock the drive.
  
Examples:
  DriveLocker.exe -l D:
  DriveLocker.exe --unlock D
"""

[<EntryPoint>]
let main args =
    try
        match args with
        | [|Command lock; DriveLetter letter|] ->
            lockDrive lock letter; 0
        | _ ->
            Console.Error.WriteLine("Invalid command line arguments.")
            Console.Error.Write usage; 1
    with
    | ex ->
        Console.Error.WriteLine ex.Message; 1
