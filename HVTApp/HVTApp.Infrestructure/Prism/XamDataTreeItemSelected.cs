using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Infragistics.Controls.Menus;

namespace HVTApp.Infrastructure.Prism
{
    public class XamDataTreeItemSelected
    {
        public static readonly DependencyProperty SelectedCommandBehaviorProperty = DependencyProperty.Register(
            "SelectedCommandBehavior", typeof(XamDataTreeCommandBehavior), typeof(XamDataTree), new PropertyMetadata());





        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(XamDataTree), new PropertyMetadata(OnCommandChangeCallback));
        public static ICommand GetCommand(XamDataTree menuItem)
        {
            return menuItem.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommand(XamDataTree menuItem, ICommand command)
        {
            menuItem.SetValue(CommandProperty, command);
        }
        private static void OnCommandChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree menuItem = d as XamDataTree;
            if (menuItem != null)
            {
                XamDataTreeCommandBehavior behavior = GetOrCreateCommandBehavior(menuItem);
                behavior.Command = e.NewValue as ICommand;
            }
        }


        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(XamDataTree), new PropertyMetadata(OnCommandParameterChangeCallback));

        public static object GetCommandParameter(XamDataTree menuItem)
        {
            return menuItem.GetValue(CommandParameterProperty);
        }
        public static void SetCommandProperty(XamDataTree menuItem, object parameter)
        {
            menuItem.SetValue(CommandParameterProperty, parameter);
        }

        private static void OnCommandParameterChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree menuItem = d as XamDataTree;
            if (menuItem != null)
            {
                XamDataTreeCommandBehavior behavior = GetOrCreateCommandBehavior(menuItem);
                behavior.CommandParameter = e.NewValue;
            }
        }




        private static XamDataTreeCommandBehavior GetOrCreateCommandBehavior(XamDataTree menuItem)
        {
            XamDataTreeCommandBehavior behavior = menuItem.GetValue(SelectedCommandBehaviorProperty) as XamDataTreeCommandBehavior;
            if (behavior == null)
            {
                behavior = new XamDataTreeCommandBehavior(menuItem);
                menuItem.SetValue(SelectedCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
    }
}
