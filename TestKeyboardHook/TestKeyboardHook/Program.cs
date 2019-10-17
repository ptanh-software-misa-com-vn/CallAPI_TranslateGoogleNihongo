﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anh.KeyboardHook
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InterceptKeys._hookID = InterceptKeys.SetHook(InterceptKeys._proc);
            Application.Run(new Form1());
            InterceptKeys.DoUnhookWindowsHookEx(InterceptKeys._hookID);
        }
    }
}
