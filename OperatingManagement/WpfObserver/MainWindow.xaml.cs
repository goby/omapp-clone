using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using OperatingManagement.WpfObserver;

namespace WpfObserver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSatellites();

            CreateStationView();

            LoadServers();

            this.SizeChanged += new SizeChangedEventHandler(MainWindow_SizeChanged);
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double deltaX = e.NewSize.Width / 800;
            double deltaY = e.NewSize.Height / 600;
            gridTrans.ScaleX = deltaX;
            gridTrans.ScaleY = deltaY;
        }

        #region -Border-
        Border RenderBorder(double width, double height) {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0.5, 1);
            gradient.EndPoint = new Point(0.5, 0);

            GradientStop color1 = new GradientStop();
            color1.Color = Colors.AliceBlue;
            color1.Offset = 0.2;
            gradient.GradientStops.Add(color1);

            GradientStop color2 = new GradientStop();
            color2.Color = Colors.WhiteSmoke;
            color2.Offset = 0.5;
            gradient.GradientStops.Add(color2);

            GradientStop color3 = new GradientStop();
            color3.Color = Colors.LightBlue;
            color3.Offset = 0.8;
            gradient.GradientStops.Add(color3);
            Border b = new Border()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
                Opacity = 0.4,
                Width = width,
                Height = height,
                CornerRadius = new CornerRadius(4),
                Background = gradient
            };
            return b;
        }
        #endregion

        #region -Servers-
        void LoadServers() {
            var titles = new string[] { "信息交换子系统", "任务规划子系统", "载荷控制管理子系统", "数据存储子系统",
                "监视演示子系统", "遥测处理子系统", "实验数据处理子系统", "评估应用子系统", "协同控制子系统", };
            GroupBox gb = new GroupBox()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                BorderBrush =new SolidColorBrush(Colors.Gray),
                Header = "中心",
                Width = 780,
                Height = 210
            };
            gb.SetValue(Grid.RowProperty, 2); 
            
            int row = 2;
            int col = 5;
            Grid g = new Grid()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Width = 750
            };

            for (int i = 0; i < row; i++)
            {
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(56) });
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(34) });
                for (int j = 0; j < col; j++)
                {
                    if (i * col + j == titles.Length) continue;
                    g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
                    Image img = new Image()
                    {
                        Width = 40,
                        Height = 55,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        Stretch = Stretch.Fill,
                        Source = new BitmapImage(new Uri("/OperatingManagement.WpfObserver;component/Resources/server-group.png", UriKind.Relative)),
                        Cursor = Cursors.Hand
                    };
                    img.SetValue(Grid.RowProperty, i * 2);
                    img.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(img);

                    TextBlock txt = new TextBlock()
                    {
                        Height = 24,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        Text = titles[i * col + j],
                        TextAlignment = System.Windows.TextAlignment.Center,
                        TextWrapping = TextWrapping.Wrap
                    };
                    txt.SetValue(Grid.RowProperty, i * 2 + 1);
                    txt.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(txt);

                    Ellipse ell = new Ellipse()
                    {
                        Width = 10,
                        Height = 10,
                        Fill = new SolidColorBrush(Colors.Green),
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                    };

                    ell.SetValue(Grid.RowProperty, i * 2);
                    ell.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(ell);
                }
            }
            gb.Content = g;
            LayoutRoot.Children.Add(gb);
        }
        #endregion

        #region -Stations-
        void CreateStationView() {
            Grid g = new Grid()
            {
                Width = 780,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Margin=new Thickness(0,5,0,0)
            };
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(117) });

            GroupBox sgGB = LoadSGStation();
            sgGB.SetValue(Grid.ColumnProperty, 0);
            g.Children.Add(sgGB);

            GroupBox xgGB = LoadXGStation();
            xgGB.SetValue(Grid.ColumnProperty, 1);
            g.Children.Add(xgGB);

            GroupBox scGB = LoadSCStation();
            scGB.SetValue(Grid.ColumnProperty, 2);
            g.Children.Add(scGB);

            g.SetValue(Grid.RowProperty, 1);
            LayoutRoot.Children.Add(g);
        }
        GroupBox LoadSGStation() {
            var titles = new string[] { "喀什站", "厦门站" };
            GroupBox gb = new GroupBox()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Height = 240,
                BorderBrush = new SolidColorBrush(Colors.Gray),
                Header = "S地面站"
            };
            gb.SetValue(Grid.ColumnProperty, 0);

            Grid g = new Grid()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0,0,0,0)
            };

            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });
            for (int i = 0; i < titles.Length; i++)
            {
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(56) });
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24) });
                Image img = new Image()
                {
                    Width = 50,
                    Height = 55,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Source = new BitmapImage(new Uri("/OperatingManagement.WpfObserver;component/Resources/station.png", UriKind.Relative)),
                    Cursor = Cursors.Hand
                };
                img.SetValue(Grid.RowProperty, i*2);
                img.SetValue(Grid.ColumnProperty, 0);
                g.Children.Add(img);

                TextBlock txt = new TextBlock()
                {
                    Height = 24,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    TextAlignment = System.Windows.TextAlignment.Center,
                    Text = titles[i]
                };
                txt.SetValue(Grid.RowProperty, i*2 + 1);
                txt.SetValue(Grid.ColumnProperty, 0);
                g.Children.Add(txt);

                Ellipse ell = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Green),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };

                ell.SetValue(Grid.RowProperty, i*2);
                ell.SetValue(Grid.ColumnProperty, 1);
                g.Children.Add(ell);
            }
            gb.Content = g;
            return gb;
        }
        GroupBox LoadXGStation()
        {
            var titles = new string[] { "青岛站", "喀什站", "瑞典站", "总参二部信息处理中心", "总参二部牡丹江站", "总参三部技侦中心",
            "总参三部长春站", "总参三部乌鲁木齐站", "总参三部广州站", "总参气象水文空间天气总站气象处理中心", "总参气象水文空间天气总站北京站"};
            GroupBox gb = new GroupBox()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Height = 240,
                Margin = new Thickness(5, 0, 5, 0),
                BorderBrush = new SolidColorBrush(Colors.Gray),
                Header = "X地面站"
            };
            gb.SetValue(Grid.ColumnProperty, 0);
            int row = 2;
            int col = 6;
            Grid g = new Grid()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Width = 540
            };

            for (int i = 0; i < row; i++)
            {
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(56) });
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                for (int j = 0; j < col; j++)
                {
                    if (i * col + j == titles.Length) continue;
                    g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(85) });
                    Image img = new Image()
                    {
                        Width = 50,
                        Height = 55,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        Stretch = Stretch.Fill,
                        Source = new BitmapImage(new Uri("/OperatingManagement.WpfObserver;component/Resources/station.png", UriKind.Relative)),
                        Cursor = Cursors.Hand
                    };
                    img.SetValue(Grid.RowProperty, i * 2);
                    img.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(img);

                    TextBlock txt = new TextBlock()
                    {
                        Width = 80,
                        Height = 50,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        Text = titles[i * col + j],
                        TextAlignment = System.Windows.TextAlignment.Center,
                        TextWrapping = TextWrapping.Wrap
                    };
                    txt.SetValue(Grid.RowProperty, i * 2 + 1);
                    txt.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(txt);

                    Ellipse ell = new Ellipse()
                    {
                        Width = 10,
                        Height = 10,
                        Fill = new SolidColorBrush(Colors.Green),
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                    };

                    ell.SetValue(Grid.RowProperty, i * 2);
                    ell.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(ell);
                }
            }
            gb.Content = g;
            return gb;
        }
        GroupBox LoadSCStation()
        {
            var titles = new string[] { "东风站", "遥科学综合站1", "遥科学综合站2" };
            GroupBox gb = new GroupBox()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Height = 240,
                BorderBrush = new SolidColorBrush(Colors.Gray),
                Header = "遥科学站"
            };
            gb.SetValue(Grid.ColumnProperty, 0);

            Grid g = new Grid()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };

            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });
            for (int i = 0; i < titles.Length; i++)
            {
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24) });
                Image img = new Image()
                {
                    Width = 40,
                    Height = 45,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Source = new BitmapImage(new Uri("/OperatingManagement.WpfObserver;component/Resources/station.png", UriKind.Relative)),
                    Cursor = Cursors.Hand
                };
                img.SetValue(Grid.RowProperty, i * 2);
                img.SetValue(Grid.ColumnProperty, 0);
                g.Children.Add(img);

                TextBlock txt = new TextBlock()
                {
                    Height = 24,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    TextAlignment =  System.Windows.TextAlignment.Center,
                    Text = titles[i]
                };
                txt.SetValue(Grid.RowProperty, i * 2 + 1);
                txt.SetValue(Grid.ColumnProperty, 0);
                g.Children.Add(txt);

                Ellipse ell = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Green),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };

                ell.SetValue(Grid.RowProperty, i * 2);
                ell.SetValue(Grid.ColumnProperty, 1);
                g.Children.Add(ell);
            }
            gb.Content = g;
            return gb;
        }
        #endregion

        #region -Satellites-
        void LoadSatellites()
        {
            var titles = new string[] { "TS-3卫星", "TS-4-A卫星", "TS-4-B卫星", "TS-5-A卫星", "TS-5-B卫星", "TS-2卫星" };
            GroupBox gb = new GroupBox()
            {
                Header = "卫星",
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Height = 105,
                Width = 780,
                BorderBrush = new SolidColorBrush(Colors.Gray)
            };
            gb.SetValue(Grid.RowProperty, 0);

            Grid g = new Grid()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Width = 750
            };
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(66) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24) });
            for (int i = 0; i < titles.Length; i++) {
                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(780/titles.Length,GridUnitType.Star) });
                if (i == 2)
                {
                    Border b = RenderBorder(90, 85);
                    b.Name = string.Format("sb{0}", i);
                    b.SetValue(Grid.ColumnProperty, i);
                    b.SetValue(Grid.RowSpanProperty, 2);
                    g.Children.Add(b);

                }
                Image img = new Image()
                {
                    Width = 85,
                    Height = 60,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Source = new BitmapImage(new Uri("/OperatingManagement.WpfObserver;component/Resources/satellite.png", UriKind.Relative)),
                    Cursor = Cursors.Hand
                };
                img.SetValue(Grid.RowProperty, 0);
                img.SetValue(Grid.ColumnProperty, i);
                g.Children.Add(img);

                TextBlock txt = new TextBlock()
                {
                    Height = 24,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Text = titles[i]
                };
                txt.SetValue(Grid.RowProperty, 1);
                txt.SetValue(Grid.ColumnProperty, i);
                g.Children.Add(txt);

                Ellipse ell = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Green),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    Margin = new Thickness(0, 10, 0, 0)
                };

                if (i == 3)
                {
                    ell.Fill = new SolidColorBrush(Colors.Red);
                    MyTip tip = new MyTip()
                    {
                        Width = 100,
                        Height = 60,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        Text = "异常信息：无法获取参数。。。",
                        Margin = new Thickness(0,0,0,0)
                    };
                    tip.SetValue(Canvas.ZIndexProperty, 100);
                    tip.SetValue(Grid.ColumnProperty, i+1);
                    tip.SetValue(Grid.ColumnSpanProperty, 3);
                    g.Children.Add(tip);
                }
                ell.SetValue(Grid.RowProperty, 0);
                ell.SetValue(Grid.ColumnProperty, i);    
                g.Children.Add(ell);
            }
            gb.Content = g;

            LayoutRoot.Children.Add(gb);
        }
        #endregion
    }
}
