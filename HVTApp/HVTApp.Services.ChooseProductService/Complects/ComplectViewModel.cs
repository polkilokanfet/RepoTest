using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Complects
{
    public class ComplectViewModel : ViewModelBase
    {
        private readonly List<Parameter> _complectTypes;
        private readonly ParameterRelationWrapper _relation;
        private ParameterWrapper _parameterComplectType;

        public bool IsSaved { get; private set; } = false;

        public ProductWrapper Product { get; }

        public ParameterWrapper ParameterComplectType
        {
            get => _parameterComplectType;
            private set
            {
                _parameterComplectType = value;
                RaisePropertyChanged();
            }
        }

        public ParameterWrapper ParameterComplectDesignation { get; }

        public string StructureCost { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand SelectTypeCommand { get; }

        public ComplectViewModel(IUnityContainer container) : base(container)
        {

            SaveCommand = new DelegateCommand(
                () =>
                {
                    //Обозначение продукта
                    var designation = $"{ParameterComplectType.Value} {ParameterComplectDesignation.Value}".GetFirstSimbols(255);

                    Product.DesignationSpecial = Product.ProductBlock.DesignationSpecial = designation;
                    if (UnitOfWork.SaveEntity(Product.Model).OperationCompletedSuccessfully)
                    {
                        Product.AcceptChanges();
                        IsSaved = true;
                        SaveEvent?.Invoke();
                    }
                }, 
                () => ParameterComplectType != null && Product != null && Product.IsValid && !string.IsNullOrWhiteSpace(ParameterComplectDesignation.Value));

            SelectTypeCommand = new DelegateCommand(
                () =>
                {
                    var complectTypesViewModel = new ComplectTypesViewModel(_complectTypes, UnitOfWork);
                    complectTypesViewModel.ShowDialog();
                    if (complectTypesViewModel.IsSelected && complectTypesViewModel.SelectedItem.Id != ParameterComplectType?.Id)
                    {
                        if (ParameterComplectType != null)
                        {
                            _relation.RequiredParameters.Remove(ParameterComplectType);
                            Product.ProductBlock.Parameters.Remove(ParameterComplectType);
                        }

                        ParameterComplectType = new ParameterWrapper(complectTypesViewModel.SelectedItem);
                        _relation.RequiredParameters.Add(ParameterComplectType);
                        Product.ProductBlock.Parameters.Add(ParameterComplectType);

                        _complectTypes.ReAddById(ParameterComplectType.Model);
                    }
                });

            var parameterComplects = UnitOfWork.Repository<Parameter>().GetById(GlobalAppProperties.Actual.ComplectsParameter.Id);

            Product = new ProductWrapper(new Product {ProductBlock = new ProductBlock()});
            Product.ProductBlock.Parameters.Add(new ParameterWrapper(parameterComplects));
            Product.PropertyChanged += (sender, args) => ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();

            ParameterComplectDesignation = new ParameterWrapper(new Parameter {ParameterGroup = UnitOfWork.Repository<ParameterGroup>().GetById(GlobalAppProperties.Actual.ComplectDesignationGroup.Id) });
            ParameterComplectDesignation.Value = "0БП.000.000";
            _relation = new ParameterRelationWrapper(new ParameterRelation());
            _relation.ParameterId = ParameterComplectDesignation.Id;
            _relation.RequiredParameters.Add(new ParameterWrapper(parameterComplects));
            ParameterComplectDesignation.ParameterRelations.Add(_relation);
            Product.ProductBlock.Parameters.Add(ParameterComplectDesignation);

            _complectTypes = UnitOfWork.Repository<Parameter>().Find(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id);
            var parameterComplectType = _complectTypes.FirstOrDefault();
            if (parameterComplectType != null)
            {
                ParameterComplectType = new ParameterWrapper(parameterComplectType);
                Product.ProductBlock.Parameters.Add(ParameterComplectType);
                _relation.RequiredParameters.Add(ParameterComplectType);
            }

        }

        public void ShowDialog()
        {
            var complectWindow = new ComplectWindow(this);
            complectWindow.ShowDialog();
        }

        public event Action SaveEvent;
    }
}