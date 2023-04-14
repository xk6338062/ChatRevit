using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.DependencyInjection;
using Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatRevit
{
    public class ChatRevitService : IDockablePaneService
    {
        public static string PageGuid = "A811F969-2114-4AC8-A289-F5A8982946CE";

        public ChatRevitService()
        {
        }

        public FrameworkElement GetDockablePane()
        {
            var mainPage = Ioc.Default.GetService<ChatRevitPanel>();
            mainPage.DataContext = Ioc.Default.GetService<ChatRevitViewModel>();
            return mainPage;
        }

        public DockablePaneId GetDockablePaneId()
        {
            return new DockablePaneId(new Guid(PageGuid));
        }

        public string GetDockablePaneTitle()
        {
            return "ChatRevit";
        }

        public Type GetServiceType()
        {
            return typeof(ChatRevitService);
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this.GetDockablePane();
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Bottom
            };
            data.VisibleByDefault = true;
            data.EditorInteraction = new EditorInteraction(EditorInteractionType.KeepAlive);
        }
    }
}
