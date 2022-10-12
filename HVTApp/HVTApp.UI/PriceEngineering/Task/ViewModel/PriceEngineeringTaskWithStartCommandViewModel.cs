using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskWithStartCommandViewModel : PriceEngineeringTaskViewModel
    {
        /// <summary>
        /// Событие старта задачи
        /// </summary>
        public event Action TaskStartedAction;

        public DelegateLogCommand StartCommand { get; private set; }

        protected PriceEngineeringTaskWithStartCommandViewModel(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected PriceEngineeringTaskWithStartCommandViewModel(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        protected override void InCtor()
        {
            base.InCtor();

            StartCommand = new DelegateLogCommand(() => { StartCommandExecute(true); },
                () =>
                    this.IsValid &&
                    this.IsChanged &&
                    (Status == PriceEngineeringTaskStatusEnum.Created || Status == PriceEngineeringTaskStatusEnum.Stopped) &&
                    UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) != null);

            this.PropertyChanged += (sender, args) => StartCommand.RaiseCanExecuteChanged();

        }


        /// <summary>
        /// Старт задачи
        /// </summary>
        /// <param name="saveChanges">Сохранить в конце и принять изменения?</param>
        public bool StartCommandExecute(bool saveChanges)
        {
            var messageService = Container.Resolve<IMessageService>();
            if (saveChanges)
            {
                if (messageService.ShowYesNoMessageDialog($"Вы уверены, что хотите cтартовать задачу?\n{this}", defaultNo: true) != MessageDialogResult.Yes)
                    return false;

                if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Model.Id) == null)
                {
                    UnitOfWork.Repository<PriceEngineeringTask>().Add(this.Model);
                }
            }

            this.Statuses.Add(PriceEngineeringTaskStatusEnum.Started);

            //если запускается только конкретная задача
            if (saveChanges)
            {
                var text = GetStartMessageText();
                this.SaveCommand.Execute();
                Messenger.SendMessage(text);
                Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Publish(this.Model);
            }
            //если запускаются все задачи в задании
            else
            {
                foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
                {
                    if (childPriceEngineeringTask is PriceEngineeringTaskWithStartCommandViewModel vm)
                    {
                        vm.StartCommandExecute(false);
                    }
                }
            }

            StartCommand.RaiseCanExecuteChanged();
            TaskStartedAction?.Invoke();

            if (saveChanges)
            {
                messageService.ShowOkMessageDialog("Уведомление", $"Задача успешно стартована!\n{this}");
            }

            return true;
        }

        private string GetStartMessageText()
        {
            var sb = new StringBuilder();
            if (this.FilesTechnicalRequirements.IsChanged)
            {
                sb.AppendLine("Внесены изменения в Техническое Задание.");
            }

            var actualFiles = FilesTechnicalRequirements.Where(x => x.IsActual).OrderBy(x => x.CreationMoment).ToList();
            if (actualFiles.Any())
            {
                sb.AppendLine("Актуальные файлы:");
                foreach (var file in actualFiles)
                {
                    sb.AppendLine($" + {file.CreationMoment} {file.Name}");
                }
            }

            var notActualFiles = FilesTechnicalRequirements.Where(x => x.IsActual == false).OrderBy(x => x.CreationMoment).ToList();
            if (notActualFiles.Any())
            {
                sb.AppendLine("Не актуальные файлы:");
                foreach (var file in notActualFiles)
                {
                    sb.AppendLine($" - {file.CreationMoment} {file.Name}");
                }
            }

            return sb.ToString().TrimEnd('\n', '\r');
        }
    }
}