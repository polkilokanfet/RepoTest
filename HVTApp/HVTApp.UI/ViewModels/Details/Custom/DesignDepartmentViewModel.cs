using System.Collections.Specialized;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class DesignDepartmentViewModel : DesignDepartmentDetailsViewModel
    {
        private DesignDepartmentParametersWrapper _selectedDesignDepartmentParameters;
        private ParameterWrapper _selectedParameter;
        private DesignDepartmentParametersAddedBlocksWrapper _selectedDesignDepartmentParametersAdded;
        private ParameterWrapper _selectedParameterAdded;

        public DesignDepartmentParametersWrapper SelectedDesignDepartmentParameters
        {
            get => _selectedDesignDepartmentParameters;
            set
            {
                _selectedDesignDepartmentParameters = value;
                RaisePropertyChanged();
                RemoveParameterCommand.RaiseCanExecuteChanged();
                this.SelectedParameterSetsItem = value;
            }
        }

        public ParameterWrapper SelectedParameter
        {
            get => _selectedParameter;
            set
            {
                _selectedParameter = value;
                RemoveParameterCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand CreateParameterSetCommand { get; }
        public DelegateLogCommand RemoveParameterCommand { get; }


        public DesignDepartmentParametersAddedBlocksWrapper SelectedDesignDepartmentParametersAdded
        {
            get => _selectedDesignDepartmentParametersAdded;
            set
            {
                _selectedDesignDepartmentParametersAdded = value;
                RaisePropertyChanged();
                RemoveParameterAddedCommand.RaiseCanExecuteChanged();
                SelectedParameterSetsAddedBlocksItem = value;
            }
        }

        public ParameterWrapper SelectedParameterAdded
        {
            get => _selectedParameterAdded;
            set
            {
                _selectedParameterAdded = value;
                RemoveParameterAddedCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand CreateParameterSetAddedCommand { get; }
        public DelegateLogCommand RemoveParameterAddedCommand { get; }


        public DesignDepartmentViewModel(IUnityContainer container) : base(container)
        {
            CreateParameterSetCommand = new DelegateLogCommand(
                () =>
                {
                    var block = container.Resolve<IGetProductService>().GetProductBlock();
                    if (block != null)
                    {
                        var wrapper = new DesignDepartmentParametersWrapper(
                            new DesignDepartmentParameters()
                            {
                                Name = block.ToString(),
                                Parameters = block.Parameters.Select(x => UnitOfWork.Repository<Parameter>().GetById(x.Id)).ToList()
                            });
                        this.Item.ParameterSets.Add(wrapper);
                    }
                });

            RemoveParameterCommand = new DelegateLogCommand(
                () =>
                {
                    SelectedDesignDepartmentParameters.Parameters.Remove(SelectedParameter);
                },
                () => SelectedDesignDepartmentParameters != null && SelectedParameter != null);

            CreateParameterSetAddedCommand = new DelegateLogCommand(
                () =>
                {
                    var block = container.Resolve<IGetProductService>().GetProductBlock();
                    if (block != null)
                    {
                        var wrapper = new DesignDepartmentParametersAddedBlocksWrapper(
                            new DesignDepartmentParametersAddedBlocks()
                            {
                                Name = block.ToString(),
                                Parameters = block.Parameters.Select(x => UnitOfWork.Repository<Parameter>().GetById(x.Id)).ToList()
                            });
                        this.Item.ParameterSetsAddedBlocks.Add(wrapper);
                    }
                });

            RemoveParameterAddedCommand = new DelegateLogCommand(
                () =>
                {
                    SelectedDesignDepartmentParametersAdded.Parameters.Remove(SelectedParameterAdded);
                },
                () => SelectedDesignDepartmentParametersAdded != null && SelectedParameterAdded != null);
        }

        protected override void AfterLoading()
        {
            this.Item.ParameterSets.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var designDepartmentParametersWrapper in args.OldItems.Cast<DesignDepartmentParametersWrapper>())
                    {
                        var designDepartmentParameters = UnitOfWork.Repository<DesignDepartmentParameters>().GetById(designDepartmentParametersWrapper.Id);
                        if (designDepartmentParameters != null)
                        {
                            UnitOfWork.Repository<DesignDepartmentParameters>().Delete(designDepartmentParameters);
                        }
                    }
                }
            };

            this.Item.ParameterSetsAddedBlocks.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var designDepartmentParametersAddedBlocksWrapper in args.OldItems.Cast<DesignDepartmentParametersAddedBlocksWrapper>())
                    {
                        var departmentParametersAddedBlocks = UnitOfWork.Repository<DesignDepartmentParametersAddedBlocks>().GetById(designDepartmentParametersAddedBlocksWrapper.Id);
                        if (departmentParametersAddedBlocks != null)
                        {
                            UnitOfWork.Repository<DesignDepartmentParametersAddedBlocks>().Delete(departmentParametersAddedBlocks);
                        }
                    }
                }
            };

            base.AfterLoading();
        }

        //загрузка при копировании департамента
        public void LoadCopy(DesignDepartment designDepartment)
        {
            this.Load(new DesignDepartment());

            designDepartment = UnitOfWork.Repository<DesignDepartment>().GetById(designDepartment.Id);
            this.Item.Name = $"Copy {designDepartment.Name}";
            this.Item.Head = new UserWrapper(designDepartment.Head);
            this.Item.Staff.AddRange(designDepartment.Staff.Select(x => new UserWrapper(x)));

            foreach (var parameterSet in designDepartment.ParameterSets)
            {
                var departmentParameters = new DesignDepartmentParameters()
                {
                    Name = parameterSet.Name,
                    Parameters = parameterSet.Parameters.ToList()
                };

                this.Item.ParameterSets.Add(new DesignDepartmentParametersWrapper(departmentParameters));
            }

            foreach (var parametersAddedBlocks in designDepartment.ParameterSetsAddedBlocks)
            {
                var departmentParametersAddedBlocks = new DesignDepartmentParametersAddedBlocks()
                {
                    Name = parametersAddedBlocks.Name,
                    Parameters = parametersAddedBlocks.Parameters.ToList()
                };

                this.Item.ParameterSetsAddedBlocks.Add(new DesignDepartmentParametersAddedBlocksWrapper(departmentParametersAddedBlocks));
            }
        }
    }
}