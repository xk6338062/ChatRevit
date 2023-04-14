using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Revit.Toolkit.Mvvm;
using Revit.Toolkit.Mvvm.Interfaces;
using System;

namespace ChatRevit
{
    public class AppLoad : ApplicationBase
    {
        public override void RegisterTypes()
        {
            Services.AddSingleton<IDockablePaneService, ChatRevitService>();
            Services.AddSingleton<IApplicationUI, AppUIManager>();
            Services.AddSingleton(typeof(ChatRevitPanel));
            Services.AddSingleton(typeof(ChatRevitViewModel));
            Ioc.Default.ConfigureServices(
                Services
                .BuildServiceProvider());
        }
    }
}
