using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;

namespace ZrClient.Model
{
    public class TabModel : ObservableObject
    {
        private string header;
        private FrameworkElement content;
        private string code;

        public string Header
        {
            get { return header; }
            set { header = value; RaisePropertyChanged(); }
        }

        public FrameworkElement Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }

        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(); }
        }
    }
}
