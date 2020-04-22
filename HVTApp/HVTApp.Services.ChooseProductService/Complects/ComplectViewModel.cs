using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Complects
{
    public class ComplectViewModel : ViewModelBase
    {
        private List<Parameter> _complectTypes;
        private Parameter _complectType;
        private string _designation = "0¡œ.000.000";
        private string _comment;

        public Product Product { get; private set; }

        public Parameter ComplectType
        {
            get { return _complectType; }
            set
            {
                _complectType = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public string Designation
        {
            get { return _designation; }
            set
            {
                _designation = value;
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand SelectTypeCommand { get; }

        public ComplectViewModel(IUnityContainer container) : base(container)
        {
            _complectTypes = UnitOfWork.Repository<Parameter>().Find(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id);
            ComplectType = _complectTypes.FirstOrDefault();

            SaveCommand = new DelegateCommand(
                () =>
                {
                    var relation = new ParameterRelation();
                    relation.RequiredParameters.Add(UnitOfWork.Repository<Parameter>().GetById(GlobalAppProperties.Actual.ComplectsParameter.Id));
                    relation.RequiredParameters.Add(ComplectType);

                    var parameter = new Parameter
                    {
                        ParameterGroup = UnitOfWork.Repository<ParameterGroup>() .GetById(GlobalAppProperties.Actual.ComplectDesignationGroup.Id),
                        Value = Designation
                    };
                    parameter.ParameterRelations.Add(relation);
                    relation.ParameterId = parameter.Id;

                    var block = new ProductBlock
                    {
                        DesignationSpecial = Designation,
                        Parameters = relation.RequiredParameters.Union(new List<Parameter> {parameter}).ToList()
                    };

                    Product = new Product
                    {
                        DesignationSpecial = Designation,
                        ProductBlock = block
                    };

                    UnitOfWork.Repository<Product>().Add(Product);
                    UnitOfWork.SaveChanges();

                    SaveEvent?.Invoke();
                }, 
                () => ComplectType != null && !string.IsNullOrEmpty(Designation));

            SelectTypeCommand = new DelegateCommand(
                () =>
                {
                    var complectTypesViewModel = new ComplectTypesViewModel(_complectTypes);
                    complectTypesViewModel.ShowDialog();
                    if (complectTypesViewModel.IsSelected)
                    {
                        this.ComplectType = complectTypesViewModel.SelectedItem;
                        _complectTypes.ReAddById(ComplectType);
                    }
                });
        }

        public void ShowDialog()
        {
            var complectWindow = new ComplectWindow(this);
            complectWindow.ShowDialog();
        }

        public event Action SaveEvent;
    }
}