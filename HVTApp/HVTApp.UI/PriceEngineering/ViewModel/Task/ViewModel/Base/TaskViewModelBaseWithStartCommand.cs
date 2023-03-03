using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelBaseWithStartCommand : TaskViewModel
    {
        /// <summary>
        /// ������� ������ ������
        /// </summary>
        public event Action TaskStartedAction;

        public DelegateLogCommand StartCommand { get; private set; }

        protected TaskViewModelBaseWithStartCommand(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected TaskViewModelBaseWithStartCommand(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        protected override void InCtor()
        {
            base.InCtor();

            StartCommand = new DelegateLogCommand(() => { StartCommandExecute(true); },
                () =>
                    this.IsValid &&
                    this.IsChanged &&
                    (Status.Equals(ScriptStep.Create) || Status.Equals(ScriptStep.Stop) || Status.Equals(ScriptStep.RejectByHead) || Status.Equals(ScriptStep.RejectByConstructor)) &&
                    UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Model.Id) != null);
        }


        /// <summary>
        /// ����� ������
        /// </summary>
        /// <param name="saveChanges">��������� � ����� � ������� ���������?</param>
        public bool StartCommandExecute(bool saveChanges)
        {
            var messageService = Container.Resolve<IMessageService>();
            if (saveChanges)
            {
                if (messageService.ShowYesNoMessageDialog($"�� �������, ��� ������ c��������� ������?\n{this}", defaultNo: true) != MessageDialogResult.Yes)
                    return false;

                if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Model.Id) == null)
                {
                    UnitOfWork.Repository<PriceEngineeringTask>().Add(this.Model);
                }
            }

            this.Statuses.Add(ScriptStep.Start);

            //���� ����������� ������ ���������� ������
            if (saveChanges)
            {
                var text = GetStartMessageText();
                this.SaveCommand.Execute();
                if(GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
                    Messenger.SendMessage(text);
                Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Publish(this.Model);
            }
            //���� ����������� ��� ������ � �������
            else
            {
                foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
                {
                    if (childPriceEngineeringTask is TaskViewModelBaseWithStartCommand vm)
                    {
                        vm.StartCommandExecute(false);
                    }
                }
            }

            StartCommand.RaiseCanExecuteChanged();
            TaskStartedAction?.Invoke();

            if (saveChanges)
            {
                messageService.ShowOkMessageDialog("�����������", $"������ ������� ����������!\n{this}");
            }

            return true;
        }

        private string GetStartMessageText()
        {
            var sb = new StringBuilder();
            if (this.FilesTechnicalRequirements.IsChanged)
            {
                sb.AppendLine("������� ��������� � ����������� �������.");
            }

            var actualFiles = FilesTechnicalRequirements.Where(x => x.IsActual).OrderBy(x => x.CreationMoment).ToList();
            if (actualFiles.Any())
            {
                sb.AppendLine("���������� �����:");
                foreach (var file in actualFiles)
                {
                    sb.AppendLine($" + {file.CreationMoment} {file.Name}");
                }
            }

            var notActualFiles = FilesTechnicalRequirements.Where(x => x.IsActual == false).OrderBy(x => x.CreationMoment).ToList();
            if (notActualFiles.Any())
            {
                sb.AppendLine("�� ���������� �����:");
                foreach (var file in notActualFiles)
                {
                    sb.AppendLine($" - {file.CreationMoment} {file.Name}");
                }
            }

            return sb.ToString().TrimEnd('\n', '\r');
        }
    }
}