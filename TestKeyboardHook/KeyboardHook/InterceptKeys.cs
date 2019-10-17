using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anh.KeyboardHook
{
  public class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;        
        private const int WM_CTRL = 162;
        private const int WM_Q = 81;
        private static bool ctrl_flg = false;
        public static LowLevelKeyboardProc _proc = HookCallback;
        public static IntPtr _hookID = IntPtr.Zero;

        //public static void Main()
        //{
        //    _hookID = SetHook(_proc);
        //    Application.Run();
        //    UnhookWindowsHookEx(_hookID);
        //}
        public static bool DoUnhookWindowsHookEx(IntPtr hookID)
        {
            return UnhookWindowsHookEx(hookID);
        }
        public static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate void MyHookHandler(KeysData m, EventArgs e);
        public static event MyHookHandler MyHook;
        public class KeysData
        {
            public int vkCode;
            public KeysData(int avkCode)
            {
                this.vkCode = avkCode;
            }
        }
        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == WM_CTRL)
                {
                    ctrl_flg = true;
                }
                if (ctrl_flg == true && vkCode == WM_Q)
                {
                    ctrl_flg = false;
                    if (MyHook!=null)
                    {
                        MyHook(new KeysData(vkCode), new EventArgs());
                    }
                }
                //Console.WriteLine((Keys)vkCode);
            }
            if (nCode >=0 && wParam ==(IntPtr)WM_KEYUP)
            {
                if (ctrl_flg)
                {
                    ctrl_flg = false;
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern long GetWindowThreadProcessId(IntPtr hWnd,long lpdwProcessId);
        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();
    }
}
