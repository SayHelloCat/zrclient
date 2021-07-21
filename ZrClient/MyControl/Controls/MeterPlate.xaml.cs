using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZrClient.MyControl.Controls
{
    /// <summary>
    /// MeterPlate.xaml 的交互逻辑
    /// </summary>
    public partial class MeterPlate : UserControl
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(MeterPlate), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(MeterPlate), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(MeterPlate), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(double), typeof(MeterPlate), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        public int ScaleCount
        {
            get { return (int)GetValue(ScaleCountProperty); }
            set { SetValue(ScaleCountProperty, value); }
        }
        public static readonly DependencyProperty ScaleCountProperty =
            DependencyProperty.Register("ScaleCount", typeof(int), typeof(MeterPlate), new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));

        public Brush PlateBackground
        {
            get { return (Brush)GetValue(PlateBackgroundProperty); }
            set { SetValue(PlateBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PlateBackgroundProperty =
            DependencyProperty.Register("PlateBackground", typeof(Brush), typeof(MeterPlate), null);


        public Brush PlateBorderBrush
        {
            get { return (Brush)GetValue(PlateBorderBrushProperty); }
            set { SetValue(PlateBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty PlateBorderBrushProperty =
            DependencyProperty.Register("PlateBorderBrush", typeof(Brush), typeof(MeterPlate), null);


        public Thickness PlateBorderThickness
        {
            get { return (Thickness)GetValue(PlateBorderThicknessProperty); }
            set { SetValue(PlateBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty PlateBorderThicknessProperty =
            DependencyProperty.Register("PlateBorderThickness", typeof(Thickness), typeof(MeterPlate), null);

        public double ScaleThickness
        {
            get { return (double)GetValue(ScaleThicknessProperty); }
            set { SetValue(ScaleThicknessProperty, value); }
        }
        public static readonly DependencyProperty ScaleThicknessProperty =
            DependencyProperty.Register("ScaleThickness", typeof(double), typeof(MeterPlate), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        public Brush ScaleBrush
        {
            get { return (Brush)GetValue(ScaleBrushProperty); }
            set { SetValue(ScaleBrushProperty, value); }
        }
        public static readonly DependencyProperty ScaleBrushProperty =
            DependencyProperty.Register("ScaleBrush", typeof(Brush), typeof(MeterPlate), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnPropertyChanged)));

        public Brush PointerBrush
        {
            get { return (Brush)GetValue(PointerBrushProperty); }
            set { SetValue(PointerBrushProperty, value); }
        }
        public static readonly DependencyProperty PointerBrushProperty =
            DependencyProperty.Register("PointerBrush", typeof(Brush), typeof(MeterPlate), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnPropertyChanged)));

        public new double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        public static new readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(MeterPlate), new PropertyMetadata(default(double), new PropertyChangedCallback(OnPropertyChanged)));

        public MeterPlate()
        {
            InitializeComponent();

            SetCurrentValue(MinimumProperty, 0d);
            SetCurrentValue(MaximumProperty, 100d);
            SetCurrentValue(IntervalProperty, 10d);

            SizeChanged += (se, ev) => { (se as MeterPlate)?.Refresh(); };
        }

        static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
            (d as MeterPlate).RefreshValue();

        private void RefreshValue()
        {
            this.SetCurrentValue(ValueProperty, Value - this.Minimum);
        }

        static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as MeterPlate).Refresh();

        private double Normalize(double v) => (v - Minimum) / (Maximum - Minimum) * 270;
        private void Refresh()
        {
            this.border.Width = Math.Min(RenderSize.Width, RenderSize.Height);
            this.border.Height = Math.Min(RenderSize.Width, RenderSize.Height);
            this.border.CornerRadius = new CornerRadius(this.border.Width / 2);

            double radius = this.border.Width / 2;

            this.canvasPlate.Children.Clear();
            if (ScaleCount <= 0) return;

            double label = this.Minimum;
            double interval = 0;
            double step = 270.0 / (this.Maximum - this.Minimum);

            for (int i = 0; i < this.Maximum - this.Minimum; i++)
            {
                //添加刻度线
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 13) * Math.Cos(step * i * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 13) * Math.Sin(step * i * Math.PI / 180);
                lineScale.X2 = radius - (radius - 8) * Math.Cos(step * i * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin(step * i * Math.PI / 180);

                lineScale.Stroke = this.ScaleBrush;
                lineScale.StrokeThickness = this.ScaleThickness;

                this.canvasPlate.Children.Add(lineScale);
            }

            do
            {
                //添加刻度线
                Line lineScale = new Line();
                lineScale.X1 = radius - (radius - 20) * Math.Cos(interval * step * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 20) * Math.Sin(interval * step * Math.PI / 180);
                lineScale.X2 = radius - (radius - 8) * Math.Cos(interval * step * Math.PI / 180);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin(interval * step * Math.PI / 180);

                lineScale.Stroke = this.ScaleBrush;
                lineScale.StrokeThickness = this.ScaleThickness;

                this.canvasPlate.Children.Add(lineScale);

                TextBlock txtScale = new TextBlock();
                txtScale.Text = label.ToString("0");
                txtScale.Width = 34;
                txtScale.TextAlignment = TextAlignment.Center;
                txtScale.Foreground = new SolidColorBrush(Colors.White);
                txtScale.RenderTransform = new RotateTransform() { Angle = 45, CenterX = 17, CenterY = 8 };
                txtScale.FontSize = this.FontSize;
                Canvas.SetLeft(txtScale, radius - (radius - 36) * Math.Cos(interval * step * Math.PI / 180) - 14);
                Canvas.SetTop(txtScale, radius - (radius - 36) * Math.Sin(interval * step * Math.PI / 180) - 10);
                this.canvasPlate.Children.Add(txtScale);

                interval += (this.Maximum - this.Minimum) / this.ScaleCount;
                label += (this.Maximum - this.Minimum) / this.ScaleCount;

            } while (interval <= this.Maximum - this.Minimum);


            // 修改指针
            string sData = "M{0} {1},{2} {0},{0} {3},{3} {0},{0} {1}";
            sData = string.Format(sData, radius - 2, radius + 5, this.border.Width - radius / 3, radius - 10);
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            this.pointer.Data = (Geometry)converter.ConvertFrom(sData);
            this.pointer.Fill = this.PointerBrush;

            DoubleAnimation da = new DoubleAnimation((Value - Minimum) * step + 135, new Duration(TimeSpan.FromMilliseconds(200)));
            this.rtPointer.BeginAnimation(RotateTransform.AngleProperty, da);
            //this.rtPointer.Angle = (Value - Minimum) * step + 135;

            // 修改圆  M100 200 A100 100 0 1 1 200 300
            sData = "M{0} {1} A{0} {0} 0 1 1 {1} {2}";
            sData = string.Format(sData, radius * 0.5, radius, radius * 1.5);
            this.circle.Data = (Geometry)converter.ConvertFrom(sData);
            this.circle.Visibility = Visibility.Visible;

            if (this.border.Width < 200)
                this.circle.Visibility = Visibility.Collapsed;

            //this.DrawScale();
        }

        /// <summary>
        /// 画表盘的刻度
        /// </summary>
        private void DrawScale()
        {
            for (double i = 0; i <= 100; i += 2)
            {
                //添加刻度线
                Line lineScale = new Line();

                if (i % 10 == 0)
                {
                    lineScale.X1 = 200 - 160 * Math.Cos(i * 2.7 * Math.PI / 180);
                    lineScale.Y1 = 200 - 160 * Math.Sin(i * 2.7 * Math.PI / 180);
                    lineScale.Stroke = this.ScaleBrush;
                    lineScale.StrokeThickness = this.ScaleThickness;

                    //添加刻度值
                    TextBlock txtScale = new TextBlock();
                    txtScale.Text = (i).ToString();
                    txtScale.Width = 34;
                    txtScale.TextAlignment = TextAlignment.Center;
                    txtScale.Foreground = new SolidColorBrush(Colors.White);
                    txtScale.RenderTransform = new RotateTransform() { Angle = 45, CenterX = 17, CenterY = 8 };
                    txtScale.FontSize = 20;
                    if (i * 2.7 <= 90)//对坐标值进行一定的修正
                    {
                        Canvas.SetLeft(txtScale, 200 - 155 * Math.Cos(i * 2.7 * Math.PI / 180) - 7);
                        Canvas.SetTop(txtScale, 200 - 150 * Math.Sin(i * 2.7 * Math.PI / 180) - 7);
                    }
                    else
                    {
                        Canvas.SetLeft(txtScale, 170 - 135 * Math.Cos(i * 2.7 * Math.PI / 180) + 22);
                        Canvas.SetTop(txtScale, 170 - 150 * Math.Sin(i * 2.7 * Math.PI / 180) + 18);
                    }
                    this.canvasPlate.Children.Add(txtScale);
                }
                else
                {
                    lineScale.X1 = 200 - 170 * Math.Cos(i * 2.7 * Math.PI / 180);
                    lineScale.Y1 = 200 - 170 * Math.Sin(i * 2.7 * Math.PI / 180);
                    lineScale.Stroke = new SolidColorBrush(Colors.White);
                    lineScale.StrokeThickness = 1;
                }

                lineScale.X2 = 200 - 180 * Math.Cos(i * 2.7 * Math.PI / 180);
                lineScale.Y2 = 200 - 180 * Math.Sin(i * 2.7 * Math.PI / 180);

                this.canvasPlate.Children.Add(lineScale);
            }
        }
    }
}
