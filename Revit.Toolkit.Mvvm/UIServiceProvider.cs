using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Toolkit.Mvvm
{
	public class UIServiceProvider : IUIServiceProvider
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002547 File Offset: 0x00000747
		public UIServiceProvider(UIControlledApplication application)
		{
			this._application = application;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002558 File Offset: 0x00000758
		public AddInId GetAddInId()
		{
			return this.GetUIApplication().ActiveAddInId;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002578 File Offset: 0x00000778
		public ControlledApplication GetApplication()
		{
			return this.GetUIApplication().ControlledApplication;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002598 File Offset: 0x00000798
		public UIControlledApplication GetUIApplication()
		{
			return this._application;
		}

		// Token: 0x04000007 RID: 7
		private UIControlledApplication _application;
	}
}
