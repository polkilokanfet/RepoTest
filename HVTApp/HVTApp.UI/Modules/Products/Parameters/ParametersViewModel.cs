using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Products.Parameters
{
    public class ParametersViewModel : ParameterDetailsViewModel
    {
        public ProductReplacer ProductReplacer { get; }
        /// <summary>
        /// Список всех параметров
        /// </summary>
        public ParameterLookups ParameterLookups { get; }

        public IEnumerable<ParameterWrapper> PotentialRelationParameters
        {
            get
            {
                //все параметры
                var parameters = ParameterLookups.Select(x => x.Entity).ToList();

                if (SelectedRelation == null) return null;

                //группы выбранных параметров
                var groups = SelectedRelation.RequiredParameters.Select(x => x.ParameterGroup.Model).Distinct().ToList();
                //параметры этих групп
                var sameGroupParameters = parameters.Where(x => groups.Contains(x.ParameterGroup));

                return parameters.Except(sameGroupParameters).Select(x => new ParameterWrapper(x));
            }
        }

        public ObservableCollection<PathToOrigin> Paths { get; } = new ObservableCollection<PathToOrigin>();

        #region Selected

        private ParameterLookup _selectedParameterLookup;
        private ParameterRelationWrapper _selectedRelation;
        private ParameterWrapper _selectedPotentialParameter;
        private ParameterWrapper _selectedParameterInRelation;

        /// <summary>
        /// Выбранный параметр
        /// </summary>
        public ParameterLookup SelectedParameterLookup
        {
            get => _selectedParameterLookup;
            set
            {
                if (Equals(_selectedParameterLookup, value)) return;
                _selectedParameterLookup = value;

                //отмена изменений в выбранном параметре
                if(Item != null && Item.IsChanged)
                    Item.RejectChanges();

                if (value == null) return;

                //загрузка нового параметра
                this.Load(new ParameterWrapper(value.Entity), UnitOfWork);

                //обновление возможных путей параметра
                Paths.Clear();
                Paths.AddRange(Item.Model.Paths());

                AddSimilarParameterCommand.RaiseCanExecuteChanged();
                AddSimilarParametersCommand.RaiseCanExecuteChanged();
                AddRelationCommand.RaiseCanExecuteChanged();
                RemoveParameterCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Выбранная связь
        /// </summary>
        public ParameterRelationWrapper SelectedRelation
        {
            get => _selectedRelation;
            set
            {
                if (Equals(_selectedRelation, value)) return;
                _selectedRelation = value;
                SelectedPotentialParameter = null;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(PotentialRelationParameters));
                RemoveRelationCommand.RaiseCanExecuteChanged();
                AddParameterToRelationCommand.RaiseCanExecuteChanged();
                RemoveParameterFromRelationCommand.RaiseCanExecuteChanged();
            }
        }

        public ParameterWrapper SelectedPotentialParameter
        {
            get => _selectedPotentialParameter;
            set
            {
                _selectedPotentialParameter = value;
                AddParameterToRelationCommand.RaiseCanExecuteChanged();
                RemoveParameterFromRelationCommand.RaiseCanExecuteChanged();
            }
        }

        public ParameterWrapper SelectedParameterInRelation
        {
            get => _selectedParameterInRelation;
            set
            {
                _selectedParameterInRelation = value;
                RemoveParameterFromRelationCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public DelegateLogCommand AddParameterCommand { get; }
        public DelegateLogCommand AddSimilarParameterCommand { get; }

        public DelegateLogConfirmationCommand RemoveParameterCommand { get; }

        public DelegateLogCommand AddRelationCommand { get; }
        public DelegateLogCommand RemoveRelationCommand { get; }

        public DelegateLogCommand AddParameterToRelationCommand { get; }
        public DelegateLogCommand RemoveParameterFromRelationCommand { get; }

        #endregion

        #region AddSimilarParameters

        public int ParameterValueStart { get; set; } = 5;
        public int ParameterValueEnd { get; set; } = 100;
        public int ParameterValueStep{ get; set; } = 5;

        public DelegateLogCommand AddSimilarParametersCommand { get; }
        
        #endregion

        public ParametersViewModel(IUnityContainer container) : base(container)
        {
            ParameterLookups = new ParameterLookups(UnitOfWork.Repository<Parameter>().GetAll(), this);

            ProductReplacer = new ProductReplacer(container, this);

            #region Commands

            AddParameterCommand = new DelegateLogCommand(
                () =>
                {
                    this.Item = new ParameterWrapper(new Parameter());
                    AddRelationCommand.RaiseCanExecuteChanged();
                }
            );

            AddSimilarParameterCommand = new DelegateLogCommand(
                () =>
                {
                    //создаем подобный парметр
                    var similarParameter = SelectedParameterLookup.Entity.GetSimilarParameter();

                    if (UnitOfWork.SaveEntity(similarParameter).OperationCompletedSuccessfully)
                    {
                        var lookup = new ParameterLookup(similarParameter);
                        ParameterLookups.Add(lookup);
                        SelectedParameterLookup = lookup;
                    }
                },
                () => SelectedParameterLookup != null);

            AddSimilarParametersCommand = new DelegateLogCommand(
                () =>
                {
                    var parameterGroup = SelectedParameterLookup.ParameterGroup.Entity;
                    int value = this.ParameterValueStart;
                    while (value <= this.ParameterValueEnd)
                    {
                        var parameterLookup = this.ParameterLookups
                            .SingleOrDefault(x => x.ParameterGroup.Id == parameterGroup.Id && value == int.Parse(x.Value));

                        if (parameterLookup == null)
                        {
                            this.AddSimilarParameterCommand.Execute();
                            SelectedParameterLookup.Entity.Value = value.ToString();
                        }
                        else
                        {
                            SelectedParameterLookup = parameterLookup;
                        }

                        SelectedParameterLookup.Entity.Rang = value * (-1);
                        this.SaveItem();

                        value += this.ParameterValueStep;
                    }
                },
                () => SelectedParameterLookup != null);

            RemoveParameterCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    var unitOfWork = container.Resolve<IUnitOfWork>();
                    var parameter = unitOfWork.Repository<Parameter>().GetById(SelectedParameterLookup.Entity.Id);

                    var relations = unitOfWork.Repository<ProductRelation>().Find(relation => relation.ParentProductParameters.Contains(parameter) || relation.ChildProductParameters.Contains(parameter));
                    if (relations.Any())
                    {
                        container.Resolve<IMessageService>().ShowOkMessageDialog("Info", $"Удалите сначала связи между блоками: {relations.ToStringEnum()}");
                        return;
                    }


                    var blocks = unitOfWork.Repository<ProductBlock>().Find(block => block.Parameters.Contains(parameter));
                    if (blocks.Any())
                    {
                        container.Resolve<IMessageService>().ShowOkMessageDialog("Info", $"Удалите сначала параметр из блоков: {blocks.ToStringEnum()}");
                        return;
                    }
                    //foreach (var block in blocks)
                    //{
                    //    block.Parameters.Remove(parameter);
                    //}

                    foreach (var relation in parameter.ParameterRelations.ToList())
                    {
                        unitOfWork.Repository<ParameterRelation>().Delete(relation);
                    }

                    //var relations = unitOfWork.Repository<ParameterRelation>().Find(x => x.RequiredParameters.Contains(parameter));
                    //foreach (var relation in relations)
                    //{
                    //    relation.RequiredParameters.Remove(parameter);
                    //}

                    unitOfWork.Repository<Parameter>().Delete(parameter);

                    unitOfWork.SaveChanges();

                    this.ParameterLookups.Remove(SelectedParameterLookup);
                    SelectedParameterLookup = null;
                },
                () => SelectedParameterLookup != null);

            AddRelationCommand = new DelegateLogCommand(
                () =>
                {
                    var relation = new ParameterRelationWrapper(new ParameterRelation())
                    {
                        ParameterId = Item.Id
                    };
                    Item.ParameterRelations.Add(relation);
                    SelectedRelation = relation;
                },
                () => Item != null);

            RemoveRelationCommand = new DelegateLogCommand(
                () =>
                {
                    this.UnitOfWork.Repository<ParameterRelation>().Delete(SelectedRelation.Model);
                    Item.ParameterRelations.Remove(SelectedRelation);
                },
                () => SelectedRelation != null);

            AddParameterToRelationCommand = new DelegateLogCommand(
                () => { SelectedRelation.RequiredParameters.Add(SelectedPotentialParameter); },
                () => SelectedRelation != null && SelectedPotentialParameter != null);

            RemoveParameterFromRelationCommand = new DelegateLogCommand(
                () =>
                {
                    this.SelectedRelation.RequiredParameters.Remove(SelectedParameterInRelation);
                },
                () => this.SelectedRelation != null && this.SelectedParameterInRelation != null);

            #endregion
        }

        protected override void SaveItem()
        {
            base.SaveItem();
            this.ParameterLookups.Refresh(Item.Model);
        }
    }
}
