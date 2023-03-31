using Autodesk.Revit.UI;
using Revit.Toolkit.Mvvm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Revit.Toolkit.Mvvm
{
    public abstract class ApplicationBase : IExternalApplication
    {
        public abstract void RegisterTypes();

        public static IServiceCollection Services { get; } = new ServiceCollection();

        public Result OnShutdown(UIControlledApplication application)
        {
            var events = Ioc.Default.GetService<IEventManager>();
            if (events != null)
            {
                events.Unsubscribe();
            }
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            Services
                .AddSingleton(application)
                .AddSingleton<IUIProvider, UIProvider>()
                .AddScoped<IDataContext, DataContext>()
                .AddSingleton<IUIServiceProvider, UIServiceProvider>()
                .AddSingleton<IExternalEventService, ExternalEventService>();

            RegisterTypes();

            var events = Ioc.Default.GetService<IEventManager>();
            if (events != null)
            {
                events.Subscribe();
            }

            var appUI = Ioc.Default.GetService<IApplicationUI>();
            return appUI == null ? Result.Cancelled : appUI.Initial();
        }
    }
}
