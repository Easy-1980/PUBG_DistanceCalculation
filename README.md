# PUBG Distance Calculation

## 项目简介
PUBG Distance Calculation 是一个用于 PUBG 游戏测距的工具。  
该工具由两个项目组成：
1. **PUBG_DistanceCalculation** - WinForms 主程序
2. **OverlayWindow** - WPF 测距窗口库

启动主程序后，可以在游戏窗口上弹出透明 Overlay，进行测距操作。

---

## 功能说明

### Overlay 测距逻辑
1. **校准点**
   - 第一次点击：校准起点（蓝点）
   - 第二次点击：校准终点（蓝点）
   - 根据两点距离校准 100 米的像素比例

2. **测距点**
   - 第三次点击：测距起点（红点）
   - 第四次及之后的点击：测距终点（红点）
   - 显示测距线和计算出的距离（单位：米）

### 窗口特性
- 全屏透明 Overlay 窗口  
- 可在屏幕上自由点击标点  
- 保持在最上层显示，不阻塞主程序  

---

## 退出方式
Overlay 窗口可以通过以下方式关闭：
- **Ctrl + `(反引号)** ：全局热键，按下立即关闭 Overlay  

---

## 安装与运行
1. **Release下载Single.ZIP**  
   - 解压后直接运行 `PUBG_DistanceCalculation.exe`  
   - 右键exe程序，发送到桌面，生成快捷方式  

2. **启动程序**
   - 双击 `PUBG_DistanceCalculation.exe`  
   - 点击“开始测距”按钮弹出 Overlay  
   - 先依次点击最大级数地图的最小格子进行100m校准，然后点击人物位置和测距点进行测距，屏幕上自动显示距离

3. **退出 Overlay**
   - 使用 **Ctrl + `(反引号)** 热键关闭 Overlay  

---

## 发布信息
- 打包方式：Visual Studio 2022 发布 → 单文件 
- 图标：distance.ico  
- .NET 版本： .NET 8.0 WinForms + WPF

---

## 开发说明
- 主程序使用 WinForms  
- Overlay 使用 WPF Canvas 绘制  
- 测距逻辑完全在 OverlayWindow 中实现  
- 支持修改热键和图标，可根据需要自行调整  

---

## 联系
如有问题，请联系Easy-1980：3211618922@qq.com。
