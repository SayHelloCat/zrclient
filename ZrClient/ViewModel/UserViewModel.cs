using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZrClient.Model;

namespace ZrClient.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel()
        {

            GridModelList = new ObservableCollection<UserModel>();
            GridModelList.Add(new UserModel() { Index="1", Name = "Vaughan", Address = "Delaware", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S1", BackColor = "#FF7000" });
            GridModelList.Add(new UserModel() { Index = "2", Name = "Abbey", Address = "Florida", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S2", BackColor = "#FFC100" });
            GridModelList.Add(new UserModel() { Index = "3", Name = "Dahlia", Address = "Illinois", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S1", BackColor = "#FF7000" });
            GridModelList.Add(new UserModel() { Index = "4", Name = "Fallon", Address = "Tennessee", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S3", BackColor = "#59E6B5" });
            GridModelList.Add(new UserModel() { Index = "5", Name = "Hannah", Address = "Washington", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S4", BackColor = "#FFC100" });
            GridModelList.Add(new UserModel() { Index = "6", Name = "Laura", Address = "Mississippi", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S2", BackColor = "#59E6B5" });
            GridModelList.Add(new UserModel() { Index = "7", Name = "Lauren", Address = "Wyoming", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S3", BackColor = "#FFC100" });
        }
        private ObservableCollection<UserModel> gridModelList;
        public ObservableCollection<UserModel> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
    }
}
