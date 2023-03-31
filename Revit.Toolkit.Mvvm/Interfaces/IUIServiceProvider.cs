using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Toolkit.Mvvm.Interfaces
{
    public interface IUIServiceProvider
    {
        // Token: 0x06000086 RID: 134
        UIControlledApplication GetUIApplication();

        // Token: 0x06000087 RID: 135
        ControlledApplication GetApplication();

        // Token: 0x06000088 RID: 136
        AddInId GetAddInId();
    }
}
