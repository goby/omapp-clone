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

namespace OperatingManagement.WpfObserver
{
    /// <summary>
    /// Interaction logic for MyTip.xaml
    /// </summary>
    public partial class MyTip : UserControl
    {
        public MyTip()
        {
            InitializeComponent();
        }
        double _Width;
        public new double Width
        {
            get { return _Width; }
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    this.Width = _Width;
                    myTip.Width = _Width - 2;
                    myTipText.Width = _Width - 20;
                }
            }
        }
        double _Height;
        public new double Height
        {
            get { return _Height; }
            set
            {
                if (_Height != value)
                {
                    _Height = value;
                    this.Height = _Height;
                    myTip.Height = _Height - 2;
                    myTipText.Height = _Height - 10;
                }
            }
        }
        string _Text;
        public string Text
        {
            get { return _Text; }
            set {
                if (_Text != value)
                {
                    _Text = value;
                    myTipText.Text = _Text;
                }
            }
        }
    }
}
