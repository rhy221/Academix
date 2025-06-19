using System;
using System.Windows.Controls;

namespace Academix.Helpers
{
    public static class ViewAccessHelper
    {
        public static bool HasAccess(string viewName)
        {
            return Session.ManHinhDuocLoadDuocPhep.Contains(viewName);
        }

        public static UserControl? CreateViewInstance(string viewName)
        {
            var fullName = $"Academix.Views.{viewName}";
            var viewType = Type.GetType(fullName);
            if (viewType != null && typeof(UserControl).IsAssignableFrom(viewType))
                return Activator.CreateInstance(viewType) as UserControl;
            return null;
        }
    }
}
