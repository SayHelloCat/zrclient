using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ZrClient.ViewModel
{
     public class BaseViewModel<TView>: ObservableObject where TView : Window, new()
    {
        public BaseViewModel()
        {
            ExitCommand = new RelayCommand(Exit);
        }
        public TView view = new TView();
        public RelayCommand ExitCommand { get; private set; }


        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> ShowDialog()
        {
            this.SubscribeMessenger();
            this.SubscribeEvent();
            //this.BindDefaultViewModel();
            var result = view.ShowDialog();
            return await Task.FromResult((bool)result);
        }
        /// <summary>
        /// 注册默认事件
        /// </summary>
        public void SubscribeEvent()
        {
            view.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    view.DragMove();
            };
        }
        public virtual void SubscribeMessenger()
        {
            //最小化
            Messenger.Default.Register<string>(this, "WindowMinimize", (sender) =>
            {
                view.WindowState = System.Windows.WindowState.Minimized;
            });
            //最大化
            Messenger.Default.Register<string>(this, "WindowMaximize", (sender) =>
            {
                if (view.WindowState == System.Windows.WindowState.Maximized)
                    view.WindowState = System.Windows.WindowState.Normal;
                else
                    view.WindowState = System.Windows.WindowState.Maximized;
            });
            //关闭系统
            Messenger.Default.Register<string>(this, "Exit", async (sender) =>
            {
                // if (!await Msg.Question("确认退出系统?")) return;
                Environment.Exit(0);
            });
        }
        public virtual void UnsubscribeMessenger()
        {
            Messenger.Default.Unregister(this);
        }
        /// <summary>
        /// 传递True代表需要确认用户是否关闭,你可以选择传递false强制关闭
        /// </summary>
        public virtual void Exit()
        {
            Messenger.Default.Send("", "Exit");
        }

        private bool isOpen;


        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="msg"></param>
        public void SnackBar(string msg)
        {
            Messenger.Default.Send(msg, "Snackbar");
        }
    }
}
