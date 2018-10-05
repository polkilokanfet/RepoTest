using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Products.ViewModels
{
    public class ParametersViewModel : ParameterDetailsViewModel
    {
        private ParameterLookup _selectedParameterLookup;
        private ParameterRelationWrapper _selectedRelation;
        public ObservableCollection<ParameterLookup> ParameterLookups { get; }

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
            }
        }

        public ParameterRelationWrapper SelectedRelation
        {
            get { return _selectedRelation; }
            set
            {
                if (Equals(_selectedRelation, value)) return;
                _selectedRelation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PotentialRelationParameters));
            }
        }

        public IEnumerable<ParameterWrapper> PotentialRelationParameters
        {
            get
            {
                var parameters = UnitOfWork.Repository<Parameter>().Find(x => true);

                if(SelectedRelation == null)
                    return parameters.Select(x => new ParameterWrapper(x));

                var groups = SelectedRelation.RequiredParameters.Select(x => x.ParameterGroup.Model).Distinct().ToList();
                var sameGroupParameters = parameters.Where(x => groups.Contains(x.ParameterGroup));

                return parameters.Except(sameGroupParameters).Select(x => new ParameterWrapper(x));
            }
        }

        public ObservableCollection<PathToOrigin> Paths { get; } = new ObservableCollection<PathToOrigin>();

        public ParametersViewModel(IUnityContainer container) : base(container)
        {
            var parameters = UnitOfWork.Repository<Parameter>().Find(x => true);
            ParameterLookups = new ObservableCollection<ParameterLookup>(parameters.Select(x => new ParameterLookup(x)));
        }

        protected override void SaveCommand_Execute()
        {
            base.SaveCommand_Execute();
            SelectedParameterLookup.Refresh(Item.Model);
        }
    }
}
