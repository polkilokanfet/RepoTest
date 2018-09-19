using System.Collections.Generic;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookup : IUnitLookup
    {
        [Designation("����������� �������")]
        public List<PaymentActualLookup> PaymentsActual { get; private set; }

        [Designation("����������� �������")]
        public List<PaymentPlannedLookup> PaymentsPlanned { get; private set; }

        [Designation("���������� � ��������� ��������")]
        public List<ProductIncludedLookup> ProductsIncluded { get; private set; }
    }
}