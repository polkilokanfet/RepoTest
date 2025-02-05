using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1
{
    public class PriceCalculationViewModel : ViewModelBaseCanExportToExcel
    {
        private object _selectedItem;
        private PriceCalculation2Wrapper _priceCalculationWrapper;
        public string TceNumber { get; private set; }

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                AddStructureCostCommand.RaiseCanExecuteChanged();
                RemoveStructureCostCommand.RaiseCanExecuteChanged();
                RemoveGroupCommand.RaiseCanExecuteChanged();
                FinishCommand.RaiseCanExecuteChanged();
                DivideCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CurrentUserIsManager => GlobalAppProperties.UserIsManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.UserIsBackManager;
        public bool CurrentUserIsPricer => GlobalAppProperties.User.RoleCurrent == Role.Pricer;

        private string GetBlockInfo(PriceEngineeringTask task)
        {
            return $"   ���� ({task.ProductBlock.Designation} (ID � �� ���: {task.Number}))";
        }
        public string DesignDocumentationInfo
        {
            get
            {
                if (this.PriceCalculationWrapper == null) 
                    return null;

                if (this.PriceCalculationWrapper.Model.PriceEngineeringTasksId.HasValue == false)
                    return "���� ������ �� ������ � �����-���� ������� ���";

                var sb = new StringBuilder();
                var priceEngineeringTasks = this.UnitOfWork.Repository<PriceEngineeringTasks>().GetById(this.PriceCalculationWrapper.Model.PriceEngineeringTasksId.Value);
                foreach (var task in priceEngineeringTasks.ChildPriceEngineeringTasks)
                {
                    var allTasks = task.GetAllPriceEngineeringTasks().ToList();

                    var tasksNotFinished = allTasks.Where(x => x.IsFinishedByConstructor == false).ToList();
                    var tasksWithNoInfo = allTasks.Except(tasksNotFinished).Where(x => x.HasDesignDocumentationInfo == false).ToList();
                    var tasksNeedDoc = allTasks.Except(tasksNotFinished).Except(tasksWithNoInfo).Where(x => x.NeedDesignDocumentationDevelopment).ToList();

                    if (tasksNotFinished.Any() || tasksWithNoInfo.Any() || tasksNeedDoc.Any())
                    {
                        sb.AppendLine();
                        sb.AppendLine($"���: {task.ProductBlock.ProductType}; �����������: {task.ProductBlock.Designation}");
                        
                        foreach (var t in tasksNotFinished)
                            sb.AppendLine($"{GetBlockInfo(t)} ������������ �� ���������� ������������ ��� ���.");

                        foreach (var t in tasksWithNoInfo)
                            sb.AppendLine($"{GetBlockInfo(t)} �� ����� ���������� ���������� � �� (���������� �� ��������� ���������������� ������).");

                        foreach (var t in tasksNeedDoc)
                            sb.AppendLine($"{GetBlockInfo(t)}. ���������� �� �� (���. {t.UserConstructor?.Employee.Person}): {t.GetDesignDocumentationAvailabilityInfo()}");
                    }
                }

                var result = sb.ToString();
                sb.Clear();
                sb.AppendLine($"���������� ��� ��� �� ������� �� (ID � �� ���: {priceEngineeringTasks.NumberFull}; ID � TeamCenter: {priceEngineeringTasks.TceNumber}):");
                sb.AppendLine(string.IsNullOrWhiteSpace(result)
                    ? "������������ � ������� (�� ����������� ������� �� � ����������)"
                    : result);

                return sb.ToString().TrimEnd('\n', '\r');
            }
        }

        public bool StartVisibility
        {
            get
            {
                if (CurrentUserIsManager == false && CurrentUserIsBackManager == false) return false;
                return SccIsExpandable;
            }
        }

        public bool SccIsExpandable
        {
            get
            {
                if (this.PriceCalculationWrapper == null)
                    return false;

                if (this.PriceCalculationWrapper.Model.IsTceConnected && 
                    this.PriceCalculationWrapper.Model.LastHistoryItem?.Type == PriceCalculationHistoryItemType.Create)
                    return false;

                return true;
            }
        }

        public bool IsStarted => 
            PriceCalculationWrapper != null && 
            PriceCalculationWrapper.Model.History.Any() &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type != PriceCalculationHistoryItemType.Reject &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type != PriceCalculationHistoryItemType.Stop &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type != PriceCalculationHistoryItemType.Create;

        public bool IsFinished =>
            PriceCalculationWrapper != null &&
            PriceCalculationWrapper.Model.History.Any() &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type == PriceCalculationHistoryItemType.Finish;

        public bool IsRejected =>
            PriceCalculationWrapper != null &&
            PriceCalculationWrapper.Model.History.Any() &&
            PriceCalculationWrapper.Model.LastHistoryItem.Type == PriceCalculationHistoryItemType.Reject;

        public bool CanChangePrice => CurrentUserIsPricer && this.IsFinished == false;

        public bool CalculationHasFile => PriceCalculationWrapper != null && PriceCalculationWrapper.Files.Any();

        /// <summary>
        /// ������� �������
        /// </summary>
        public PriceCalculationHistoryItemWrapper HistoryItem { get; private set; }

        //������� ��� ������
        public IUnitOfWork UnitOfWork1 => this.UnitOfWork;

        #region ICommand

        public SaveCommand SaveCommand { get; }
        public AddStructureCostCommand AddStructureCostCommand { get; }
        public RemoveStructureCostCommand RemoveStructureCostCommand { get; }

        public AddGroupCommand AddGroupCommand { get; }
        public RemoveGroupCommand RemoveGroupCommand { get; }

        public StartCommand StartCommand { get; }
        public FinishCommand FinishCommand { get; }

        public CancelCommand CancelCommand { get; }
        public RejectCommand RejectCommand { get; }


        public MergeCommand MergeCommand { get; }
        public DivideCommand DivideCommand { get; }

        public LoadFileToDbCommand LoadFileToDbCommand { get; }
        public LoadFileFromDbCommand LoadFileFromDbCommand { get; }

        public DelegateLogCommand ChangePaymentsCommand { get; }

        public DelegateLogCommand LoadCostsFromFileCommand { get; }

        #endregion

        public PriceCalculation2Wrapper PriceCalculationWrapper
        {
            get => _priceCalculationWrapper;
            private set
            {
                _priceCalculationWrapper = value;
                //������� �� ��������� � ������
                PriceCalculationWrapper.PropertyChanged += (sender, args) =>
                {
                    SaveCommand.RaiseCanExecuteChanged();
                    StartCommand.RaiseCanExecuteChanged();
                    FinishCommand.RaiseCanExecuteChanged();
                    LoadFileFromDbCommand.RaiseCanExecuteChanged();
                };
                PriceCalculationWrapper.History.CollectionChanged += (sender, args) =>
                {
                    RaisePropertyChanged(nameof(StartVisibility));
                    RaisePropertyChanged(nameof(SccIsExpandable));
                    this.ChangePaymentsCommand.RaiseCanExecuteChanged();
                };
                RaisePropertyChanged(nameof(IsStarted));
                RaisePropertyChanged(nameof(CanChangePrice));
                RaisePropertyChanged(nameof(CalculationHasFile));
                RaisePropertyChanged(nameof(StartVisibility));
                RaisePropertyChanged(nameof(SccIsExpandable));
                this.ChangePaymentsCommand.RaiseCanExecuteChanged();
            }
        }

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new SaveCommand(this, this.UnitOfWork, this.Container); //���������� ���������
            AddStructureCostCommand = new AddStructureCostCommand(this); //���������� ������������
            RemoveStructureCostCommand = new RemoveStructureCostCommand(this, this.Container); //�������� ������������
            AddGroupCommand = new AddGroupCommand(this, this.Container, this.UnitOfWork); //���������� ������ ������������
            RemoveGroupCommand = new RemoveGroupCommand(this, this.Container); //�������� ������

            StartCommand = new StartCommand(this, this.Container);
            FinishCommand = new FinishCommand(this, this.Container);
            CancelCommand = new CancelCommand(this, this.Container);
            RejectCommand = new RejectCommand(this, this.Container);

            MergeCommand = new MergeCommand(this, this.Container);
            DivideCommand = new DivideCommand(this, this.Container);
            LoadFileToDbCommand = new LoadFileToDbCommand(this, this.Container);
            LoadFileFromDbCommand = new LoadFileFromDbCommand(this, this.Container);

            ChangePaymentsCommand = new DelegateLogCommand(
                () =>
                {
                    if (SelectedItem is PriceCalculationItem2Wrapper priceCalculationItem)
                    {
                        var paymentConditionSets = UnitOfWork.Repository<PaymentConditionSet>().GetAll();
                        var paymentConditionSet = container.Resolve<ISelectService>().SelectItem(paymentConditionSets);
                        if (paymentConditionSet != null && paymentConditionSet.Id !=
                            priceCalculationItem.PaymentConditionSet?.Model.Id)
                        {
                            priceCalculationItem.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(paymentConditionSet);
                        }
                    }
                },
                () => IsStarted == false && IsFinished == false);

            LoadCostsFromFileCommand = new DelegateLogCommand(
                () =>
                {
                    var path = container.Resolve<IGetFilePaths>().GetFilePath();
                    if (path == default) return;

                    var sccs = this.PriceCalculationWrapper.PriceCalculationItems
                        .SelectMany(x => x.StructureCosts)
                        .ToList();

                    var costs = container.Resolve<IGetInformationFromExcelFileService>().GetCostsDictionaryFromR3File(path);
                    foreach (var cost in costs)
                    {
                        foreach (var scc in sccs.Where(x => cost.Key.ToLower().Contains(x.Number.ToLower())))
                        {
                            scc.UnitPrice = cost.Value;
                        }
                    }
                });

            PriceCalculationWrapper = new PriceCalculation2Wrapper(new PriceCalculation());
            GenerateNewHistoryItem();
        }

        public void GenerateNewHistoryItem()
        {
            HistoryItem = new PriceCalculationHistoryItemWrapper(
                new PriceCalculationHistoryItem
                {
                    User = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)
                });
            
            RaisePropertyChanged(nameof(HistoryItem));
        }

        /// <summary>
        /// �������� ������������ ����������� ��� �������� �����
        /// </summary>
        /// <param name="priceCalculation"></param>
        public void Load(PriceCalculation priceCalculation)
        {
            var priceCalculationLoaded = UnitOfWork.Repository<PriceCalculation>().GetById(priceCalculation.Id);

            PriceCalculationWrapper = priceCalculationLoaded != null
                ? new PriceCalculation2Wrapper(priceCalculationLoaded)
                : new PriceCalculation2Wrapper(priceCalculation);
        }

        /// <summary>
        /// �������� ����� �����������
        /// </summary>
        /// <param name="priceCalculation">����� ������ �����������</param>
        /// <param name="technicalRequrementsTask2">�� ����� ������</param>
        public void CreateCopy(PriceCalculation priceCalculation, TechnicalRequrementsTask technicalRequrementsTask2)
        {
            CreateCopy(priceCalculation);
            var technicalRequirementsTask = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask2.Id);
            technicalRequirementsTask.PriceCalculations.Add(PriceCalculationWrapper.Model);
        }

        /// <summary>
        /// �������� ����� �����������
        /// </summary>
        /// <param name="priceCalculation2">����� ������ �����������</param>
        public void CreateCopy(PriceCalculation priceCalculation2)
        {
            var priceCalculation = UnitOfWork.Repository<PriceCalculation>().GetById(priceCalculation2.Id);

            PriceCalculationWrapper = new PriceCalculation2Wrapper(new PriceCalculation())
            {
                Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)),
                IsNeedExcelFile = priceCalculation.IsNeedExcelFile
            };

            foreach (var calculationItem in priceCalculation.PriceCalculationItems)
            {
                var priceCalculationItem2Wrapper = new PriceCalculationItem2Wrapper(calculationItem.SalesUnits.ToList())
                {
                    PositionInTeamCenter = calculationItem.PositionInTeamCenter,
                    PaymentConditionSet = new PaymentConditionSetEmptyWrapper(calculationItem.PaymentConditionSet)
                };

                foreach (var structureCost in calculationItem.StructureCosts)
                {
                    var structureCostWrapper = new StructureCost2Wrapper(new StructureCost())
                    {
                        AmountNumerator = structureCost.AmountNumerator,
                        AmountDenomerator = structureCost.AmountDenomerator,
                        Number = structureCost.Number,
                        Comment = structureCost.Comment,
                        UnitPrice = structureCost.UnitPrice
                    };

                    priceCalculationItem2Wrapper.StructureCosts.Add(structureCostWrapper);
                }

                PriceCalculationWrapper.PriceCalculationItems.Add(priceCalculationItem2Wrapper);
            }
        }


        /// <summary>
        /// �������� ��� �������� ������ ������� �� �������� ������
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitWrappers = salesUnits
                .Select(salesUnit => UnitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                .ToList();
            
            salesUnitWrappers
                .GroupBy(salesUnitEmptyWrapper => salesUnitEmptyWrapper, new SalesUnit2Comparer())
                .ForEach(x => { PriceCalculationWrapper.PriceCalculationItems.Add(GetPriceCalculationItem2Wrapper(x)); });

            //��������� ������
            if(this.PriceCalculationWrapper.Initiator == null)
                this.PriceCalculationWrapper.Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
        }

        public void Load(TechnicalRequrementsTask technicalRequirementsTask)
        {
            //��������� ������ ���
            technicalRequirementsTask = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequirementsTask.Id);
            //��������� ������ �� � ����������� ������, ���� �� ��� �� ��������
            if (!technicalRequirementsTask.PriceCalculations.ContainsById(PriceCalculationWrapper.Model))
            {
                technicalRequirementsTask.PriceCalculations.Add(this.PriceCalculationWrapper.Model);
            }

            //����� � ���
            TceNumber = technicalRequirementsTask.TceNumber;

            //��������� � ������ �� ������������
            foreach (var technicalRequrements in technicalRequirementsTask.Requrements.Where(technicalRequrements => technicalRequrements.IsActual))
            {
                var saleUnits = technicalRequrements.SalesUnits.ToList();
                var item = GetPriceCalculationItem2Wrapper(saleUnits);
                item.PositionInTeamCenter = technicalRequrements.PositionInTeamCenter;
                PriceCalculationWrapper.PriceCalculationItems.Add(item);
            }

            //��������� ������
            if (this.PriceCalculationWrapper.Initiator == null)
                this.PriceCalculationWrapper.Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));

            //������������� ����� excel
            this.PriceCalculationWrapper.IsNeedExcelFile = technicalRequirementsTask.ExcelFileIsRequired;
        }

        /// <summary>
        /// �������� ������� �� ���
        /// </summary>
        /// <param name="priceEngineeringTasks"></param>
        /// <param name="isTceConnected"></param>
        public void Load(PriceEngineeringTasks priceEngineeringTasks, bool isTceConnected)
        {
            //��������� ������
            priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);

            TceNumber = priceEngineeringTasks.TceNumber;

            //��������� � ������ �� ������������
            foreach (var task in priceEngineeringTasks.ChildPriceEngineeringTasks)
            {
                if (task.SalesUnits.Any())
                {
                    var priceCalculationItem = GetPriceCalculationItem2Wrapper(priceEngineeringTasks, task);
                    if (string.IsNullOrEmpty(task.TcePosition) == false &&
                        int.TryParse(task.TcePosition, out var positionInTce))
                    {
                        priceCalculationItem.PositionInTeamCenter = positionInTce;
                    }
                    PriceCalculationWrapper.PriceCalculationItems.Add(priceCalculationItem);
                }
            }

            //��������� ������ �� � ����������� ������
            priceEngineeringTasks.PriceCalculations.Add(this.PriceCalculationWrapper.Model);
            
            //��������� ������
            PriceCalculationWrapper.Initiator = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
            
            //������������� ����� excel
            this.PriceCalculationWrapper.IsNeedExcelFile = isTceConnected;

            //����� ������� � ���
            this.PriceCalculationWrapper.Model.IsTceConnected = isTceConnected;
        }

        public void RegenerateScc(PriceCalculation calculation)
        {
            this.Load(calculation);

            var priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(calculation.PriceEngineeringTasksId.Value);

            foreach (var priceCalculationItem in PriceCalculationWrapper.PriceCalculationItems)
            {
                //������� ������ ������������
                priceCalculationItem.StructureCosts.ForEach(x => UnitOfWork.Repository<StructureCost>().Delete(x.Model));
                priceCalculationItem.StructureCosts.Clear();

                //���������� ����� ������������
                var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(priceCalculationItem.Model.PriceEngineeringTaskId.Value);
                var sccList = priceEngineeringTask.GetStructureCosts(priceEngineeringTasks.TceNumber);
                priceCalculationItem.StructureCosts.AddRange(sccList.Select(x => new StructureCost2Wrapper(x)));
            }
        }


        public PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(IEnumerable<SalesUnit> salesUnits)
        {
            var result = new PriceCalculationItem2Wrapper(salesUnits);

            //�������� ��������� ������������
            var structureCost = new StructureCost
            {
                Comment = $"{result.Product}",
                AmountNumerator = 1,
                AmountDenomerator = 1,
                Number = $"{TceNumber} V"
            };
            result.StructureCosts.Add(new StructureCost2Wrapper(structureCost));

            //�������� ������������� ���.������������
            foreach (var productIncluded in result.SalesUnits.First().Model.ProductsIncluded.Where(x => !x.Product.ProductBlock.IsService))
            {
                var structureCostPrIncl = new StructureCost
                {
                    Comment = $"{productIncluded.Product}",
                    AmountNumerator = (double)productIncluded.Amount / result.SalesUnits.Count,
                    AmountDenomerator = 1,
                    Number = $"{TceNumber} V"
                };
                result.StructureCosts.Add(new StructureCost2Wrapper(structureCostPrIncl));
            }

            return result;
        }

        private PriceCalculationItem2Wrapper GetPriceCalculationItem2Wrapper(PriceEngineeringTasks priceEngineeringTasks, PriceEngineeringTask priceEngineeringTask)
        {
            var item = new PriceCalculationItem2Wrapper(priceEngineeringTask.SalesUnits.ToList());
            item.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(priceEngineeringTask.SalesUnits.First().PaymentConditionSet);
            item.Model.PriceEngineeringTaskId = priceEngineeringTask.Id;

            var priceService = Container.Resolve<IPriceService>();
            foreach (var structureCost in priceEngineeringTask.GetStructureCosts(priceEngineeringTasks.TceNumber, null, priceService))
            {
                item.StructureCosts.Add(new StructureCost2Wrapper(structureCost));
            }

            return item;
        }


        public void RefreshCommands()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            StartCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            RejectCommand.RaiseCanExecuteChanged();
            AddStructureCostCommand.RaiseCanExecuteChanged();
            AddGroupCommand.RaiseCanExecuteChanged();
            RemoveStructureCostCommand.RaiseCanExecuteChanged();
            RemoveGroupCommand.RaiseCanExecuteChanged();
            MergeCommand.RaiseCanExecuteChanged();
            DivideCommand.RaiseCanExecuteChanged();
        }

        public void CanChangePriceOnPropertyChanged()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanChangePrice)));
        }

        public void CalculationHasFileOnPropertyChanged()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CalculationHasFile)));
        }
    }
}