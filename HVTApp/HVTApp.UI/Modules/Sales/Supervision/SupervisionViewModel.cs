using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.Supervision;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Supervision
{
    public class SupervisionViewModel : PlanAndEconomy.Supervision.SupervisionViewModel
    {
        public string SupervisionCall
        {
            get
            {
                if (SelectedUnits == null || !SelectedUnits.Any())
                    return "�������� ������ � ���-��������.";

                var supervisions = SelectedUnits.Cast<SupervisionWr>().ToList();

                var sb = new StringBuilder();

                sb.AppendLine($"���������� ������: {GlobalAppProperties.Actual.SenderOfferEmployee}");

                var specification = supervisions.First().Model.SalesUnit.Specification;
                sb.AppendLine($"����� ���������� ���-������ � ������������ � ��������� ������������ �{specification?.Number} � �������� {specification?.Contract.Number} �� {specification?.Contract.Date.ToShortDateString()} �. �� ��������� ������������:");

                foreach (var supGroup in supervisions.GroupBy(x => x.Model.SalesUnit.Facility))
                {
                    //������
                    var facility = supGroup.Key;
                    var address = facility.Address?.ToString() ?? facility.GetRegion().ToString();
                    sb.AppendLine($"������: {facility} (��������������: {address})");

                    //������������
                    var i = 1;
                    foreach (var supervision in supGroup.OrderBy(x => x.Model.SalesUnit.SerialNumber))
                    {
                        var salesUnit = supervision.Model.SalesUnit;
                        sb.AppendLine($"{i}. {salesUnit.Product}, ���.�{salesUnit.SerialNumber}, ���� �������: {supervision.DateRequired?.ToShortDateString()}");
                        i++;
                    }
                }

                sb.Append("���������� ����: *������� ���������� ����*");

                return sb.ToString();
            }
        }

        public SupervisionViewModel(IUnityContainer container) : base(container)
        {
            Load();
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectedUnits))
                {
                    OnPropertyChanged(nameof(SupervisionCall));
                }
            };
        }

        protected override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var userId = GlobalAppProperties.User.Id;

            //���-������� (�����������)
            var supervisions = UnitOfWork.Repository<Model.POCOs.Supervision>().Find(x => x.SalesUnit.Project.Manager.Id == userId);
            var units = supervisions.Select(supervision => new SupervisionWr(supervision)).ToList();

            //���������� ������������ �� ���������� ���-��������
            var salesUnits = UnitOfWork.Repository<SalesUnit>()
                    .Find(x => x.IsWon && x.Project.Manager.Id == userId) //������ ���������� ������������
                    .Except(supervisions.Select(x => x.SalesUnit)) //��� �� �����������
                    .Where(x => x.ProductsIncluded.Any(pi => pi.Product.ProductBlock.IsSupervision)); //� ������� ������� ���-������

            units.AddRange(salesUnits.Select(x => new SupervisionWr(x)));

            Units.Clear();

            Units.AddRange(units.OrderBy(x => x.DateFinish).ThenBy(x => x.Model.SalesUnit.OrderInTakeDate));
            Units.AcceptChanges();
        }
    }
}