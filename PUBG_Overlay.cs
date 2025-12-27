using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using ClickableTransparentOverlay;
using ImGuiNET;

namespace PUBG_CalculateDistance
{
    public class PUBG_Overlay : Overlay
    {
        // ================= Windows API =================
        [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int vKey);
        [DllImport("user32.dll")] private static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")] private static extern int GetSystemMetrics(int nIndex);

        [StructLayout(LayoutKind.Sequential)] public struct POINT { public int X; public int Y; }
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        // 键位定义
        private const int VK_LBUTTON = 0x01;
        private const int VK_RBUTTON = 0x02;
        private const int VK_SHIFT = 0x10;
        private const int VK_CONTROL = 0x11;
        private const int VK_S = 0x53;
        private const int VK_OEM_3 = 0xC0; // 反引号 `

        // 变量
        private Vector2? p1 = null; private Vector2? p2 = null;
        private Vector2? p3 = null; private Vector2? p4 = null;
        private bool wasMouseDown = false;
        private bool isFirstFrame = true;
        private DateTime startupTime = DateTime.Now;
        private float VisualScale = 1.3f;
        private double CurrentPixelsPer100m = 0;
        private const string ConfigFile = "calibration.txt";

        public PUBG_Overlay() : base("PUBG Distance Calculator")
        {
            LoadConfig();
        }

        private void LoadConfig() { if (File.Exists(ConfigFile)) { try { string content = File.ReadAllText(ConfigFile); if (double.TryParse(content, out double val)) CurrentPixelsPer100m = val; } catch { } } }
        private void SaveConfig() { if (CurrentPixelsPer100m > 0) File.WriteAllText(ConfigFile, CurrentPixelsPer100m.ToString()); }

        protected override void Render()
        {
            if (isFirstFrame)
            {
                int realW = GetSystemMetrics(SM_CXSCREEN);
                int realH = GetSystemMetrics(SM_CYSCREEN);
                this.Position = new System.Drawing.Point(0, 0);
                this.Size = new System.Drawing.Size(realW, realH);
                isFirstFrame = false;
            }

            GetCursorPos(out POINT winPoint);
            Vector2 mousePos = new Vector2(winPoint.X, winPoint.Y);
            var drawList = ImGui.GetBackgroundDrawList();

            bool isCtrlDown = (GetAsyncKeyState(VK_CONTROL) & 0x8000) != 0;
            bool isShiftDown = (GetAsyncKeyState(VK_SHIFT) & 0x8000) != 0;
            bool isLeftDown = (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0;
            bool isRightDown = (GetAsyncKeyState(VK_RBUTTON) & 0x8000) != 0;
            bool isSDown = (GetAsyncKeyState(VK_S) & 0x8000) != 0;
            bool isBacktickDown = (GetAsyncKeyState(VK_OEM_3) & 0x8000) != 0;

            // ===================================================
            // 【核心修改】退出逻辑：只关闭 Overlay 窗口，不杀进程
            // ===================================================
            if (isCtrlDown && isBacktickDown)
            {
                this.Close(); // 这会让 Run() 方法结束，从而回到 WinForm
                return;
            }

            // 启动提示
            if ((DateTime.Now - startupTime).TotalSeconds < 2)
            {
                drawList.AddText(ImGui.GetFont(), 30.0f * VisualScale, new Vector2(50, 50), 0xFF00FF00, "PUBG Overlay Running!");
                drawList.AddText(ImGui.GetFont(), 20.0f * VisualScale, new Vector2(50, 90), 0xFFFFFFFF, "Hold [Ctrl] to use");
            }

            if (isCtrlDown)
            {
                string status = (CurrentPixelsPer100m > 0) ? $"Ready ({CurrentPixelsPer100m:F1}px/100m)" : "Need Calibration";
                string tip = "";

                if (CurrentPixelsPer100m > 0 && p1 == null)
                {
                    if (p3 == null) tip = $"[Click 1] Start\n{status}";
                    else if (p4 == null) tip = "[Click 2] End";
                }
                else
                {
                    if (p1 == null) tip = "[Calibrate] Grid Start";
                    else if (p2 == null) tip = "[Calibrate] Grid End";
                }

                if (CurrentPixelsPer100m > 0) tip += "\n[R-Click] Clear Line | [Shift+R-Click] Reset Config";
                else tip += "\n[R-Click] Clear Points";
                tip += "\n[Ctrl+S] Save | [Ctrl+`] Quit Overlay";

                drawList.AddText(ImGui.GetFont(), 18.0f * VisualScale, mousePos + new Vector2(20, 20), 0xFF00FFFF, tip);

                if (isSDown && CurrentPixelsPer100m > 0)
                {
                    SaveConfig();
                    drawList.AddText(mousePos + new Vector2(20, 80), 0xFF00FF00, "CONFIG SAVED!");
                }

                if (isLeftDown && !wasMouseDown)
                {
                    if (CurrentPixelsPer100m <= 0 || p1 != null)
                    {
                        if (p1 == null) p1 = mousePos;
                        else if (p2 == null)
                        {
                            p2 = mousePos;
                            double dist = Vector2.Distance(p1.Value, p2.Value);
                            if (dist > 1.0) CurrentPixelsPer100m = dist;
                        }
                        else if (p3 == null) p3 = mousePos;
                        else if (p4 == null) p4 = mousePos;
                        else { p3 = mousePos; p4 = null; }
                    }
                    else
                    {
                        if (p3 == null) p3 = mousePos;
                        else if (p4 == null) p4 = mousePos;
                        else { p3 = mousePos; p4 = null; }
                    }
                }

                // 右键逻辑
                if (isRightDown)
                {
                    if (p3 != null || p4 != null) { p3 = null; p4 = null; }
                    else if (p1 != null || p2 != null) { p1 = null; p2 = null; }
                    else if (isShiftDown) { CurrentPixelsPer100m = 0; p1 = null; p2 = null; p3 = null; p4 = null; }
                }
            }

            wasMouseDown = isLeftDown;

            // 绘图
            float dotSize = 4.0f * VisualScale;
            float lineThick = 2.0f * VisualScale;

            if (p1.HasValue)
            {
                drawList.AddCircleFilled(p1.Value, dotSize, 0xFF00FFFF);
                if (isCtrlDown && p1 != null && p2 == null) drawList.AddLine(p1.Value, mousePos, 0x8800FFFF, lineThick);
            }
            if (p2.HasValue)
            {
                drawList.AddCircleFilled(p2.Value, dotSize, 0xFF00FFFF);
                drawList.AddLine(p1.Value, p2.Value, 0xFF00FFFF, lineThick);
            }
            if (p3.HasValue)
            {
                drawList.AddCircleFilled(p3.Value, dotSize, 0xFF0000FF);
                if (isCtrlDown && p3 != null && p4 == null) drawList.AddLine(p3.Value, mousePos, 0x880000FF, lineThick);
            }
            if (p4.HasValue)
            {
                drawList.AddCircleFilled(p4.Value, dotSize, 0xFF0000FF);
                drawList.AddLine(p3.Value, p4.Value, 0xFF0000FF, lineThick * 1.5f);
            }

            if (CurrentPixelsPer100m > 0 && p3.HasValue && p4.HasValue)
            {
                double metersPerPixel = 100.0 / CurrentPixelsPer100m;
                double targetPixelDist = Vector2.Distance(p3.Value, p4.Value);
                double resultMeters = targetPixelDist * metersPerPixel;

                string text = $"{resultMeters:F0} m";
                Vector2 textPos = p4.Value + new Vector2(10, 10);
                Vector2 textSize = ImGui.CalcTextSize(text) * (24.0f / 13.0f) * VisualScale;
                drawList.AddRectFilled(textPos, textPos + textSize + new Vector2(10, 5), 0xDD000000);
                drawList.AddText(ImGui.GetFont(), 24.0f * VisualScale, textPos + new Vector2(5, 2), 0xFFFFFFFF, text);
            }
        }
    }
}
