using System;
using System.ComponentModel;

namespace HVTApp.Model.Tests.WrapperTests
{
    public static class TestExtensions
    {
        public static bool PropertyChangedEventRised(this INotifyPropertyChanged obj, string propertyName, Action action)
        {
            var fired = false;
            var wrapper = obj;
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    fired = true;
                }
            };

            action();

            return fired;
        }
    }
}