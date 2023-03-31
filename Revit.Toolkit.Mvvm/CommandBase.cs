using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit.Toolkit.Mvvm.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Toolkit.Mvvm
{
    public abstract class CommandBase : IExternalCommand
    {

        public Window MainWindow { get; set; }
        public virtual void RegisterTypes(Ioc ioc) { }

        public abstract Window CreateMainWindow();

        public abstract Result Execute(ref string message, ElementSet elements);

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton(commandData.Application.ActiveUIDocument.Document)
                .BuildServiceProvider()); 

            RegisterTypes(Ioc.Default);

            var window = CreateMainWindow();
            if (window != null)
            {
                MainWindow = window;
            }

            Execute(ref message, elements);

            return Result.Succeeded;

        }

        protected IDataContext DataContext
        {
            get
            {
                return Ioc.Default.GetService<IDataContext>();
            }
        }
    }
}
