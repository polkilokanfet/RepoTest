using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2.SyncEntities
{
    /// <summary>
    /// Контейнер синхронизации
    /// </summary>
    public class SyncContainer : IDisposable
    {
        readonly List<ISyncUnit> _list = new List<ISyncUnit>();

        public SyncContainer(IUnityContainer container)
        {
            //Задачи из DirectumLite
            this.Add(new SyncDirectumTask(container));
            this.Add(new SyncDirectumTaskStart(container));
            this.Add(new SyncDirectumTaskStop(container));
            this.Add(new SyncDirectumTaskPerform(container));
            this.Add(new SyncDirectumTaskAccept(container));
            this.Add(new SyncDirectumTaskReject(container));

            //Задачи TCE
            this.Add(new SyncTechnicalRequrementsTask(container));
            this.Add(new SyncTechnicalRequrementsTaskStart(container));
            this.Add(new SyncTechnicalRequrementsTaskInstruct(container));
            this.Add(new SyncTechnicalRequrementsTaskReject(container));
            this.Add(new SyncTechnicalRequrementsTaskRejectByFrontManager(container));
            this.Add(new SyncTechnicalRequrementsTaskFinish(container));
            this.Add(new SyncTechnicalRequrementsTaskAccept(container));
            this.Add(new SyncTechnicalRequrementsTaskStop(container));

            //Калькуляции себестоимости
            this.Add(new SyncPriceCalculation(container));       //Калькуляции себестоимости сохранение
            this.Add(new SyncPriceCalculationStart(container));  //Калькуляции себестоимости старт
            this.Add(new SyncPriceCalculationFinish(container)); //Калькуляции себестоимости финиш
            this.Add(new SyncPriceCalculationCancel(container)); //Калькуляции себестоимости остановка
            this.Add(new SyncPriceCalculationReject(container)); //Калькуляции себестоимости отклонение
        }

        private void Add(ISyncUnit member)
        {
            if (_list.Any(syncUnit => syncUnit.ModelType == member.ModelType && syncUnit.EventType == member.EventType))
            {
                throw new ArgumentException("Попытка повторной регистрации типа");
            }

            _list.Add(member);

            member.ServiceHostDisabled += MemberOnServiceHostDisabled;
        }

        private void MemberOnServiceHostDisabled()
        {
            this.ServiceHostIsDisabled?.Invoke();
        }

        public void Connect(ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId)
        {
            foreach (var syncUnit in _list)
            {
                syncUnit.Connect(eventServiceHost, appSessionId);
            }
        }

        public void Disconnect()
        {
            foreach (var syncUnit in _list)
            {
                syncUnit.Disconnect();
            }
        }

        /// <summary>
        /// Публикация события синхронизации только внутри текущего приложения
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="model"></param>
        public void PublishWithinApp<TModel, TEvent>(TModel model)
            where TModel : BaseEntity
            where TEvent : PubSubEvent<TModel>
        {
            //переводим в основной поток
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    //публикуем событие
                    _list.Single(syncUnit => syncUnit.ModelType == typeof(TModel) && syncUnit.EventType == typeof(TEvent)).PublishWithinApp(model);
                });
        }

        public void Dispose()
        {
            foreach (var member in _list)
            {
                member.ServiceHostDisabled -= MemberOnServiceHostDisabled;
                member.Dispose();
            }
            _list.Clear();
        }

        /// <summary>
        /// Хост сервиса стал недоступен
        /// </summary>
        public event Action ServiceHostIsDisabled;
    }
}