using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class ParametersViewModel : ParameterDetailsViewModel
    {
        private ParameterLookup _selectedParameterLookup;
        private ParameterRelationWrapper _selectedRelation;
        private ParameterWrapper _selectedPotentialParameter;

        /// <summary>
        /// Список всех параметров
        /// </summary>
        public ObservableCollection<ParameterLookup> ParameterLookups { get; }

        /// <summary>
        /// Выбранный параметр
        /// </summary>
        public ParameterLookup SelectedParameterLookup
        {
            get { return _selectedParameterLookup; }
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

                ((DelegateCommand)AddSimilarParameterCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddRelationCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Выбранная связь
        /// </summary>
        public ParameterRelationWrapper SelectedRelation
        {
            get { return _selectedRelation; }
            set
            {
                if (Equals(_selectedRelation, value)) return;
                _selectedRelation = value;
                SelectedPotentialParameter = null;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PotentialRelationParameters));
                ((DelegateCommand)RemoveRelationCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddParameterToRelationCommand).RaiseCanExecuteChanged();
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
            get { return _selectedPotentialParameter; }
            set
            {
                _selectedPotentialParameter = value;
                ((DelegateCommand)AddParameterToRelationCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<PathToOrigin> Paths { get; } = new ObservableCollection<PathToOrigin>();

        public ICommand AddParameterCommand { get; }
        public ICommand AddSimilarParameterCommand { get; }

        public ICommand AddRelationCommand { get; }
        public ICommand RemoveRelationCommand { get; }

        public ICommand AddParameterToRelationCommand { get; }

        public ParametersViewModel(IUnityContainer container) : base(container)
        {
            var parameters = UnitOfWork.Repository<Parameter>().Find(x => true);
            ParameterLookups = new ObservableCollection<ParameterLookup>(parameters.Select(x => new ParameterLookup(x)));

            AddParameterCommand = new DelegateCommand(
                () =>
                {
                    this.Item = new ParameterWrapper(new Parameter());
                    ((DelegateCommand)AddRelationCommand).RaiseCanExecuteChanged();
                }
            );

            AddSimilarParameterCommand = new DelegateCommand(
                () =>
                {
                    //создаем подобный парметр
                    var similarParameter = new Parameter
                    {
                        Value = $"{SelectedParameterLookup.Entity.Value} (подобный праметр)",
                        ParameterGroup = SelectedParameterLookup.Entity.ParameterGroup,
                        Rang = SelectedParameterLookup.Entity.Rang,
                        Comment = SelectedParameterLookup.Entity.Comment
                    };

                    foreach (var parameterRelation in SelectedParameterLookup.Entity.ParameterRelations)
                    {
                        var similarParameterRelation = new ParameterRelation
                        {
                            ParameterId = similarParameter.Id,
                            RequiredParameters = parameterRelation.RequiredParameters.ToList()
                        };
                        similarParameter.ParameterRelations.Add(similarParameterRelation);
                    }

                    UnitOfWork.Repository<Parameter>().Add(similarParameter);
                    UnitOfWork.SaveChanges();

                    var lookup = new ParameterLookup(similarParameter);
                    ParameterLookups.Add(lookup);

                    SelectedParameterLookup = lookup;
                },
                () => SelectedParameterLookup != null);

            AddRelationCommand = new DelegateCommand(
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

            RemoveRelationCommand = new DelegateCommand(
                () => { Item.ParameterRelations.Remove(SelectedRelation); },
                () => SelectedRelation != null);

            AddParameterToRelationCommand = new DelegateCommand(
                () => { SelectedRelation.RequiredParameters.Add(SelectedPotentialParameter); },
                () => SelectedRelation != null && SelectedPotentialParameter != null);
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
