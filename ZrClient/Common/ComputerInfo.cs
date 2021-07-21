using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZrClient.Common
{
    public class ComputerInfo
    {
        private PerformanceCounter cpuCounter;
        private ManagementClass memCounter;

        private static ComputerInfo instance;

        public static ComputerInfo GetInstance()
        {
            lock ("instance")
            {
                if (instance == null)
                {
                    instance = new ComputerInfo();

                    instance.cpuCounter = new PerformanceCounter();
                    instance.cpuCounter.CategoryName = "Processor";
                    instance.cpuCounter.CounterName = "% Processor Time";
                    instance.cpuCounter.InstanceName = "_Total";
                    instance.cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");


                    instance.memCounter = new ManagementClass();
                }

                return instance;
            }
        }

        public double GetMemInfo()
        {
            memCounter.Path = new ManagementPath("Win32_PhysicalMemory");
            ManagementObjectCollection moc = memCounter.GetInstances();
            double available = 0, capacity = 0;

            foreach (ManagementObject mo1 in moc)
            {
                capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
            }
            moc.Dispose();

            memCounter.Path = new ManagementPath("Win32_PerfFormattedData_PerfOS_Memory");
            moc = memCounter.GetInstances();
            foreach (ManagementObject mo2 in moc)
            {
                available += ((Math.Round(Int64.Parse(mo2.Properties["AvailableMBytes"].Value.ToString()) / 1024.0, 1)));
            }
            moc.Dispose();

            return (capacity - available) / capacity * 100;
        }

        public double GetCPUInfo()
        {
            return this.cpuCounter.NextValue();
        }

        public void Dispose()
        {
            this.memCounter?.Dispose();
            this.cpuCounter?.Dispose();
        }
    }
}
