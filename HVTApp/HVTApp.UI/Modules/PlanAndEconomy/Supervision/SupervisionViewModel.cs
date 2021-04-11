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
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.PlanAndEconomy.Supervision
{
    public class SupervisionViewModel : LoadableExportableViewModel
    {
        private object[] _selectedUnits;

        public IValidatableChangeTrackingCollection<SupervisionWr> Units { get; } = new ValidatableChangeTrackingCollection<SupervisionWr>(new List<SupervisionWr>());

        public object[] SelectedUnits
        {
            get { return _selectedUnits; }
            set
            {
                _selectedUnits = value;
                ((DelegateCommand)PrintLetterCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public ICommand PrintLetterCommand { get; }

        public SupervisionViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    foreach (var supervisionWr in Units.Where(x => x.IsNew && x.IsValid && x.IsChanged).ToList())
                    {
                        var supervision = UnitOfWork.Repository<Model.POCOs.Supervision>()
                            .Find(x => x.SalesUnit.Id == supervisionWr.Model.SalesUnit.Id)
                            .SingleOrDefault();

                        if (supervision == null)
                        {
                            UnitOfWork.Repository<Model.POCOs.Supervision>().Add(supervisionWr.Model);
                            supervisionWr.IsNew = false;
                        }
                        //если кто-то раньше сохранил
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
                    Units.AcceptChanges();

                    UnitOfWork.SaveChanges();
                }, 
                () => Units.IsValid && Units.IsChanged);

            PrintLetterCommand = new DelegateCommand(
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
                        Comment = $"Шеф-монтаж на {supervisions.Select(supervision => supervision.SalesUnit.Facility).Distinct().ToStringEnum(", ")}".GetFirstSimbols(150),
                        Author = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id).Employee,
                        SenderEmployee = unitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.SenderOfferEmployee.Id),
                        RecipientEmployee = unitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.RecipientSupervisionLetterEmployee.Id)
                    };

                    unitOfWork.Repository<Document>().Add(letter);
                    unitOfWork.SaveChanges();

                    Container.Resolve<IPrintSupervisionLetterService>()
                        .PrintSupervisionLetter(SelectedUnits.Cast<SupervisionWr>().Select(x => x.Model), letter, PathGetter.GetPath(letter));
                },
                () => SelectedUnits != null && SelectedUnits.Any());

            Units.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
            };
        }

        protected List<SupervisionWr> Wrappers;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //шеф-монтажи (сохраненные)
            var supervisions = UnitOfWork.Repository<Model.POCOs.Supervision>().GetAll();
            Wrappers = supervisions.Select(supervision => new SupervisionWr(supervision)).ToList();

            //выигранное оборудование со включенным шеф-монтажом
            var salesUnits = UnitOfWork.Repository<SalesUnit>()
                    .Find(x => !x.IsRemoved && x.IsWon) //только выигранное оборудование
                    .Except(supervisions.Select(x => x.SalesUnit)) //еще не сохраненное
                    .Where(x => x.ProductsIncluded.Any(pi => pi.Product.ProductBlock.IsSupervision)); //в которое включен шеф-монтаж

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