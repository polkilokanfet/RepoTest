using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskWithStartCommandViewModel
    {
        public override bool IsTarget => true;

        public override bool IsEditMode
        {
            get
            {
                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                    case PriceEngineeringTaskStatusEnum.Stopped:
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return true;
                }

                return false;
            }
        }

        #region Commands

        public DelegateLogCommand AddTechnicalRequrementsFilesCommand { get; private set; }
        public DelegateLogConfirmationCommand RemoveTechnicalRequrementsFilesCommand { get; private set; }

        #endregion


        #region ctors

        /// <summary>
        /// ��� �������� (��������������) ��������� ������
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected PriceEngineeringTaskViewModelManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId) { }

        /// <summary>
        /// ��� �������� ����� ������
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        protected PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork) { }
        
        protected override void InCtor()
        {
            base.InCtor();
            
            #region Commands

            var messageService = this.Container.Resolve<IMessageService>();

            AddTechnicalRequrementsFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //�������� ������ ����
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            var fileWrapper = new PriceEngineeringTaskFileTechnicalRequirementsWrapper(new PriceEngineeringTaskFileTechnicalRequirements())
                            {
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50),
                                Path = fileName
                            };
                            this.FilesTechnicalRequirements.Add(fileWrapper);
                        }
                    }
                }, 
                () => IsEditMode);

            RemoveTechnicalRequrementsFilesCommand = new DelegateLogConfirmationCommand(
                messageService,
                "�� �������, ��� ������ ������� ���������� ����������� �������?",
                () =>
                {
                    if (string.IsNullOrEmpty(SelectedTechnicalRequrementsFile.Path))
                    {
                        SelectedTechnicalRequrementsFile.IsActual = false;
                    }
                    else
                    {
                        this.FilesTechnicalRequirements.Remove(SelectedTechnicalRequrementsFile);
                    }
                },
                () => IsEditMode && this.SelectedTechnicalRequrementsFile != null);

            #endregion

            this.SelectedTechnicalRequrementsFileIsChanged += () =>
            {
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
            };

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                //SelectDesignDepartmentCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();
                AddTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
            };
        }

        #endregion

        protected override void SaveCommand_ExecuteMethod()
        {
            LoadNewTechnicalRequirementFilesInStorage();
            base.SaveCommand_ExecuteMethod();
        }

        /// <summary>
        /// ��������� ��� ����������� ����� �� � ���������
        /// </summary>
        public void LoadNewTechnicalRequirementFilesInStorage()
        {
            foreach (var fileWrapper in this.FilesTechnicalRequirements.AddedItems)
            {
                var destFileName = $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}";
                if (File.Exists(destFileName) == false && string.IsNullOrEmpty(fileWrapper.Path) == false)
                {
                    File.Copy(fileWrapper.Path, destFileName);
                    fileWrapper.Path = null;
                }
            }

            foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
            {
                if (childPriceEngineeringTask is PriceEngineeringTaskViewModelManager vm)
                    vm.LoadNewTechnicalRequirementFilesInStorage();
            }
        }


        #region SetStatus

        /// <summary>
        /// ������� ������
        /// </summary>
        protected void SetStatusAccept()
        {
            this.SetStatus(PriceEngineeringTaskStatusEnum.Accepted, "���������� ������ �������.");
        }

        /// <summary>
        /// ��������� ���������� ������
        /// </summary>
        protected void SetStatusRejectedByManager()
        {
            this.SetStatus(PriceEngineeringTaskStatusEnum.RejectedByManager, "���������� ������ ���������.");
        }

        /// <summary>
        /// ���������� ���������� ������
        /// </summary>
        protected void SetStatusStop()
        {
            this.SetStatus(PriceEngineeringTaskStatusEnum.Stopped, "���������� ������ �����������.");
        }

        #endregion
    }
}