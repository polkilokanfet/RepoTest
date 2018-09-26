using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Reports.ViewModels
{
    public class SalesReportViewModel : ViewModelBase
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

        public void Load()
        {
            IsLoaded = false;
            Units.Clear();

            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = CommonOptions.User.RoleCurrent == Role.SalesManager 
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && x.Project.Manager.Id == CommonOptions.User.Id) 
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);

            Units.AddRange(salesUnits.OrderBy(x => x.OrderInTakeDate).Select(x => new SalesReportUnit(x)));
            IsLoaded = true;
        }
    }
}
