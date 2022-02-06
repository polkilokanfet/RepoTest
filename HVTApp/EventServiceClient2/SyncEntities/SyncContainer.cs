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

        public SyncContainer(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId)
        {
            //Задачи из DirectumLite
            this.Add(new SyncDirectumTask(container, eventServiceHost, appSessionId));
            this.Add(new SyncDirectumTaskStart(container, eventServiceHost, appSessionId));
            this.Add(new SyncDirectumTaskStop(container, eventServiceHost, appSessionId));
            this.Add(new SyncDirectumTaskPerform(container, eventServiceHost, appSessionId));
            this.Add(new SyncDirectumTaskAccept(container, eventServiceHost, appSessionId));
            this.Add(new SyncDirectumTaskReject(container, eventServiceHost, appSessionId));

            //Задачи TCE
            this.Add(new SyncTechnicalRequrementsTask(container, eventServiceHost, appSessionId));
            this.Add(new SyncTechnicalRequrementsTaskStart(container, eventServiceHost, appSessionId));
            this.Add(new SyncTechnicalRequrementsTaskInstruct(container, eventServiceHost, appSessionId));
            this.Add(new SyncTechnicalRequrementsTaskReject(container, eventServiceHost, appSessionId));
            this.Add(new SyncTechnicalRequrementsTaskRejectByFrontManager(container, eventServiceHost, appSessionId));
            this.Add(new SyncTechnicalRequrementsTaskFinish(container, eventServiceHost, appSessionId));
            this.Add(new SyncTechnicalRequrementsTaskAccept(container, eventServiceHost, appSessionId));
            this.Add(new SyncTechnicalRequrementsTaskStop(container, eventServiceHost, appSessionId));

            //Калькуляции себестоимости
            this.Add(new SyncPriceCalculation(container, eventServiceHost, appSessionId));       //Калькуляции себестоимости сохранение
            this.Add(new SyncPriceCalculationStart(container, eventServiceHost, appSessionId));  //Калькуляции себестоимости старт
            this.Add(new SyncPriceCalculationFinish(container, eventServiceHost, appSessionId)); //Калькуляции себестоимости финиш
            this.Add(new SyncPriceCalculationCancel(container, eventServiceHost, appSessionId)); //Калькуляции себестоимости остановка
            this.Add(new SyncPriceCalculationReject(container, eventServiceHost, appSessionId)); //Калькуляции себестоимости отклонение
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