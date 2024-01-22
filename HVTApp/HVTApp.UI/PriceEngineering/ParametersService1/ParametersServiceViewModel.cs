using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering.ParametersService1
{
    public class ParametersServiceViewModel : BindableBase, IDialogRequestClose
    {
        private IUnitOfWork _unitOfWork;
        private ParameterWrapper _parameterWrapper;
        private ProductBlock _baseProductBlock;
        private Parameter _baseParameter;

        public ParameterWrapper ParameterWrapper
        {
            get => _parameterWrapper;
            private set
            {
                if (_parameterWrapper != null)
                    _parameterWrapper.PropertyChanged -= ParameterWrapperOnPropertyChanged;
                _parameterWrapper = value;
                if (_parameterWrapper != null)
                    _parameterWrapper.PropertyChanged += ParameterWrapperOnPropertyChanged;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private void ParameterWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        public ProductBlock BaseProductBlock
        {
            get => _baseProductBlock;
            private set
            {
                _baseProductBlock = value;
                BaseParameter = null;
                SelectBaseParameterCommand.RaiseCanExecuteChanged();
                SaveCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public Parameter BaseParameter
        {
            get => _baseParameter;
            private set
            {
                _baseParameter = value;
                SaveCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public DelegateLogCommand SelectBaseProductBlockCommand { get; }
        public DelegateLogCommand SelectBaseParameterCommand { get; }
        public DelegateLogCommand SaveCommand { get; }

        private ParametersServiceViewModel(IUnityContainer container)
        {
            _unitOfWork = container.Resolve<IUnitOfWork>();

            SelectBaseParameterCommand = new DelegateLogCommand(
                () =>
                {
                    //поиск конечных параметров
                    var parameters = BaseProductBlock.Parameters.ToList();
                    foreach (var parameter in BaseProductBlock.Parameters)
                    {
                        foreach (var pathToOrigin in parameter.Paths())
                        {
                            foreach (var parameter1 in pathToOrigin.Parameters.Where(x => !Equals(x, parameter)))
                            {
                                parameters.RemoveIfContainsById(parameter1);
                            }
                        }
                    }

                    var result = container.Resolve<ISelectService>().SelectItem(parameters);
                    if (result != null && result.Id != this.ParameterWrapper?.Id)
                    {
                        this.BaseParameter = _unitOfWork.Repository<Parameter>().GetById(result.Id);
                        ParameterWrapper = new ParameterWrapper(this.BaseParameter.GetSimilarParameter());
                    }
                },
                () => BaseProductBlock != null);

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    ParameterWrapper.AcceptChanges();
                    _unitOfWork.SaveEntity(ParameterWrapper.Model);
                    container.Resolve<IEventAggregator>().GetEvent<AfterSaveParameterEvent>().Publish(ParameterWrapper.Model);
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                },
                () => 
                    ParameterWrapper != null && 
                    ParameterWrapper.IsValid &&
                    BaseProductBlock != null &&
                    BaseParameter != null);

            ParameterWrapper = new ParameterWrapper(new Parameter());
        }

        public ParametersServiceViewModel(IUnityContainer container, DesignDepartment designDepartment) : this(container)
        {
            var designDepartment1 = _unitOfWork.Repository<DesignDepartment>().GetById(designDepartment.Id);

            SelectBaseProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    if (designDepartment1.ParameterSetsAddedBlocks.Any() == false)
                    {
                        container.Resolve<IMessageService>().Message("Уведомление", $"В КБ \"{designDepartment1.Name}\" нет ни одного дополнительного блока. Обратитесь к администратору.");
                        return;
                    }
                    var block = container.Resolve<IGetProductService>().GetProductBlock(designDepartment1.ParameterSetsAddedBlocks);
                    if (block != null && block.Id != BaseProductBlock?.Id)
                    {
                        BaseProductBlock = _unitOfWork.Repository<ProductBlock>().GetById(block.Id);
                    }
                });
        }

        public ParametersServiceViewModel(IUnityContainer container, ProductBlock productBlock, IEnumerable<Parameter> requiredParameters) : this(container)
        {
            SelectBaseProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var block = container.Resolve<IGetProductService>().GetProductBlock(productBlock, requiredParameters);
                    if (block != null && block.Id != BaseProductBlock?.Id)
                    {
                        BaseProductBlock = _unitOfWork.Repository<ProductBlock>().GetById(block.Id);
                    }
                });
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}