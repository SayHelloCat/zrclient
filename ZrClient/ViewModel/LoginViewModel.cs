using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZrClient.View;

namespace ZrClient.ViewModel
{
     public class LoginViewModel: ViewModelBase
    {
        
        public LoginViewModel() 
        {
            loginCmd = new RelayCommand<object> (LoginMethod);
        }
        #region =====data
        /// <summary>
        /// 用户名
        /// </summary>
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 密码
        /// </summary>
        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }
        #endregion
        #region ====Cmd
        public RelayCommand<object> loginCmd { get; set; }
        #endregion
        private void LoginMethod(object o)
        {
            var values = (object[])o;
            var psdControl =(PasswordBox) values[1];
            string psdStr = psdControl.Password;
            if (string.IsNullOrEmpty(userName))
            {
                Messenger.Default.Send("登录名不能为空", "UserNameErrorToken");
                return;
            }
            if (string.IsNullOrEmpty(psdStr))
            {
                psdControl.IsError = true;
                psdControl.ErrorStr = "密码不能为空";
                return;
            }
            MainWindow mainView = new MainWindow();
            (values[0] as System.Windows.Window).Close();
            mainView.ShowDialog();
        }
    }
}
