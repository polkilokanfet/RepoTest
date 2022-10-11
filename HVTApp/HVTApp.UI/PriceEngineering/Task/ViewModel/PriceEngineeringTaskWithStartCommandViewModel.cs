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
        /// ������� ������ ������
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

            this.SetStatusStart();

            //���� ����������� ������ ���������� ������
            if (saveChanges)
            {
                this.SaveCommand.Execute();
                Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Publish(this.Model);
            }
            //���� ����������� ��� ������ � �������
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
                messageService.ShowOkMessageDialog("�����������", $"������ ������� ����������!\n{this}");
            }

            return true;
        }

        private void SetStatusStart()
        {
            var sb = new StringBuilder();
            sb.AppendLine("������ �������� �� ����������.");
            if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) != null)
            {
                if (this.FilesTechnicalRequirements.IsChanged)
                {
                    sb.AppendLine("������� ��������� � ��.");
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

            }

            if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) != null)
            {
                this.SetStatus(PriceEngineeringTaskStatusEnum.Started, sb.ToString().TrimEnd('\n', '\r'));
            }
            else
            {
                this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus
                {
                    StatusEnum = PriceEngineeringTaskStatusEnum.Started
                }));

                this.Model.Messages.Add(new PriceEngineeringTaskMessage
                {
                    Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                    Message = sb.ToString().TrimEnd('\n', '\r')
                });
            }
        }
    }
}