using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
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
        private ParameterLookup _selectedParameterLookup;
        private ParameterRelationWrapper _selectedRelation;
        private ParameterWrapper _selectedPotentialParameter;
        private ParameterWrapper _selectedParameterInRelation;

        /// <summary>
        /// Список всех параметров
        /// </summary>
        public ObservableCollection<ParameterLookup> ParameterLookups { get; }

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
                AddRelationCommand.RaiseCanExecuteChanged();
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

        public ObservableCollection<PathToOrigin> Paths { get; } = new ObservableCollection<PathToOrigin>();

        public DelegateLogCommand AddParameterCommand { get; }
        public DelegateLogCommand AddSimilarParameterCommand { get; }

        public DelegateLogCommand AddRelationCommand { get; }
        public DelegateLogCommand RemoveRelationCommand { get; }

        public DelegateLogCommand AddParameterToRelationCommand { get; }
        public DelegateLogCommand RemoveParameterFromRelationCommand { get; }

        public ParametersViewModel(IUnityContainer container) : base(container)
        {
            var parameters = UnitOfWork.Repository<Parameter>().Find(x => true);
            ParameterLookups = new ObservableCollection<ParameterLookup>(parameters.Select(x => new ParameterLookup(x)));

            AddParameterCommand = new DelegateLogCommand(
                () =>
                {
                    this.Item = new ParameterWrapper(new Parameter());
                    (AddRelationCommand).RaiseCanExecuteChanged();
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
        }

        protected override void SaveItem()
        {
            base.SaveItem();

            if (ParameterLookups.ContainsById(Item.Model))
            {
                var lookup = ParameterLookups.Single(x => x.Entity.Id == Item.Id);
                lookup.Refresh(Item.Model);
            }
            else
            {
                var lookup = new ParameterLookup(Item.Model);
                ParameterLookups.Add(lookup);
                SelectedParameterLookup = lookup;
            }
        }
    }
}
