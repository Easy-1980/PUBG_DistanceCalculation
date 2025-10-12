using System;
using System.Drawing;
using System.Windows.Forms;

namespace PUBG_DistanceCalculation
{
    public partial class OverlayForm : Form
    {
        private Point? firstPoint = null;
        private Point? secondPoint = null;

        public OverlayForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

            this.BackColor = Color.Black;
            this.Opacity = 0.01; // 半透明，不使用 TransparencyKey
            this.DoubleBuffered = true;

            this.MouseClick += OverlayForm_MouseClick;
            this.Paint += OverlayForm_Paint;
        }

        private void OverlayForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (firstPoint == null)
                firstPoint = e.Location;
            else
                secondPoint = e.Location;

            this.Invalidate(); // 刷新绘制
        }

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            if (firstPoint != null && secondPoint != null)
            {
                using Pen pen = new Pen(Color.Red, 3);
                e.Graphics.DrawLine(pen, firstPoint.Value, secondPoint.Value);

                double dx = secondPoint.Value.X - firstPoint.Value.X;
                double dy = secondPoint.Value.Y - firstPoint.Value.Y;
                double distancePx = Math.Sqrt(dx * dx + dy * dy);

                double distanceM = distancePx * 5.56; // 示例比例尺
                e.Graphics.DrawString($"{distanceM:F0} m",
                    new Font("Segoe UI", 14),
                    Brushes.Red,
                    secondPoint.Value);
            }
        }


        public void ResetOverlay()
        {
            firstPoint = null;
            secondPoint = null;
            this.Invalidate();
        }
    }
}
