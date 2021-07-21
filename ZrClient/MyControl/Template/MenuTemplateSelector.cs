using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZrClient.Model;

namespace ZrClient.MyControl.Template
{
    public class MenuTemplateSelector: DataTemplateSelector
    {
        public DataTemplate GroupTemplate { get; set; }
        public DataTemplate ExpanderTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ModuleGroup group = (ModuleGroup)item;
            if (group != null)
            {
                if (!group.ContractionTemplate)
                    return ExpanderTemplate;
                else
                    return GroupTemplate;
            }
            return ExpanderTemplate;
        }
    }
}
