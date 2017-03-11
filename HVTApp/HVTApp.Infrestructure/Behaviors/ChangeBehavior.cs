using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace HVTApp.Infrastructure.Behaviors
{
    public static class ChangeBehavior
    {
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty OriginalValueProperty;
        public static readonly DependencyProperty IsChangedProperty;
        public static readonly DependencyProperty OriginalValueConverterProperty;

        private static readonly Dictionary<Type, DependencyProperty> DefaultProperties;

        static ChangeBehavior()
        {
            IsActiveProperty = DependencyProperty.RegisterAttached("IsActive", typeof (bool),
                typeof (ChangeBehavior), new PropertyMetadata(false, OnIsActivePropertyChanged));
            OriginalValueProperty = DependencyProperty.RegisterAttached("OriginalValue", typeof (object),
                typeof (ChangeBehavior), new PropertyMetadata(null));
            IsChangedProperty = DependencyProperty.RegisterAttached("IsChanged", typeof (bool),
                typeof (ChangeBehavior), new PropertyMetadata(false));
            OriginalValueConverterProperty = DependencyProperty.RegisterAttached("OriginalValueConverter",
                typeof (IValueConverter), typeof (ChangeBehavior), new PropertyMetadata(null, OnOriginalValueConverterPropertyChanged));

            DefaultProperties = new Dictionary<Type, DependencyProperty>
            {
                [typeof (TextBox)] = TextBox.TextProperty,
                [typeof (CheckBox)] = ToggleButton.IsCheckedProperty,
                [typeof(DatePicker)] = DatePicker.SelectedDateProperty,
                [typeof(ComboBox)] = Selector.SelectedItemProperty
            };
        }


        public static bool GetIsActive(DependencyObject obj) { return (bool)obj.GetValue(IsActiveProperty); }
        public static void SetIsActive(DependencyObject obj, bool value) { obj.SetValue(IsActiveProperty, value); }

        public static object GetOriginalValue(DependencyObject obj) { return (object)obj.GetValue(OriginalValueProperty); }
        public static void SetOriginalValue(DependencyObject obj, object value) { obj.SetValue(OriginalValueProperty, value); }

        public static bool GetIsChanged(DependencyObject obj) { return (bool)obj.GetValue(IsChangedProperty); }
        public static void SetIsChanged(DependencyObject obj, bool value) { obj.SetValue(IsChangedProperty, value); }



        public static IValueConverter GetOriginalValueConverter(DependencyObject obj)
        {
            return (IValueConverter)obj.GetValue(OriginalValueConverterProperty);
        }

        public static void SetOriginalValueConverter(DependencyObject obj, IValueConverter value)
        {
            obj.SetValue(OriginalValueConverterProperty, value);
        }


        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DefaultProperties.ContainsKey(d.GetType()))
            {
                var defaultProperty = DefaultProperties[d.GetType()];
                if ((bool) e.NewValue)
                {
                    var binding = BindingOperations.GetBinding(d, defaultProperty);
                    if (binding != null)
                    {
                        var bindingPath = binding.Path.Path;
                        BindingOperations.SetBinding(d, IsChangedProperty, 
                            new Binding(bindingPath + "IsChanged"));

                        CreateOriginalValueBinding(d, bindingPath + "OriginalValue");
                    }
                }
                else
                {
                    BindingOperations.ClearBinding(d, IsChangedProperty);
                    BindingOperations.ClearBinding(d, OriginalValueProperty);
                }
            }
        }

        private static void OnOriginalValueConverterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var originalValueBinding = BindingOperations.GetBinding(d, OriginalValueProperty);
            if (originalValueBinding != null)
            {
                CreateOriginalValueBinding(d, originalValueBinding.Path.Path);
            }
        }

        private static void CreateOriginalValueBinding(DependencyObject d, string originalValueBinding)
        {
            Binding newBinding = new Binding(originalValueBinding)
            {
                Converter = GetOriginalValueConverter(d)
            };
            BindingOperations.SetBinding(d, OriginalValueProperty, newBinding);
        }
    }
}
