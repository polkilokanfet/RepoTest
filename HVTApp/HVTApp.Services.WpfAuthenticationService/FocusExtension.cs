using System;
using System.Windows;

namespace HVTApp.Services.WpfAuthenticationService
{
    public static class FocusExtension
    {
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            "IsFocused", typeof(bool), typeof(FocusExtension), new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ((UIElement) d).Focus();
            }
        }

        public static bool GetIsFocused(DependencyObject element)
        {
            return (bool) element.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject element, bool value)
        {
            element.SetValue(IsFocusedProperty, value);
        }



    }
}