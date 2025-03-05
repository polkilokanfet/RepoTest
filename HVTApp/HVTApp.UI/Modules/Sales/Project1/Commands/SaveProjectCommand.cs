using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class SaveProjectCommand : RaiseCanExecuteChangedCommand
    {
        private readonly ProjectWrapper1 _projectWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;

        public SaveProjectCommand(ProjectWrapper1 projectWrapper, IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            _projectWrapper = projectWrapper;
            _unitOfWork = unitOfWork;
            _eventAggregator = eventAggregator;
            _projectWrapper.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ProjectWrapper1.IsValid) ||
                    args.PropertyName == nameof(ProjectWrapper1.IsChanged))
                    RaiseCanExecuteChanged();
            };
        }

        public override bool CanExecute(object parameter)
        {
            return _projectWrapper.IsValid &&
                   _projectWrapper.IsChanged;
        }

        public override void Execute(object parameter)
        {
            var changedSalesUnits = _projectWrapper.Units.Where(projectUnit => projectUnit.IsChanged).Select(projectUnit => projectUnit.Model);
            var addedSalesUnits = _projectWrapper.Units.AddedItems.Select(projectUnit => projectUnit.Model);
            var unionSalesUnits = changedSalesUnits.Union(addedSalesUnits).Distinct().ToList();

            _projectWrapper.AcceptChanges();
            _unitOfWork.SaveChanges();
            base.Execute(null);

            foreach (var salesUnit in unionSalesUnits)
            {
                _eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(salesUnit);
            }
            _eventAggregator.GetEvent<AfterSaveProjectEvent>().Publish(_projectWrapper.Model);
        }
    }
}