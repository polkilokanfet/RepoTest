using HVTApp.Model.POCOs;

namespace HVTApp.Modules.Reports.ViewModels
{
    public class SalesReportUnit
    {
        public SalesUnit SalesUnit { get; }

        public SalesReportUnit(SalesUnit salesUnit)
        {
            SalesUnit = salesUnit;
        }
    }
}