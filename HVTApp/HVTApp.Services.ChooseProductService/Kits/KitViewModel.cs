using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Kits
{
    public class KitViewModel : ViewModelBase
    {
        private readonly List<Parameter> _kitsTypes;
        private readonly ParameterRelationWrapper _relation;
        private ParameterWrapper _parameterKitType;

        private DesignDepartment _designDepartment;

        public bool IsSaved { get; private set; } = false;

        public ProductWrapper Product { get; }

        public ParameterWrapper ParameterComplectType
        {
            get => _parameterKitType;
            private set => SetProperty(ref _parameterKitType, value);
        }

        public ParameterWrapper ParameterComplectDesignation { get; }

        public string StructureCost { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand SelectTypeCommand { get; }

        public KitViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    //Обозначение продукта
                    var designation = $"{ParameterComplectType.Value} {ParameterComplectDesignation.Value}".GetFirstSimbols(255);
                    Product.DesignationSpecial = Product.ProductBlock.DesignationSpecial = designation;

                    if (_designDepartment != null)
                    {
                        Product.DesignDepartmentsKits.Add(new DesignDepartmentWrapper(_designDepartment));
                    }

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
                    var kitTypesViewModel = new KitTypesViewModel(_kitsTypes, UnitOfWork);
                    kitTypesViewModel.ShowDialog();
                    if (kitTypesViewModel.IsSelected && 
                        kitTypesViewModel.SelectedItem.Id != ParameterComplectType?.Id)
                    {
                        if (ParameterComplectType != null)
                        {
                            _relation.RequiredParameters.Remove(ParameterComplectType);
                            Product.ProductBlock.Parameters.Remove(ParameterComplectType);
                        }

                        ParameterComplectType = new ParameterWrapper(kitTypesViewModel.SelectedItem);
                        _relation.RequiredParameters.Add(ParameterComplectType);
                        Product.ProductBlock.Parameters.Add(ParameterComplectType);

                        _kitsTypes.ReAddById(ParameterComplectType.Model);
                    }
                });

            var parameterKits = UnitOfWork.Repository<Parameter>().GetById(GlobalAppProperties.Actual.ComplectsParameter.Id);

            Product = new ProductWrapper(new Product {ProductBlock = new ProductBlock()});
            Product.ProductBlock.Parameters.Add(new ParameterWrapper(parameterKits));
            Product.PropertyChanged += (sender, args) => ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();

            ParameterComplectDesignation = new ParameterWrapper(new Parameter {ParameterGroup = UnitOfWork.Repository<ParameterGroup>().GetById(GlobalAppProperties.Actual.ComplectDesignationGroup.Id) });
            ParameterComplectDesignation.Value = "0БП.000.000";
            _relation = new ParameterRelationWrapper(new ParameterRelation());
            _relation.ParameterId = ParameterComplectDesignation.Id;
            _relation.RequiredParameters.Add(new ParameterWrapper(parameterKits));
            ParameterComplectDesignation.ParameterRelations.Add(_relation);
            Product.ProductBlock.Parameters.Add(ParameterComplectDesignation);

            _kitsTypes = UnitOfWork.Repository<Parameter>().Find(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id);
            var parameterKitType = _kitsTypes.FirstOrDefault();
            if (parameterKitType != null)
            {
                ParameterComplectType = new ParameterWrapper(parameterKitType);
                Product.ProductBlock.Parameters.Add(ParameterComplectType);
                _relation.RequiredParameters.Add(ParameterComplectType);
            }
        }

        public void Load(DesignDepartment designDepartment)
        {
            if (designDepartment == null) return;
            _designDepartment = UnitOfWork.Repository<DesignDepartment>().GetById(designDepartment.Id);
        }

        public void ShowDialog()
        {
            var kitWindow = new KitWindow(this);
            kitWindow.ShowDialog();
        }

        public event Action SaveEvent;
    }
}