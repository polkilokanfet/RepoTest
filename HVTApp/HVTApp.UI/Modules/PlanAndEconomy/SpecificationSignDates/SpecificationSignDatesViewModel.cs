using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.SpecificationSignDates
{
    public class SpecificationSignDatesViewModel : LoadableExportableViewModel
    {
        private IUnitOfWork _unitOfWork;
        private IValidatableChangeTrackingCollection<SpecificationSignDatesWrapper> _specifications;

        public ObservableCollection<SpecificationSignDatesWrapper> Specifications { get; } = new ObservableCollection<SpecificationSignDatesWrapper>();

        public DelegateLogCommand SaveCommand { get; }

        public SpecificationSignDatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    //принимаем все изменения
                    _specifications.AcceptChanges();
                    //сохраняем изменения
                    _unitOfWork.SaveChanges();
                    //проверяем актуальность команды
                    SaveCommand.RaiseCanExecuteChanged();
                },
                () => _specifications != null && _specifications.IsValid && _specifications.IsChanged);
        }

        protected override void GetData()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            var specifications = _unitOfWork.Repository<Specification>().GetAll()
                .OrderByDescending(specification => specification.Date)
                .Select(specification => new SpecificationSignDatesWrapper(specification))
                .ToList();

            _specifications = new ValidatableChangeTrackingCollection<SpecificationSignDatesWrapper>(specifications);
        }

        protected override void AfterGetData()
        {
            Specifications.Clear();
            Specifications.AddRange(_specifications);
            _specifications.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
            };
        }
    }
}