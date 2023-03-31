using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Toolkit.Mvvm.Interfaces
{
    public interface IEventManager
    {
        void Subscribe();

        void Unsubscribe();
    }
}
