using Autodesk.Revit.UI;
using Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Toolkit.Mvvm
{
    public class ExternalEventService : IExternalEventService
    {

        public ExternalEventService()
        {
            _externalEvent = ExternalEvent.Create(this);
        }

        public void Execute(UIApplication app)
        {
            _action(app);
        }

        public string GetName()
        {
            return "hide-show";
        }

        public async Task<ExternalEventRequest> Raise(Action<UIApplication> action)
        {
            var request = default(ExternalEventRequest);
            if (action != null)
            {
                request = await Task.Factory.StartNew(() =>
                {
                    _action = action;
                    return _externalEvent.Raise();
                });
            }
            return request;
        }

        private ExternalEvent _externalEvent;

        private Action<UIApplication> _action;
    }
}
