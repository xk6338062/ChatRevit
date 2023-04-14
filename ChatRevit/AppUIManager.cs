using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.DependencyInjection;
using Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRevit
{
    public class AppUIManager : IApplicationUI
    {
        public Result Initial()
        {
            var application = Ioc.Default.GetService<UIControlledApplication>();
            var provider = Ioc.Default.GetService<IDockablePaneService>();
            var paneId = provider.GetDockablePaneId();
            application.RegisterDockablePane(paneId, "ChatRevit", provider);
            //var pane = application.GetDockablePane(paneId);
            //if (!pane.IsShown())
            //    pane.Show();
            return Result.Succeeded;

        }
    }
}
