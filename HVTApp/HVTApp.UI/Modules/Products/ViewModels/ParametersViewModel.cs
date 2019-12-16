using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class ParametersViewModel : ParameterDetailsViewModel
    {
        private ParameterLookup _selectedParameterLookup;
        private ParameterRelationWrapper _selectedRelation;

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

                ((DelegateCommand) AddSimilarParameterCommand).RaiseCanExecuteChanged();
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
                OnPropertyChanged();
                OnPropertyChanged(nameof(PotentialRelationParameters));
                ((DelegateCommand)RemoveRelationCommand).RaiseCanExecuteChanged();
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

        public ObservableCollection<PathToOrigin> Paths { get; } = new ObservableCollection<PathToOrigin>();

        public ICommand AddParameterCommand { get; }
        public ICommand AddSimilarParameterCommand { get; }

        public ICommand AddRelationCommand { get; }
        public ICommand RemoveRelationCommand { get; }

        public ParametersViewModel(IUnityContainer container) : base(container)
        {
            var parameters = UnitOfWork.Repository<Parameter>().Find(x => true);
            ParameterLookups = new ObservableCollection<ParameterLookup>(parameters.Select(x => new ParameterLookup(x)));

            AddParameterCommand = new DelegateCommand(
                () =>
                {
                    this.Item = new ParameterWrapper(new Parameter());
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
                    var relation = new ParameterRelationWrapper(new ParameterRelation());
                    Item.ParameterRelations.Add(relation);
                    SelectedRelation = relation;
                });

            RemoveRelationCommand = new DelegateCommand(
                () => { Item.ParameterRelations.Remove(SelectedRelation); },
                () => SelectedRelation != null);
        }

        protected override async Task SaveItemTask()
        {
            await base.SaveItemTask();
            SelectedParameterLookup.Refresh(Item.Model);
        }
    }
}
