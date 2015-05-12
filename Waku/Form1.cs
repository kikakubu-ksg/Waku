using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Waku
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        extern static IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        extern static IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, 
            String lpszClass, String lpszWindow);
        [DllImport("user32.dll")]
        extern static Int32 SendMessage(IntPtr hwnd, int MSG, int wParam, int lParam);
        [DllImport("user32.dll")]
        extern static Int32 GetWindowTextLength(int hwnd);
        [DllImport("user32.dll")]
        extern static Int32 GetWindowText(int hwnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

          public const int BM_CLICK = 0xF5;
          public const int WM_GETTEXT = 0xD;
          public const int WM_GETTEXTLENGTH = 0xE;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hwnd = new IntPtr[4];
            hwnd[0] = FindWindow(null, "無題 - メモ帳");
            hwnd[1] = FindWindowEx(hwnd[0], (IntPtr)0, "Button", "9");
            hwnd[2] = FindWindowEx(hwnd[0], (IntPtr)0, "Button", "*");
            hwnd[3] = FindWindowEx(hwnd[0], (IntPtr)0, "Button", "=");

            // それぞれのキーを押す [9]→[*]→[9]→[=]
            SendMessage(hwnd[1], BM_CLICK, 0, 0); //'[9];
            SendMessage(hwnd[2], BM_CLICK, 0, 0); //'[*];
            SendMessage(hwnd[1], BM_CLICK, 0, 0); //'[9];
            SendMessage(hwnd[3], BM_CLICK, 0, 0); //'[=];

            IntPtr hwnd2; //表示部のハンドル取得
            hwnd2 = FindWindowEx(hwnd[0], (IntPtr)0, "Edit", "");

            int length; //表示部の文字列のバイト数を調べる
            length = SendMessage(hwnd2, WM_GETTEXTLENGTH, 0, 0);

            bool Ret;
            var sb = new StringBuilder("", length); //sbという変数を受け取る変数分確保する
            Ret = SendMessage(hwnd2, WM_GETTEXT, sb.Capacity, sb);
            Console.WriteLine(sb.ToString());
        }
    }
}
