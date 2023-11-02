using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.TceReport
{
    public class TceReportViewModel : LoadableExportableExpandCollapseViewModel
    {
        private readonly List<TceReportUnit> _units = new List<TceReportUnit>();
        public ObservableCollection<TceReportUnit> Units { get; } = new ObservableCollection<TceReportUnit>();

        public TceReportViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            List<TechnicalRequrementsTask> technicalRequrementsTasks = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(requrementsTask => requrementsTask.Start.HasValue);

            var tenders = UnitOfWork.Repository<Tender>().GetAll();
            TceReportUnit.Offers.Clear();
            TceReportUnit.Offers.AddRange(UnitOfWork.Repository<Offer>().GetAll());

            _units.Clear();
            foreach (var requrementsTask in technicalRequrementsTasks)
            {
                foreach (var requrement in requrementsTask.Requrements)
                {
                    if (requrement.SalesUnits.Any() == false)
                        continue;
                    _units.Add(new TceReportUnit(requrementsTask, requrement, tenders));
                }
            }
        }

        protected override void AfterGetData()
        {
            Units.Clear();
            Units.AddRange(_units);
            Container.Resolve<IMessageService>().Message("Загрузка данных", "Загрузка отчета завершена.");
        }
    }
}