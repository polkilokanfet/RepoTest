using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.Supervision
{
    public class SupervisionViewModel : LoadableExportableViewModel
    {
        private readonly IFileManagerService _fileManagerService;

        private object[] _selectedUnits;

        public IValidatableChangeTrackingCollection<SupervisionWr> Units { get; } = new ValidatableChangeTrackingCollection<SupervisionWr>(new List<SupervisionWr>());

        public object[] SelectedUnits
        {
            get => _selectedUnits;
            set
            {
                _selectedUnits = value;
                PrintLetterCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public DelegateLogCommand SaveCommand { get; }

        public DelegateLogCommand PrintLetterCommand { get; }

        public SupervisionViewModel(IUnityContainer container) : base(container)
        {
            _fileManagerService = container.Resolve<IFileManagerService>();

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    foreach (var supervisionWr in Units.Where(x => x.IsNew && x.IsValid && x.IsChanged).ToList())
                    {
                        var supervision = UnitOfWork.Repository<Model.POCOs.Supervision>()
                            .Find(supervision1 => supervision1.SalesUnit.Id == supervisionWr.Model.SalesUnit.Id)
                            .SingleOrDefault();

                        if (supervision == null)
                        {
                            if (UnitOfWork.Repository<Model.POCOs.Supervision>().Add(supervisionWr.Model).OperationCompletedSuccessfully == false)
                            {
                                return;
                            }
                            supervisionWr.IsNew = false;
                        }
                        //���� ���-�� ������ ��������
                        else
                        {
                            var supervisionWr2 = new SupervisionWr(supervision);

                            if (supervisionWr.DateFinishIsChanged)
                                supervisionWr2.DateFinish = supervisionWr.DateFinish;

                            if (supervisionWr.DateRequiredIsChanged)
                                supervisionWr2.DateRequired = supervisionWr.DateRequired;

                            if (supervisionWr.ServiceOrderNumberIsChanged)
                                supervisionWr2.ServiceOrderNumber = supervisionWr.ServiceOrderNumber;

                            if (supervisionWr.ClientOrderNumberIsChanged)
                                supervisionWr2.ClientOrderNumber = supervisionWr.ClientOrderNumber;

                            Units.Remove(supervisionWr);
                            Units.Add(supervisionWr2);
                        }

                    }

                    if (UnitOfWork.SaveChanges().OperationCompletedSuccessfully)
                    {
                        Units.AcceptChanges();
                    }
                }, 
                () => Units.IsValid && Units.IsChanged);

            PrintLetterCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var supervisions = SelectedUnits
                        .Cast<SupervisionWr>()
                        .Select(x => x.Model)
                        .OrderBy(supervision => supervision.SalesUnit.SerialNumber)
                        .ToList();

                    var letter = new Document
                    {
                        Number = new DocumentNumber(),
                        Date = DateTime.Today,
                        Comment = $"� ���-������� �� ��������: {supervisions.Select(supervision => supervision.SalesUnit.Facility).Distinct().ToStringEnum(", ")}".GetFirstSimbols(150),
                        Author = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id).Employee,
                        SenderEmployee = unitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.SenderOfferEmployee.Id),
                        RecipientEmployee = unitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.RecipientSupervisionLetterEmployee.Id)
                    };

                    if (unitOfWork.SaveEntity(letter).OperationCompletedSuccessfully)
                    {
                        Container.Resolve<IPrintSupervisionLetterService>()
                            .PrintSupervisionLetter(SelectedUnits.Cast<SupervisionWr>().Select(x => x.Model), letter, _fileManagerService.GetPath(letter));
                    }

                },
                () => SelectedUnits != null && SelectedUnits.Any());

            Units.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
            };
        }

        protected List<SupervisionWr> Wrappers;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //���-������� (�����������)
            var supervisions = UnitOfWork.Repository<Model.POCOs.Supervision>().GetAll();
            Wrappers = supervisions.Select(supervision => new SupervisionWr(supervision)).ToList();

            //���������� ������������ �� ���������� ���-��������
            var salesUnits = UnitOfWork.Repository<SalesUnit>()
                    .Find(x => !x.IsRemoved && x.IsWon) //������ ���������� ������������
                    .Except(supervisions.Select(x => x.SalesUnit)) //��� �� �����������
                    .Where(x => x.ProductsIncluded.Any(pi => pi.Product.ProductBlock.IsSupervision)); //� ������� ������� ���-������

            Wrappers.AddRange(salesUnits.Select(x => new SupervisionWr(x)));
        }

        protected override void AfterGetData()
        {
            Units.Clear();

            Units.AddRange(Wrappers.OrderBy(x => x.DateFinish).ThenBy(x => x.Model.SalesUnit.OrderInTakeDate));
            Units.AcceptChanges();
        }
    }
}