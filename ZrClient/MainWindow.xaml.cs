using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZrClient.Common;
using ZrClient.ViewModel;
using System.Windows.Controls.Primitives;

namespace ZrClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            Messenger.Default.Register<string>(this, "ExpandMenu", arg =>
            {
                if (this.menu.Width < 200)
                {
                    this.userName.Visibility = Visibility.Visible;
                    AnimationHelper.CreateWidthChangedAnimation(this.menu, 60, 200, new TimeSpan(0, 0, 0, 0, 300));
                }
                else
                {
                    this.userName.Visibility = Visibility.Collapsed;
                    AnimationHelper.CreateWidthChangedAnimation(this.menu, 200, 60, new TimeSpan(0, 0, 0, 0, 300));
                }
                   

                //由于...
                var template = this.IC.ItemTemplateSelector;
                this.IC.ItemTemplateSelector = null;
                this.IC.ItemTemplateSelector = template;
               
            });
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void MinWin_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxWin_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseWin_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Grid gridtemp = (Grid) btn.Template.FindName("gridtemp",btn);
            Popup menuPop = (Popup)gridtemp.FindName("menuPop");
            menuPop.IsOpen = true;
        }
    }
}
