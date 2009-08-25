using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gean
{
    public class GlobaShortcut
    {
        static class Hotkey
        {
            #region 系统api
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool RegisterHotKey(IntPtr hWnd, int id, HotkeyModifiers fsModifiers, Keys vk);

            [DllImport("user32.dll")]
            static extern bool UnregisterHotKey(IntPtr hWnd, int id);
            #endregion

            /// <summary> 
            /// 注册快捷键 
            /// </summary> 
            /// <param name="hWnd">持有快捷键窗口的句柄</param> 
            /// <param name="fsModifiers">组合键</param> 
            /// <param name="vk">快捷键的虚拟键码</param> 
            /// <param name="callBack">回调函数</param> 
            public static void Regist(IntPtr hWnd, HotkeyModifiers fsModifiers, Keys vk, HotKeyCallBackHanlder callBack)
            {
                int id = keyid++;
                if (!RegisterHotKey(hWnd, id, fsModifiers, vk))
                    throw new Exception("regist hotkey fail.");
                keymap[id] = callBack;
            }

            /// <summary> 
            /// 注销快捷键 
            /// </summary> 
            /// <param name="hWnd">持有快捷键窗口的句柄</param> 
            /// <param name="callBack">回调函数</param> 
            public static void UnRegist(IntPtr hWnd, HotKeyCallBackHanlder callBack)
            {
                foreach (KeyValuePair<int, HotKeyCallBackHanlder> var in keymap)
                {
                    if (var.Value == callBack)
                        UnregisterHotKey(hWnd, var.Key);
                }
            }

            /// <summary> 
            /// 快捷键消息处理 
            /// </summary> 
            public static void ProcessHotKey(System.Windows.Forms.Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    int id = m.WParam.ToInt32();
                    HotKeyCallBackHanlder callback;
                    if (keymap.TryGetValue(id, out callback))
                    {
                        callback();
                    }
                }
            }

            const int WM_HOTKEY = 0x312;
            static int keyid = 10;
            static Dictionary<int, HotKeyCallBackHanlder> keymap = new Dictionary<int, HotKeyCallBackHanlder>();

            public delegate void HotKeyCallBackHanlder();
        }

        enum HotkeyModifiers
        {
            MOD_ALT = 0x1,
            MOD_CONTROL = 0x2,
            MOD_SHIFT = 0x4,
            MOD_WIN = 0x8
        }

    }
}

//void Test()
//{
//    MessageBox.Show("Test");
//}
//protected override void WndProc(ref Message m)
//{
//    base.WndProc(ref m);
//    Hotkey.ProcessHotKey(m);
//}
//private void button1_Click(object sender, EventArgs e)
//{
//    Hotkey.UnRegist(this.Handle, Test);
//} 

//当程序form1启动时，注册了两个快捷键Alt+T和Ctrl+Shift+K，单击button1的时候会注销快捷键Alt+T。代码比较简单，这里就不多介绍了。
//注：快捷键是通过消息触发的，因此要重载WndProc函数，在里面添加对快捷键回调消息的处理方法Hotkey.ProcessHotKey(m)。