using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZrClient.Model
{
     public class ModuleGroup : ObservableObject
    {
        private string groupName;
        private bool contractionTemplate = true;
        private ObservableCollection<Modules> modules;
        private string icon;
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon
        {
            get { return icon; }
            set { icon = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 收缩面板-模板
        /// </summary>
        public bool ContractionTemplate
        {
            get { return contractionTemplate; }
            set { contractionTemplate = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 包含的子模块
        /// </summary>
        public ObservableCollection<Modules> Modules
        {
            get { return modules; }
            set { modules = value; RaisePropertyChanged(); }
        }
    }
}
