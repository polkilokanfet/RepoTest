﻿using System.Linq;
using System.Windows;
using EventServiceClient2;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.OutlookBar.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Views
{
    public partial class MainWindow
    {
        private readonly IUnityContainer _container;

        public MainWindow(IUnityContainer container)
        {
            _container = container;

            InitializeComponent();

            this.Closing += (sender, args) =>
            {
#if DEBUG
#else
                var dr = _container.Resolve<IMessageService>().ConfirmationDialog("Выход", "Вы уверены, что хотите выйти?", defaultNo:true);
                if (dr == false)
                {
                    args.Cancel = true;
                    return;
                }
#endif

                //остановка синхронизатора
                //_container.Resolve<IEventServiceClient>().Stop();
            };
        }

        /// <summary>
        /// Реакция на смену выделенной группы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XamOutlookBar_OnSelectedGroupChanging(object sender, SelectedGroupChangingEventArgs e)
        {
            if (e.NewSelectedOutlookBarGroup is IOutlookBarGroup outlookBarGroup)
            {
                //грузим вид из этой группы
                Commands.NavigateCommand.Execute(outlookBarGroup.DefaultViewUri);
            }
        }

        //нужно, чтобы при первой загрузке отображался дефолтный вид
        private void XamOutlookBar_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is XamOutlookBar outlookBar)
            {
                //if (outlookBar.SelectedGroup == null)
                //{
                //    return;
                //}

                outlookBar.SelectedGroup.Loaded += (s, args) =>
                {
                    if (s is IOutlookBarGroup outlookBarGroup)
                    {
                        Commands.NavigateCommand.Execute(outlookBarGroup.DefaultViewUri);
                    }
                };
            }
        }
    }
}
