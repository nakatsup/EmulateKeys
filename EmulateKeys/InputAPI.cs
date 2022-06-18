using System;
using System.Runtime.InteropServices;


namespace Project.API.User
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Point
    {
        public Int32 X;
        public Int32 Y;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public Int32 dx;
        public Int32 dy;
        public Int32 mouseData;
        public Int32 dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public Int16 wVk;
        public Int16 wScan;
        public Int32 dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public Int32 uMsg;
        public Int16 wParamL;
        public Int16 wParamH;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT_UNION
    {
        [FieldOffset(0)] public MOUSEINPUT mouse;
        [FieldOffset(0)] public KEYBDINPUT keyboard;
        [FieldOffset(0)] public HARDWAREINPUT hardware;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public Int32 type;
        public INPUT_UNION ui;
    };

    public enum TYPE_INPUT
    {
        MOUSE = 0,
        KEYBOARD,
        HARDWARE,
    }

    public enum MOUSEEVENTF
    {
        MOVE = 0x1,
        ABSOLUTE = 0x8000,
        LEFTDOWN = 0x2,
        LEFTUP = 0x4,
        RIGHTDOWN = 0x8,
        RIGHTUP = 0x10,
        MIDDLEDOWN = 0x20,
        MIDDLEUP = 0x40,
        WHEEL = 0x800,
    }

    public enum KEYEVENTF
    {
        KEYDOWN = 0x0,
        EXTENDEDKEY = 0x1,
        KEYUP = 0x2,
    }

    public enum WHEEL
    {
        DELTA = 120,
    }

    public static class InputAPI
    {

        [DllImport("user32.dll")]
        private static extern void SendInput(Int32 nInputs, ref INPUT pInputs, Int32 cbsize);

        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
        private static extern Int32 MapVirtualKey(Int32 wCode, Int32 wMapType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputs"></param>
        /// <param name="pInputs"></param>
        /// <param name="cbsize"></param>
        public static void SendKey(Int32 nInputs, ref INPUT pInputs, Int32 cbsize)
        {
            SendInput(nInputs, ref pInputs, cbsize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wCode"></param>
        /// <param name="wMapType"></param>
        /// <returns></returns>
        public static Int32 FindMapViratualKey(Int32 wCode, Int32 wMapType)
        {
            return MapVirtualKey(wCode, wMapType);
        }
    }
}
