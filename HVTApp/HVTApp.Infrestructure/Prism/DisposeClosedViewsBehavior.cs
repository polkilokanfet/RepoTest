using System;
using System.Collections.Specialized;
using System.Windows;
using Prism.Regions;

namespace HVTApp.Infrastructure.Prism
{
    /// <summary>
    /// Поведение, при котором при удалении вида вызывается Dispose
    /// </summary>
    public class DisposeClosedViewsBehavior : RegionBehavior
    {
        protected override void OnAttach()
        {
            Region.Views.CollectionChanged += Views_CollectionChanged;
        }

        private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Remove) return;

            foreach (var removedView in e.OldItems)
            {
                IDisposable disposableView = removedView as IDisposable;
                IDisposable disposableViewModel = null;

                if (removedView is FrameworkElement frameworkElement)
                {
                    disposableViewModel = frameworkElement.DataContext as IDisposable;
                }

                disposableView?.Dispose();
                disposableViewModel?.Dispose();
            }
        }
    }
}