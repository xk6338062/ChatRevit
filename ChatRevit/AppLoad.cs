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
            Services.AddScoped<IDockablePaneService, ChatRevitService>();
            Services.AddSingleton<IApplicationUI, AppUIManager>();
            Services.AddScoped(typeof(ChatRevitPanel));
            Services.AddScoped(typeof(ChatRevitViewModel));
            Ioc.Default.ConfigureServices(
                Services
                .BuildServiceProvider());
        }
    }
}
