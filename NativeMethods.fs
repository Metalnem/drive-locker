namespace Win32
    
open System
open System.IO
open System.Runtime.InteropServices
open System.Threading
open Microsoft.Win32.SafeHandles

module NativeMethods = 
    [<DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)>]
    extern SafeFileHandle CreateFile(
        string lpFileName,
        [<MarshalAs(UnmanagedType.U4)>] FileAccess dwDesiredAccess,
        [<MarshalAs(UnmanagedType.U4)>] FileShare dwShareMode,
        IntPtr lpSecurityAttributes,
        [<MarshalAs(UnmanagedType.U4)>] FileMode dwCreationDisposition,
        [<MarshalAs(UnmanagedType.U4)>] FileAttributes dwFlagsAndAttributes,
        IntPtr hTemplateFile
    )

    [<DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)>]
    extern bool DeviceIoControl(
        SafeFileHandle hDevice,
        ControlCode dwIoControlCode,
        [<MarshalAs(UnmanagedType.AsAny)>] obj lpInBuffer,
        uint32 nInBufferSize,
        [<MarshalAs(UnmanagedType.AsAny)>] obj lpOutBuffer,
        uint32 nOutBufferSize,
        uint32& lpBytesReturned,
        NativeOverlapped& lpOverlapped
    )
