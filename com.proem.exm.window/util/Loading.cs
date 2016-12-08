﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace sorteSystem
{
    public partial class Loading : Form
    {
        
        private bool _IsLoading = false;
        private int _NumberOfSpoke = 12;//辐条数目
        private int _ProgressValue = 0;//进度值
        private PointF _CenterPointF;
        private int _InnerCircleRadius = 10;
        private int _OutnerCircleRadius = 20;
        private Color _ThemeColor = Color.Red;
        private int _Speed = 100;
        private Color[] _Colors;
        private int _lineWidth = 3;

        public Loading()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            _Timer.Tick += new EventHandler(_Timer_Tick);
            _Timer.Interval = _Speed;
            _Colors = GetColors(_ThemeColor, _NumberOfSpoke, _IsLoading);

        }




        //画带有圆角线帽和指定颜色的直线（带alpha）
        private void DrawLine(Graphics g, PointF pointF1, PointF pointF2, Color color)
        {
            //强制资源管理 （管理非托管资源）
            using (Pen pen = new Pen(new SolidBrush(color), _lineWidth))
            {
                //指定线帽子
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, pointF1, pointF2);
            }
        }
        //根据中心点 半径 和角度，获取从中心点出发的线段终点
        private PointF GetPointF(PointF centerPointF, int r, double angle)
        {
            double A = Math.PI * angle / 180;//(angle/360)*2PI
            float xF = centerPointF.X + r * (float)Math.Cos(A);
            float yF = centerPointF.Y + r * (float)Math.Sin(A);

            return (new PointF(xF, yF));
        }
        //根据辐条数量和主题颜色返回一个Color数组
        private Color[] GetColors(Color color, int spokeMember, bool isLoading)
        {
            Color[] colors = new Color[spokeMember];
            int offseAlpha = 255 / spokeMember;//alpha偏差量
            if (isLoading == true)
            {
                for (int i = 0; i < spokeMember; i++)
                {
                    colors[i] = Color.FromArgb(i * offseAlpha, color);//反向储存 形成顺时针旋转效果
                }
            }
            else
            {
                for (int i = 0; i < spokeMember; i++)
                {
                    colors[i] = color;
                }
            }
            return colors;
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            _ProgressValue++;
            if (_ProgressValue > 11)
                _ProgressValue = 0;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            _CenterPointF = new PointF(this.Width / 2, this.Height / 2);
            if (_NumberOfSpoke > 0)
            {
                pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                double offsetAngle = (double)360 / _NumberOfSpoke;
                double currentAngle = _ProgressValue * offsetAngle;
                for (int i = 0; i < _NumberOfSpoke; i++)
                {
                    DrawLine(pe.Graphics, GetPointF(_CenterPointF, _InnerCircleRadius, currentAngle), GetPointF(_CenterPointF, _OutnerCircleRadius, currentAngle), _Colors[i]);
                    currentAngle += offsetAngle;
                }
            }
            base.OnPaint(pe);
        }

        public void Start()
        {
            _IsLoading = true;
            _Colors = GetColors(_ThemeColor, _NumberOfSpoke, _IsLoading);
            _Timer.Start();
        }
        public void Stop()
        {
            _Timer.Stop();
            _IsLoading = false;
            _Colors = GetColors(_ThemeColor, _NumberOfSpoke, _IsLoading);
            Invalidate();
        }


        public int Speed
        {
            get { return _Speed; }
            set
            {
                _Speed = value;
                _Timer.Interval = _Speed;
            }
        }
        public int SpokesMember
        {
            get { return _NumberOfSpoke; }
            set
            {
                _NumberOfSpoke = value;
                Invalidate();
            }
        }
        public int InnerCircleRadius
        {
            get { return _InnerCircleRadius; }
            set
            {
                _InnerCircleRadius = value;
                Invalidate();
            }
        }
        public int OutnerCircleRadius
        {
            get { return _OutnerCircleRadius; }
            set
            {
                _OutnerCircleRadius = value;
                Invalidate();
            }
        }
        public Color ThemeColor
        {
            get { return _ThemeColor; }
            set
            {
                _ThemeColor = value;
                _Colors = GetColors(_ThemeColor, _NumberOfSpoke, _IsLoading);
                Invalidate();
            }
        }
        public int LineWidth
        {
            get { return _lineWidth; }
            set
            {
                _lineWidth = value;
                Invalidate();
            }
        }
        public bool IsActive
        {
            get
            {
                return _Timer.Enabled;
            }
        }

        
    }
}
