using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Infragistics.Documents.Excel.Charts;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class SaveProjectCommand : ICommand
    {
        private readonly ProjectWrapper1 _projectWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private bool _canExecuteFlag;

        public SaveProjectCommand(ProjectWrapper1 projectWrapper, IUnitOfWork unitOfWork)
        {
            _projectWrapper = projectWrapper;
            _unitOfWork = unitOfWork;
            _canExecuteFlag = CanExecute(null);
            _projectWrapper.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ProjectWrapper1.IsValid) ||
                    args.PropertyName == nameof(ProjectWrapper1.IsChanged))
                    RaiseCanExecuteChanged();
            };
        }

        public bool CanExecute(object parameter)
        {
            return _projectWrapper.IsValid &&
                   _projectWrapper.IsChanged;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var canExecute = CanExecute(null);
            if (_canExecuteFlag == canExecute) return;
            _canExecuteFlag = canExecute;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            _projectWrapper.AcceptChanges();
            MapProject();
            _unitOfWork.SaveEntity(_projectWrapper.Model);
            RaiseCanExecuteChanged();
        }

        private void MapProject()
        {
            var project = this._projectWrapper.Model;
            foreach (var salesUnit in project.SalesUnits)
            {
                if (salesUnit.Producer != null)
                    salesUnit.Producer = _unitOfWork.Repository<Company>().GetById(salesUnit.Producer.Id);
                salesUnit.Facility = _unitOfWork.Repository<Facility>().GetById(salesUnit.Facility.Id);
                salesUnit.Product = _unitOfWork.Repository<Product>().GetById(salesUnit.Product.Id);
                salesUnit.PaymentConditionSet = _unitOfWork.Repository<PaymentConditionSet>().GetById(salesUnit.PaymentConditionSet.Id);
                foreach (var productIncluded in salesUnit.ProductsIncluded)
                {
                    productIncluded.Product = _unitOfWork.Repository<Product>().GetById(productIncluded.Product.Id);
                }
            }
        }
    }
}