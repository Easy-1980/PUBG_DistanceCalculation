using System;
using System.Threading.Tasks; 
using System.Windows.Forms;
using PUBG_CalculateDistance; // 引用 Overlay 的命名空间

namespace PUBG_DistanceCalculation
{
    public partial class PUBG_Dist : Form
    {
        // 用于防止重复点击“开始”按钮
        private bool isOverlayRunning = false;

        public PUBG_Dist()
        {
            InitializeComponent();
        }

        private void Setup_Click(object sender, EventArgs e)
        {
            // 启动 Overlay 线程
            StartOverlayAsync();
        }

        private async void StartOverlayAsync()
        {
            isOverlayRunning = true;
            this.WindowState = FormWindowState.Minimized; // 启动后自动最小化 WinForm，不挡视野

            // 在后台线程运行 Overlay，这样不会卡死 WinForm 界面
            await Task.Run(async () =>
            {
                try
                {
                    // 创建 Overlay 实例
                    using (var overlay = new PUBG_Overlay())
                    {
                        // 启动！这行代码会一直阻塞，直到你在 Overlay 里调用 this.Close()
                        await overlay.Run();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Overlay 运行出错: {ex.Message}");
                }
            });

            // 当 overlay.Run() 结束后（即你在 Overlay 里按了 Ctrl+`），代码会走到这里
            isOverlayRunning = false;
        }

        // 窗体关闭时，确保强制退出所有线程
        private void PUBG_Dist_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Prompt_Click(object sender, EventArgs e)
        {

        }
    }
}