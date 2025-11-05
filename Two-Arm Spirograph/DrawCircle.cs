using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class DrawCircle
{
    public Window win;

    Canvas canvas;

    readonly int longTurns = 5;     
    readonly float k = 10.1f;       
    readonly float longFrac = 0.33f; 
    readonly float shortToLong = 0.3f; 

    public DrawCircle()
    {
        float winHeight = 360;
        float winWidth = 640;

        win = new Window
        {
            Height = winHeight,
            Width = winWidth,
            Title = "Two-Arm Spirograph",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        canvas = new Canvas
        {
            Background = Brushes.Black,
        };

        win.Resized += Draw;
        win.Content = canvas;
        win.Show();
    }

    void Draw(object sender, WindowResizedEventArgs e)
    {
        canvas.Children.Clear();

        float w = (float)win.Width;
        float h = (float)win.Height;
        float smallerDim = h;
        if (w < smallerDim) smallerDim = w;

        float cx = w / 2f;
        float cy = h / 2f;

        float R = smallerDim * longFrac;          
        float r = R * shortToLong;                

        int steps = Math.Max(1000, longTurns * 2000);
        float thetaMax = longTurns * 2f * (float)Math.PI;

        float thetaL0 = 0f;
        float thetaS0 = k * thetaL0;
        float xL0 = R * (float)Math.Cos(thetaL0);
        float yL0 = R * (float)Math.Sin(thetaL0);
        float xS0 = r * (float)Math.Cos(thetaS0);
        float yS0 = r * (float)Math.Sin(thetaS0);
        float x0 = xL0 + xS0;
        float y0 = yL0 + yS0;

        for (int i = 1; i <= steps; i++)
        {
            float thetaL = i * thetaMax / steps;
            float thetaS = k * thetaL;

            float xL = R * (float)Math.Cos(thetaL);
            float yL = R * (float)Math.Sin(thetaL);

            float xS = r * (float)Math.Cos(thetaS);
            float yS = r * (float)Math.Sin(thetaS);

            float x1 = xL + xS;
            float y1 = yL + yS;

            AddLine(canvas, cx + x0, cy + y0, cx + x1, cy + y1);

            x0 = x1;
            y0 = y1;
        }
    }

    void AddLine(Canvas canvas,
        float startX, float startY,
        float endX, float endY)
    {
        var line = new Line
        {
            StartPoint = new Point(startX, startY),
            EndPoint = new Point(endX, endY),
            Stroke = Brushes.White,
            StrokeThickness = 1.2,
        };
        canvas.Children.Add(line);
    }
}