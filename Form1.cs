using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using OverlayDistance;

namespace PUBG_DistanceCalculation
{
    public partial class PUBG_Dist : Form
    {
        // ---------- 全局热键注册常量 ----------
        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID = 0x9000;
        private const uint MOD_CONTROL = 0x0002;
        private const uint VK_OEM_3 = 0xC0; // 反引号 ` 键

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        private OverlayDistance.OverlayWindow overlayWindow;
        public PUBG_Dist()
        {
            InitializeComponent();
        }

        private void Setup_Click(object sender, EventArgs e)
        {
            try
            {
                if (overlayWindow == null)
                {
                    overlayWindow = new OverlayDistance.OverlayWindow();
                }

                overlayWindow.Topmost = true;
                overlayWindow.Show();
                overlayWindow.Activate();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("启动测距界面时出错：" + ex.Message);
            }
        }

        // 程序窗口显示时注册全局热键
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            bool success = RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL, VK_OEM_3);
            if (!success)
            {
                System.Windows.Forms.MessageBox.Show("热键注册失败，请检查是否有其他程序占用 Ctrl+`。");
            }
        }

        // 捕获全局热键消息
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                CloseOverlay();
                return; 
            }
            base.WndProc(ref m);
        }

        // 手动关闭 Overlay（供热键调用）
        private void CloseOverlay()
        {
            if (overlayWindow != null)
            {
                try
                {
                    overlayWindow.Dispatcher.Invoke(() =>
                    {
                        try { overlayWindow.Close(); } catch { }
                    });
                }
                catch { }

                overlayWindow = null;
            }
        }

        // 关闭主窗体时注销热键并关闭 Overlay
        protected override void OnClosing(CancelEventArgs e)
        {
            try { UnregisterHotKey(this.Handle, HOTKEY_ID); } catch { }
            CloseOverlay();
            base.OnClosing(e);
        }


        private void PUBG_Dist_FormClosing(object sender, FormClosingEventArgs e)
        {
            overlayWindow?.Close();
        }
    }
}
