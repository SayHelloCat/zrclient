using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZrClient.Common;
using ZrClient.Model;

namespace ZrClient.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {

        public HomeViewModel() {

            GridModelList = new ObservableCollection<UserModel>();
            GridModelList.Add(new UserModel() { Name = "Vaughan", Address = "Delaware", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S1", BackColor = "#FF7000" });
            GridModelList.Add(new UserModel() { Name = "Abbey", Address = "Florida", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S2", BackColor = "#FFC100" });
            GridModelList.Add(new UserModel() { Name = "Dahlia", Address = "Illinois", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S1", BackColor = "#FF7000" });
            GridModelList.Add(new UserModel() { Name = "Fallon", Address = "Tennessee", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S3", BackColor = "#59E6B5" });
            GridModelList.Add(new UserModel() { Name = "Hannah", Address = "Washington", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S4", BackColor = "#FFC100" });
            GridModelList.Add(new UserModel() { Name = "Laura", Address = "Mississippi", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S2", BackColor = "#59E6B5" });
            GridModelList.Add(new UserModel() { Name = "Lauren", Address = "Wyoming", Email = "jack163@outlook.com", UserType = "Quality inspector", Status = "S3", BackColor = "#FFC100" });
            Task.Run(new Action(() =>
            {
                this.InitCPUDatas();
                this.InitMemDatas();
            }));
        }
        #region =====data
        List<Task> taskList = new List<Task>();
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public ChartValues<ObservableValue> MemValues { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> CPUValues { get; set; }
        private ObservableCollection<UserModel> gridModelList;
        public ObservableCollection<UserModel> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
        private double currentCpu;
        public double CurrentCPU
        {
            get { return currentCpu; }
            set
            {
                currentCpu = Math.Ceiling(value);

                this.CPUValues.Add(new ObservableValue(currentCpu));
                this.CPUValues.RemoveAt(0);
            }
        }
        private double currnetMem;

        public double CurrentMem
        {
            get { return currnetMem; }
            set
            {
                currnetMem = Math.Ceiling(value);

                this.MemValues.Add(new ObservableValue(currnetMem));
                this.MemValues.RemoveAt(0);
            }
        }
        #endregion
        #region =====methods
        bool taskSwitch = true;
        Random random = new Random();
        private void InitCPUDatas()
        {
            this.CPUValues = new ChartValues<ObservableValue>();
            for (int i = 0; i < 40; i++)
            {
                this.CPUValues.Add(new ObservableValue(0.0f));
            }

            ComputerInfo monitor = ComputerInfo.GetInstance();

            var task = Task.Factory.StartNew(new Action(async () =>
            {
                while (taskSwitch)
                {
                    this.CurrentCPU = monitor.GetCPUInfo();
                    await Task.Delay(1000);
                }
            }));

            this.taskList.Add(task);
        }
        private void InitMemDatas()
        {
            this.MemValues.Clear();
            for (int i = 0; i < 40; i++)
            {
                this.MemValues.Add(new ObservableValue(0.0f));
            }
            var task = Task.Factory.StartNew(new Action(async () =>
            {
                while (taskSwitch)
                {
                    this.CurrentMem = random.Next((int)Math.Max(this.CurrentMem - 5, 0), (int)(this.CurrentMem + 5));
                    await Task.Delay(1000);
                }
            }));
            this.taskList.Add(task);
        }
        public void Dispose()
        {
            try
            {
                this.taskSwitch = false;
                Task.WaitAll(this.taskList.ToArray());
                ComputerInfo.GetInstance().Dispose();
            }
            catch { }

        }
        #endregion

    }
}
