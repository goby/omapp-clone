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
using System.Xml.Linq;
using System.IO;

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
        List<IconSet> _iconSets = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConfig();

            LoadSatellites();

            LoadStations();

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
        #region -Tooltip-
        static readonly string warning = "/OperatingManagement.WpfObserver;component/Resources/warning.png";
        private ToolTip GetToolTip(string title, string content)
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0.5, 1);
            gradient.EndPoint = new Point(0.5, 0);

            GradientStop color1 = new GradientStop();
            color1.Color = Colors.LightGray;
            color1.Offset = 0.2;
            gradient.GradientStops.Add(color1);

            GradientStop color2 = new GradientStop();
            color2.Color = Colors.WhiteSmoke;
            color2.Offset = 0.5;
            gradient.GradientStops.Add(color2);

            GradientStop color3 = new GradientStop();
            color3.Color = Colors.WhiteSmoke;
            color3.Offset = 0.8;
            gradient.GradientStops.Add(color3);

            StackPanel sp = new StackPanel();
            sp.Width = 200;

            Label lb = new Label()
            {
                Width = 200,
                FontWeight = FontWeights.Bold,
                Background = gradient,
                Foreground = Brushes.Black,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = title
            };
            sp.Children.Add(lb);

            Line ln = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 0.5,
                X2 = 200
            };
            sp.Children.Add(ln);

            TextBlock tb = new TextBlock()
            {
                Padding = new Thickness(10),
                TextWrapping = TextWrapping.WrapWithOverflow,
                Width = 200,
                Text = content
            };
            sp.Children.Add(tb);

            StackPanel sp2 = new StackPanel();
            sp2.Orientation = Orientation.Horizontal;

            sp.Children.Add(sp2);

            ToolTip ttp = new ToolTip();
            ttp.Content = sp;
            return (ttp);
        }
        #endregion

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
            var servers = new object[]{
                new IconTitle[]{
                    new IconTitle{type = "device", title = "数据收发设备"},
                    new IconTitle{type = "system", title = "解密机"},
                    new IconTitle{type = "server", title = "遥测处理服务器"},
                    new IconTitle{type = "server", title = "数据管理服务器"}
                },
                new IconTitle[]{
                    new IconTitle{type = "system", title = "仿真推演分系统"},
                    new IconTitle{type = "system", title = "天基目标观测应用研究分系统"}
                },
                new IconTitle[]{
                    new IconTitle{type = "server", title = "实时控制与同控计算服务器"},
                    new IconTitle{type = "server", title = "数传数据处理"},
                    new IconTitle{type = "system", title = "实时控制工作站"}
                },
                new IconTitle[]{
                    new IconTitle{type = "system", title = "空间遥操作应用研究分系统"},
                    new IconTitle{type = "system", title = "空间机动应用研究分系统"}
                }
            };
            Dictionary<string,IconSet> icons = new Dictionary<string,IconSet>();
            icons.Add("system", _iconSets.Find(i=>i.Type == "system"));
            icons.Add("device", _iconSets.Find(i => i.Type == "device"));
            icons.Add("server", _iconSets.Find(i => i.Type == "server"));

            Grid outerGrid = new Grid()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 0)
            };

            double cw = icons["system"].Size.Width + 70;
            double ch = icons["system"].Size.Height + 50;
            outerGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(cw *4) });
            outerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(cw * 2) });
            outerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(ch) });
            outerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(ch) });


            outerGrid.Children.Add(RenderCell((IconTitle[])servers[0], icons, 0, 0, cw, ch));
            outerGrid.Children.Add(RenderCell((IconTitle[])servers[1], icons, 0, 1, cw, ch));
            outerGrid.Children.Add(RenderCell((IconTitle[])servers[2], icons, 1, 0, cw, ch));
            outerGrid.Children.Add(RenderCell((IconTitle[])servers[3], icons, 1, 1, cw, ch));

            outerGrid.SetValue(Grid.RowProperty, 3);
            LayoutRoot.Children.Add(outerGrid);
        }
        Grid RenderCell(IconTitle[] titles, Dictionary<string, IconSet> icons, int row, int col, double cw, double ch)
        {
            Grid g = new Grid()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Width = cw * titles.Length,
                Height = ch,
                Margin = new Thickness(10, 0, 0, 0)
            };
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(ch - 35) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            for (int i = 0; i < titles.Length; i++) {
                IconTitle title = titles[i];
                IconSet icon = icons[title.type];
                var source = string.Format("/OperatingManagement.WpfObserver;component/Resources/{0}", icon.Source);
                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(cw) });

                Border b = RenderBorder(icon.Size.Width + 10, icon.Size.Height);
                Image img = new Image()
                {
                    Width = icon.Size.Width - 10,
                    Height = icon.Size.Height - 10,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Source = new BitmapImage(new Uri(source, UriKind.Relative)),
                    Cursor = Cursors.Hand
                };
                b.Child = img;
                b.SetValue(Grid.ColumnProperty, i);
                b.SetValue(Grid.RowProperty, 0);
                g.Children.Add(b);

                TextBlock txt = new TextBlock()
                {
                    Width = icon.Size.Width + 50,
                    Height = 50,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Text = title.title,
                    TextAlignment = System.Windows.TextAlignment.Center,
                    TextWrapping = TextWrapping.Wrap
                };
                txt.SetValue(Grid.ColumnProperty, i);
                txt.SetValue(Grid.RowProperty, 1);
                g.Children.Add(txt);

                Ellipse ell = new Ellipse()
                {
                    Width = 15,
                    Height = 15,
                    Fill = new SolidColorBrush(Colors.Green),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    Margin = new Thickness(0, 0, 25, 0)
                };

                ell.SetValue(Grid.RowProperty, 0);
                ell.SetValue(Grid.ColumnProperty, i);
                g.Children.Add(ell);
            }
            g.SetValue(Grid.RowProperty, row);
            g.SetValue(Grid.ColumnProperty, col);
            return g;
        }
        #endregion

        #region -Stations-
        void LoadStations()
        {
            var titles = new string[] { "青岛站", "喀什站", "瑞典站", "东风站","厦门站", "总参二部牡丹江站", 
                                        "遥科学综合站1", "遥科学综合站2", "总参三部广州站", "总参气象水文空间天气总站北京站"};
            var icon = _iconSets.Find(i => i.Type == "station");
            var source = string.Format("/OperatingManagement.WpfObserver;component/Resources/{0}", icon.Source);
            int col = 6;
            int mc = (int)(titles.Length / col) + (titles.Length % col > 0 ? 1 : 0);
            Grid g = new Grid()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 0),
                Width = col * (icon.Size.Width + 70),
                Height = mc * (icon.Size.Height + 50)
            };
            for (int i = 0; i < mc; i++)
            {
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(icon.Size.Height + 10) });
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                for (int j = 0; j < col; j++)
                {
                    if (i * col + j >= titles.Length) continue;
                    g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(icon.Size.Width + 70) });
                    Border b = RenderBorder(icon.Size.Width + 10, icon.Size.Height);
                    Image img = new Image()
                    {
                        Width = icon.Size.Width - 10,
                        Height = icon.Size.Height - 10,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        Stretch = Stretch.Fill,
                        Source = new BitmapImage(new Uri(source, UriKind.Relative)),
                        Cursor = Cursors.Hand
                    };
                    b.Child = img;
                    b.SetValue(Grid.RowProperty, i * 2);
                    b.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(b);

                    TextBlock txt = new TextBlock()
                    {
                        Width = icon.Size.Width + 50,
                        Height = 50,
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
                        Width = 15,
                        Height = 15,
                        Fill = new SolidColorBrush(Colors.Green),
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                        Margin = new Thickness(0, 0, 25, 0)
                    };

                    ell.SetValue(Grid.RowProperty, i * 2);
                    ell.SetValue(Grid.ColumnProperty, j);
                    g.Children.Add(ell);
                }
            }
            g.SetValue(Grid.RowProperty, 2);
            LayoutRoot.Children.Add(g);
        }
        #endregion

        #region -Satellites-
        void LoadSatellites()
        {
            var titles = new string[] { "TS-2", "TS-3", "TS-4", "TS-5"};
            var icon = _iconSets.Find(i => i.Type == "satallite");
            var source = string.Format("/OperatingManagement.WpfObserver;component/Resources/{0}", icon.Source);
            Grid g = new Grid()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            };
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(icon.Size.Height + 20) });
            g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24) });
            for (int i = 0; i < titles.Length; i++) {
                g.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(icon.Size.Width + 30)
                });
                Border b = RenderBorder(icon.Size.Width, icon.Size.Height);

                Image img = new Image()
                {
                    Width = icon.Size.Width - 10,
                    Height = icon.Size.Height - 10,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Source = new BitmapImage(new Uri(source, UriKind.Relative)),
                    Cursor = Cursors.Hand
                };

                b.Child = img;
                b.SetValue(Grid.RowProperty, 0);
                b.SetValue(Grid.ColumnProperty, i);
                g.Children.Add(b);

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
                    Width = 15,
                    Height = 15,
                    Fill = new SolidColorBrush(Colors.Green),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    Margin = new Thickness(0, 5, 10, 0)
                };
                if (i == 2) {
                    ell.Fill = new SolidColorBrush(Colors.Red);
                    ell.ToolTip = GetToolTip("异常信息", "无法定位卫星位置，请尝试重新启动程序。");
                }

                ell.SetValue(Grid.RowProperty, 0);
                ell.SetValue(Grid.ColumnProperty, i); 
                g.Children.Add(ell);
            }
            g.SetValue(Grid.RowProperty, 1);
            LayoutRoot.Children.Add(g);
        }
        #endregion

        #region -Load Config-
        void LoadConfig() {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            XElement xe = XElement.Load(System.IO.Path.Combine(dir, "App_Data/Setting.xml"));

            var query = from q in xe.Element("icons").Elements("icon")
                        select new IconSet()
                        {
                            Size = new Size(int.Parse(q.Element("width").Value), int.Parse(q.Element("height").Value)),
                            Type = q.Attribute("type").Value,
                            Source = q.Element("source").Value
                        };
            _iconSets = query.ToList();
        }
        #endregion
    }

    internal class IconSet
    {
        public string Type { get; set; }
        public Size Size { get; set; }
        public string Source { get; set; }
    }
    internal struct IconTitle
    {
        public string type;
        public string title;
    }
}
