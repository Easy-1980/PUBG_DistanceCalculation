using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PUBG_CalculateDistance;

class Program
{
    // 引入系统 API 来设置 DPI 感知
    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();

    static async Task Main(string[] args)
    {

        SetProcessDPIAware();

        Console.WriteLine(" PUBG 测距工具 ");
        Console.WriteLine("按住 Left CTRL 开始测距...");

        // 启动 Overlay
        using var overlay = new PUBG_Overlay();
        await overlay.Run();
    }
}