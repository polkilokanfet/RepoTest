using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Reports.ViewModels
{
    public class SalesReportViewModel : BindableBaseCanExportToExcel
    {
        private bool _isLoaded;
        public ObservableCollection<SalesReportUnit> Units { get; } = new ObservableCollection<SalesReportUnit>();

        public ICommand ReloadCommand { get; }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public SalesReportViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            Load();
        }

        /// <summary>
        /// Загрузка отчета
        /// </summary>
        public void Load()
        {
            IsLoaded = false;
            Units.Clear();

            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager 
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && x.Project.Manager.IsAppCurrentUser()) 
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);

            var groups = salesUnits.OrderBy(x => x.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().Find(x => true);
            var blocks = UnitOfWork.Repository<ProductBlock>().Find(x => true);
            var countryUnions = UnitOfWork.Repository<CountryUnion>().Find(x => true);

            Units.AddRange(groups.Select(x => new SalesReportUnit(x, tenders.Where(t => Equals(x.Key.Project, t.Project)), blocks, countryUnions)));

            IsLoaded = true;
        }
    }
}
