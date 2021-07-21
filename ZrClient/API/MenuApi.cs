using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZrClient.Model;

namespace ZrClient.API
{
     public class MenuApi
    {
        public async Task<List<ModuleGroup>> getGroup() 
        {
            List<ModuleGroup> list = new List<ModuleGroup>();
            list.Add(new ModuleGroup
            {
                GroupName = "系统管理",
                Icon= "\ue691",
                ContractionTemplate = false,
                Modules = new System.Collections.ObjectModel.ObservableCollection<Modules>(getModule())
            });
            list.Add(new ModuleGroup { 
              GroupName="统计报表",
              ContractionTemplate=false,
              Icon= "\ue670"
            });
            return list;
        }
        public List<Modules> getModule()
        {
            List<Modules> list = new List<Modules>();
            list.Add(new Modules
            {
                Code = "\ue693",
                Name = "用户管理",
                TypeName = "system.user.User"
            });
            list.Add(new Modules
            {
                Code = "\ue663",
                Name = "角色管理",
                TypeName = "system.user.User"
            });
            list.Add(new Modules
            {
                Code = "\ue695",
                Name = "通知公告",
                TypeName = "system.user.User"
            });
            return list;
        }
    }
}
