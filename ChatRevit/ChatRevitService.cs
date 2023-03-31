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
        public static string PageGuid = "3C3F2CCB-CA0B-4427-AACE-2694B685A4B2";

        public ChatRevitService()
        {
        }

        public FrameworkElement GetDockablePane()
        {
            var mainPage = Ioc.Default.GetService<ChatRevitPanel>();
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
