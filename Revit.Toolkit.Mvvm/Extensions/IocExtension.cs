using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Toolkit.Mvvm.Extensions
{
    public static class IocExtension
    {
        public static TView Resolve<TView, TViewModel>(this Ioc container, bool modeless = false) where TView : Window where TViewModel : class
        {
            //var view=modeless?container.GetService<TView>():container.GetService<>
            var view = container.GetService<TView>();
            view.DataContext = container.GetService(typeof(TViewModel));
            return view;
        }
    }
}
