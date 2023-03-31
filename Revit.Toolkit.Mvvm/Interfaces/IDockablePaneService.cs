using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Toolkit.Mvvm.Interfaces
{

    public interface IDockablePaneService : IDockablePaneProvider
    {
        Type GetServiceType();

        Autodesk.Revit.UI.DockablePaneId GetDockablePaneId();

        FrameworkElement GetDockablePane();

        string GetDockablePaneTitle();
    }
}
