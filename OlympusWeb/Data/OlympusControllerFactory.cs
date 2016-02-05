using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace OlympusWeb.Data
{
    public class OlympusControllerFactory: DefaultControllerFactory 
    {
        private DataContext dataContext = new DataContext();

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)Activator.CreateInstance(controllerType, dataContext);
        }
    }
}